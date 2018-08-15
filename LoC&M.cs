#define DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//todo: pass useful
//todo: use eval
//todo: count cardDraw
//todo: count runes
public class PlayerStats {
    public int Health { get; set; }
    public int Mana { get; set; }
    public int Deck { get; set; }
    public int Rune { get; set; }
    public int Hand { get; set; }
    public int NextTurnDraw { get; set; }

    public PlayerStats (string[] data) {
        this.Health = int.Parse (data[0]);
        this.Mana = int.Parse (data[1]);
        this.Deck = int.Parse (data[2]);
        this.Rune = int.Parse (data[3]);
        this.NextTurnDraw = 1;
    }
    public void setHand (int value) {
        this.Hand = value;
    }
}

public class CardDataBase {
    List<CardData> AllCards = new List<CardData> ();

    public CardDataBase () {
        string RawData = "1;0;1;2;1;------;1;0;0.2;0;1;1;2;------;0;-1;0.3;0;1;2;2;------;0;0;0.4;0;2;1;5;------;0;0;0.5;0;2;4;1;------;0;0;0.6;0;2;3;2;------;0;0;0.7;0;2;2;2;-----W;0;0;0.8;0;2;2;3;------;0;0;0.9;0;3;3;4;------;0;0;0.10;0;3;3;1;--D---;0;0;0.11;0;3;5;2;------;0;0;0.12;0;3;2;5;------;0;0;0.13;0;4;5;3;------;1;-1;0.14;0;4;9;1;------;0;0;0.15;0;4;4;5;------;0;0;0.16;0;4;6;2;------;0;0;0.17;0;4;4;5;------;0;0;0.18;0;4;7;4;------;0;0;0.19;0;5;5;6;------;0;0;0.20;0;5;8;2;------;0;0;0.21;0;5;6;5;------;0;0;0.22;0;6;7;5;------;0;0;0.23;0;7;8;8;------;0;0;0.24;0;1;1;1;------;0;-1;0.25;0;2;3;1;------;-2;-2;0.26;0;2;3;2;------;0;-1;0.27;0;2;2;2;------;2;0;0.28;0;2;1;2;------;0;0;1.29;0;2;2;1;------;0;0;1.30;0;3;4;2;------;0;-2;0.31;0;3;3;1;------;0;-1;0.32;0;3;3;2;------;0;0;1.33;0;4;4;3;------;0;0;1.34;0;5;3;5;------;0;0;1.35;0;6;5;2;B-----;0;0;1.36;0;6;4;4;------;0;0;2.37;0;6;5;7;------;0;0;1.38;0;1;1;3;--D---;0;0;0.39;0;1;2;1;--D---;0;0;0.40;0;3;2;3;--DG--;0;0;0.41;0;3;2;2;-CD---;0;0;0.42;0;4;4;2;--D---;0;0;0.43;0;6;5;5;--D---;0;0;0.44;0;6;3;7;--D-L-;0;0;0.45;0;6;6;5;B-D---;-3;0;0.46;0;9;7;7;--D---;0;0;0.47;0;2;1;5;--D---;0;0;0.48;0;1;1;1;----L-;0;0;0.49;0;2;1;2;---GL-;0;0;0.50;0;3;3;2;----L-;0;0;0.51;0;4;3;5;----L-;0;0;0.52;0;4;2;4;----L-;0;0;0.53;0;4;1;1;-C--L-;0;0;0.54;0;3;2;2;----L-;0;0;0.55;0;2;0;5;---G--;0;0;0.56;0;4;2;7;------;0;0;0.57;0;4;1;8;------;0;0;0.58;0;6;5;6;B-----;0;0;0.59;0;7;7;7;------;1;-1;0.60;0;7;4;8;------;0;0;0.61;0;9;10;10;------;0;0;0.62;0;12;12;12;B--G--;0;0;0.63;0;2;0;4;---G-W;0;0;0.64;0;2;1;1;---G-W;0;0;0.65;0;2;2;2;-----W;0;0;0.66;0;5;5;1;-----W;0;0;0.67;0;6;5;5;-----W;0;-2;0.68;0;6;7;5;-----W;0;0;0.69;0;3;4;4;B-----;0;0;0.70;0;4;6;3;B-----;0;0;0.71;0;4;3;2;BC----;0;0;0.72;0;4;5;3;B-----;0;0;0.73;0;4;4;4;B-----;4;0;0.74;0;5;5;4;B--G--;0;0;0.75;0;5;6;5;B-----;0;0;0.76;0;6;5;5;B-D---;0;0;0.77;0;7;7;7;B-----;0;0;0.78;0;8;5;5;B-----;0;-5;0.79;0;8;8;8;B-----;0;0;0.80;0;8;8;8;B--G--;0;0;1.81;0;9;6;6;BC----;0;0;0.82;0;7;5;5;B-D--W;0;0;0.83;0;0;1;1;-C----;0;0;0.84;0;2;1;1;-CD--W;0;0;0.85;0;3;2;3;-C----;0;0;0.86;0;3;1;5;-C----;0;0;0.87;0;4;2;5;-C-G--;0;0;0.88;0;5;4;4;-C----;0;0;0.89;0;5;4;1;-C----;2;0;0.90;0;8;5;5;-C----;0;0;0.91;0;0;1;2;---G--;0;1;0.92;0;1;0;1;---G--;2;0;0.93;0;1;2;1;---G--;0;0;0.94;0;2;1;4;---G--;0;0;0.95;0;2;2;3;---G--;0;0;0.96;0;2;3;2;---G--;0;0;0.97;0;3;3;3;---G--;0;0;0.98;0;3;2;4;---G--;0;0;0.99;0;3;2;5;---G--;0;0;0.100;0;3;1;6;---G--;0;0;0.101;0;4;3;4;---G--;0;0;0.102;0;4;3;3;---G--;0;-1;0.103;0;4;3;6;---G--;0;0;0.104;0;4;4;4;---G--;0;0;0.105;0;5;4;6;---G--;0;0;0.106;0;5;5;5;---G--;0;0;0.107;0;5;3;3;---G--;3;0;0.108;0;5;2;6;---G--;0;0;0.109;0;5;5;6;------;0;0;0.110;0;5;0;9;---G--;0;0;0.111;0;6;6;6;---G--;0;0;0.112;0;6;4;7;---G--;0;0;0.113;0;6;2;4;---G--;4;0;0.114;0;7;7;7;---G--;0;0;0.115;0;8;5;5;---G-W;0;0;0.116;0;12;8;8;BCDGLW;0;0;0.117;1;1;1;1;B-----;0;0;0.118;1;0;0;3;------;0;0;0.119;1;1;1;2;------;0;0;0.120;1;2;1;0;----L-;0;0;0.121;1;2;0;3;------;0;0;1.122;1;2;1;3;---G--;0;0;0.123;1;2;4;0;------;0;0;0.124;1;3;2;1;--D---;0;0;0.125;1;3;1;4;------;0;0;0.126;1;3;2;3;------;0;0;0.127;1;3;0;6;------;0;0;0.128;1;4;4;3;------;0;0;0.129;1;4;2;5;------;0;0;0.130;1;4;0;6;------;4;0;0.131;1;4;4;1;------;0;0;0.132;1;5;3;3;B-----;0;0;0.133;1;5;4;0;-----W;0;0;0.134;1;4;2;2;------;0;0;1.135;1;6;5;5;------;0;0;0.136;1;0;1;1;------;0;0;0.137;1;2;0;0;-----W;0;0;0.138;1;2;0;0;---G--;0;0;1.139;1;4;0;0;----LW;0;0;0.140;1;2;0;0;-C----;0;0;0.141;2;0;-1;-1;------;0;0;0.142;2;0;0;0;BCDGLW;0;0;0.143;2;0;0;0;---G--;0;0;0.144;2;1;0;-2;------;0;0;0.145;2;3;-2;-2;------;0;0;0.146;2;4;-2;-2;------;0;-2;0.147;2;2;0;-1;------;0;0;1.148;2;2;0;-2;BCDGLW;0;0;0.149;2;3;0;0;BCDGLW;0;0;1.150;2;2;0;-3;------;0;0;0.151;2;5;0;-99;BCDGLW;0;0;0.152;2;7;0;-7;------;0;0;1.153;2;2;0;0;------;5;0;0.154;2;2;0;0;------;0;-2;1.155;2;3;0;-3;------;0;-1;0.156;2;3;0;0;------;3;-3;0.157;2;3;0;-1;------;1;0;1.158;2;3;0;-4;------;0;0;0.159;2;4;0;-3;------;3;0;0.160;2;2;0;0;------;2;-2;0";
        // string[] SlicedData = RawData.Split ('.');
        foreach (string data in RawData.Split ('.')) {
            AllCards.Add (new CardData (data.Split (';')));
        }
    }
}

