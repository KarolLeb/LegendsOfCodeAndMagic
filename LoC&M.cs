using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class PlayerStats {
    public int Health { get; protected set; }
    public int Mana { get; protected set; }
    public int Deck { get; protected set; }
    public int Rune { get; protected set; }
    public int Hand { get; protected set; }

    public PlayerStats (string[] data) {
        Health = int.Parse (data[0]);
        Mana = int.Parse (data[1]);
        Deck = int.Parse (data[2]);
        Rune = int.Parse (data[3]);
    }
    public void setHand (int value) {
        Hand = value;
    }
}

public class Card {
    public int CardNumber { get; protected set; }
    public int Id { get; protected set; }
    public int Location { get; set; } // -1:opponentBoard, 0:playersHand, 1:playe protectedrsBoard
    public int CardType { get; protected set; }
    public int Cost { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public int MyHealthChange { get; protected set; }
    public int OppHealthChange { get; protected set; }
    public int CardDraw { get; protected set; }
    public Keywords Abilities;

    public Card (int cardNumber, int instanceId, int location, int cardType, int cost, int attack,
        int defense, Keywords abilities, int myHealthChange, int oppHealthChange, int cardDraw) {
        CardNumber = cardNumber;
        Id = instanceId;
        Location = location;
        CardType = cardType;
        Cost = cost;
        Attack = attack;
        Defense = defense;
        Abilities = new Keywords (abilities);
        MyHealthChange = myHealthChange;
        OppHealthChange = oppHealthChange;
        CardDraw = cardDraw;
    }

    public Card (string[] data) {
        CardNumber = int.Parse (data[0]);
        Id = int.Parse (data[1]);
        Location = int.Parse (data[2]);
        CardType = int.Parse (data[3]);
        Cost = int.Parse (data[4]);
        Attack = int.Parse (data[5]);
        Defense = int.Parse (data[6]);
        Abilities = new Keywords (data[7]);
        MyHealthChange = int.Parse (data[8]);
        OppHealthChange = int.Parse (data[9]);
        CardDraw = int.Parse (data[10]);
    }
}

public class Unit {
    public int Id { get; protected set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }
    public int Cost { get; protected set; }
    public int MyHealthChange { get; protected set; }
    public int OppHealthChange { get; protected set; }
    public int CardDraw { get; protected set; }
    public bool CanAttack { get; protected set; }
    public bool HasAttacked { get; protected set; }
    public Keywords Abilities;
    public Card BaseCard;

    public Unit (Unit creature) {
        Id = creature.Id;
        Attack = creature.Attack;
        Defense = creature.Defense;
        Cost = creature.Cost;
        CanAttack = creature.CanAttack;
        HasAttacked = creature.HasAttacked;
        Abilities = new Keywords (creature.Abilities);
        BaseCard = creature.BaseCard;
    }

    public Unit (Card card) {
        Id = card.Id;
        Attack = card.Attack;
        Defense = card.Defense;
        Cost = card.Cost;
        CanAttack = Abilities.HasCharge;
        MyHealthChange = card.MyHealthChange;
        OppHealthChange = card.OppHealthChange;
        CardDraw = card.CardDraw;
        Abilities = new Keywords (card.Abilities);
        BaseCard = card;
    }
    public bool canKillEnemy (Unit enemy) {
        if (enemy.Defense <= this.Attack) {
            return true;
        }
        return false;
    }

    public bool survivesTrade (Unit enemy) {
        if (enemy.Attack < this.Defense) {
            return true;
        }
        return false;
    }

    public bool myValuableTrade (Unit enemy) {
        if (this.canKillEnemy (enemy) && this.survivesTrade (enemy)) {
            return true;
        }
        return false;
    }

    public bool enemyValuableTrade (Unit enemy) {
        if (enemy.canKillEnemy (this) && enemy.survivesTrade (this)) {
            return true;
        }
        return false;
    }

    public Unit enemyAfterTrade (Unit enemy) {
        enemy.Defense -= this.Attack;
        return enemy;
    }
}

public class Keywords {

