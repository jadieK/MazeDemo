using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBlock : MonoBehaviour
{
    public bool IsVisited;
    public MazeWall[] Walls;

    private int _blockIndex;

    public void InitMazeBlock(int blockIndex)
    {
        IsVisited = false;
        Walls = new MazeWall[(int)MazeWall.WallDirection.COUNT];
        _blockIndex = blockIndex;
        UpdateBlockPos();
    }

    private void UpdateBlockPos()
    {
        
    }
    
}