public class GameDataBase {
    public List<Pick> DraftPicks; // holds certain pick
    public List<int> Uncertain; // holds CardData.CardNumber enemy played
    public Dictionary<int, int> GameCards; //holds card possible copies <CardData.CardNumber, Number>

    public GameDataBase () {
        DraftPicks = new List<Pick> ();
        Uncertain = new List<int> ();
        GameCards = new Dictionary<int, int> ();
    }
}

public class Pick {
    CardData MyPick;
    CardData EnemyPick;
    CardData card1;
    CardData card2;
    CardData card3;

    public Pick (Dictionary<int, int> GameCards, CardData card1, CardData card2, CardData card3) {
        this.card1 = card1;
        this.card2 = card2;
        this.card3 = card3;
        if (GameCards.ContainsKey (card1.CardNumber)) {
            GameCards[card1.CardNumber] = GameCards[card1.CardNumber]++;
        } else {
            GameCards.Add (card1.CardNumber, 1);
        }
        if (GameCards.ContainsKey (card2.CardNumber)) {
            GameCards[card2.CardNumber] = GameCards[card2.CardNumber]++;
        } else {
            GameCards.Add (card2.CardNumber, 1);
        }
        if (GameCards.ContainsKey (card3.CardNumber)) {
            GameCards[card3.CardNumber] = GameCards[card3.CardNumber]++;
        } else {
            GameCards.Add (card3.CardNumber, 1);
        }
        Eval ();
    }

