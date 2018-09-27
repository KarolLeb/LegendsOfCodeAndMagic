using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats
{
public int Health;
public int Mana;
public int Deck;
public int Rune;
public int Hand;

public PlayerStats(int health, int mana, int deck, int rune)
{
    this.Health = health;
    this.Mana = mana;
    this.Deck = deck;
    this.Rune = rune;
}
public PlayerStats(int health, int mana, int deck, int rune, int hand)
{
    this.Health = health;
    this.Mana = mana;
    this.Deck = deck;
    this.Rune = rune;
    this.Hand = hand;
}
public void changeHealth(int value)
{
    Health += value;
}
}

public class Card
{
public int cardNumber;
public int instanceId;
public int location;  // -1:opponentBoard, 0:playersHand, 1:playersBoard
public int cardType;
public int cost;
public int attack;
public int defense;
public string abilities;  // B:Breakthrough, C:Charge, G:Guard
public int myHealthChange;
public int opponentHealthChange;
public int cardDraw;

public bool canAttack;

public Card(int cardNumber, int instanceId, int location, int cardType, int cost, int attack, 
            int defense, string abilities, int myHealthChange, int opponentHealthChange, int cardDraw, bool canAttack)
{
    this.cardNumber = cardNumber;
    this.instanceId = instanceId;
    this.location = location;
    this.cardType = cardType;
    this.cost = cost;
    this.attack = attack;
    this.defense = defense;
    this.abilities = abilities;
    this.myHealthChange = myHealthChange;
    this.opponentHealthChange = opponentHealthChange;
    this.cardDraw = cardDraw;
    
    this.canAttack = canAttack;
}

public bool canKillEnemy(Card enemy)
{
    if (enemy.defense <= this.attack)
    {
        return true;
    }
    return false;
}

public bool survivesTrade(Card enemy)
{
    if (enemy.attack < this.defense)
    {
        return true;
    }
    return false;
}

public bool myValuableTrade(Card enemy)
{
    if (this.canKillEnemy(enemy)  &&  this.survivesTrade(enemy))
    {
        return true;
    }
    return false;
}

public bool enemyValuableTrade(Card enemy)
{
    if (enemy.canKillEnemy(this)  &&  enemy.survivesTrade(this))
    {
        return true;
    }
    return false;
}

public Card enemyAfterTrade(Card enemy)
{
    enemy.defense -= this.attack;
    return enemy;
}
}

public class Actions
{
public bool connected;
public float rating;

public Actions()
{
    rating = 0f;
}

public void setRating(float value)
{
    rating = value;
}
}

// public class ConnectedActions : Actions
// {
//     SimpleAction action;
//     Actions actionRest;

//     public ConnectedActions(SimpleAction action,Actions actionRest)
//     {
//         this.action = action;
//         this.actionRest = actionRest;
//         this.connected = true;
//     }            
// }

public class SimpleAction : Actions
{
public int type; // 1:summon, 2:attack,     later items
public Card cardId;
public Card targetId;  // used in attack

public SimpleAction(int type, Card cardId, Card targetId)
{
    this.connected = false;
    this.type = type;
    this.cardId = cardId;
    this.targetId = targetId;
}
}

public static class Globals
{
    public const Int32 maxBoard = 6;
    public const int maxHand = 8;
    public const int maxCardCost = 12;
    public const bool debug = false;
    
}

