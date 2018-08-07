        using System;
        using System.Linq;
        using System.IO;
        using System.Text;
        using System.Collections;
        using System.Collections.Generic;

        public class Card
        {
            public int cardNumber;
            public int instanceId;
            public int location;
            public int cardType;
            public int cost;
            public int attack;
            public int defense;
            public string abilities;
            public int myHealthChange;
            public int opponentHealthChange;
            public int cardDraw;

            public Card(int cardNumber, int instanceId, int location, int cardType, int cost, int attack, 
                        int defense, string abilities, int myHealthChange, int opponentHealthChange, int cardDraw)
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
            }
        }

        class Player
        {
            static void Main(string[] args)
            {
                bool debug = false;
                
                string[] inputs;
                int max_board = 6;
                int max_hand = 8;
                
                while (true)
                {
                    string actions = "";
                    inputs = Console.ReadLine().Split(' ');
                    int playerHealth = int.Parse(inputs[0]);
                    int playerMana = int.Parse(inputs[1]);
                    int playerDeck = int.Parse(inputs[2]);
                    int playerRune = int.Parse(inputs[3]);
                
                    inputs = Console.ReadLine().Split(' ');
                    int opponentHealth = int.Parse(inputs[0]);
                    int opponentMana = int.Parse(inputs[1]);
                    int opponentRune = int.Parse(inputs[3]);
                    int opponentDeck = int.Parse(inputs[2]);
                    int opponentHand = int.Parse(Console.ReadLine());

                    int cardCount = int.Parse(Console.ReadLine());
                    
                    Card[] PlayerHand = new Card[8];
                    Card[] PlayerBoard = new Card[6];
                    Card[] OpponentBoard = new Card[6];
                    int playerHand = 0;
                    int playerBoard = 0;
                    int opponentBoard = 0;
                    
                    if(playerMana == 0)
                    {
                        inputs = Console.ReadLine().Split(' ');
                        Card card1 = new Card(
                                        int.Parse(inputs[0]),
                                        int.Parse(inputs[1]),
                                        int.Parse(inputs[2]),
                                        int.Parse(inputs[3]),
                                        int.Parse(inputs[4]),
                                        int.Parse(inputs[5]),
                                        int.Parse(inputs[6]),
                                        inputs[7],
                                        int.Parse(inputs[8]),
                                        int.Parse(inputs[9]),
                                        int.Parse(inputs[10]));
                        
                        inputs = Console.ReadLine().Split(' ');
                        Card card2 = new Card(
                                        int.Parse(inputs[0]),
                                        int.Parse(inputs[1]),
                                        int.Parse(inputs[2]),
                                        int.Parse(inputs[3]),
                                        int.Parse(inputs[4]),
                                        int.Parse(inputs[5]),
                                        int.Parse(inputs[6]),
                                        inputs[7],
                                        int.Parse(inputs[8]),
                                        int.Parse(inputs[9]),
                                        int.Parse(inputs[10]));

                        inputs = Console.ReadLine().Split(' ');
                        Card card3 = new Card(
                                        int.Parse(inputs[0]),
                                        int.Parse(inputs[1]),
                                        int.Parse(inputs[2]),
                                        int.Parse(inputs[3]),
                                        int.Parse(inputs[4]),
                                        int.Parse(inputs[5]),
                                        int.Parse(inputs[6]),
                                        inputs[7],
                                        int.Parse(inputs[8]),
                                        int.Parse(inputs[9]),
                                        int.Parse(inputs[10]));  
                        Console.WriteLine("PASS");
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
                                    OpponentBoard[opponentBoard] = new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack,  defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw);
                                    opponentBoard++;
                                    break;
                                case 0:
                                    PlayerHand[playerHand] = new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack,  defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw);
                                    playerHand++;
                                    break;
                                case 1:
                                    PlayerBoard[playerBoard] = new Card(cardNumber,  instanceId,  location,  cardType,  cost,  attack,  defense,  abilities,  myHealthChange,  opponentHealthChange,  cardDraw);
                                    playerBoard++;
                                    break;
                                default:
                                    break;
                            }
                        }

                        string IDs = "";
                        for (int i = 0; i < playerHand; i++)
                        {
                            if(PlayerHand[i].cost <= playerMana)
                            {
                                IDs += PlayerHand[i].instanceId.ToString();
                                playerMana -= PlayerHand[i].cost;
                            }
                        }
                        for (int i = 0; i < IDs.Length; i++)
                        {
                            if(actions != "")
                                actions += ";";
                            actions += "SUMMON " + IDs[i];
                        }  
                        for (int i = 0; i < playerBoard; i++)
                        {
                            if(actions != "")
                                actions += ";";
                            actions += "ATTACK " + PlayerBoard[i].instanceId + " " + -1;                            
                        }
                        
                        if(actions == "")
                            Console.WriteLine("PASS");
                        else
                            Console.WriteLine(actions);
                    }
                    
                }
            }
        }