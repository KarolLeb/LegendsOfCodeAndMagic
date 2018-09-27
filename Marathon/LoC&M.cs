#pragma warning disable 0162
// #define DEBUG

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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

    public PlayerStats (PlayerStats PlayerStats) {
        this.Health = PlayerStats.Health;
        this.Mana = PlayerStats.Mana;
        this.Deck = PlayerStats.Deck;
        this.Rune = PlayerStats.Rune;
        this.Hand = PlayerStats.Hand;
        this.NextTurnDraw = PlayerStats.NextTurnDraw;
    }
}

public class CardDataBase {
    public List<CardData> AllCards = new List<CardData> ();

    public CardDataBase () {
        string RawData = "1;0;1;2;1;------;1;0;0.2;0;1;1;2;------;0;-1;0.3;0;1;2;2;------;0;0;0.4;0;2;1;5;------;0;0;0.5;0;2;4;1;------;0;0;0.6;0;2;3;2;------;0;0;0.7;0;2;2;2;-----W;0;0;0.8;0;2;2;3;------;0;0;0.9;0;3;3;4;------;0;0;0.10;0;3;3;1;--D---;0;0;0.11;0;3;5;2;------;0;0;0.12;0;3;2;5;------;0;0;0.13;0;4;5;3;------;1;-1;0.14;0;4;9;1;------;0;0;0.15;0;4;4;5;------;0;0;0.16;0;4;6;2;------;0;0;0.17;0;4;4;5;------;0;0;0.18;0;4;7;4;------;0;0;0.19;0;5;5;6;------;0;0;0.20;0;5;8;2;------;0;0;0.21;0;5;6;5;------;0;0;0.22;0;6;7;5;------;0;0;0.23;0;7;8;8;------;0;0;0.24;0;1;1;1;------;0;-1;0.25;0;2;3;1;------;-2;-2;0.26;0;2;3;2;------;0;-1;0.27;0;2;2;2;------;2;0;0.28;0;2;1;2;------;0;0;1.29;0;2;2;1;------;0;0;1.30;0;3;4;2;------;0;-2;0.31;0;3;3;1;------;0;-1;0.32;0;3;3;2;------;0;0;1.33;0;4;4;3;------;0;0;1.34;0;5;3;5;------;0;0;1.35;0;6;5;2;B-----;0;0;1.36;0;6;4;4;------;0;0;2.37;0;6;5;7;------;0;0;1.38;0;1;1;3;--D---;0;0;0.39;0;1;2;1;--D---;0;0;0.40;0;3;2;3;--DG--;0;0;0.41;0;3;2;2;-CD---;0;0;0.42;0;4;4;2;--D---;0;0;0.43;0;6;5;5;--D---;0;0;0.44;0;6;3;7;--D-L-;0;0;0.45;0;6;6;5;B-D---;-3;0;0.46;0;9;7;7;--D---;0;0;0.47;0;2;1;5;--D---;0;0;0.48;0;1;1;1;----L-;0;0;0.49;0;2;1;2;---GL-;0;0;0.50;0;3;3;2;----L-;0;0;0.51;0;4;3;5;----L-;0;0;0.52;0;4;2;4;----L-;0;0;0.53;0;4;1;1;-C--L-;0;0;0.54;0;3;2;2;----L-;0;0;0.55;0;2;0;5;---G--;0;0;0.56;0;4;2;7;------;0;0;0.57;0;4;1;8;------;0;0;0.58;0;6;5;6;B-----;0;0;0.59;0;7;7;7;------;1;-1;0.60;0;7;4;8;------;0;0;0.61;0;9;10;10;------;0;0;0.62;0;12;12;12;B--G--;0;0;0.63;0;2;0;4;---G-W;0;0;0.64;0;2;1;1;---G-W;0;0;0.65;0;2;2;2;-----W;0;0;0.66;0;5;5;1;-----W;0;0;0.67;0;6;5;5;-----W;0;-2;0.68;0;6;7;5;-----W;0;0;0.69;0;3;4;4;B-----;0;0;0.70;0;4;6;3;B-----;0;0;0.71;0;4;3;2;BC----;0;0;0.72;0;4;5;3;B-----;0;0;0.73;0;4;4;4;B-----;4;0;0.74;0;5;5;4;B--G--;0;0;0.75;0;5;6;5;B-----;0;0;0.76;0;6;5;5;B-D---;0;0;0.77;0;7;7;7;B-----;0;0;0.78;0;8;5;5;B-----;0;-5;0.79;0;8;8;8;B-----;0;0;0.80;0;8;8;8;B--G--;0;0;1.81;0;9;6;6;BC----;0;0;0.82;0;7;5;5;B-D--W;0;0;0.83;0;0;1;1;-C----;0;0;0.84;0;2;1;1;-CD--W;0;0;0.85;0;3;2;3;-C----;0;0;0.86;0;3;1;5;-C----;0;0;0.87;0;4;2;5;-C-G--;0;0;0.88;0;5;4;4;-C----;0;0;0.89;0;5;4;1;-C----;2;0;0.90;0;8;5;5;-C----;0;0;0.91;0;0;1;2;---G--;0;1;0.92;0;1;0;1;---G--;2;0;0.93;0;1;2;1;---G--;0;0;0.94;0;2;1;4;---G--;0;0;0.95;0;2;2;3;---G--;0;0;0.96;0;2;3;2;---G--;0;0;0.97;0;3;3;3;---G--;0;0;0.98;0;3;2;4;---G--;0;0;0.99;0;3;2;5;---G--;0;0;0.100;0;3;1;6;---G--;0;0;0.101;0;4;3;4;---G--;0;0;0.102;0;4;3;3;---G--;0;-1;0.103;0;4;3;6;---G--;0;0;0.104;0;4;4;4;---G--;0;0;0.105;0;5;4;6;---G--;0;0;0.106;0;5;5;5;---G--;0;0;0.107;0;5;3;3;---G--;3;0;0.108;0;5;2;6;---G--;0;0;0.109;0;5;5;6;------;0;0;0.110;0;5;0;9;---G--;0;0;0.111;0;6;6;6;---G--;0;0;0.112;0;6;4;7;---G--;0;0;0.113;0;6;2;4;---G--;4;0;0.114;0;7;7;7;---G--;0;0;0.115;0;8;5;5;---G-W;0;0;0.116;0;12;8;8;BCDGLW;0;0;0.117;1;1;1;1;B-----;0;0;0.118;1;0;0;3;------;0;0;0.119;1;1;1;2;------;0;0;0.120;1;2;1;0;----L-;0;0;0.121;1;2;0;3;------;0;0;1.122;1;2;1;3;---G--;0;0;0.123;1;2;4;0;------;0;0;0.124;1;3;2;1;--D---;0;0;0.125;1;3;1;4;------;0;0;0.126;1;3;2;3;------;0;0;0.127;1;3;0;6;------;0;0;0.128;1;4;4;3;------;0;0;0.129;1;4;2;5;------;0;0;0.130;1;4;0;6;------;4;0;0.131;1;4;4;1;------;0;0;0.132;1;5;3;3;B-----;0;0;0.133;1;5;4;0;-----W;0;0;0.134;1;4;2;2;------;0;0;1.135;1;6;5;5;------;0;0;0.136;1;0;1;1;------;0;0;0.137;1;2;0;0;-----W;0;0;0.138;1;2;0;0;---G--;0;0;1.139;1;4;0;0;----LW;0;0;0.140;1;2;0;0;-C----;0;0;0.141;2;0;-1;-1;------;0;0;0.142;2;0;0;0;BCDGLW;0;0;0.143;2;0;0;0;---G--;0;0;0.144;2;1;0;-2;------;0;0;0.145;2;3;-2;-2;------;0;0;0.146;2;4;-2;-2;------;0;-2;0.147;2;2;0;-1;------;0;0;1.148;2;2;0;-2;BCDGLW;0;0;0.149;2;3;0;0;BCDGLW;0;0;1.150;2;2;0;-3;------;0;0;0.151;2;5;0;-99;BCDGLW;0;0;0.152;2;7;0;-7;------;0;0;1.153;2;2;0;0;------;5;0;0.154;2;2;0;0;------;0;-2;1.155;2;3;0;-3;------;0;-1;0.156;2;3;0;0;------;3;-3;0.157;2;3;0;-1;------;1;0;1.158;2;3;0;-4;------;0;0;0.159;2;4;0;-3;------;3;0;0.160;2;2;0;0;------;2;-2;0";
        foreach (string data in RawData.Split ('.')) {
            AllCards.Add (new CardData (data.Split (';')));
        }
    }
}

