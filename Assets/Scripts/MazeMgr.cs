using System.Collections.Generic;
using UnityEngine;

public class MazeMgr
{
    private static MazeMgr _instance;
    private List<MazeBlock> _mazeBlocks = new List<MazeBlock>();

    public static MazeMgr Instance()
    {
        if (_instance == null)
        {
            _instance = new MazeMgr();
        }

        return _instance;
    }

    private MazeMgr()
    {
    }

    public void RegisterBlock(MazeBlock block)
    {
        _mazeBlocks.Add(block);
    }

    public void InitBlockNeighbours()
    {
        for (int blockIndex = 0; blockIndex < MazeConfig.MazeTotalBlock; blockIndex++)
        {
            MazeBlock currentBlock = _mazeBlocks[blockIndex];
            int[] neighboursIndex = new[]
            {
                blockIndex - MazeConfig.MazeTotalBlockX, blockIndex + MazeConfig.MazeTotalBlockX, blockIndex - 1,
                blockIndex + 1
            };
            for (int direction = 0; direction < MazeConfig.DirectionCount; direction++)
            {
                currentBlock.Neighbours[direction] =
                    0 < neighboursIndex[direction] && neighboursIndex[direction] < MazeConfig.MazeTotalBlock
                        ? _mazeBlocks[neighboursIndex[direction]]
                        : null;
            }

        }
    }

    private List<int> _tempWallList = new List<int>(4);
    public bool ResolveNextBlock()
    {
        _tempWallList.Clear();
        (MazeBlock target, int blockIndex) = GetNextRandomBlock();
        if (target != null)
        {
            target.gameObject.GetComponent<Renderer>().material.color = Color.red;
            target.IsVisited = true;
            for (int direction = MazeConfig.DirectionUp; direction < MazeConfig.DirectionCount; direction++)
            {
                if (target.Neighbours[direction] != null && target.Neighbours[direction].IsVisited && target.Walls[direction].activeSelf)
                {
                    _tempWallList.Add(direction);
                }
            }

            if (_tempWallList.Count > 0)
            {
                var targetDirection = _tempWallList[Random.Range(0, _tempWallList.Count)];
                target.Walls[targetDirection].SetActive(false);
                target.Neighbours[targetDirection].Walls[MazeConfig.DirectionReverse[targetDirection]].SetActive(false);
            }
            _mazeBlocks.RemoveAt(blockIndex);
        }

        return blockIndex != -1;
    }

    private (MazeBlock, int) GetNextRandomBlock()
    {
        if (_mazeBlocks.Count <= 0)
        {
            return (null, -1);
        }
        int targetIndex = Random.Range(0, _mazeBlocks.Count);
        return (_mazeBlocks[targetIndex], targetIndex);
    }
    
    
}