    public bool HasBreakthrough { get; protected set; }
    public bool HasCharge { get; protected set; }
    public bool HasDrain { get; protected set; }
    public bool HasGuard { get; protected set; }
    public bool HasLethal { get; protected set; }
    public bool HasWard { get; protected set; }

    public bool hasAnyKeyword () {
        return HasBreakthrough || HasCharge || HasDrain || HasGuard || HasLethal || HasWard;
    }

    public Keywords (string data) {
        HasBreakthrough = data[0] == 'B';
        HasCharge = data[1] == 'C';
        HasDrain = data[2] == 'D';
        HasGuard = data[3] == 'G';
        HasLethal = data[4] == 'L';
        HasWard = data[5] == 'W';
    }

    public Keywords (Keywords keywords) {
        HasBreakthrough = keywords.HasBreakthrough;
        HasCharge = keywords.HasCharge;
        HasDrain = keywords.HasDrain;
        HasGuard = keywords.HasGuard;
        HasLethal = keywords.HasLethal;
        HasWard = keywords.HasWard;
    }

    public string toString () {
        string toS = "";
        toS += (HasBreakthrough ? 'B' : '-');
        toS += (HasCharge ? 'C' : '-');
        toS += (HasDrain ? 'D' : '-');
        toS += (HasGuard ? 'G' : '-');
        toS += (HasLethal ? 'L' : '-');
        toS += (HasWard ? 'W' : '-');
        return toS;
    }
}

public class Action {
    public float Rating { get; protected set; }
    public int Type { get; protected set; }
    public int Id1 { get; protected set; }
    public int Id2 { get; protected set; }
    public Card CardRef;
    public Card TargetRef;
    public ActionResult Result;

    public Actions (int type, int id1, int id2, Card cardref, card targetref) {
        Rating = 0f;
        Type = type;
        Id1 = id1;
        Id2 = id2;
        CardRef = cardref;
        TargetRef = targetref;
    }

    public void setRating (float value) {
        Rating = value;
    }
}

public class ActionResult {
    public bool IsValid { get; protected set; }
    public Unit Attacker;
    public Unit Defender;
    public int AttackerHealthChange { get; protected set; }
    public int DefenderHealthChange { get; protected set; }
    public int AttackerAttackChange { get; protected set; }
    public int DefenderAttackChange { get; protected set; }
    public int AttackerDefenseChange { get; protected set; }
    public int DefenderDefenseChange { get; protected set; }
    public bool AttackerDied { get; protected set; }
    public bool DefenderDied { get; protected set; }

    public ActionResult (bool isValid) {
        IsValid = isValid;
        Attacker = null;
        Defender = null;
        AttackerHealthChange = 0;
        DefenderHealthChange = 0;
    }

    public ActionResult (Unit attacker, Unit defender, bool attackerDied, bool defenderDied, int attackerHealthChange, int defenderHealthChange) {
        IsValid = true;
        Attacker = attacker;
        Defender = defender;
        AttackerDied = attackerDied;
        DefenderDied = defenderDied;
        AttackerHealthChange = attackerHealthChange;
        DefenderHealthChange = defenderHealthChange;
    }
    // public ActionResult (Unit attacker, Unit defender, int healthGain, int healthTaken) {
    //     ActionResult (attacker, defender, false, false, healthGain, healthTaken);
    // }
}

public static class Globals {
    public const Int32 maxBoard = 6;
    public const int maxHand = 8;
    public const int maxCardCost = 12;
    public const bool debug = false;

}

class Player {
    static void Main (string[] args) {
        string[] inputs;

        PlayerStats player;
        PlayerStats opponent;

        while (true) {
            string actions = "";

            // inputs = ;
            player = new PlayerStats (Console.ReadLine ().Split (' '));

            // inputs = Console.ReadLine ().Split (' ');
            opponent = new PlayerStats (Console.ReadLine ().Split (' '));
            opponent.setHand (int.Parse (Console.ReadLine ()));

            int cardCount = int.Parse (Console.ReadLine ());

            Queue PlayerHand = new Queue ();
            Queue PlayerBoard = new Queue ();
            Queue OpponentBoard = new Queue ();

            if (player.Mana == 0) {

                // inputs = Console.ReadLine ().Split (' ');
                Card card1 = new Card (Console.ReadLine ().Split (' '));

                // inputs = Console.ReadLine ().Split (' ');
                Card card2 = new Card (Console.ReadLine ().Split (' '));

                // inputs = Console.ReadLine ().Split (' ');
                Card card3 = new Card (Console.ReadLine ().Split (' '));

                EvalPick (card1, card2, card3);
            } else {
                for (int i = 0; i < cardCount; i++) {
                    // inputs = Console.ReadLine ().Split (' ');
                    Card card = new Card (Console.ReadLine ().Split (' '));

                    switch (card.Location) {
                        case -1:
                            OpponentBoard.Enqueue (card);
                            break;
                        case 0:
                            PlayerHand.Enqueue (card);
                            break;
                        case 1:
                            PlayerBoard.Enqueue (card);
                            break;
                        default:
                            Console.Error.WriteLine ("Data loading error #1");
                            break;
                    }
                }
                PlayTurn (PlayerHand, PlayerBoard, OpponentBoard, player, opponent);
            }
        }
    }