class Player
{
static void Main(string[] args)
{
    string[] inputs;

    //playersData
    PlayerStats player;
    PlayerStats opponent;

    while (true)
    {
        string actions = "";

        inputs = Console.ReadLine().Split(' ');
        player = new PlayerStats(int.Parse(inputs[0]), int.Parse(inputs[1]), int.Parse(inputs[2]), int.Parse(inputs[3]));

        inputs = Console.ReadLine().Split(' ');
        opponent = new PlayerStats(int.Parse(inputs[0]), int.Parse(inputs[1]), int.Parse(inputs[2]), int.Parse(inputs[3]), int.Parse(Console.ReadLine()));

        int cardCount = int.Parse(Console.ReadLine());
        
        // needed separation?
        // queue?
        // List<Card> PlayerHand = new List<Card>();
        // List<Card> PlayerBoard = new List<Card>();
        // List<Card> OpponentBoard = new List<Card>();
        
        Queue PlayerHand = new Queue();
        Queue PlayerBoard = new Queue();
        Queue OpponentBoard = new Queue();

        if(player.Mana == 0)
        {
            inputs = Console.ReadLine().Split(' ');
            Card card1 = new Card(int.Parse(inputs[0]),int.Parse(inputs[1]),int.Parse(inputs[2]),int.Parse(inputs[3]),int.Parse(inputs[4]),int.Parse(inputs[5]),int.Parse(inputs[6]),inputs[7],int.Parse(inputs[8]),int.Parse(inputs[9]),int.Parse(inputs[10]), false);
            
            inputs = Console.ReadLine().Split(' ');
            Card card2 = new Card(int.Parse(inputs[0]),int.Parse(inputs[1]),int.Parse(inputs[2]),int.Parse(inputs[3]),int.Parse(inputs[4]),int.Parse(inputs[5]),int.Parse(inputs[6]),inputs[7],int.Parse(inputs[8]),int.Parse(inputs[9]),int.Parse(inputs[10]), false);

            inputs = Console.ReadLine().Split(' ');
            Card card3 = new Card(int.Parse(inputs[0]),int.Parse(inputs[1]),int.Parse(inputs[2]),int.Parse(inputs[3]),int.Parse(inputs[4]),int.Parse(inputs[5]),int.Parse(inputs[6]),inputs[7],int.Parse(inputs[8]),int.Parse(inputs[9]),int.Parse(inputs[10]), false);  
            
            EvalPick(card1, card2, card3);
        }
        else
        {
            for (int i = 0; i < cardCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int cardNumber = int.Parse(inputs[0]);
                int instanceId = int.Parse(inputs[1]);
                int location = int.Parse(inputs[2]);
                int cardType = int.Parse(inputs[3]);
                int cost = int.Parse(inputs[4]);
                int attack = int.Parse(inputs[5]);
                int defense = int.Parse(inputs[6]);
                string abilities = inputs[7];
                int myHealthChange = int.Parse(inputs[8]);
                int opponentHealthChange = int.Parse(inputs[9]);
                int cardDraw = int.Parse(inputs[10]);

                switch (location)
                {
                    case -1:
                        OpponentBoard.Enqueue(new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack, defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw, false));
                        break;
                    case 0:
                        PlayerHand.Enqueue(new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack, defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw, false));
                        break;
                    case 1:
                        PlayerBoard.Enqueue(new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack, defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw, true));
                        break;
                    default:
                        Console.Error.WriteLine("Data loading error #1");
                        break;
                }
            }
            PlayTurn(PlayerHand, PlayerBoard, OpponentBoard, player, opponent);
        }
    }
}

static Queue LegalizeHand(Queue cards, PlayerStats player)
{
    Queue LegalHand = new Queue();
    foreach (Card card in cards)
    {
        if (card.cost <= player.Mana)
        {
            LegalHand.Enqueue(card);
        }
    }
    return LegalHand;
}
static Queue LegalizeMyBoard(Queue cards, PlayerStats player)
{
    Queue LegalBoard = new Queue();
    foreach (Card card in cards)
    {
        if (card.canAttack  &&  card.defense > 0)
        {
            LegalBoard.Enqueue(card);
        }
    }
    return LegalBoard;
}
static Queue LegalizeEnemyBoard(Queue cards)
{
    Queue LegalBoard = new Queue();
    foreach (Card card in cards)
    {
        if (card.defense > 0)
        {
            LegalBoard.Enqueue(card);
        }
    }
    return LegalBoard;
}

