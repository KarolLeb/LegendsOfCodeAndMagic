using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class PlayerStats {
    public int Health { get; set; }
    public int Mana { get; set; }
    public int Deck { get; set; }
    public int Rune { get; set; } //todo
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

public class CardDataBase {
    List<CardData> AllCards = new List<CardData> ();

    public CardDataBase () {
        string RawData = "1;Slimer;0;1;2;1;------;1;0;0.2;Scuttler;0;1;1;2;------;0;-1;0.3;Beavrat;0;1;2;2;------;0;0;0.4;PlatedToad;0;2;1;5;------;0;0;0.5;GrimeGnasher;0;2;4;1;------;0;0;0.6;Murgling;0;2;3;2;------;0;0;0.7;RootkinSapling;0;2;2;2;-----W;0;0;0.8;Psyshroom;0;2;2;3;------;0;0;0.9;CorruptedBeavrat;0;3;3;4;------;0;0;0.10;CarnivorousBush;0;3;3;1;--D---;0;0;0.11;Snowsaur;0;3;5;2;------;0;0;0.12;Woodshroom;0;3;2;5;------;0;0;0.13;SwampTerror;0;4;5;3;------;1;-1;0.14;FangedLunger;0;4;9;1;------;0;0;0.15;PouncingFlailmouth;0;4;4;5;------;0;0;0.16;WranglerFish;0;4;6;2;------;0;0;0.17;AshWalker;0;4;4;5;------;0;0;0.18;AcidGolem;0;4;7;4;------;0;0;0.19;Foulbeast;0;5;5;6;------;0;0;0.20;HedgeDemon;0;5;8;2;------;0;0;0.21;CrestedScuttler;0;5;6;5;------;0;0;0.22;Sigbovak;0;6;7;5;------;0;0;0.23;TitanCaveHog;0;7;8;8;------;0;0;0.24;ExplodingSkitterbug;0;1;1;1;------;0;-1;0.25;SpineyChompleaf;0;2;3;1;------;-2;-2;0.26;RazorCrab;0;2;3;2;------;0;-1;0.27;NutGatherer;0;2;2;2;------;2;0;0.28;InfestedToad;0;2;1;2;------;0;0;1.29;SteelplumeNestling;0;2;2;1;------;0;0;1.30;VenomousBogHopper;0;3;4;2;------;0;-2;0.31;WoodlandHunter;0;3;3;1;------;0;-1;0.32;Sandsplat;0;3;3;2;------;0;0;1.33;Chameleskulk;0;4;4;3;------;0;0;1.34;EldritchCyclops;0;5;3;5;------;0;0;1.35;Snail-eyedHulker;0;6;5;2;B-----;0;0;1.36;PossessedSkull;0;6;4;4;------;0;0;2.37;EldritchMulticlops;0;6;5;7;------;0;0;1.38;Imp;0;1;1;3;--D---;0;0;0.39;VoraciousImp;0;1;2;1;--D---;0;0;0.40;RockGobbler;0;3;2;3;--DG--;0;0;0.41;BlizzardDemon;0;3;2;2;-CD---;0;0;0.42;FlyingLeech;0;4;4;2;--D---;0;0;0.43;ScreechingNightmare;0;6;5;5;--D---;0;0;0.44;Deathstalker;0;6;3;7;--D-L-;0;0;0.45;NightHowler;0;6;6;5;B-D---;-3;0;0.46;SoulDevourer;0;9;7;7;--D---;0;0;0.47;Gnipper;0;2;1;5;--D---;0;0;0.48;VenomHedgehog;0;1;1;1;----L-;0;0;0.49;ShinyProwler;0;2;1;2;---GL-;0;0;0.50;PuffBiter;0;3;3;2;----L-;0;0;0.51;EliteBilespitter;0;4;3;5;----L-;0;0;0.52;Bilespitter;0;4;2;4;----L-;0;0;0.53;PossessedAbomination;0;4;1;1;-C--L-;0;0;0.54;ShadowBiter;0;3;2;2;----L-;0;0;0.55;HermitSlime;0;2;0;5;---G--;0;0;0.56;GiantLouse;0;4;2;7;------;0;0;0.57;Dream-Eater;0;4;1;8;------;0;0;0.58;DarkscalePredator;0;6;5;6;B-----;0;0;0.59;SeaGhost;0;7;7;7;------;1;-1;0.60;GritsuckTroll;0;7;4;8;------;0;0;0.61;AlphaTroll;0;9;10;10;------;0;0;0.62;MutantTroll;0;12;12;12;B--G--;0;0;0.63;RootkinDrone;0;2;0;4;---G-W;0;0;0.64;CoppershellTortoise;0;2;1;1;---G-W;0;0;0.65;SteelplumeDefender;0;2;2;2;-----W;0;0;0.66;StaringWickerbeast;0;5;5;1;-----W;0;0;0.67;FlailingHammerhead;0;6;5;5;-----W;0;-2;0.68;GiantSquid;0;6;7;5;-----W;0;0;0.69;ChargingBoarhound;0;3;4;4;B-----;0;0;0.70;Murglord;0;4;6;3;B-----;0;0;0.71;FlyingMurgling;0;4;3;2;BC----;0;0;0.72;ShufflingNightmare;0;4;5;3;B-----;0;0;0.73;BogBounder;0;4;4;4;B-----;4;0;0.74;Crusher;0;5;5;4;B--G--;0;0;0.75;TitanProwler;0;5;6;5;B-----;0;0;0.76;CrestedChomper;0;6;5;5;B-D---;0;0;0.77;LumberingGiant;0;7;7;7;B-----;0;0;0.78;Shambler;0;8;5;5;B-----;0;-5;0.79;ScarletColossus;0;8;8;8;B-----;0;0;0.80;CorpseGuzzler;0;8;8;8;B--G--;0;0;1.81;FlyingCorpseGuzzler;0;9;6;6;BC----;0;0;0.82;SlitheringNightmare;0;7;5;5;B-D--W;0;0;0.83;RestlessOwl;0;0;1;1;-C----;0;0;0.84;FighterTick;0;2;1;1;-CD--W;0;0;0.85;HeartlessCrow;0;3;2;3;-C----;0;0;0.86;CrazedNose-pincher;0;3;1;5;-C----;0;0;0.87;BloatDemon;0;4;2;5;-C-G--;0;0;0.88;AbyssNightmare;0;5;4;4;-C----;0;0;0.89;Boombeak;0;5;4;1;-C----;2;0;0.90;EldritchSwooper;0;8;5;5;-C----;0;0;0.91;Flumpy;0;0;1;2;---G--;0;1;0.92;Wurm;0;1;0;1;---G--;2;0;0.93;Spinekid;0;1;2;1;---G--;0;0;0.94;RootkinDefender;0;2;1;4;---G--;0;0;0.95;Wildum;0;2;2;3;---G--;0;0;0.96;PrairieProtector;0;2;3;2;---G--;0;0;0.97;Turta;0;3;3;3;---G--;0;0;0.98;LillyHopper;0;3;2;4;---G--;0;0;0.99;CaveCrab;0;3;2;5;---G--;0;0;0.100;Stalagopod;0;3;1;6;---G--;0;0;0.101;Engulfer;0;4;3;4;---G--;0;0;0.102;MoleDemon;0;4;3;3;---G--;0;-1;0.103;MutatingRootkin;0;4;3;6;---G--;0;0;0.104;DeepwaterShellcrab;0;4;4;4;---G--;0;0;0.105;KingShellcrab;0;5;4;6;---G--;0;0;0.106;Far-reachingNightmare;0;5;5;5;---G--;0;0;0.107;WorkerShellcrab;0;5;3;3;---G--;3;0;0.108;RootkinElder;0;5;2;6;---G--;0;0;0.109;ElderEngulfer;0;5;5;6;------;0;0;0.110;Gargoyle;0;5;0;9;---G--;0;0;0.111;TurtaKnight;0;6;6;6;---G--;0;0;0.112;RootkinLeader;0;6;4;7;---G--;0;0;0.113;TamedBilespitter;0;6;2;4;---G--;4;0;0.114;Gargantua;0;7;7;7;---G--;0;0;0.115;RootkinWarchief;0;8;5;5;---G-W;0;0;0.116;EmperorNightmare;0;12;8;8;BCDGLW;0;0;0.117;Protein;1;1;1;1;B-----;0;0;0.118;RoyalHelm;1;0;0;3;------;0;0;0.119;SerratedShield;1;1;1;2;------;0;0;0.120;Venomfruit;1;2;1;0;----L-;0;0;0.121;EnchantedHat;1;2;0;3;------;0;0;1.122;BolsteringBread;1;2;1;3;---G--;0;0;0.123;Wristguards;1;2;4;0;------;0;0;0.124;BloodGrapes;1;3;2;1;--D---;0;0;0.125;HealthyVeggies;1;3;1;4;------;0;0;0.126;HeavyShield;1;3;2;3;------;0;0;0.127;ImperialHelm;1;3;0;6;------;0;0;0.128;EnchantedCloth;1;4;4;3;------;0;0;0.129;EnchantedLeather;1;4;2;5;------;0;0;0.130;HelmofRemedy;1;4;0;6;------;4;0;0.131;HeavyGauntlet;1;4;4;1;------;0;0;0.132;HighProtein;1;5;3;3;B-----;0;0;0.133;PieofPower;1;5;4;0;-----W;0;0;0.134;LightTheWay;1;4;2;2;------;0;0;1.135;ImperialArmour;1;6;5;5;------;0;0;0.136;Buckler;1;0;1;1;------;0;0;0.137;Ward;1;2;0;0;-----W;0;0;0.138;GrowHorns;1;2;0;0;---G--;0;0;1.139;GrowStingers;1;4;0;0;----LW;0;0;0.140;GrowWings;1;2;0;0;-C----;0;0;0.141;ThrowingKnife;2;0;-1;-1;------;0;0;0.142;StaffofSuppression;2;0;0;0;BCDGLW;0;0;0.143;PierceArmour;2;0;0;0;---G--;0;0;0.144;RuneAxe;2;1;0;-2;------;0;0;0.145;CursedSword;2;3;-2;-2;------;0;0;0.146;CursedScimitar;2;4;-2;-2;------;0;-2;0.147;QuickShot;2;2;0;-1;------;0;0;1.148;HelmCrusher;2;2;0;-2;BCDGLW;0;0;0.149;RootkinRitual;2;3;0;0;BCDGLW;0;0;1.150;ThrowingAxe;2;2;0;-3;------;0;0;0.151;Decimate;2;5;0;-99;BCDGLW;0;0;0.152;MightyThrowingAxe;2;7;0;-7;------;0;0;1.153;HealingPotion;2;2;0;0;------;5;0;0.154;Poison;2;2;0;0;------;0;-2;1.155;ScrollofFirebolt;2;3;0;-3;------;0;-1;0.156;MajorLifeStealPotion;2;3;0;0;------;3;-3;0.157;LifeSapDrop;2;3;0;-1;------;1;0;1.158;TomeofThunder;2;3;0;-4;------;0;0;0.159;VialofSoulDrain;2;4;0;-3;------;3;0;0.160;MinorLifeStealPotion;2;2;0;0;------;2;-2;0";
        string[] SlicedData = RawData.Split ('.');
        foreach (string data in SlicedData) {
            AllCards.Add (new CardData (data.Split (';')));
        }
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
        this.Type = int.Parse (data[2]);
        this.Cost = int.Parse (data[3]);
        this.Attack = int.Parse (data[4]);
        this.Defense = int.Parse (data[5]);
        this.Abilities = new Keywords (data[6]);
        this.MyHealthChange = int.Parse (data[7]);
        this.OppHealthChange = int.Parse (data[8]);
        this.CardDraw = int.Parse (data[9]);
    }
}

public class Card : CardData {
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
        }
    }

    //JUST LEAVE IT
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
    public int AttackerAttackChange { get; set; }
    public int DefenderAttackChange { get; set; }
    public int AttackerDefenseChange { get; set; }
    public int DefenderDefenseChange { get; set; }
    public int AttackerExcessiveDamage { get; set; }
    public bool AbilitiesChange { get; set; }
    public bool AttackerDied { get; set; }
    public bool DefenderDied { get; set; }
    public bool AttackerLostWard { get; set; }
    public bool DefenderLostWard { get; set; }
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
    public bool goFace { get; set; }

    public AttackResult (Unit Attacker, Unit Defender) {
        this.Attacker = Attacker;
        this.Defender = Defender;
        this.goFace = false;
        someMath ();
    }

    public AttackResult (Unit Attacker) {
        this.Attacker = Attacker;
        this.Defender = null;
        this.goFace = true;
        someMath ();
    }

    private void someMath () {
        if (goFace) {
            DefenderHealthChange = -Attacker.Attack;
            if (Attacker.Abilities.HasDrain) { //Drain A
                AttackerHealthChange = -Attacker.Attack;
            } else {
                AttackerHealthChange = 0;
            }
        } else {

            if (Attacker.Abilities.HasWard) { //Ward A
                AttackerDefenseChange = 0;
                if (Defender.Attack > 0) {
                    AttackerLostWard = true;
                }
            } else {
                AttackerDefenseChange = -Math.Min (Defender.Attack, Attacker.Defense);
            }

            if (Defender.Abilities.HasWard) { //Ward D
                DefenderDefenseChange = 0;
                if (Attacker.Attack > 0) {
                    DefenderLostWard = true;
                } else {
                    DefenderLostWard = false;
                }
            } else {
                DefenderDefenseChange = -Math.Min (Attacker.Attack, Defender.Defense);
            }

            if ((DefenderDefenseChange + Defender.Defense < 0) ||
                (DefenderDefenseChange != 0 && Attacker.Abilities.HasLethal)) { //Lethal A
                DefenderDied = true;
            } else {
                DefenderDied = false;
            }

            if ((AttackerDefenseChange + Defender.Defense < 0) ||
                (AttackerDefenseChange != 0 && Defender.Abilities.HasLethal)) { //Lethal D
                AttackerDied = true;
            } else {
                AttackerDied = false;
            }

            if (Attacker.Abilities.HasBreakthrough) { //Breakthrough A
                if (Attacker.Attack > Defender.Defense && DefenderDied) {
                    AttackerExcessiveDamage = Defender.Defense - Attacker.Attack;
                    if (Attacker.Abilities.HasDrain) { //Drain A
                        AttackerHealthChange -= AttackerExcessiveDamage;
                    } else {
                        AttackerHealthChange = 0;
                    }
                } else {
                    AttackerExcessiveDamage = 0;
                }
            } else {
                AttackerExcessiveDamage = 0;
            }

            if (Attacker.Abilities.HasDrain &&
                DefenderDefenseChange != 0) { //Drain A
                AttackerHealthChange += DefenderDefenseChange;
            } else {
                AttackerHealthChange = 0;
            }
        }
    }
}