    static Queue LegalizeHand (Queue cards, PlayerStats player) {
        Queue LegalHand = new Queue ();
        foreach (Card card in cards) {
            if (card.Cost <= player.Mana) {
                LegalHand.Enqueue (card);
            }
        }
        return LegalHand;
    }
    static Queue LegalizeMyBoard (Queue cards, PlayerStats player) {
        Queue LegalBoard = new Queue ();
        foreach (Card card in cards) {
            if (card.CanAttack && card.Defense > 0) {
                LegalBoard.Enqueue (card);
            }
        }
        return LegalBoard;
    }
    static Queue LegalizeEnemyBoard (Queue cards) {
        Queue LegalBoard = new Queue ();
        foreach (Card card in cards) {
            if (card.defense > 0) {
                LegalBoard.Enqueue (card);
            }
        }
        return LegalBoard;
    }

    static void EvalPick (Card card1, Card card2, Card card3) {
        int eval1 = 0;
        int eval2 = 0;
        int eval3 = 0;

        // public Eval(){}
        // public void Pick(){}

        Console.WriteLine ("PASS");
    }

    static void PlayTurn (Queue LegalHand, Queue MyLegalBoard, Queue EnemyLegalBoard, PlayerStats player, PlayerStats opponent) {
        List<SimpleAction> PlannedActions = new List<SimpleAction> ();
        List<SimpleAction> PotencialActions;

        bool notPass = true;

        do {
            if (Globals.debug) {
                Console.Error.WriteLine ("LegalBoard:" + MyLegalBoard.Count);
            }
            PotencialActions = new List<SimpleAction> ();
            LegalHand = LegalizeHand (LegalHand, player);
            MyLegalBoard = LegalizeMyBoard (MyLegalBoard, player);
            EnemyLegalBoard = LegalizeEnemyBoard (EnemyLegalBoard);

            foreach (Card unit in MyLegalBoard) {
                foreach (Card enemy in EnemyLegalBoard) {
                    PotencialActions.Add (new SimpleAction (2, unit, enemy));
                }
                PotencialActions.Add (new SimpleAction (2, unit, null));
            }
            if (Globals.debug) {
                Console.Error.WriteLine ("LegalHand:" + LegalHand.Count);
            }
            foreach (Card card in LegalHand) {
                PotencialActions.Add (new SimpleAction (1, card, null));
            }
            if (Globals.debug) {
                Console.Error.WriteLine ("PotAct:" + PotencialActions.Count);
            }
            foreach (SimpleAction action in PotencialActions) {
                action.setRating (EvalPlay (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent));
            }
            if (Globals.debug) {
                foreach (SimpleAction action in PotencialActions) {
                    Console.Error.WriteLine ("action.type:" + action.type.ToString ());
                    Console.Error.WriteLine ("action.cardId.instance.Id:" + action.cardId.instanceId);
                    if (action.targetId != null) {
                        Console.Error.WriteLine ("action.targetId:" + action.targetId.ToString ());
                    } else {
                        Console.Error.WriteLine ("action.targetId:" + "null");
                    }
                    Console.Error.WriteLine ("action.rate:" + action.rating);
                }
            }
            if (PotencialActions.Count > 0) {
                SimpleAction best = PotencialActions.First ();
                foreach (SimpleAction action in PotencialActions) {
                    if (best.rating < action.rating) {
                        best = action;
                    }
                }
                if (best.rating < 0) {
                    break;
                } else {
                    PlannedActions.Add (best);
                    best.cardId.canAttack = false;
                    PerformAction (best, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                    if (Globals.debug) {
                        Console.Error.WriteLine ("PlaAct:" + PlannedActions.Count);
                    }
                }
            } else {
                notPass = false;
            }

        } while (notPass && LegalHand.Count > 0 && MyLegalBoard.Count > 0);
        if (Globals.debug) {
            Console.Error.WriteLine ("PlaAct:" + PlannedActions.Count);
        }
        if (PlannedActions.Count > 0) {
            string command = "";
            foreach (SimpleAction action in PlannedActions) {
                if (command != "") {
                    command += "; ";
                }
                switch (action.type) {
                    case 1:
                        command += "SUMMON " + action.cardId.instanceId;
                        break;
                    case 2:
                        command += "ATTACK " + action.cardId.instanceId + ' ';
                        if (action.targetId == null) {
                            command += -1;
                        } else {
                            command += action.targetId.instanceId;
                        }
                        break;
                    default:
                        Console.Error.WriteLine ("Command construction error #1");
                        break;
                }
            }
            Console.WriteLine (command);
        } else {
            Console.WriteLine ("PASS");
        }
    }
    static float EvalPlay (SimpleAction action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
        float value = 0;
        switch (action.type) {
            case 1: //summon
                if (MyBoard.Count == 8) {
                    value -= 100;
                }
                if (player.Mana == action.cardId.cost) {
                    value += 2;
                }
                if (action.cardId.abilities.Contains ("G")) {
                    value += 1;
                }
                if (action.cardId.cardDraw > 0) {
                    if (MyHand.Count + action.cardId.cardDraw >= Globals.maxBoard - 1) {
                        if (MyHand.Count == Globals.maxBoard) {
                            value -= 5;
                        }
                        value -= 5;
                    } else {
                        value += 5;
                    }
                }
                if (action.cardId.abilities.Contains ("C")) {
                    value += 3;
                }
                break;
            case 2: //attack

                if (action.targetId == null) {
                    if (opponent.Health <= action.cardId.attack) {
                        value += 98;
                    }
                    value += 1;
                } else {
                    if (action.targetId.abilities.Contains ("G")) {
                        value += 5;
                    }
                    if (action.targetId.abilities.Contains ("B")) {
                        value += 2;
                    }
                    if (action.targetId.attack < 1) {
                        value += 1;
                    }
                }
                if (action.cardId.attack < 1) {
                    value -= 100;
                }
                break;
            default:
                Console.Error.WriteLine ("Eval error #1");
                break;
        }
        return value;
    }
    static void PerformAction (SimpleAction action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
        switch (action.type) {
            case 1: // summon
                player.Mana -= action.cardId.cost;
                if (action.cardId.abilities.Contains ("C")) {
                    action.cardId.canAttack = true;
                }
                if (action.cardId.cardDraw > 0) {
                    for (int i = 0; i < action.cardId.cardDraw; i++) {
                        MyBoard.Enqueue (new Card (0, 0, 0, 0, Globals.maxCardCost + 1, 0, 0, "", 0, 0, 0, false));
                    }
                }
                while (MyHand.Peek () != action.cardId) {
                    MyHand.Enqueue (MyHand.Dequeue ());
                }
                MyHand.Dequeue ();
                MyBoard.Enqueue (action.cardId);
                break;
            case 2: // attack
                if (action.targetId == null) {
                    opponent.changeHealth (action.cardId.attack);
                } else {
                    action.targetId.defense -= action.cardId.attack;
                }
                break;
            default:
                Console.Error.WriteLine ("Perform error #1");
                break;
        }
    }
}