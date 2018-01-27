using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Scholars
{
    Neutral,
    Enemy,
    Friend
}

public struct Node
{
    public int id;
    public bool LastVisited;
    public Transform Transform;
}

public class Level
{
    Scholars[,] _schollarArray;
    
    public Level()
    {
        _schollarArray = new Scholars[4,4];
    }
}