    void Eval () {
        int eval1 = 0;
        int eval2 = 0;
        int eval3 = 0;

        // if (card1.Type == 0) {
        //     MyPick = card1;
        //     Console.WriteLine ("PICK 0");

        // } else if (card2.Type == 0) {
        //     MyPick = card2;
        //     Console.WriteLine ("PICK 1");

        // } else if (card3.Type == 0) {
        //     MyPick = card3;
        //     Console.WriteLine ("PICK 2");

        // } else {
        MyPick = card1;
        Console.WriteLine ("PASS");
        // }
    }
}

public class CardData {
    public int CardNumber { get; set; }
    public int Type { get; set; } // 0:Creature, 1:Green, 2:Red, 3:Blue
    public int Cost { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public Keywords Abilities;
    public int MyHealthChange { get; set; }
    public int OppHealthChange { get; set; }
    public int CardDraw { get; set; }

    public CardData (string[] data) {
        this.CardNumber = int.Parse (data[0]);
        this.Type = int.Parse (data[1]);
        this.Cost = int.Parse (data[2]);
        this.Attack = int.Parse (data[3]);
        this.Defense = int.Parse (data[4]);
        this.Abilities = new Keywords (data[5]);
        this.MyHealthChange = int.Parse (data[6]);
        this.OppHealthChange = int.Parse (data[7]);
        this.CardDraw = int.Parse (data[8]);
    }
    public CardData () { }
}

public class Card : CardData {

    public int Id { get; set; }
    public int Location { get; set; } // -1:opponentBoard, 0:playersHand, 1:playersBoard

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
    public bool CanAttack { get; set; }
    public bool HasAttacked { get; set; }
    public Keywords Abilities;
    public Card BaseCard;