public class GameDataBase {
    public ManaCurve ManaCurve;
    public List<Pick> DraftPicks; // holds certain pick
    public List<int> Uncertain; // holds CardData.CardNumber enemy played
    public Dictionary<int, int> GameCards; //holds card possible copies <CardData.CardNumber, Number>

    public GameDataBase () {
        ManaCurve = new ManaCurve ();
        DraftPicks = new List<Pick> ();
        Uncertain = new List<int> ();
        GameCards = new Dictionary<int, int> ();
    }
}

public class ManaCurve {
    List<int> Curve;
    public double Rating { get; set; }
    private int CardNumber { get; set; }

    public ManaCurve () {
        Curve = new List<int> ();
        for (int i = 0; i <= Globals.maxCardCost; i++) {
            Curve.Add (0);
        }
        CardNumber = 0;
    }

    public ManaCurve (ManaCurve MC, CardData card) {
        this.Curve = new List<int> (MC.Curve);
        this.Curve[card.Cost]++;
        this.CardNumber = MC.CardNumber + 1;
        this.EvalManaCurve ();
    }
    void EvalManaCurve () {
        List<int> maxNumber = new List<int> { 0, 2, 6, 7, 8, 8, 7, 6, 5, 4, 3, 3, 3 };
        int tmp = 0;
        int i = 0;
        double ratio1 = CardNumber / 30;
        double ratio2 = 1; //Math.Exp (0.03 * CardNumber);

        for (; i <= MCSettings.low; i++) {
            tmp += Curve[i];
        }
        Rating += -Math.Abs (ratio1 * MCSettings.lowNum - tmp) * ratio2;

        tmp = 0;
        for (i++; i <= MCSettings.med; i++) {
            tmp += Curve[i];
        }
        Rating += -Math.Abs (ratio1 * MCSettings.medNum - tmp) * ratio2;

        tmp = 0;
        for (i++; i <= MCSettings.high; i++) {
            tmp += Curve[i];
        }
        Rating += -Math.Abs (ratio1 * MCSettings.highNum - tmp) * ratio2;

        if (MCSettings.shighNum > 0) {
            tmp = 0;
            for (i++; i <= Globals.maxCardCost; i++) {
                tmp += Curve[i];
            }
            Rating += -Math.Abs (ratio1 * MCSettings.shighNum - tmp) * ratio2;
        }

        for (i = 0; i <= Globals.maxCardCost; i++) {
            if (maxNumber[i] > Curve[i]) {
                Rating -= 2 * (maxNumber[i] - Curve[i]);
            }
        }

        for (i = 0; i <= Globals.maxCardCost; i++) {
            if (2 * maxNumber[i] < Curve[i]) {
                Rating -= Curve[i] - maxNumber[i];
            }
        }
    }

