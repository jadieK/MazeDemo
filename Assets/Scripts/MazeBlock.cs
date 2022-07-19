using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeBlock : MonoBehaviour
{
    public GameObject[] Walls;
    
    [NonSerialized]
    public bool IsVisited;
    
    [NonSerialized]
    public MazeBlock[] Neighbours = new MazeBlock[MazeConfig.DirectionCount];

    [NonSerialized]
    public int BlockIndex;

    public void InitMazeBlock(int blockIndex)
    {
        IsVisited = false;
        BlockIndex = blockIndex;
        UpdateBlockPos();
        MazeMgr.Instance().RegisterBlock(this);
    }

    private void UpdateBlockPos()
    {
        transform.position = MazeUtils.GetBlockPosByIndex(BlockIndex);
    }
}
