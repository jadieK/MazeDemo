using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeBlock : MonoBehaviour
{
    public MazeWall[] Walls = new MazeWall[MazeConfig.DirectionCount];
    public MazeBlock[] NeighbourBlocks = new MazeBlock[MazeConfig.DirectionCount];
    
    [NonSerialized]
    public bool IsVisited;
    
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
