            using System.Collections.Generic;
            using System.Collections;
            using System.IO;
            using System.Linq;
            using System.Text;
            using System;

            public class PlayerStats {
                public int Health { get; set; }
                public int Mana { get; set; }
                public int Deck { get; set; }
                public int Rune { get; set; }
                public int Hand { get; set; }

                public PlayerStats (string[] data) {
                    this.Health = int.Parse (data[0]);
                    this.Mana = int.Parse (data[1]);
                    this.Deck = int.Parse (data[2]);
                    this.Rune = int.Parse (data[3]);
                }
                public void setHand (int value) {
                    this.Hand = value;
                }
            }

            public class Card {
                public int CardNumber { get; set; }
                public int Id { get; set; }
                public int Location { get; set; } // -1:opponentBoard, 0:playersHand, 1:playersBoard
                public int Type { get; set; } // 0:Creature, 1:Green, 2:Red, 3:Blue
                public int Cost { get; set; }
                public int Attack { get; set; }
                public int Defense { get; set; }
                public int MyHealthChange { get; set; }
                public int OppHealthChange { get; set; }
                public int CardDraw { get; set; }
                public Keywords Abilities;

                public Card (string[] data) {
                    this.CardNumber = int.Parse (data[0]);
                    this.Id = int.Parse (data[1]);
                    this.Location = int.Parse (data[2]);
                    this.Type = int.Parse (data[3]);
                    this.Cost = int.Parse (data[4]);
                    this.Attack = int.Parse (data[5]);
                    this.Defense = int.Parse (data[6]);
                    this.Abilities = new Keywords (data[7]);
                    this.MyHealthChange = int.Parse (data[8]);
                    this.OppHealthChange = int.Parse (data[9]);
                    this.CardDraw = int.Parse (data[10]);
                }
            }

            public class Unit {
                public int Id { get; set; }
                public int Attack { get; set; }
                public int Defense { get; set; }
                public int Cost { get; set; } //?
                public int MyHealthChange { get; set; } //?
                public int OppHealthChange { get; set; } //?
                public int CardDraw { get; set; } //?
                public bool CanAttack { get; set; }
                public bool HasAttacked { get; set; }
                public Keywords Abilities;
                public Card BaseCard;

                public Unit (Unit creature) {
                    this.Id = creature.Id;
                    this.Attack = creature.Attack;
                    this.Defense = creature.Defense;
                    this.Cost = creature.Cost;
                    this.CanAttack = creature.CanAttack;
                    this.HasAttacked = creature.HasAttacked;
                    this.Abilities = new Keywords (creature.Abilities.toString ());
                    this.BaseCard = creature.BaseCard;
                }

                public Unit (Card card) {
                    this.Id = card.Id;
                    this.Attack = card.Attack;
                    this.Defense = card.Defense;
                    this.Cost = card.Cost;
                    this.CanAttack = card.Abilities.HasCharge;
                    this.HasAttacked = false;
                    this.MyHealthChange = card.MyHealthChange;
                    this.OppHealthChange = card.OppHealthChange;
                    this.CardDraw = card.CardDraw;
                    this.Abilities = new Keywords (card.Abilities.toString ());
                    this.BaseCard = card;
                }

                public Unit (Card card, bool CanAttack) {
                    this.Id = card.Id;
                    this.Attack = card.Attack;
                    this.Defense = card.Defense;
                    this.Cost = card.Cost;
                    this.CanAttack = CanAttack;
                    this.HasAttacked = false;
                    this.MyHealthChange = card.MyHealthChange;
                    this.OppHealthChange = card.OppHealthChange;
                    this.CardDraw = card.CardDraw;
                    this.Abilities = new Keywords (card.Abilities.toString ());
                    this.BaseCard = card;
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
                public bool HasBreakthrough { get; set; }
                public bool HasCharge { get; set; }
                public bool HasDrain { get; set; }
                public bool HasGuard { get; set; }
                public bool HasLethal { get; set; }
                public bool HasWard { get; set; }

                public bool hasAnyKeyword () {
                    return HasBreakthrough || HasCharge || HasDrain || HasGuard || HasLethal || HasWard;
                }

                public Keywords (string data) {
                    this.HasBreakthrough = data[0] == 'B';
                    this.HasCharge = data[1] == 'C';
                    this.HasDrain = data[2] == 'D';
                    this.HasGuard = data[3] == 'G';
                    this.HasLethal = data[4] == 'L';
                    this.HasWard = data[5] == 'W';
                }

                public Keywords (Keywords keywords) {
                    this.HasBreakthrough = keywords.HasBreakthrough;
                    this.HasCharge = keywords.HasCharge;
                    this.HasDrain = keywords.HasDrain;
                    this.HasGuard = keywords.HasGuard;
                    this.HasLethal = keywords.HasLethal;
                    this.HasWard = keywords.HasWard;
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
                public int Type { get; set; } // 1:SUMMON, 2:ATTACK, 3:USE, 4:PASS
                public int Id1 { get; set; }
                public int Id2 { get; set; }
                public Unit UnitRef;
                public Card CardRef;
                public Unit TargetRef;
                public bool IsValid { get; protected set; }
                public float Rating { get; protected set; }
                public ActionResult Result;

                // SUMMON   Action (1, Id1, 0, null, CardRef, null);
                // ATTACK   Action (2, Id1, Id2/-1, UnitRef, null, TargetRef/null);
                // USE      Action (3, Id1, Id2/-1, null, CardRef, TargetRef/null);
                // PASS     Action (4, 0, 0, null, null, null);

                public string TypeToString () {
                    switch (Type) {
                        case 1:
                            return "SUMMON";
                        case 2:
                            return "ATTACK";
                        case 3:
                            return "USE";
                        case 4:
                            return "PASS";
                        default:
                            Console.Error.WriteLine ("TypeToString ERROR");
                            return "";
                            break;
                    }
                }

                public Action () { }
                // MAIN
                public Action (int Type, int Id1, int Id2, Unit UnitRef, Card CardRef, Unit TargetRef) {
                    this.Type = Type;
                    this.Id1 = Id1;
                    this.Id2 = Id2;
                    this.UnitRef = UnitRef;
                    this.CardRef = CardRef;
                    this.TargetRef = TargetRef;
                    this.IsValid = true;
                }

                public void setRating (float Rating) {
                    this.Rating = Rating;
                }

                public void setIsValid (bool IsValid) {
                    this.IsValid = IsValid;
                }

                public void resolve () {
                    switch (Type) {
                        case 1: // SUMMON
                            Result = new SummonResult (CardRef);
                            break;
                        case 2: // ATTACK
                            if (Id2 == -1) {
                                Result = new AttackResult (UnitRef);
                            } else {
                                Result = new AttackResult (UnitRef, TargetRef);
                            }
                            break;
                        case 3:
                            Console.Error.WriteLine ("resolve:use not handled");
                            break;
                        case 4:
                            Console.Error.WriteLine ("resolve:pass not handled");
                            break;
                        default:
                            Console.Error.WriteLine ("Resolve error ##################1");
                            break;
                    }
                }
            }

            public class ActionResult {
                public int AttackerHealthChange { get; set; }
                public int DefenderHealthChange { get; set; }

                public int AttackerDefenseChange { get; set; }
                public int DefenderDefenseChange { get; set; }

                public bool AttackerDied { get; set; }
                public bool DefenderDied { get; set; }
            }

            public class SummonResult : ActionResult {
                public Card Summoning;

                public SummonResult (Card Summoning) {
                    this.Summoning = Summoning;
                }
            }

            public class AttackResult : ActionResult {
                public Unit Attacker;
                public Unit Defender;
                public bool Face { get; set; }

                public AttackResult (Unit Attacker, Unit Defender) {
                    this.Attacker = Attacker;
                    this.Defender = Defender;
                    this.Face = false;
                    abc ();
                }

                public AttackResult (Unit Attacker) {
                    this.Attacker = Attacker;
                    this.Defender = null;
                    this.Face = true;
                    abc ();
                }

                void abc () {
                    //todo
                }
            }

            public class UseResult : ActionResult {
                public int AttackerAttackChange { get; set; }
                public int DefenderAttackChange { get; set; }
                public Card Item;
                public Unit Target;

                public UseResult (Card Item, Unit Target) {
                    this.Item = Item;
                    this.Target = Target;
                    sth ();
                }

                void sth () {
                    //todo
                }

            }

            public static class Globals {
                public const Int32 maxBoard = 6;
                public const int maxHand = 8;
                public const int maxCardCost = 12;
                public const bool debug = false;
            }

            class Player {
                static void Main (string[] args) {

                    PlayerStats player;
                    PlayerStats opponent;

                    while (true) {

                        player = new PlayerStats (Console.ReadLine ().Split (' '));
                        opponent = new PlayerStats (Console.ReadLine ().Split (' '));
                        opponent.setHand (int.Parse (Console.ReadLine ()));

                        int cardCount = int.Parse (Console.ReadLine ());

                        Queue PlayerHand = new Queue ();
                        Queue PlayerBoard = new Queue ();
                        Queue OpponentBoard = new Queue ();

                        if (player.Mana == 0) {

                            EvalPick (new Card (Console.ReadLine ().Split (' ')),
                                new Card (Console.ReadLine ().Split (' ')),
                                new Card (Console.ReadLine ().Split (' ')));

                        } else {
                            for (int i = 0; i < cardCount; i++) {

                                Card card = new Card (Console.ReadLine ().Split (' '));

                                if (Globals.debug) {
                                    if (card == null) {
                                        Console.Error.WriteLine ("card.isNull:" + "true");
                                    }
                                    if (card.Abilities == null) {
                                        Console.Error.WriteLine ("card.Abilities:" + "null");
                                    }
                                }
                                switch (card.Location) {
                                    case -1:
                                        OpponentBoard.Enqueue (new Unit (card, true));
                                        break;
                                    case 0:
                                        PlayerHand.Enqueue (card);
                                        break;
                                    case 1:
                                        PlayerBoard.Enqueue (new Unit (card, true));
                                        break;
                                    default:
                                        Console.Error.WriteLine ("Data loading error ##################1");
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
                static Queue LegalizeMyBoard (Queue units, PlayerStats player) {
                    Queue LegalBoard = new Queue ();
                    foreach (Unit unit in units) {
                        if (unit.Defense > 0 || unit.HasAttacked == false) {
                            LegalBoard.Enqueue (unit);
                        }
                    }
                    return LegalBoard;
                }
                static Queue LegalizeEnemyBoard (Queue units) {
                    Queue LegalBoard = new Queue ();
                    foreach (Unit unit in units) {
                        if (unit.Defense > 0) {
                            LegalBoard.Enqueue (unit);
                        }
                    }
                    return LegalBoard;
                }

                static void ValidAction (Action action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
                    switch (action.Type) {
                        case 1: // SUMMON
                            if (action.CardRef.Type != 0 ||
                                MyBoard.Count == Globals.maxBoard) {
                                action.setIsValid (false);
                            }
                            break;
                        case 2: // ATTACK
                            if (action.UnitRef.CanAttack == false) {
                                action.setIsValid (false);
                            }
                            bool enemyHasGuard = false;
                            foreach (Unit enemy in EnemyBoard) {
                                if (enemy.Abilities.HasGuard) {
                                    enemyHasGuard = true;
                                    break;
                                }
                            }
                            if (enemyHasGuard) {
                                if (action.Id2 == -1) {
                                    action.setIsValid (false);
                                } else if (action.TargetRef.Abilities.HasGuard == false) {
                                    action.setIsValid (false);
                                }
                            }
                            break;
                        default:
                            Console.Error.WriteLine ("ValidAction error ##################1");
                            break;
                    }
                }

                static void EvalPick (Card card1, Card card2, Card card3) {
                    int eval1 = 0;
                    int eval2 = 0;
                    int eval3 = 0;

                    // public Eval(){}
                    // public void Pick(){}

                    if (card1.Type == 0) {
                        Console.WriteLine ("PICK 0");

                    } else if (card2.Type == 0) {
                        Console.WriteLine ("PICK 1");

                    } else if (card3.Type == 0) {
                        Console.WriteLine ("PICK 2");

                    } else {
                        Console.WriteLine ("PASS");
                    }
                }

                static void PlayTurn (Queue LegalHand, Queue MyLegalBoard, Queue EnemyLegalBoard, PlayerStats player, PlayerStats opponent) {

                    List<Action> PlannedActions = new List<Action> ();
                    List<Action> PotencialActions;

                    bool notPass = true;

                    do {

                        if (Globals.debug) {
                            Console.Error.WriteLine ("LegalBoard:" + MyLegalBoard.Count);
                        }

                        PotencialActions = new List<Action> ();
                        LegalHand = LegalizeHand (LegalHand, player);
                        MyLegalBoard = LegalizeMyBoard (MyLegalBoard, player);
                        EnemyLegalBoard = LegalizeEnemyBoard (EnemyLegalBoard);

                        foreach (Unit unit in MyLegalBoard) {
                            foreach (Unit enemy in EnemyLegalBoard) {
                                PotencialActions.Add (new Action (2, unit.Id, enemy.Id, unit, null, enemy));
                            }
                            PotencialActions.Add (new Action (2, unit.Id, -1, unit, null, null));
                        }

                        if (Globals.debug) {
                            Console.Error.WriteLine ("LegalHand:" + LegalHand.Count);
                        }

                        foreach (Card card in LegalHand) {
                            PotencialActions.Add (new Action (1, card.Id, 0, null, card, null));
                        }

                        if (Globals.debug) {
                            Console.Error.WriteLine ("PotencialActions:" + PotencialActions.Count);
                        }

                        foreach (Action action in PotencialActions) {
                            ValidAction (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                        }

                        foreach (Action action in PotencialActions) {
                            if (action.IsValid) {
                                action.setRating (EvalPlay (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent));
                            }
                        }

                        if (Globals.debug) {
                            foreach (Action action in PotencialActions) {
                                if (action.IsValid) {
                                    Console.Error.WriteLine ("----------");
                                    Console.Error.Write ("Type:" + action.TypeToString ());
                                    Console.Error.WriteLine (" " + action.Id1 + " " + action.Id2);
                                    Console.Error.WriteLine ("Rating:" + action.Rating);
                                    Console.Error.WriteLine ("**********");
                                }
                            }
                        }

                        if (PotencialActions.Count > 0) {
                            Action best = new Action ();
                            foreach (Action action in PotencialActions) {
                                if (action.IsValid) {
                                    best = action;
                                    break;
                                }
                            }
                            foreach (Action action in PotencialActions) {
                                if (best.Rating < action.Rating) {
                                    best = action;
                                }
                            }

                            if (best.Rating <= 0) {
                                break;
                            } else {
                                PlannedActions.Add (best);
                                PerformAction (best, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                            }
                        } else {
                            notPass = false;
                        }
                        if (Globals.debug) {
                            Console.Error.WriteLine ("PlannedActions: " + PlannedActions.Count);
                            foreach (Action action in PlannedActions) {
                                Console.Error.Write (action.TypeToString () + " ");
                                Console.Error.Write (action.Id1);
                                if (action.Type == 3 || action.Type == 2) {
                                    Console.Error.WriteLine (" " + action.Id2);
                                } else {
                                    Console.Error.WriteLine ();
                                }
                            }
                        }

                    }
                    while (notPass && (LegalHand.Count > 0 || MyLegalBoard.Count > 0));

                    if (PlannedActions.Count > 0) {

                        string command = "";

                        foreach (Action action in PlannedActions) {

                            if (command != "") {
                                command += "; ";
                            }

                            switch (action.Type) {

                                case 1:
                                    command += "SUMMON " + action.CardRef.Id;
                                    break;
                                case 2:
                                    command += "ATTACK " + action.UnitRef.Id + " ";
                                    if (action.TargetRef == null) {
                                        command += -1;
                                    } else {
                                        command += action.TargetRef.Id;
                                    }
                                    break;
                                case 3:
                                    command += "USE " + action.CardRef.Id + " ";
                                    if (action.CardRef.Type == 1 || action.CardRef.Type == 2) {
                                        command += action.TargetRef.Id;
                                    } else {
                                        command += "-1";
                                    }
                                    break;
                                case 4:
                                    Console.Error.WriteLine ("Command construction error ##################1");
                                    break;
                                default:
                                    Console.Error.WriteLine ("Command construction error ##################2");
                                    break;
                            }
                        }
                        Console.WriteLine (command);
                    } else {
                        Console.WriteLine ("PASS");
                    }
                }

                static float EvalPlay (Action action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
                    float value = 0;
                    switch (action.Type) {
                        case 1: //SUMMON
                            if (player.Mana >= action.CardRef.Cost) {
                                value += 1;
                            }
                            if (player.Mana == action.CardRef.Cost) {
                                value += 1;
                            }
                            if (action.CardRef.Abilities.HasGuard) {
                                value += 1;
                            }
                            if (action.CardRef.CardDraw > 0) {
                                if (MyHand.Count + action.CardRef.CardDraw >= Globals.maxBoard - 1) {
                                    if (MyHand.Count == Globals.maxBoard) {
                                        value -= 5;
                                    }
                                    value -= 5;
                                } else {
                                    value += 5;
                                }
                            }
                            if (action.CardRef.Abilities.HasCharge) {
                                value += 1;
                            }
                            break;
                        case 2: //ATTACK

                            if (action.Id2 == -1) {
                                if (opponent.Health <= action.UnitRef.Attack) {
                                    value += 99;
                                }
                                value += 1;
                            } else {
                                if (action.TargetRef.Abilities.HasBreakthrough) {
                                    value += 2;
                                }
                                if (action.TargetRef.Attack < 1) {
                                    value += 1;
                                }
                            }
                            if (action.UnitRef.Attack < 1) {
                                value -= 100;
                            }
                            break;
                        case 3:
                            Console.Error.WriteLine ("eval:use not handled");
                            break;
                        case 4:
                            Console.Error.WriteLine ("eval:pass not handled");
                            break;
                        default:
                            Console.Error.WriteLine ("Eval error ##################1");
                            break;
                    }
                    return value;
                }

                static void PerformAction (Action action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
                    switch (action.Type) {

                        case 1: // SUMMON
                            Unit unit = new Unit (action.CardRef);
                            player.Mana -= action.CardRef.Cost;
                            while (MyHand.Peek () != action.CardRef) {
                                MyHand.Enqueue (MyHand.Dequeue ());
                            }
                            MyHand.Dequeue ();
                            MyBoard.Enqueue (unit);
                            break;

                        case 2: // ATTACK
                            action.UnitRef.CanAttack = false;
                            action.UnitRef.HasAttacked = true;
                            if (action.Id2 == -1) {
                                opponent.Health -= action.UnitRef.Attack;
                            } else {
                                action.TargetRef.Defense -= action.UnitRef.Attack;
                            }
                            break;

                        case 3: // USE
                            Console.Error.WriteLine ("perform: use not handled");
                            break;

                        case 4: // PASS
                            Console.Error.WriteLine ("pass: use not handled");
                            break;

                        default:
                            Console.Error.WriteLine ("Perform error ##################1");
                            break;
                    }
                }
            }