    public Unit (Unit creature) {
        this.Id = creature.Id;
        this.Attack = creature.Attack;
        this.Defense = creature.Defense;
        this.CanAttack = creature.CanAttack;
        this.HasAttacked = creature.HasAttacked;
        this.Abilities = new Keywords (creature.Abilities.toString ());
        this.BaseCard = creature.BaseCard;
    }

    public Unit (Card card) {
        this.Id = card.Id;
        this.Attack = card.Attack;
        this.Defense = card.Defense;
        this.CanAttack = card.Abilities.HasCharge;
        this.HasAttacked = false;
        this.Abilities = new Keywords (card.Abilities.toString ());
        this.BaseCard = card;
    }

    public Unit (Card card, bool CanAttack) {
        this.Id = card.Id;
        this.Attack = card.Attack;
        this.Defense = card.Defense;
        this.CanAttack = CanAttack;
        this.HasAttacked = false;
        this.Abilities = new Keywords (card.Abilities.toString ());
        this.BaseCard = card;
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

    public void ChangeKeywords (Keywords keywords, bool value) {
        if (keywords.HasBreakthrough) {
            this.HasBreakthrough = value;
        }
        if (keywords.HasCharge) {
            this.HasCharge = value;
        }
        if (keywords.HasDrain) {
            this.HasDrain = value;
        }
        if (keywords.HasGuard) {
            this.HasGuard = value;
        }
        if (keywords.HasLethal) {
            this.HasLethal = value;
        }
        if (keywords.HasWard) {
            this.HasWard = value;
        }
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
        }
    }

    // MAIN
    public Action (int Type, int Id1, int Id2, Unit UnitRef, Card CardRef, Unit TargetRef) {
        this.Type = Type;
        this.Id1 = Id1;
        this.Id2 = Id2;
        this.UnitRef = UnitRef;
        this.CardRef = CardRef;
        this.TargetRef = TargetRef;
        this.IsValid = false;
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
                Result = new AttackResult (UnitRef, TargetRef);
                break;
            case 3: //USE
                Result = new UseResult (CardRef, TargetRef);
                break;
            case 4:
                Console.Error.WriteLine ("resolve:pass not handled");
                break;
            default:
                Console.Error.WriteLine ("resolve error ##################1");
                break;
        }
    }
}

public class ActionResult { //Player:Attacker, Enemy:Defender
    public int AttackerHealthChange { get; set; }
    public int DefenderHealthChange { get; set; }
    public bool AttackerDied { get; set; }
    public bool DefenderDied { get; set; }
    public Unit NewAttacker;
    public Unit NewDefender;
    public Unit NewTargetRef;
}

public class SummonResult : ActionResult {

    public SummonResult (Card CardRef) {
        this.NewAttacker = new Unit (CardRef);
    }
}

public class AttackResult : ActionResult {
    public Unit Attacker;
    public Unit Defender;
    public bool goFace { get; set; }

    public AttackResult (Unit Attacker, Unit Defender) {
        this.Attacker = Attacker;
        this.Defender = Defender;
        this.AttackerHealthChange = 0;
        this.DefenderHealthChange = 0;
        this.NewAttacker = new Unit (Attacker);
        this.NewAttacker.HasAttacked = true;
        if (Defender == null) {
            this.goFace = true;
            this.NewDefender = null;
        } else {
            this.goFace = false;
            this.NewDefender = new Unit (Defender);
        }
        someMath ();
    }