    static class MCSettings {
        public const int low = 3;
        public const int med = 6;
        public const int high = 9;
        public const int lowNum = 10;
        public const int medNum = 10;
        public const int highNum = 5;
        public const int shighNum = 30 - (lowNum + medNum + highNum);
    }
}

public class Pick {
    CardData MyPick;
    CardData EnemyPick;
    CardData card1;
    CardData card2;
    CardData card3;

    public Pick (GameDataBase GameDB, CardData card1, CardData card2, CardData card3) {
        this.card1 = card1;
        this.card2 = card2;
        this.card3 = card3;
        if (GameDB.GameCards.ContainsKey (card1.CardNumber)) {
            GameDB.GameCards[card1.CardNumber] = GameDB.GameCards[card1.CardNumber]++;
        } else {
            GameDB.GameCards.Add (card1.CardNumber, 1);
        }
        if (GameDB.GameCards.ContainsKey (card2.CardNumber)) {
            GameDB.GameCards[card2.CardNumber] = GameDB.GameCards[card2.CardNumber]++;
        } else {
            GameDB.GameCards.Add (card2.CardNumber, 1);
        }
        if (GameDB.GameCards.ContainsKey (card3.CardNumber)) {
            GameDB.GameCards[card3.CardNumber] = GameDB.GameCards[card3.CardNumber]++;
        } else {
            GameDB.GameCards.Add (card3.CardNumber, 1);
        }
        EvalPick (GameDB);
    }