public class UseResult : ActionResult {
    public Card CardRef;
    public Unit TargetRef;

    public UseResult (Card CardRef, Unit TargetRef) {
        this.CardRef = CardRef;
        this.TargetRef = TargetRef;
        this.DefenderDefenseChange = 0;
        someMath ();
    }

    void someMath () {

        if (CardRef.Type == 1) { //Green

            AttackerAttackChange = CardRef.Attack;
            AttackerDefenseChange = CardRef.Defense;
            AbilitiesChange = CardRef.Abilities.hasAnyKeyword ();
            AttackerHealthChange = CardRef.MyHealthChange;

        } else if (CardRef.Type == 2) { //Red

            AbilitiesChange = CardRef.Abilities.hasAnyKeyword ();
            DefenderHealthChange = CardRef.OppHealthChange;
            if (CardRef.Defense != 0) {
                if (TargetRef.Abilities.HasWard) {
                    DefenderLostWard = true;
                    if (AbilitiesChange) {
                        DefenderAttackChange = CardRef.Attack;
                        DefenderDefenseChange = CardRef.Defense;
                    } else {
                        DefenderAttackChange = 0;
                        DefenderDefenseChange = 0;
                    }
                } else {
                    DefenderAttackChange = CardRef.Attack;
                    DefenderDefenseChange = CardRef.Defense;
                }
            }
        } else { //Blue

            AttackerHealthChange = CardRef.MyHealthChange;
            DefenderHealthChange = CardRef.OppHealthChange;
            if (TargetRef != null) {
                if (!TargetRef.Abilities.HasWard) {
                    DefenderDefenseChange = CardRef.Defense;
                }
            }
        }
    }
}