    private void someMath () {
        if (goFace) {
            DefenderHealthChange = -Attacker.Attack;
            //Drain A
            if (Attacker.Abilities.HasDrain) {
                AttackerHealthChange = Attacker.Attack;
            }
        } else {

            //Ward A
            if (Attacker.Abilities.HasWard && Defender.Attack > 0) {
                NewAttacker.Abilities.HasWard = false;
            } else {
                NewAttacker.Defense -= Defender.Attack;
            }

            //Ward D
            if (Defender.Abilities.HasWard && Attacker.Attack > 0) {
                NewDefender.Abilities.HasWard = false;
            } else {
                NewDefender.Defense -= Attacker.Attack;
            }

            //Lethal A
            if (NewDefender.Defense < 0 ||
                (NewDefender.Defense < Defender.Defense &&
                    Attacker.Abilities.HasLethal)) {
                DefenderDied = true;
            } else {
                DefenderDied = false;
            }

            //Lethal D
            if (NewAttacker.Defense < 0 ||
                (NewAttacker.Defense < Attacker.Defense &&
                    Defender.Abilities.HasLethal)) {
                AttackerDied = true;
            } else {
                AttackerDied = false;
            }

            //Breakthrough A
            if (Attacker.Abilities.HasBreakthrough) {
                if (NewDefender.Defense < 0) {
                    //Drain A
                    if (Attacker.Abilities.HasDrain) {
                        AttackerHealthChange += -NewDefender.Defense;
                    }
                }
            }

            //Drain A
            if (Attacker.Abilities.HasDrain &&
                NewDefender.Defense < Defender.Defense) {
                AttackerHealthChange += Math.Abs (NewDefender.Defense - Defender.Defense);
            }
        }
    }
}

public class UseResult : ActionResult {
    public Card CardRef;

    public UseResult (Card CardRef, Unit TargetRef) {
        this.CardRef = CardRef;
        if (TargetRef != null) {
            this.NewTargetRef = new Unit (TargetRef);
        }
        someMath ();
    }

    void someMath () {

        if (CardRef.Type == 1) { //Green

            AttackerHealthChange = CardRef.MyHealthChange;
            NewTargetRef.Attack += CardRef.Attack;
            NewTargetRef.Defense += CardRef.Defense;
            if (CardRef.Abilities.hasAnyKeyword ()) {
                NewTargetRef.Abilities.ChangeKeywords (CardRef.Abilities, true);
            }

        } else if (CardRef.Type == 2) { //Red

            DefenderHealthChange = CardRef.OppHealthChange;
            if (CardRef.Abilities.hasAnyKeyword ()) {
                NewTargetRef.Abilities.ChangeKeywords (CardRef.Abilities, false);
            }
            if (CardRef.Defense != 0) {
                if (NewTargetRef.Abilities.HasWard) {
                    NewTargetRef.Abilities.HasWard = false;
                } else {
                    NewTargetRef.Attack += CardRef.Attack;
                    NewTargetRef.Defense += CardRef.Defense;
                }
            }
            if (NewTargetRef.Defense < 0) {
                DefenderDied = true;
            }

        } else { //Blue

            AttackerHealthChange = CardRef.MyHealthChange;
            DefenderHealthChange = CardRef.OppHealthChange;
            if (NewTargetRef != null) {
                if (CardRef.Defense != 0) {
                    if (NewTargetRef.Abilities.HasWard) {
                        NewTargetRef.Abilities.HasWard = false;
                    } else {
                        NewTargetRef.Defense += CardRef.Defense;
                    }
                }
                if (NewTargetRef.Defense < 0) {
                    DefenderDied = true;
                }
            }
        }
    }
}

public static class Globals {
    public const Int32 maxBoard = 6;
    public const int maxHand = 8;
    public const int maxCardCost = 12;
}

