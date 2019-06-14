using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject[] TileList;

    public GameObject StartTile;
    public GameObject EndTile;

    public GameObject Player1;
    public GameObject Player2;

    ////////////////////////////
    public GameObject Player3;
    public GameObject Player4;
    /////////////////////////////
    public GameObject Snake1Start;
    public GameObject Snake1End;

    public GameObject Ladder1Start;
    public GameObject Ladder1End;

    public GameObject LadderLine1;
    public GameObject SnakeLine1;
    
    public GameObject Snake2Start;
    public GameObject Snake2End;

    public GameObject Ladder2Start;
    public GameObject Ladder2End;

    public GameObject LadderLine2;
    public GameObject SnakeLine2;

    public GameObject MainMenuObject;

    public Text RollText;
    public Text SkipText;
    public Text MoveText;
    public Text P1Stats;
    public Text P2Stats;
    public Text P3Stats;
    public Text P4Stats;

    //public bool IfTrueP1;       // Bool To Keep Track Of turns
    public int PlayerTurn;

    public int snake1Start;
    public int snake1End;

    public int ladder1Start;
    public int ladder1End;
    
    public int snake2Start;
    public int snake2End;

    public int ladder2Start;
    public int ladder2End;

    public Button RollButton;
    public Button MoveButton;
    public Button SkipButton;

    private bool GameOver;

    private IEnumerator MoveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuObject.GetComponent<MainMenu>().StartGame();


        //Set Player to Start
        Player1.transform.position = StartTile.transform.position;
        //P1_position = StartTile.gameObject.GetComponent<Tile>().tileNumber;
        Player1.GetComponent<Player>().TilePosition = StartTile.gameObject.GetComponent<Tile>().tileNumber;

        Player2.transform.position = StartTile.transform.position;
        Player2.GetComponent<Player>().TilePosition = StartTile.gameObject.GetComponent<Tile>().tileNumber;

        Player3.transform.position = StartTile.transform.position;
        Player3.GetComponent<Player>().TilePosition = StartTile.gameObject.GetComponent<Tile>().tileNumber;

        Player4.transform.position = StartTile.transform.position;
        Player4.GetComponent<Player>().TilePosition = StartTile.gameObject.GetComponent<Tile>().tileNumber;

        //IfTrueP1 = true; // P1 Starts
        PlayerTurn = 1; //P1 Starts
        GameOver = false; // GameOver bool for Debugging;

        //set snakes
        // 8 - 62 start for snake
        // 0 - 55 end for snake

        snake1Start = (int)(Random.Range(8, 62.9f));        //set snake 1
        while(true)
        {
            snake1End = (int)(Random.Range(0, 55.9f));
            if (snake1Start > snake1End)        //check snake 1
            {
                break;
            }
        }

        while(true)
        {
            snake2Start = (int)(Random.Range(8, 62.9f));        //set snake 2
            if((snake2Start!=snake1Start)&&(snake2Start!=snake1End))        //check snake 1 and snake 2
            {
                while (true)
                {
                    snake2End = (int)(Random.Range(0, 55.9f));
                    if ((snake2Start > snake2End)&&(snake2End != snake1Start))
                    {
                        break;
                    }
                }
                break;
            }
        }



        TileList[snake1Start].gameObject.tag = "SpecialTile";
        TileList[snake1End].gameObject.tag = "SpecialTile";

        TileList[snake2Start].gameObject.tag = "SpecialTile";
        TileList[snake2End].gameObject.tag = "SpecialTile";

        Snake1Start.transform.position = TileList[snake1Start].gameObject.transform.position;
        Snake1End.transform.position = TileList[snake1End].gameObject.transform.position;
        Snake1Start.GetComponent<SpecialTile>().nextTile = Snake1End;
        Snake1End.GetComponent<SpecialTile>().nextTile = TileList[snake1End].gameObject.GetComponent<Tile>().nextTile;

        SnakeLine1.GetComponent<LineRenderer>().SetPosition(0, Snake1Start.transform.position);
        SnakeLine1.GetComponent<LineRenderer>().SetPosition(1, Snake1End.transform.position);

        SnakeLine1.transform.position = Vector3.zero;

        //////////////////////////////////////////////////////

        Snake2Start.transform.position = TileList[snake2Start].gameObject.transform.position;
        Snake2End.transform.position = TileList[snake2End].gameObject.transform.position;
        Snake2Start.GetComponent<SpecialTile>().nextTile = Snake2End;
        Snake2End.GetComponent<SpecialTile>().nextTile = TileList[snake2End].gameObject.GetComponent<Tile>().nextTile;

        SnakeLine2.GetComponent<LineRenderer>().SetPosition(0, Snake2Start.transform.position);
        SnakeLine2.GetComponent<LineRenderer>().SetPosition(1, Snake2End.transform.position);

        SnakeLine2.transform.position = Vector3.zero;



        //set ladders
        // 2 - 55 start for ladder
        // 8 - 63 end for ladder



        while (true)
        {
            ladder1Start = (int)(Random.Range(2, 55.9f));
            if((ladder1Start != snake1Start)&&(ladder1Start != snake1End)&&(ladder1Start != snake2Start) &&(ladder1Start != snake2End))
            {
                break;
            }
        }

        while (true)
        {
            ladder1End = (int)(Random.Range(8, 63.9f));
            if((ladder1Start < ladder1End)&&(ladder1End != snake1Start)&&(ladder1End != snake1End)&& (ladder1End != snake2Start) && (ladder1End != snake2End))          //check with ladder 1, snake 1, snake 2
            {
                break;
            }
        }



        TileList[ladder1Start].gameObject.tag = "SpecialTile";
        TileList[ladder1End].gameObject.tag = "SpecialTile";

        Ladder1Start.transform.position = TileList[ladder1Start].gameObject.transform.position;
        Ladder1End.transform.position = TileList[ladder1End].gameObject.transform.position;
        Ladder1Start.GetComponent<SpecialTile>().nextTile = Ladder1End;
        Ladder1End.GetComponent<SpecialTile>().nextTile = TileList[ladder1End].gameObject.GetComponent<Tile>().nextTile;

        LadderLine1.GetComponent<LineRenderer>().SetPosition(0, Ladder1Start.transform.position);
        LadderLine1.GetComponent<LineRenderer>().SetPosition(1, Ladder1End.transform.position);

        LadderLine1.transform.position = Vector3.zero;

        /////////////////////////////////////////////////

        while (true)
        {
            ladder2Start = (int)(Random.Range(2, 55.9f));
            if ((ladder2Start != snake2Start) && (ladder2Start != snake2End) && (ladder2Start != snake1Start) && (ladder2Start != snake1End) && (ladder2Start != ladder1Start) && (ladder2Start != ladder1End))
            {
                break;
            }
        }

        while (true)
        {
            ladder2End = (int)(Random.Range(8, 63.9f));
            if ((ladder2Start < ladder2End) && (ladder2End != snake1Start) && (ladder2End != snake1End) && (ladder2End != snake2Start) && (ladder2End != snake2End) && (ladder2End != ladder1Start) && (ladder2End != ladder1End))          //check with ladder 2, ladder 1, snake 1, snake 2
            {
                break;
            }
        }



        TileList[ladder2Start].gameObject.tag = "SpecialTile";
        TileList[ladder2End].gameObject.tag = "SpecialTile";

        Ladder2Start.transform.position = TileList[ladder2Start].gameObject.transform.position;
        Ladder2End.transform.position = TileList[ladder2End].gameObject.transform.position;
        Ladder2Start.GetComponent<SpecialTile>().nextTile = Ladder2End;
        Ladder2End.GetComponent<SpecialTile>().nextTile = TileList[ladder2End].gameObject.GetComponent<Tile>().nextTile;

        LadderLine2.GetComponent<LineRenderer>().SetPosition(0, Ladder2Start.transform.position);
        LadderLine2.GetComponent<LineRenderer>().SetPosition(1, Ladder2End.transform.position);

        LadderLine2.transform.position = Vector3.zero;


        ///////////////////////////////////////////////////
      
        ///Add Button Listeners

        RollButton.onClick.AddListener(Roll);
        MoveButton.onClick.AddListener(Move);
        SkipButton.onClick.AddListener(Skip);

    }

    // Update is called once per frame
    void Update()
    {
        if(GameOver)
        {
            return;
        }

        if((PlayerTurn == 1) && Player1.GetComponent<Player>().SkippedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player1.GetComponent<Player>().RolledThisTurn = false;
            Player1.GetComponent<Player>().MovedThisTurn = false;
            Player1.GetComponent<Player>().SkippedThisTurn = false;
        }
        else if((PlayerTurn == 2) && Player2.GetComponent<Player>().SkippedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player2.GetComponent<Player>().RolledThisTurn = false;
            Player2.GetComponent<Player>().MovedThisTurn = false;
            Player2.GetComponent<Player>().SkippedThisTurn = false;
        }
        /////////////////////////////////////////////////////////////////////
        else if ((PlayerTurn == 3) && Player3.GetComponent<Player>().SkippedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player3.GetComponent<Player>().RolledThisTurn = false;
            Player3.GetComponent<Player>().MovedThisTurn = false;
            Player3.GetComponent<Player>().SkippedThisTurn = false;
        }
        else if ((PlayerTurn == 4) && Player4.GetComponent<Player>().SkippedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player4.GetComponent<Player>().RolledThisTurn = false;
            Player4.GetComponent<Player>().MovedThisTurn = false;
            Player4.GetComponent<Player>().SkippedThisTurn = false;
        }
        ///////////////////////////////////////////////////////////

        else if ((PlayerTurn == 1) && Player1.GetComponent<Player>().Roll6Skip)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player1.GetComponent<Player>().Roll6Skip = false;
            Player1.GetComponent<Player>().RolledThisTurn = false;
            Player1.GetComponent<Player>().MovedThisTurn = false;
        }
        else if ((PlayerTurn == 2) && Player2.GetComponent<Player>().Roll6Skip)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player2.GetComponent<Player>().Roll6Skip = false;
            Player2.GetComponent<Player>().RolledThisTurn = false;
            Player2.GetComponent<Player>().MovedThisTurn = false;
        }
        ///////////////////////////////////////////////////////////////
        else if ((PlayerTurn == 3) && Player3.GetComponent<Player>().Roll6Skip)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player3.GetComponent<Player>().Roll6Skip = false;
            Player3.GetComponent<Player>().RolledThisTurn = false;
            Player3.GetComponent<Player>().MovedThisTurn = false;
        }
        else if ((PlayerTurn == 4) && Player4.GetComponent<Player>().Roll6Skip)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player4.GetComponent<Player>().Roll6Skip = false;
            Player4.GetComponent<Player>().RolledThisTurn = false;
            Player4.GetComponent<Player>().MovedThisTurn = false;
        }
        /////////////////////////////////////////////////////////////

        else if(Player1.GetComponent<Player>().RolledThisTurn && Player1.GetComponent<Player>().MovedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player1.GetComponent<Player>().RolledThisTurn = false;
            Player1.GetComponent<Player>().MovedThisTurn = false;
        }
        else if(Player2.GetComponent<Player>().RolledThisTurn && Player2.GetComponent<Player>().MovedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player2.GetComponent<Player>().RolledThisTurn = false;
            Player2.GetComponent<Player>().MovedThisTurn = false;
        }
        ///////////////////////////////////////////////////////////////////////
        ///
        else if (Player3.GetComponent<Player>().RolledThisTurn && Player3.GetComponent<Player>().MovedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player3.GetComponent<Player>().RolledThisTurn = false;
            Player3.GetComponent<Player>().MovedThisTurn = false;
        }
        else if (Player4.GetComponent<Player>().RolledThisTurn && Player4.GetComponent<Player>().MovedThisTurn)
        {
            //IfTrueP1 = !IfTrueP1;
            PlayerTurn = NextTurn();

            Player4.GetComponent<Player>().RolledThisTurn = false;
            Player4.GetComponent<Player>().MovedThisTurn = false;
        }

        /////////////////////////////////////////////////////////////////////
        if ((PlayerTurn == 1))
        {
            Debug.Log("Player 1 Turn");

            UIUpadate();
        }
        else if (PlayerTurn == 2)
        {
            Debug.Log("Player 2 Turn");

            UIUpadate();
        }
        else if (PlayerTurn == 3)
        {
            Debug.Log("Player 3 Turn");

            UIUpadate();
        }
        else if (PlayerTurn == 4)
        {
            Debug.Log("Player 4 Turn");

            UIUpadate();
        }
    }

    private void UIUpadate()
    {
        if((PlayerTurn == 1))
        {
            RollText.text = "P1 Roll";
            MoveText.text = "P1 Move";
            SkipText.text = Player1.GetComponent<Player>().MaxSkips + " skips left";
        }
        else if (PlayerTurn == 2)
        {
            RollText.text = "P2 Roll";
            MoveText.text = "P2 Move";
            SkipText.text = Player2.GetComponent<Player>().MaxSkips + " skips left";
        }
        else if (PlayerTurn == 3)
        {
            RollText.text = "P3 Roll";
            MoveText.text = "P3 Move";
            SkipText.text = Player3.GetComponent<Player>().MaxSkips + " skips left";
        }
        else if (PlayerTurn == 4)
        {
            RollText.text = "P4 Roll";
            MoveText.text = "P4 Move";
            SkipText.text = Player4.GetComponent<Player>().MaxSkips + " skips left";
        }

        P1Stats.text = "Player 1\nTile Position: " + Player1.GetComponent<Player>().TilePosition.ToString() + "\nCurrentRoll: " + Player1.GetComponent<Player>().Roll.ToString() + "\nPreviousRoll: " + Player1.GetComponent<Player>().LastRoll.ToString() + "\n" + (63 - Player1.GetComponent<Player>().TilePosition).ToString() + " tiles away from Victory!";
        P2Stats.text = "Player 2\nTile Position: " + Player2.GetComponent<Player>().TilePosition.ToString() + "\nCurrentRoll: " + Player2.GetComponent<Player>().Roll.ToString() + "\nPreviousRoll: " + Player2.GetComponent<Player>().LastRoll.ToString() + "\n" + (63 - Player2.GetComponent<Player>().TilePosition).ToString() + " tiles away from Victory!";
        P3Stats.text = "Player 3\nTile Position: " + Player3.GetComponent<Player>().TilePosition.ToString() + "\nCurrentRoll: " + Player3.GetComponent<Player>().Roll.ToString() + "\nPreviousRoll: " + Player3.GetComponent<Player>().LastRoll.ToString() + "\n" + (63 - Player3.GetComponent<Player>().TilePosition).ToString() + " tiles away from Victory!";
        P4Stats.text = "Player 4\nTile Position: " + Player4.GetComponent<Player>().TilePosition.ToString() + "\nCurrentRoll: " + Player4.GetComponent<Player>().Roll.ToString() + "\nPreviousRoll: " + Player4.GetComponent<Player>().LastRoll.ToString() + "\n" + (63 - Player4.GetComponent<Player>().TilePosition).ToString() + " tiles away from Victory!";

    }

    public void Roll()
    {
        if (((PlayerTurn == 1)) &&(!Player1.GetComponent<Player>().RolledThisTurn)&&(!Player1.GetComponent<Player>().MovedThisTurn))            // if your turn, not rolled this turn, not moved this turn
        {
            RollP1();
            Player1.GetComponent<Player>().RolledThisTurn = true;
        }
        else if (((PlayerTurn == 2)) && (!Player2.GetComponent<Player>().RolledThisTurn) &&(!Player2.GetComponent<Player>().MovedThisTurn))
        {
            RollP2();
            Player2.GetComponent<Player>().RolledThisTurn = true;
        }
        else if (((PlayerTurn == 3)) && (!Player3.GetComponent<Player>().RolledThisTurn) && (!Player3.GetComponent<Player>().MovedThisTurn))
        {
            RollP3();
            Player3.GetComponent<Player>().RolledThisTurn = true;
        }
        else if (((PlayerTurn == 4)) && (!Player4.GetComponent<Player>().RolledThisTurn) && (!Player4.GetComponent<Player>().MovedThisTurn))
        {
            RollP4();
            Player4.GetComponent<Player>().RolledThisTurn = true;
        }
    }

    public void Move()
    {
        if (((PlayerTurn == 1)) && (!Player1.GetComponent<Player>().MovedThisTurn) && (Player1.GetComponent<Player>().RolledThisTurn) && (!Player1.GetComponent<Player>().Roll6Skip))              //if your turn, not moved this turn, successfully rolled this turn, and not forced to skip due to consecutive 6s
        {
            MoveP1();
            Player1.GetComponent<Player>().MovedThisTurn = true;
        }
        else if (((PlayerTurn == 2)) && (!Player2.GetComponent<Player>().MovedThisTurn) && (Player2.GetComponent<Player>().RolledThisTurn) && (!Player2.GetComponent<Player>().Roll6Skip))
        {
            MoveP2();
            Player2.GetComponent<Player>().MovedThisTurn = true;
        }
        else if (((PlayerTurn == 3)) && (!Player3.GetComponent<Player>().MovedThisTurn) && (Player3.GetComponent<Player>().RolledThisTurn) && (!Player3.GetComponent<Player>().Roll6Skip))
        {
            MoveP3();
            Player3.GetComponent<Player>().MovedThisTurn = true;
        }
        else if (((PlayerTurn == 4)) && (!Player4.GetComponent<Player>().MovedThisTurn) && (Player4.GetComponent<Player>().RolledThisTurn) && (!Player4.GetComponent<Player>().Roll6Skip))
        {
            MoveP4();
            Player4.GetComponent<Player>().MovedThisTurn = true;
        }
    }

    public void RollP1()
    {
        UIUpadate();
        Player1.GetComponent<Player>().LastRoll = Player1.GetComponent<Player>().Roll;

        Player1.GetComponent<Player>().Roll = (int)(Random.Range(1.0f, 6.9f));          //Dice Roll 1 - 6

        Debug.Log("P1 Roll: " + Player1.GetComponent<Player>().Roll + "\n");

        if ((Player1.GetComponent<Player>().Roll == 6) && (Player1.GetComponent<Player>().Roll == Player1.GetComponent<Player>().LastRoll))
        {
            //skip P1 turn
            Debug.Log("P1 Rolled two 6's consecutively, loses their next turn\n");
            Player1.GetComponent<Player>().Roll6Skip = true;
        }

        if(Player1.GetComponent<Player>().UsedSkip)
        {
            Player1.GetComponent<Player>().Roll += Player1.GetComponent<Player>().LastRoll;
            UIUpadate();
            Player1.GetComponent<Player>().UsedSkip = false;
        }
    }

    public void RollP2()
    {
        //Roll
        UIUpadate();
        Player2.GetComponent<Player>().LastRoll = Player2.GetComponent<Player>().Roll;

        Player2.GetComponent<Player>().Roll = (int)(Random.Range(1.0f, 6.9f));          //Dice Roll 1 - 6

        Debug.Log("P2 Roll: " + Player2.GetComponent<Player>().Roll + "\n");

        if ((Player2.GetComponent<Player>().Roll == 6) && (Player2.GetComponent<Player>().Roll == Player2.GetComponent<Player>().LastRoll))
        {
            //skip P2 turn
            Debug.Log("P2 Rolled two 6's consecutively, loses a turn\n");
            Player2.GetComponent<Player>().Roll6Skip = true;
        }

        if(Player2.GetComponent<Player>().UsedSkip)
        {
            Player2.GetComponent<Player>().Roll += Player2.GetComponent<Player>().LastRoll;
            UIUpadate();
            Player2.GetComponent<Player>().UsedSkip = false;
        }
      
    }

    public void RollP3()
    {
        UIUpadate();
        Player3.GetComponent<Player>().LastRoll = Player3.GetComponent<Player>().Roll;

        Player3.GetComponent<Player>().Roll = (int)(Random.Range(1.0f, 6.9f));          //Dice Roll 1 - 6

        Debug.Log("P3 Roll: " + Player3.GetComponent<Player>().Roll + "\n");

        if ((Player3.GetComponent<Player>().Roll == 6) && (Player3.GetComponent<Player>().Roll == Player3.GetComponent<Player>().LastRoll))
        {
            //skip P3 turn
            Debug.Log("P3 Rolled two 6's consecutively, loses their next turn\n");
            Player3.GetComponent<Player>().Roll6Skip = true;
        }

        if (Player3.GetComponent<Player>().UsedSkip)
        {
            Player3.GetComponent<Player>().Roll += Player3.GetComponent<Player>().LastRoll;
            UIUpadate();
            Player3.GetComponent<Player>().UsedSkip = false;
        }
    }

    public void RollP4()
    {
        UIUpadate();
        Player4.GetComponent<Player>().LastRoll = Player4.GetComponent<Player>().Roll;

        Player4.GetComponent<Player>().Roll = (int)(Random.Range(1.0f, 6.9f));          //Dice Roll 1 - 6

        Debug.Log("P4 Roll: " + Player4.GetComponent<Player>().Roll + "\n");

        if ((Player4.GetComponent<Player>().Roll == 6) && (Player4.GetComponent<Player>().Roll == Player4.GetComponent<Player>().LastRoll))
        {
            //skip P4 turn
            Debug.Log("P4 Rolled two 6's consecutively, loses their next turn\n");
            Player4.GetComponent<Player>().Roll6Skip = true;
        }

        if (Player4.GetComponent<Player>().UsedSkip)
        {
            Player4.GetComponent<Player>().Roll += Player4.GetComponent<Player>().LastRoll;
            UIUpadate();
            Player4.GetComponent<Player>().UsedSkip = false;
        }
    }

    public void MoveP1()
    {
        //move
        UIUpadate();

        //
        //Advance Player
        int i = 1;
        while (i <= Player1.GetComponent<Player>().Roll &&(!GameOver))
        {
            //Get next tile
            GameObject temp = TileList[Player1.GetComponent<Player>().TilePosition].GetComponent<Tile>().nextTile.gameObject;

            //Check if End is reached
            if (temp.tag == "End")
            {
                //Player1.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player1.transform, temp.transform);
                StartCoroutine(MoveCoroutine);
                //MoveOnBoard(ref Player1, temp.transform);

                Player1.GetComponent<Player>().TilePosition = 64;
                GameOver = true;
                MainMenuObject.GetComponent<MainMenu>().SetScorces(Player1.GetComponent<Player>().TilePosition, Player2.GetComponent<Player>().TilePosition, Player3.GetComponent<Player>().TilePosition, Player4.GetComponent<Player>().TilePosition);
                MainMenuObject.GetComponent<MainMenu>().EndGame(Player1, Player2, Player3, Player4);
            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder1Start) && ((i) == (Player1.GetComponent<Player>().Roll)))            //if next tile is ladder 1 start and it is the last move of the roll
            {
                //Player1.transform.position = TileList[ladder1Start].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[ladder1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                //MoveOnBoard(ref Player1, TileList[ladder1Start].gameObject.transform);


                Player1.GetComponent<Player>().TilePosition = ladder1Start;
                Debug.Log("P1 Climing Lader 1");
                //climb ladder
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[ladder1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                //Player1.transform.position = TileList[ladder1End].gameObject.transform.position;
                Player1.GetComponent<Player>().TilePosition = ladder1End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder2Start) && ((i) == (Player1.GetComponent<Player>().Roll)))            //if next tile is ladder 2 start and it is the last move of the roll
            {
                //Player1.transform.position = TileList[ladder2Start].gameObject.transform.position;
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[ladder2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                //MoveOnBoard(ref Player1, TileList[ladder2Start].gameObject.transform);


                Player1.GetComponent<Player>().TilePosition = ladder2Start;
                Debug.Log("P1 Climing Lader 2");
                //climb ladder
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[ladder2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                //Player1.transform.position = TileList[ladder2End].gameObject.transform.position;
                Player1.GetComponent<Player>().TilePosition = ladder2End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake1Start) && ((i) == (Player1.GetComponent<Player>().Roll)))              //if next tile is snake 1 start and it is the last move of the roll
            {
                //Player1.transform.position = TileList[snake1Start].gameObject.transform.position;
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[snake1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                //MoveOnBoard(ref Player1, TileList[snake1Start].gameObject.transform);

                Player1.GetComponent<Player>().TilePosition = snake1Start;
                Debug.Log("P1 Sliding Down The Snake 1");
                //slide snake

                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[snake1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                //Player1.transform.position = TileList[snake1End].gameObject.transform.position;
                Player1.GetComponent<Player>().TilePosition = snake1End;
            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake2Start) && ((i) == (Player1.GetComponent<Player>().Roll)))              //if next tile is snake 2 start and it is the last move of the roll
            {
                //Player1.transform.position = TileList[snake2Start].gameObject.transform.position;
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[snake2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                //MoveOnBoard(ref Player1, TileList[snake2Start].gameObject.transform);
                Player1.transform.position = temp.transform.position;
                Player1.GetComponent<Player>().TilePosition = snake2Start;
                Debug.Log("P1 Sliding Down The Snake 2");
                //slide snake
                MoveCoroutine = MoveOnBoard(Player1.transform, TileList[snake2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                //Player1.transform.position = TileList[snake2End].gameObject.transform.position;
                Player1.GetComponent<Player>().TilePosition = snake2End;
            }
            else
            {
                //Player1.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player1.transform, temp.transform);
                StartCoroutine(MoveCoroutine);

                //MoveOnBoard(ref Player1, temp.transform);
                //Player1.transform.position = temp.transform.position;
                Player1.GetComponent<Player>().TilePosition = temp.gameObject.GetComponent<Tile>().tileNumber;
            }

            i++;
            UIUpadate();
        }
    }

    public void MoveP2()
    {
        //Move
        UIUpadate();
        //Ask if skip
        //
        //Advance Player
        int i = 1;
        while ((i <= Player2.GetComponent<Player>().Roll)&&(!GameOver))
        {
            //Get next tile
            GameObject temp = TileList[Player2.GetComponent<Player>().TilePosition].GetComponent<Tile>().nextTile.gameObject;

            //Check if End is reached
            if (temp.tag == "End")
            {
                //Player2.transform.position = temp.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, temp.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = 64;
                GameOver = true;
                MainMenuObject.GetComponent<MainMenu>().SetScorces(Player1.GetComponent<Player>().TilePosition, Player2.GetComponent<Player>().TilePosition, Player3.GetComponent<Player>().TilePosition, Player4.GetComponent<Player>().TilePosition);
                MainMenuObject.GetComponent<MainMenu>().EndGame(Player2, Player1, Player3, Player4);
            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder1Start) && ((i) == (Player2.GetComponent<Player>().Roll)))            //if next tile is ladder 1 start and it is the last move of the roll
            {
                //Player2.transform.position = TileList[ladder1Start].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[ladder1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = ladder1Start;
                Debug.Log("P2 Climing Lader 1");
                //climb ladder

                //Player2.transform.position = TileList[ladder1End].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[ladder1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = ladder1End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder2Start) && ((i) == (Player2.GetComponent<Player>().Roll)))            //if next tile is ladder 2 start and it is the last move of the roll
            {
                //Player2.transform.position = TileList[ladder2Start].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[ladder2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = ladder2Start;
                Debug.Log("P2 Climing Lader 2");
                //climb ladder
                //Player2.transform.position = TileList[ladder2End].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[ladder2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = ladder2End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake1Start) && ((i) == (Player2.GetComponent<Player>().Roll)))              //if next tile is snake 1 start and it is the last move of the roll
            {
                //Player2.transform.position = TileList[snake1Start].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[snake1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = snake1Start;
                Debug.Log("P2 Sliding Down The Snake 1");
                //slide snake
                //Player2.transform.position = TileList[snake1End].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[snake1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);


                Player2.GetComponent<Player>().TilePosition = snake1End;
            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake2Start) && ((i) == (Player2.GetComponent<Player>().Roll)))              //if next tile is snake 2 start and it is the last move of the roll
            {
                //Player2.transform.position = TileList[snake2Start].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[snake2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = snake2Start;
                Debug.Log("P2 Sliding Down The Snake 2");
                //slide snake
                Player2.transform.position = TileList[snake2End].gameObject.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, TileList[snake2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = snake2End;
            }
            else
            {
                //Player2.transform.position = temp.transform.position;

                MoveCoroutine = MoveOnBoard(Player2.transform, temp.transform);
                StartCoroutine(MoveCoroutine);

                Player2.GetComponent<Player>().TilePosition = temp.gameObject.GetComponent<Tile>().tileNumber;
            }

            i++;
            UIUpadate();
        }
        //IfTrueP1 = !IfTrueP1;

    }

    public void MoveP3()
    {
        //move
        UIUpadate();

        //
        //Advance Player
        int i = 1;
        while (i <= Player3.GetComponent<Player>().Roll && (!GameOver))
        {
            //Get next tile
            GameObject temp = TileList[Player3.GetComponent<Player>().TilePosition].GetComponent<Tile>().nextTile.gameObject;

            //Check if End is reached
            if (temp.tag == "End")
            {
                //Player3.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player3.transform, temp.transform);
                StartCoroutine(MoveCoroutine);
                //MoveOnBoard(ref Player1, temp.transform);

                Player3.GetComponent<Player>().TilePosition = 64;
                GameOver = true;
                MainMenuObject.GetComponent<MainMenu>().SetScorces(Player1.GetComponent<Player>().TilePosition, Player2.GetComponent<Player>().TilePosition, Player3.GetComponent<Player>().TilePosition, Player4.GetComponent<Player>().TilePosition);
                MainMenuObject.GetComponent<MainMenu>().EndGame(Player3, Player1, Player2, Player4);
            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder1Start) && ((i) == (Player3.GetComponent<Player>().Roll)))            //if next tile is ladder 1 start and it is the last move of the roll
            {
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[ladder1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player3.GetComponent<Player>().TilePosition = ladder1Start;
                Debug.Log("P3 Climing Lader 1");
                
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[ladder1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                
                Player3.GetComponent<Player>().TilePosition = ladder1End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder2Start) && ((i) == (Player3.GetComponent<Player>().Roll)))            //if next tile is ladder 2 start and it is the last move of the roll
            {
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[ladder2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                
                Player3.GetComponent<Player>().TilePosition = ladder2Start;
                Debug.Log("P3 Climing Lader 2");
                //climb ladder
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[ladder2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                
                Player3.GetComponent<Player>().TilePosition = ladder2End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake1Start) && ((i) == (Player3.GetComponent<Player>().Roll)))              //if next tile is snake 1 start and it is the last move of the roll
            {
                
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[snake1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                
                Player3.GetComponent<Player>().TilePosition = snake1Start;
                Debug.Log("P3 Sliding Down The Snake 1");
                //slide snake

                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[snake1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);
                
                Player3.GetComponent<Player>().TilePosition = snake1End;
            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake2Start) && ((i) == (Player3.GetComponent<Player>().Roll)))              //if next tile is snake 2 start and it is the last move of the roll
            {
                
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[snake2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                
                Player3.transform.position = temp.transform.position;
                Player3.GetComponent<Player>().TilePosition = snake2Start;
                Debug.Log("P3 Sliding Down The Snake 2");
                //slide snake
                MoveCoroutine = MoveOnBoard(Player3.transform, TileList[snake2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                
                Player3.GetComponent<Player>().TilePosition = snake2End;
            }
            else
            {
                //Player1.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player3.transform, temp.transform);
                StartCoroutine(MoveCoroutine);

                Player3.GetComponent<Player>().TilePosition = temp.gameObject.GetComponent<Tile>().tileNumber;
            }

            i++;
            UIUpadate();
        }
    }

    public void MoveP4()
    {
        //move
        UIUpadate();

        //
        //Advance Player
        int i = 1;
        while (i <= Player4.GetComponent<Player>().Roll && (!GameOver))
        {
            //Get next tile
            GameObject temp = TileList[Player4.GetComponent<Player>().TilePosition].GetComponent<Tile>().nextTile.gameObject;

            //Check if End is reached
            if (temp.tag == "End")
            {
                //Player3.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player4.transform, temp.transform);
                StartCoroutine(MoveCoroutine);
                //MoveOnBoard(ref Player1, temp.transform);

                Player4.GetComponent<Player>().TilePosition = 64;
                GameOver = true;
                MainMenuObject.GetComponent<MainMenu>().SetScorces(Player1.GetComponent<Player>().TilePosition, Player2.GetComponent<Player>().TilePosition, Player3.GetComponent<Player>().TilePosition, Player4.GetComponent<Player>().TilePosition);
                MainMenuObject.GetComponent<MainMenu>().EndGame(Player4, Player1, Player2, Player3);
            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder1Start) && ((i) == (Player4.GetComponent<Player>().Roll)))            //if next tile is ladder 1 start and it is the last move of the roll
            {
                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[ladder1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player4.GetComponent<Player>().TilePosition = ladder1Start;
                Debug.Log("P4 Climing Lader 1");

                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[ladder1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player4.GetComponent<Player>().TilePosition = ladder1End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == ladder2Start) && ((i) == (Player4.GetComponent<Player>().Roll)))            //if next tile is ladder 2 start and it is the last move of the roll
            {
                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[ladder2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);


                Player4.GetComponent<Player>().TilePosition = ladder2Start;
                Debug.Log("P4 Climing Lader 2");
                //climb ladder
                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[ladder2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player4.GetComponent<Player>().TilePosition = ladder2End;

            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake1Start) && ((i) == (Player4.GetComponent<Player>().Roll)))              //if next tile is snake 1 start and it is the last move of the roll
            {

                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[snake1Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);


                Player4.GetComponent<Player>().TilePosition = snake1Start;
                Debug.Log("P4 Sliding Down The Snake 1");
                //slide snake

                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[snake1End].gameObject.transform);
                StartCoroutine(MoveCoroutine);

                Player4.GetComponent<Player>().TilePosition = snake1End;
            }
            else if ((temp.GetComponent<Tile>().tileNumber == snake2Start) && ((i) == (Player4.GetComponent<Player>().Roll)))              //if next tile is snake 2 start and it is the last move of the roll
            {

                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[snake2Start].gameObject.transform);
                StartCoroutine(MoveCoroutine);


                Player4.transform.position = temp.transform.position;
                Player4.GetComponent<Player>().TilePosition = snake2Start;
                Debug.Log("P4 Sliding Down The Snake 2");
                //slide snake
                MoveCoroutine = MoveOnBoard(Player4.transform, TileList[snake2End].gameObject.transform);
                StartCoroutine(MoveCoroutine);


                Player4.GetComponent<Player>().TilePosition = snake2End;
            }
            else
            {
                //Player1.transform.position = temp.transform.position;
                MoveCoroutine = MoveOnBoard(Player4.transform, temp.transform);
                StartCoroutine(MoveCoroutine);

                Player4.GetComponent<Player>().TilePosition = temp.gameObject.GetComponent<Tile>().tileNumber;
            }

            i++;
            UIUpadate();
        }
    }


    public void Skip()
    {
        if (((PlayerTurn == 1)) && (Player1.GetComponent<Player>().RolledThisTurn) && (!Player1.GetComponent<Player>().MovedThisTurn) && (Player1.GetComponent<Player>().MaxSkips >0))            // if your turn, sucessfully rolled this turn, not moved, legally allowed to skip
        {
            Player1.GetComponent<Player>().MaxSkips--;
            Player1.GetComponent<Player>().UsedSkip = true;
            Player1.GetComponent<Player>().SkippedThisTurn = true;
        }
        else if (((PlayerTurn == 2)) && (Player2.GetComponent<Player>().RolledThisTurn) && (!Player2.GetComponent<Player>().MovedThisTurn) && (Player2.GetComponent<Player>().MaxSkips >0) )           
        {
            Player2.GetComponent<Player>().MaxSkips--;
            Player2.GetComponent<Player>().UsedSkip = true;
            Player2.GetComponent<Player>().SkippedThisTurn = true;
        }
        else if (((PlayerTurn == 3)) && (Player3.GetComponent<Player>().RolledThisTurn) && (!Player3.GetComponent<Player>().MovedThisTurn) && (Player3.GetComponent<Player>().MaxSkips > 0))
        {
            Player3.GetComponent<Player>().MaxSkips--;
            Player3.GetComponent<Player>().UsedSkip = true;
            Player3.GetComponent<Player>().SkippedThisTurn = true;
        }
        else if (((PlayerTurn == 4)) && (Player4.GetComponent<Player>().RolledThisTurn) && (!Player4.GetComponent<Player>().MovedThisTurn) && (Player4.GetComponent<Player>().MaxSkips > 0))
        {
            Player4.GetComponent<Player>().MaxSkips--;
            Player4.GetComponent<Player>().UsedSkip = true;
            Player4.GetComponent<Player>().SkippedThisTurn = true;
        }
    }

    int NextTurn()
    {
        if (PlayerTurn == 4)
        {
            PlayerTurn = 1;
        }
        else
        {
            PlayerTurn++;
        }
        return PlayerTurn;
    }

    IEnumerator MoveOnBoard(Transform Player, Transform Destination)
    {
        bool movementComplete = false;
        float ratio = 0.0f;
        bool verticality = false;
        if(Player.position.z != Destination.position.z)
        {
            verticality = true;
        }
        while (!movementComplete)
        {
            if(verticality)
            {
                Player.position = new Vector3(Mathf.Lerp(Player.position.x, Destination.position.x, ratio), Player.position.y, Mathf.Lerp(Player.position.z, Destination.position.z, ratio));

            }
            else
            {
                Player.position = new Vector3(Mathf.Lerp(Player.position.x, Destination.position.x, ratio), Player.position.y, Destination.position.z);

            }
            ratio += 0.11f;
            if (ratio > 1)
            {
                Player.position = Destination.position;
                movementComplete = true;
            }
            yield return new WaitForSeconds(.05f);
        }
        
        //yield return null;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