public static class Globals {
    public const Int32 maxBoard = 6;
    public const int maxHand = 8;
    public const int maxCardCost = 12;
    public const bool debug = true;
}

class Player {
    static void Main (string[] args) {
        CardDataBase DataBase = new CardDataBase ();

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
                if (MyBoard.Count == Globals.maxBoard ||
                    player.Mana < action.CardRef.Cost) {
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
            case 3: // USE
                if (player.Mana < action.CardRef.Cost) {
                    action.setIsValid (false);
                }
                break;
                //todo
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

        List<Action> PotencialActions;
        List<Action> PlannedActions = new List<Action> ();

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
                    } else {
                        PotencialActions.Add (new Action (3, card.Id, -1, null, card, null));
                    }
                }
            }

            if (Globals.debug) {
                Console.Error.WriteLine ("PotencialActions:" + PotencialActions.Count);
            }

            foreach (Action action in PotencialActions) {
                ValidAction (action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
            }

            foreach (Action action in PotencialActions) {
                if (action.IsValid) {
                    action.resolve ();
                }
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
                if (action.CardRef.Type == 1) {
                    if (action.TargetRef.HasAttacked == false) {

                    }

                    if (action.TargetRef.CanAttack) {
                        if (0 < action.Result.AttackerAttackChange + action.Result.AttackerDefenseChange) {
                            value += 1;
                        }
                    }
                    if (action.TargetRef.Abilities.HasGuard) {

                    }
                    if (action.TargetRef.Abilities.HasGuard) {
                        value += 1;
                    }

                    if (true) {

                    }
                }
                if (action.Result.AbilitiesChange) {
                    if (action.TargetRef.Abilities.HasBreakthrough) {
                        value += 1;
                    }
                    if (action.TargetRef.Abilities.HasDrain) {
                        value += 1;
                    }
                    if (action.TargetRef.Abilities.HasLethal) {
                        value += 3;
                    }
                }
                if (action.Result.DefenderDied) {
                    value += 4;
                }
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
                player.Health += action.Result.AttackerHealthChange;
                opponent.Health += action.Result.DefenderHealthChange;
                if (action.Id2 != -1) {
                    action.UnitRef.Defense += action.Result.AttackerDefenseChange;
                    action.TargetRef.Defense += action.Result.DefenderDefenseChange;
                    opponent.Health += action.Result.AttackerExcessiveDamage;
                    action.UnitRef.Abilities.HasWard = action.UnitRef.Abilities.HasWard && !action.Result.AttackerLostWard;
                    action.TargetRef.Abilities.HasWard = action.TargetRef.Abilities.HasWard && !action.Result.DefenderLostWard;
                }
                break;

            case 3: // USE
                if (action.CardRef.Type == 1) { //Green

                    action.TargetRef.Attack += action.Result.AttackerAttackChange;
                    action.TargetRef.Defense += action.Result.AttackerDefenseChange;
                    player.Health += action.Result.AttackerHealthChange;
                    if (action.Result.AbilitiesChange) {
                        if (action.TargetRef.Abilities.HasBreakthrough ||
                            action.CardRef.Abilities.HasBreakthrough) {
                            action.TargetRef.Abilities.HasBreakthrough = true;
                        } else {
                            action.TargetRef.Abilities.HasBreakthrough = false;
                        }
                        if (action.TargetRef.Abilities.HasCharge ||
                            action.CardRef.Abilities.HasCharge) {
                            action.TargetRef.Abilities.HasCharge = true;
                        } else {
                            action.TargetRef.Abilities.HasCharge = false;
                        }
                        if (action.TargetRef.Abilities.HasDrain ||
                            action.CardRef.Abilities.HasDrain) {
                            action.TargetRef.Abilities.HasDrain = true;
                        } else {
                            action.TargetRef.Abilities.HasDrain = false;
                        }
                        if (action.TargetRef.Abilities.HasGuard ||
                            action.CardRef.Abilities.HasGuard) {
                            action.TargetRef.Abilities.HasGuard = true;
                        } else {
                            action.TargetRef.Abilities.HasGuard = false;
                        }
                        if (action.TargetRef.Abilities.HasLethal ||
                            action.CardRef.Abilities.HasLethal) {
                            action.TargetRef.Abilities.HasLethal = true;
                        } else {
                            action.TargetRef.Abilities.HasLethal = false;
                        }
                        if (action.TargetRef.Abilities.HasWard ||
                            action.CardRef.Abilities.HasWard) {
                            action.TargetRef.Abilities.HasWard = true;
                        } else {
                            action.TargetRef.Abilities.HasWard = false;
                        }
                    }

                } else if (action.CardRef.Type == 2) { //Red

                    action.TargetRef.Attack += action.Result.DefenderAttackChange;
                    action.TargetRef.Defense += action.Result.DefenderDefenseChange;
                    opponent.Health += action.Result.DefenderHealthChange;
                    if (action.Result.AbilitiesChange) {
                        if (action.CardRef.Abilities.HasBreakthrough) {
                            action.TargetRef.Abilities.HasBreakthrough = false;
                        }
                        if (action.CardRef.Abilities.HasCharge) {
                            action.TargetRef.Abilities.HasCharge = false;
                        }
                        if (action.CardRef.Abilities.HasDrain) {
                            action.TargetRef.Abilities.HasDrain = false;
                        }
                        if (action.CardRef.Abilities.HasGuard) {
                            action.TargetRef.Abilities.HasGuard = false;
                        }
                        if (action.CardRef.Abilities.HasLethal) {
                            action.TargetRef.Abilities.HasLethal = false;
                        }
                        if (action.CardRef.Abilities.HasWard) {
                            action.TargetRef.Abilities.HasWard = false;
                        }
                    }

                } else { // Blue

                    player.Health += action.Result.AttackerHealthChange;
                    opponent.Health += action.Result.DefenderHealthChange;
                    action.TargetRef.Defense += action.Result.DefenderDefenseChange;

                }
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