class Player {
    static void Main (string[] args) {
        CardDataBase DataBase = new CardDataBase ();
        GameDataBase GameBase = new GameDataBase ();

        PlayerStats player;
        PlayerStats opponent;

        while (true) {

            player = new PlayerStats (Console.ReadLine ().Split (' '));
            opponent = new PlayerStats (Console.ReadLine ().Split (' '));
            opponent.setHand (int.Parse (Console.ReadLine ()));

            int CardCount = int.Parse (Console.ReadLine ());

            Queue PlayerHand = new Queue ();
            Queue PlayerBoard = new Queue ();
            Queue OpponentBoard = new Queue ();

            if (player.Mana == 0) {

                GameBase.DraftPicks.Add (new Pick (GameBase.GameCards,
                    new Card (Console.ReadLine ().Split (' ')),
                    new Card (Console.ReadLine ().Split (' ')),
                    new Card (Console.ReadLine ().Split (' '))));

            } else {
                for (int i = 0; i < CardCount; i++) {

                    Card card = new Card (Console.ReadLine ().Split (' '));

#if (DEBUG) 
                    {
                        if (card == null) {
                            Console.Error.WriteLine ("card.isNull:" + "true");
                        }
                        if (card.Abilities == null) {
                            Console.Error.WriteLine ("card.Abilities:" + "null");
                        }
                    }
#endif
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

    static void ValidAction (Action action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
        bool IsValid = true;
        switch (action.Type) {
            case 1: // SUMMON
                if (MyBoard.Count == Globals.maxBoard ||
                    player.Mana < action.CardRef.Cost) {
                    IsValid = false;
                }
                break;
            case 2: // ATTACK
                if (action.UnitRef.CanAttack == false ||
                    action.UnitRef.HasAttacked == true) {
                    IsValid = false;
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
                        IsValid = false;
                    } else if (action.TargetRef.Abilities.HasGuard == false) {
                        IsValid = false;
                    }
                }
                break;
            case 3: // USE
                if (player.Mana < action.CardRef.Cost) {
                    IsValid = false;
                }
                break;
            default:
                Console.Error.WriteLine ("ValidAction error ##################1");
                break;
        }
        action.setIsValid (IsValid);
    }

    static void PlayTurn (Queue LegalHand, Queue MyLegalBoard, Queue EnemyLegalBoard, PlayerStats player, PlayerStats opponent) {

        List<Action> PotencialActions;
        List<Action> PlannedActions = new List<Action> ();

        bool Pass = false;
        int ValidPotencialActions;

        do {

            PotencialActions = new List<Action> ();
            ValidPotencialActions = 0;

            foreach (Unit unit in MyLegalBoard) {
                foreach (Unit enemy in EnemyLegalBoard) {
                    try {
                        PotencialActions.Add (new Action (2, unit.Id, enemy.Id, unit, null, enemy));
                    } catch (NullReferenceException exception) {
                        if (unit == null) {
                            Console.Error.WriteLine ("unit is null");
                        }
                        if (enemy == null) {
                            Console.Error.WriteLine ("enemy is null");
                        }
                        throw exception;
                    }
                }
                PotencialActions.Add (new Action (2, unit.Id, -1, unit, null, null));
            }

            foreach (Card card in LegalHand) {
                if (card.Type == 0) {
                    PotencialActions.Add (new Action (1, card.Id, 0, null, card, null));
                } else if (card.Type == 1) {
                    foreach (Unit unit in MyLegalBoard) {
                        PotencialActions.Add (new Action (3, card.Id, unit.Id, null, card, unit));
                    }
                } else if (card.Type == 2) {
                    foreach (Unit unit in EnemyLegalBoard) {
                        PotencialActions.Add (new Action (3, card.Id, unit.Id, null, card, unit));
                    }
                } else if (card.Type == 3) {
                    if (card.Defense != 0) {
                        foreach (Unit unit in EnemyLegalBoard) {
                            PotencialActions.Add (new Action (3, card.Id, unit.Id, null, card, unit));
                        }
                    }
                    PotencialActions.Add (new Action (3, card.Id, -1, null, card, null));
                }
            }

            foreach (Action action in PotencialActions) {
                ValidAction (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                if (action.IsValid) {
                    ValidPotencialActions++;
                    action.resolve ();
                    action.setRating (EvalPlay (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent));
                }
            }

#if (DEBUG) 
            {
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
#endif
            if (ValidPotencialActions > 0) {
                Action best = null;
                foreach (Action action in PotencialActions) {
                    if (action.IsValid) {
                        best = action;
                        break;
                    }
                }
                if (best != null) {
                    foreach (Action action in PotencialActions) {
                        if (best.Rating < action.Rating && action.IsValid) {
                            best = action;
                        }
                    }
                    if (best.Rating > 0) {
                        PlannedActions.Add (best);
                        PerformAction (best, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                    } else {
                        Pass = true;
                    }

                } else {
                    Pass = true;
                }
            } else {
                Pass = true;
            }

            if (opponent.Health <= 0) {
                Pass = true;
            }
#if (DEBUG) 
            {
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
#endif
        } while (!Pass);

        string command = "";
        if (PlannedActions.Count > 0) {

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
        }
        if (command == "") {
            Console.WriteLine ("PASS");
        }
    }

    static void PerformAction (Action action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent) {
        switch (action.Type) {

            case 1: // SUMMON
                MyBoard.Enqueue (action.Result.NewAttacker);
                player.Mana -= action.CardRef.Cost;
                while (MyHand.Peek () != action.CardRef) {
                    MyHand.Enqueue (MyHand.Dequeue ());
                }
                MyHand.Dequeue ();
                break;

            case 2: // ATTACK
                player.Health += action.Result.AttackerHealthChange;
                opponent.Health += action.Result.DefenderHealthChange;
                if (action.Id2 != -1) {
                    while (EnemyBoard.Peek () != action.TargetRef) {
                        MyBoard.Enqueue (EnemyBoard.Dequeue ());
                    }
                    EnemyBoard.Dequeue ();

                    if (!action.Result.DefenderDied) {
                        EnemyBoard.Enqueue (action.Result.NewDefender);
                    }
                }
                while (MyBoard.Peek () != action.UnitRef) {
                    MyBoard.Enqueue (MyBoard.Dequeue ());
                }
                MyBoard.Dequeue ();

                if (!action.Result.AttackerDied) {
                    MyBoard.Enqueue (action.Result.NewAttacker);
                }

                break;

            case 3: // USE
                if (action.CardRef.Type == 1) { //Green

                    player.Health += action.Result.AttackerHealthChange;
                    MyBoard.Enqueue (action.Result.NewTargetRef);

                } else if (action.CardRef.Type == 2) { //Red

                    opponent.Health += action.Result.DefenderHealthChange;
                    while (EnemyBoard.Peek () != action.TargetRef) {
                        EnemyBoard.Enqueue (EnemyBoard.Dequeue ());
                    }
                    EnemyBoard.Dequeue ();
                    if (!action.Result.DefenderDied) {
                        EnemyBoard.Enqueue (action.Result.NewTargetRef);
                    }

                } else { // Blue

                    player.Health += action.Result.AttackerHealthChange;
                    opponent.Health += action.Result.DefenderHealthChange;
                    if (action.Result.NewTargetRef != null) {

                        while (EnemyBoard.Peek () != action.TargetRef) {
                            EnemyBoard.Enqueue (EnemyBoard.Dequeue ());
                        }
                        EnemyBoard.Dequeue ();

                        if (!action.Result.DefenderDied) {
                            EnemyBoard.Enqueue (action.Result.NewTargetRef);
                        }
                    }

                }
                while (MyHand.Peek () != action.CardRef) {
                    MyHand.Enqueue (MyHand.Dequeue ());
                }
                MyHand.Dequeue ();
                break;

            case 4: // PASS
                Console.Error.WriteLine ("pass: use not handled");
                break;

            default:
                Console.Error.WriteLine ("Perform error ##################1");
                break;
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
                            value -= 1;
                        }
                        value -= 1;
                    } else {
                        value += 3;
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
                    if (action.UnitRef.Abilities.HasBreakthrough) {
                        if (action.TargetRef.Abilities.HasWard) {
                            value -= 1;
                        }
                    }
                    if (action.UnitRef.Abilities.HasLethal) {
                        value += 2;
                        if (action.TargetRef.Abilities.HasGuard) {
                            value += 3;
                        }
                        if (action.TargetRef.Abilities.HasWard) {
                            value -= 5;
                        }
                    }
                    if (action.UnitRef.Abilities.HasWard) {
                        if (action.TargetRef.Abilities.HasDrain) {
                            value += 1;
                        }
                        if (action.TargetRef.Abilities.HasLethal) {
                            value += 4;
                        }
                        if (action.TargetRef.Abilities.HasBreakthrough) {
                            value += 1;
                        }
                    }
                    if (action.TargetRef.Attack < 1) {
                        value += 1;
                    }
                }

                if (action.UnitRef.Attack < 1) { //USED AS ASSUMPTION
                    value -= 100;
                }
                break;

            case 3: //USE
                value += 1;
                // Console.Error.WriteLine ("eval:use not handled");
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
}