using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int TilePosition;
    public int Roll;
    public int LastRoll;
    public bool RolledThisTurn;
    public bool MovedThisTurn;
    public int MaxSkips = 1;
    public bool UsedSkip;
    public bool Roll6Skip;
    public bool SkippedThisTurn;

    void Start()
    {
        TilePosition = 0;
        Roll = 0;
        LastRoll = 0;
        MaxSkips = 1;
        RolledThisTurn = false;
        UsedSkip = false;
        Roll6Skip = false;
        MovedThisTurn = false;
        SkippedThisTurn = false;
    }

    
}