    void EvalPick (GameDataBase GameDB) {
        ManaCurve MC1 = new ManaCurve (GameDB.ManaCurve, card1);
        ManaCurve MC2 = new ManaCurve (GameDB.ManaCurve, card2);
        ManaCurve MC3 = new ManaCurve (GameDB.ManaCurve, card3);

        double eval1 = MC1.Rating;
        double eval2 = MC2.Rating;
        double eval3 = MC3.Rating;
#if DEBUG
        Console.Error.WriteLine ("Rating1:" + MC1.Rating);
        Console.Error.WriteLine ("Rating2:" + MC2.Rating);
        Console.Error.WriteLine ("Rating3:" + MC3.Rating);
#endif
        if (eval1 >= eval2 && eval1 >= eval3) {
            MyPick = card1;
            GameDB.ManaCurve = MC1;
            Console.WriteLine ("PICK 0");
        } else if (eval2 >= eval3) {
            MyPick = card2;
            GameDB.ManaCurve = MC2;
            Console.WriteLine ("PICK 1");
        } else {
            MyPick = card3;
            GameDB.ManaCurve = MC3;
            Console.WriteLine ("PICK 2");
        }
    }
}

public class CardData {
    public double Rating { get; set; }

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

    public bool Equals (Card card) {
        if (card == null) {
            return false;
        }
        return this.Id.Equals (card.Id);
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

    public Unit (Unit unit) {
        this.Id = unit.Id;
        this.Attack = unit.Attack;
        this.Defense = unit.Defense;
        this.CanAttack = unit.CanAttack;
        this.HasAttacked = unit.HasAttacked;
        this.Abilities = new Keywords (unit.Abilities.toString ());
        this.BaseCard = unit.BaseCard;
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

    public bool Equals (Unit unit) {
        if (unit == null) {
            return false;
        }
        return this.Id.Equals (unit.Id);
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
    public int Type { get; set; } // 1:SUMMON, 2:ATTACK, 3:USE
    public int Id1 { get; set; }
    public int Id2 { get; set; }
    public Unit UnitRef;
    public Card CardRef;
    public Unit TargetRef;
    public bool IsValid { get; protected set; }
    public ActionResult Result;

    // SUMMON   Action (1, Id1, 0, null, CardRef, null);
    // ATTACK   Action (2, Id1, Id2/-1, UnitRef, null, TargetRef/null);
    // USE      Action (3, Id1, Id2/-1, null, CardRef, TargetRef/null);

    public string TypeToString () {
        switch (Type) {
            case 1:
                return "SUMMON";
            case 2:
                return "ATTACK";
            case 3:
                return "USE";
            default:
                return "ERROR";
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

    public void setIsValid (bool IsValid) {
        this.IsValid = IsValid;
    }

    public void Resolve () {
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
            default:
                Console.Error.WriteLine ("Resolve error ##########1");
                break;
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
            this.NewAttacker = new Unit (CardRef, CardRef.Abilities.HasCharge);
        }
    }

    public class AttackResult : ActionResult {
        public Unit Attacker;
        public Unit Defender;

        public AttackResult (Unit Attacker, Unit Defender) {
            this.Attacker = Attacker;
            this.Defender = Defender;
            this.AttackerHealthChange = 0;
            this.DefenderHealthChange = 0;
            this.NewAttacker = new Unit (Attacker);
            this.NewAttacker.HasAttacked = true;
            if (Defender == null) {
                this.NewDefender = null;
            } else {
                this.NewDefender = new Unit (Defender);
            }
            someMath ();
        }

        private void someMath () {
            if (Defender == null) {
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
                if (NewDefender.Defense <= 0 ||
                    (NewDefender.Defense < Defender.Defense &&
                        Attacker.Abilities.HasLethal)) {
                    DefenderDied = true;
                } else {
                    DefenderDied = false;
                }

                //Lethal D
                if (NewAttacker.Defense <= 0 ||
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
                if (NewTargetRef.Defense <= 0) {
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
                    if (NewTargetRef.Defense <= 0) {
                        DefenderDied = true;
                    }
                }
            }
        }
    }
}

public static class Globals {
    public const int maxBoard = 6;
    public const int maxHand = 8;
    public const int maxCardCost = 12;
    public const int maxTurnTime = 90;
    public const int maxPlaysStored = 256;
    public static Stopwatch startTime;
    public static int actMillis = 0;
}

public class Best {
    public List<GameState> Plays;

    public Best () {
        Plays = new List<GameState> ();
    }

    public void AddPlay (GameState State) {
        if (Plays.Count < Globals.maxPlaysStored - 1) {
            Plays.Add (State);
        } else {
            GameState idx = Plays[0];
            foreach (GameState item in Plays) {
                if (idx.Rating > item.Rating) {
                    idx = item;
                }
            }
            if (idx.Rating < State.Rating) {
                Plays.Remove (idx);
                Plays.Add (State);
            }
        }
    }

    public void Clear () {
        Plays.Clear ();
    }

    public void GetBestState () {
        GameState idx = Plays[0];
        foreach (GameState item in Plays) {
            if (idx.Rating < item.Rating) {
                idx = item;
            }
        }
#if DEBUG
        Console.Error.WriteLine ("*****");
        Console.Error.WriteLine ("Rating:" + idx.Rating);
        Console.Error.WriteLine ("Commands:" + idx.Commands);
#endif
        if (idx.Commands == "") {
            Console.WriteLine ("PASS");
        } else {
            Console.WriteLine (idx.Commands);
        }
    }
}

public class GameState {
    public double Rating { get; set; }
    public string Commands { get; set; }
    public List<Card> MyHand;
    public List<Unit> MyBoard;
    public List<Unit> EnemyBoard;
    public PlayerStats player;
    public PlayerStats opponent;

    public GameState (PlayerStats player, PlayerStats opponent) {
        this.Commands = "";
        this.player = player;
        this.opponent = opponent;
        this.MyHand = new List<Card> ();
        this.MyBoard = new List<Unit> ();
        this.EnemyBoard = new List<Unit> ();
    }

    public GameState (GameState State) {
        this.Commands = State.Commands;
        this.player = new PlayerStats (State.player);
        this.opponent = new PlayerStats (State.opponent);
        this.MyHand = new List<Card> (State.MyHand);
        this.MyBoard = new List<Unit> (State.MyBoard);
        this.EnemyBoard = new List<Unit> (State.EnemyBoard);
    }

    public void EvalState () {
        double multiplier = 0.5f;

        if (player.Health <= 0) {
            Rating = -double.MaxValue;
        } else if (opponent.Health <= 0) {
            Rating = double.MaxValue;
        } else {
            double eval = player.Health - opponent.Health;

            foreach (Unit enemy in EnemyBoard) {
                eval -= (enemy.Attack + enemy.Defense) * multiplier;
            }

            foreach (Unit unit in MyBoard) {
                eval += (unit.Attack + unit.Defense) * multiplier;

            }
            Rating = eval;
        }
    }

    public bool Equals (GameState State) {
        if (this.Rating != State.Rating) {
            return false;
        }
        if (this.Commands.Length == State.Commands.Length) {
            List<string> orders1 = new List<string> (this.Commands.Split (';'));
            List<string> orders2 = new List<string> (State.Commands.Split (';'));
            bool value;
            foreach (string item1 in orders1) {
                value = false;
                foreach (string item2 in orders2) {
                    if (item1 == item2) {
                        value = true;
                        break;
                    }
                }
                if (!value) {
                    return false;
                }
            }
        } else {
            return false;
        }
        return true;
    }
}

class Player {
    static void Main (string[] args) {
        CardDataBase DataBase = new CardDataBase ();
        GameDataBase GameBase = new GameDataBase ();
        GameState State;
        Best BestPlays = new Best ();
        Rate (DataBase);

        // GameLoop
        while (true) {

            State = new GameState (new PlayerStats (Console.ReadLine ().Split (' ')),
                new PlayerStats (Console.ReadLine ().Split (' ')));
            State.opponent.setHand (int.Parse (Console.ReadLine ()));

            Globals.startTime = new Stopwatch ();
            Globals.startTime.Start ();

            int CardCount = int.Parse (Console.ReadLine ());

            // Draft 
            if (State.player.Mana == 0) {

                GameBase.DraftPicks.Add (new Pick (GameBase,
                    new Card (Console.ReadLine ().Split (' ')),
                    new Card (Console.ReadLine ().Split (' ')),
                    new Card (Console.ReadLine ().Split (' '))));

            }
            // Battle
            else {
                for (int i = 0; i < CardCount; i++) {

                    Card card = new Card (Console.ReadLine ().Split (' '));

                    switch (card.Location) {
                        case -1:
                            State.EnemyBoard.Add (new Unit (card, true));
                            break;
                        case 0:
                            State.MyHand.Add (card);
                            break;
                        case 1:
                            State.MyBoard.Add (new Unit (card, true));
                            break;
                        default:
                            Console.Error.WriteLine ("Data loading error ##########1");
                            break;
                    }
                }
                BestPlays.Clear ();
                State.EvalState ();

                SearchAll (State, BestPlays);

                BestPlays.GetBestState ();
            }
        }
    }

    static void SearchAll (GameState State, Best BestPlays) {
        BestPlays.AddPlay (State);
        if (Globals.actMillis != Globals.startTime.ElapsedMilliseconds) {
            Globals.actMillis = (int) Globals.startTime.ElapsedMilliseconds;
#if DEBUG
            if (Globals.actMillis % 10 == 0) {
                Console.Error.WriteLine ("Millis:" + Globals.actMillis);
            }
#endif
        }
        if (Globals.actMillis < Globals.maxTurnTime) {

            List<Action> PotencialActions = new List<Action> ();
            List<Action> Actions = new List<Action> ();

            foreach (Unit unit in State.MyBoard) {
                foreach (Unit enemy in State.EnemyBoard) {
                    PotencialActions.Add (new Action (2, unit.Id, enemy.Id, unit, null, enemy));
                }
                PotencialActions.Add (new Action (2, unit.Id, -1, unit, null, null));
            }

            foreach (Card card in State.MyHand) {
                if (card.Type == 0) {
                    PotencialActions.Add (new Action (1, card.Id, 0, null, card, null));
                } else if (card.Type == 1) {
                    foreach (Unit unit in State.MyBoard) {
                        PotencialActions.Add (new Action (3, card.Id, unit.Id, null, card, unit));
                    }
                } else if (card.Type == 2) {
                    foreach (Unit enemy in State.EnemyBoard) {
                        PotencialActions.Add (new Action (3, card.Id, enemy.Id, null, card, enemy));
                    }
                } else if (card.Type == 3) {
                    if (card.Defense != 0) {
                        foreach (Unit enemy in State.EnemyBoard) {
                            PotencialActions.Add (new Action (3, card.Id, enemy.Id, null, card, enemy));
                        }
                    }
                    PotencialActions.Add (new Action (3, card.Id, -1, null, card, null));
                }
            }

            foreach (Action action in PotencialActions) {
                ValidAction (action, State);
                if (action.IsValid) {
                    action.Resolve ();
                    Actions.Add (action);
                }
            }
#if DEBUG
            // Console.Error.WriteLine ("Commands: " + State.Commands);
            // Console.Error.WriteLine ("State.Rating: " + State.Rating);
            // Console.Error.WriteLine ("ValidA.Count: " + Actions.Count);
            // Console.Error.WriteLine ("+++++");
#endif

            foreach (Action action in Actions) {
                GameState NewState = new GameState (State);
                PerformAction (action, NewState);
                NewState.EvalState ();
                bool value = false;
                foreach (GameState item in BestPlays.Plays) {
                    if (item.Equals (NewState)) {
                        value = true;
                    }
                }
                if (value) {
                    break;
                }
                SearchAll (NewState, BestPlays);
            }
        } else {
            Console.Error.WriteLine ("Excaping timelimit !!!!!");
        }
    }

    static bool ValidAction (Action action, GameState State) {
        bool IsValid = true;
        switch (action.Type) {
            case 1: // SUMMON
                if (State.MyBoard.Count == Globals.maxBoard ||
                    State.player.Mana < action.CardRef.Cost) {
                    IsValid = false;
                }
                break;
            case 2: // ATTACK
                if (action.UnitRef.CanAttack == false ||
                    action.UnitRef.HasAttacked == true) {
                    IsValid = false;
                }
                bool enemyHasGuard = false;
                foreach (Unit enemy in State.EnemyBoard) {
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
                if (State.player.Mana < action.CardRef.Cost) {
                    IsValid = false;
                }
                break;
            default:
                Console.Error.WriteLine ("ValidAction error ##########1");
                break;
        }
        action.setIsValid (IsValid);
        return IsValid;
    }

    static void PerformAction (Action action, GameState State) {

        if (State.Commands != "") {
            State.Commands += "; ";
        }
        switch (action.Type) {

            case 1: // SUMMON
                State.Commands += "SUMMON " + action.CardRef.Id;

                State.MyBoard.Add (action.Result.NewAttacker);
                State.player.Mana -= action.CardRef.Cost;
                State.MyHand.Remove (action.CardRef);
                break;

            case 2: // ATTACK
                State.Commands += "ATTACK " + action.UnitRef.Id + " ";
                if (action.TargetRef == null) {
                    State.Commands += -1;
                } else {
                    State.Commands += action.TargetRef.Id;
                }

                State.player.Health += action.Result.AttackerHealthChange;
                State.opponent.Health += action.Result.DefenderHealthChange;
                if (action.Id2 != -1) {
                    State.EnemyBoard.Remove (action.TargetRef);
                    if (!action.Result.DefenderDied) {
                        State.EnemyBoard.Add (action.Result.NewDefender);
                    }
                }
                State.MyBoard.Remove (action.UnitRef);
                if (!action.Result.AttackerDied) {
                    State.MyBoard.Add (action.Result.NewAttacker);
                }

                break;

            case 3: // USE
                State.Commands += "USE " + action.CardRef.Id + " ";
                State.player.Mana -= action.CardRef.Cost;
                if (action.TargetRef == null) {
                    State.Commands += -1;
                } else {
                    State.Commands += action.TargetRef.Id;
                }

                if (action.CardRef.Type == 1) { //Green

                    State.player.Health += action.Result.AttackerHealthChange;
                    State.MyBoard.Remove (action.UnitRef);
                    State.MyBoard.Add (action.Result.NewTargetRef);

                } else if (action.CardRef.Type == 2) { //Red

                    State.opponent.Health += action.Result.DefenderHealthChange;
                    State.EnemyBoard.Remove (action.TargetRef);
                    if (!action.Result.DefenderDied) {
                        State.EnemyBoard.Add (action.Result.NewTargetRef);
                    }

                } else { // Blue

                    State.player.Health += action.Result.AttackerHealthChange;
                    State.opponent.Health += action.Result.DefenderHealthChange;
                    if (action.Result.NewTargetRef != null) {
                        State.EnemyBoard.Remove (action.TargetRef);
                        if (!action.Result.DefenderDied) {
                            State.EnemyBoard.Add (action.Result.NewTargetRef);
                        }
                    }

                }
                State.MyHand.Remove (action.CardRef);
                break;

            default:
                Console.Error.WriteLine ("Perform error ##########1");
                break;
        }
    }
    static void Rate (CardDataBase CDB) {
        foreach (CardData card in CDB.AllCards) {
            card.Rating = 0;
            if (card.Type == 0 ||
                card.Type == 1) {
                card.Rating += (card.Attack + card.Defense) * 1;
            } else {
                card.Rating -= (card.Attack + card.Defense) * 1;
            }
            card.Rating -= 2 * card.Cost;
            card.Rating += 1 * card.CardDraw;
            card.Rating += (card.MyHealthChange - card.OppHealthChange) * 1;
            if (card.Abilities.HasBreakthrough) {
                card.Rating += 1;
            }
            if (card.Abilities.HasCharge) {
                card.Rating += 1;
            }
            if (card.Abilities.HasDrain) {
                card.Rating += 1;
            }
            if (card.Abilities.HasGuard) {
                card.Rating += 1;
            }
            if (card.Abilities.HasLethal) {
                card.Rating += 1;
            }
            if (card.Abilities.HasWard) {
                card.Rating += 1;
            }
        }
        var SORT = from card in CDB.AllCards
        orderby card.Rating descending
        select card;
        foreach (CardData card in SORT) {
            Console.Error.WriteLine (card.CardNumber + "   " + card.Type);
            Console.Error.WriteLine (card.Cost + "  " + card.Attack + "/" + card.Defense);
            Console.Error.WriteLine (card.Abilities.toString ());
            Console.Error.WriteLine (card.MyHealthChange + "/" + card.OppHealthChange + "  " + "DRAW " + card.CardDraw);
            Console.Error.WriteLine ("Rating: " + card.Rating);
            Console.Error.WriteLine ("**********");
        }
    }
}