static void EvalPick(Card card1, Card card2, Card card3)
{
    int eval1 = 0;
    int eval2 = 0;
    int eval3 = 0;

    // public Eval(){}
    // public void Pick(){}

    Console.WriteLine("PASS");
}

    static void PlayTurn(Queue LegalHand, Queue MyLegalBoard, Queue EnemyLegalBoard, PlayerStats player, PlayerStats opponent)
    {
        List<SimpleAction> PlannedActions = new List<SimpleAction>();
        List<SimpleAction> PotencialActions;
    
        bool notPass = true;
    
        do
        {if (Globals.debug)
            {
                Console.Error.WriteLine("LegalBoard:" + MyLegalBoard.Count);
            }
            PotencialActions = new List<SimpleAction>();
            LegalHand = LegalizeHand(LegalHand, player);
            MyLegalBoard = LegalizeMyBoard(MyLegalBoard, player);
            EnemyLegalBoard = LegalizeEnemyBoard(EnemyLegalBoard);
            
            foreach (Card unit in MyLegalBoard)
            {
                foreach (Card enemy in EnemyLegalBoard)
                {
                    PotencialActions.Add(new SimpleAction(2,unit,enemy));
                }
                PotencialActions.Add(new SimpleAction(2, unit, null));
            }
            if (Globals.debug)
            {
                Console.Error.WriteLine("LegalHand:" + LegalHand.Count);
            }
            foreach (Card card in LegalHand)
            {
                PotencialActions.Add(new SimpleAction(1,card,null));
            }
            if (Globals.debug)
            {
                Console.Error.WriteLine("PotAct:" + PotencialActions.Count);
            }
            foreach (SimpleAction action in PotencialActions)
            {
                action.setRating(EvalPlay(action, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent));
            }
            if (Globals.debug)
            {
                foreach (SimpleAction action in PotencialActions)
                {
                    Console.Error.WriteLine("action.type:" + action.type.ToString());
                    Console.Error.WriteLine("action.cardId.instance.Id:" + action.cardId.instanceId);
                    if (action.targetId != null)
                    {
                        Console.Error.WriteLine("action.targetId:" + action.targetId.ToString());
                    }
                    else
                    {
                        Console.Error.WriteLine("action.targetId:" + "null");
                    }
                    Console.Error.WriteLine("action.rate:" + action.rating);
                }
            }
            if (PotencialActions.Count > 0)
            {
                SimpleAction best = PotencialActions.First();
                foreach (SimpleAction action in PotencialActions)
                {
                    if (best.rating < action.rating)
                    {
                        best = action;
                    }
                }
                if (best.rating < 0)
                {
                    break;
                }
                else
                {
                    PlannedActions.Add(best);
                    best.cardId.canAttack = false;
                    PerformAction(best, EnemyLegalBoard, MyLegalBoard, LegalHand, player, opponent);
                    if (Globals.debug)
                    {
                        Console.Error.WriteLine("PlaAct:" + PlannedActions.Count);
                    }
                }
            }
            else
            {
                notPass = false;
            }
            
        } while(notPass  &&  LegalHand.Count > 0  && MyLegalBoard.Count > 0);
        if (Globals.debug)
        {
            Console.Error.WriteLine("PlaAct:" + PlannedActions.Count);
        }
        if (PlannedActions.Count > 0)
        {
            string command = "";
            foreach (SimpleAction action in PlannedActions)
            {
                if(command != "")
                {
                    command += "; ";
                }
                switch (action.type)
                {
                    case 1:
                        command += "SUMMON " + action.cardId.instanceId;
                        break;
                    case 2:
                        command += "ATTACK " + action.cardId.instanceId + " ";
                        if (action.targetId == null)
                        {
                            command += -1;
                        }
                        else
                        {
                            command += action.targetId.instanceId;
                        }
                        break;
                    default:
                        Console.Error.WriteLine("Command construction error #1");
                        break;
                }
            }
            Console.WriteLine(command);
        }
        else
        {
            Console.WriteLine("PASS");
        }
    }
    static float EvalPlay(SimpleAction action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent)
    {
    float value = 0;
    switch (action.type)
    {
        case 1:  //summon
            if (MyBoard.Count == 8)
            {
                value -= 100;
            }            
            if(player.Mana == action.cardId.cost)
            {
                value += 2;
            }
            if (action.cardId.abilities.Contains("G"))
            {
                value += 1;
            }
            if (action.cardId.cardDraw > 0)
            {
                if (MyHand.Count + action.cardId.cardDraw >= Globals.maxBoard - 1)
                {
                    if (MyHand.Count == Globals.maxBoard)
                    {
                        value -= 5;
                    }
                    value -= 5;
                }
                else
                {
                    value += 5;
                }
            }
            if (action.cardId.abilities.Contains("C"))
            {
                value += 3;
            }
            break;
        case 2:  //attack
            
            if (action.targetId == null)
            {
                if(opponent.Health <= action.cardId.attack)
                {
                    value += 98;
                }
                value += 1;
            }
            else
            {
                if (action.targetId.abilities.Contains("G"))
                {
                    value += 5;
                }
                if (action.targetId.abilities.Contains("B"))
                {
                    value += 2;
                }
                if (action.targetId.attack < 1)
                {
                    value += 1;
                }
            }
            if (action.cardId.attack < 1)
            {
                value -= 100;
            }
            break;
        default:
            Console.Error.WriteLine("Eval error #1");
            break;
    }
    return value;
    }
    static void PerformAction(SimpleAction action, Queue EnemyBoard, Queue MyBoard, Queue MyHand, PlayerStats player, PlayerStats opponent)
    {
        switch (action.type)
        {
            case 1: // summon
                player.Mana -= action.cardId.cost;
                if (action.cardId.abilities.Contains("C"))
                {
                    action.cardId.canAttack = true;
                }
                if (action.cardId.cardDraw > 0)
                {
                    for (int i = 0; i < action.cardId.cardDraw; i++)
                    {
                        MyBoard.Enqueue(new Card(0,0,0,0,Globals.maxCardCost + 1,0,0,"",0,0,0,false));
                    }
                }
                while (MyHand.Peek() != action.cardId)
                {
                    MyHand.Enqueue(MyHand.Dequeue());
                }
                MyHand.Dequeue();
                MyBoard.Enqueue(action.cardId);
                break;
            case 2: // attack
                if (action.targetId == null)
                {
                    opponent.changeHealth(action.cardId.attack);
                }
                else
                {
                    action.targetId.defense -= action.cardId.attack;
                }
                break;
            default:
                Console.Error.WriteLine("Perform error #1");
                break;
        }
    }
}