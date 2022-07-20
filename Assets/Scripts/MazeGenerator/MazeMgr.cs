using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeMgr
{
    private static MazeMgr _instance;
    private List<MazeBlock> _mazeBlocks = new List<MazeBlock>();
    private List<MazeWall> _mazeWalls = new List<MazeWall>();
    private MazeBlock _lastBlock;
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

    public void RegisterWall(MazeWall wall)
    {
        _mazeWalls.Add(wall);
    }

    public void LinkWallsAndBlocks()
    {
        for (int wallIndex = 0; wallIndex < MazeConfig.MazeTotalBlockX * (MazeConfig.MazeTotalBlockY + 1); wallIndex++)
        {   
            if (wallIndex < MazeConfig.MazeTotalBlock)
            {//up side wall
                _mazeBlocks[wallIndex].Walls[MazeConfig.DirectionUp] = _mazeWalls[wallIndex];
                _mazeWalls[wallIndex].ConnectedBlocks[MazeConfig.ConnectionUp] = _mazeBlocks[wallIndex];
            }
            
            if (wallIndex - MazeConfig.MazeTotalBlockX >= 0)
            {//down side wall
                _mazeBlocks[wallIndex - MazeConfig.MazeTotalBlockX].Walls[MazeConfig.DirectionDown] = _mazeWalls[wallIndex];
                _mazeWalls[wallIndex].ConnectedBlocks[MazeConfig.ConnectionDown] =
                    _mazeBlocks[wallIndex - MazeConfig.MazeTotalBlockX];
            }
        }

        for (int wallIndex = MazeConfig.MazeTotalBlockX * (MazeConfig.MazeTotalBlockY + 1);
             wallIndex < MazeConfig.MazeTotalWall;
             wallIndex++)
        {
            int realWallIndex = wallIndex - MazeConfig.MazeTotalBlockX * (MazeConfig.MazeTotalBlockY + 1);
            int blockIndex = realWallIndex / (MazeConfig.MazeTotalBlockX + 1) * MazeConfig.MazeTotalBlockX +
                             Math.Min(MazeConfig.MazeTotalBlockX, realWallIndex % (MazeConfig.MazeTotalBlockX + 1)); 
            if (realWallIndex % (MazeConfig.MazeTotalBlockX + 1) < MazeConfig.MazeTotalBlockX)
            {
                _mazeBlocks[blockIndex].Walls[MazeConfig.DirectionLeft] = _mazeWalls[wallIndex];
                _mazeWalls[wallIndex].ConnectedBlocks[MazeConfig.ConnectionLeft] = _mazeBlocks[blockIndex];
            }

            if (realWallIndex % (MazeConfig.MazeTotalBlockX + 1) > 0)
            {
                _mazeBlocks[blockIndex-1].Walls[MazeConfig.DirectionRight] = _mazeWalls[wallIndex];
                _mazeWalls[wallIndex].ConnectedBlocks[MazeConfig.ConnectionRight] = _mazeBlocks[blockIndex - 1];
            }
        }
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
                currentBlock.NeighbourBlocks[direction] =
                    0 <= neighboursIndex[direction] && neighboursIndex[direction] < MazeConfig.MazeTotalBlock && !IsSideBlock(blockIndex, direction)
                        ? _mazeBlocks[neighboursIndex[direction]]
                        : null;
            }

        }
    }

    private MazeWall GetNeighbourWall(MazeBlock neighbourBlock, int neighbourWallDirection)
    {
        if (neighbourBlock != null && neighbourBlock.Walls[neighbourWallDirection] != null)
        {
            return neighbourBlock.Walls[neighbourWallDirection];
        }

        return null;
    }
    
    private bool IsSideBlock(int blockIndex, int direction)
    {
        return (blockIndex < MazeConfig.MazeTotalBlockX && direction == MazeConfig.DirectionUp) ||
               (blockIndex / MazeConfig.MazeTotalBlockX == MazeConfig.MazeTotalBlockY - 1 &&
                direction == MazeConfig.DirectionDown) ||
               (blockIndex % MazeConfig.MazeTotalBlockX == 0 && direction == MazeConfig.DirectionLeft) ||
               (blockIndex % MazeConfig.MazeTotalBlockX == MazeConfig.MazeTotalBlockX - 1 &&
                direction == MazeConfig.DirectionRight);
    }

    private List<int> _tempWallList = new List<int>();
    private List<int> _tempDirectionList = new List<int>();
    private List<MazeBlock> _adjustBlockList = new List<MazeBlock>();

    private int _visitedBlockCount = 0;
    public void Prepare()
    {
        //int startPos = Random.Range(0, _mazeBlocks.Count);
        _adjustBlockList.Add(_mazeBlocks[0]);
        _lastBlock = null;
        _visitedBlockCount = 0;
    }

    public bool ResolveNextBlock(MazeConfig.AlgorithmName algorithm)
    {
        switch (algorithm)
        {
            case MazeConfig.AlgorithmName.Prim:
                return ResolveNextBlockPrim();
            case MazeConfig.AlgorithmName.DepthFirst:
                return ResolveNextBlockDepthFirst();
            case MazeConfig.AlgorithmName.AldousBroder:
                return ResolveNextBlockAldousBroder();
                
        }

        return false;
    }

    public bool ResolveNextBlockAldousBroder()
    {
        _tempDirectionList.Clear();
        MarkVisitedBlock(_lastBlock);
        if (_visitedBlockCount < MazeConfig.MazeTotalBlock)
        {
            var currentBlock = _adjustBlockList[0];
            _lastBlock = currentBlock;
            if (!currentBlock.IsVisited)
            {
                _visitedBlockCount++;
            }
            MarkVisitingBlock(currentBlock);
            for (int direction = 0; direction < MazeConfig.DirectionCount; direction++)
            {
                if (currentBlock.NeighbourBlocks[direction] != null)
                {
                    _tempDirectionList.Add(direction);
                }
                
            }

            int nextDirection = _tempDirectionList[Random.Range(0, _tempDirectionList.Count)];
            _adjustBlockList[0] = currentBlock.NeighbourBlocks[nextDirection];

            if (!_adjustBlockList[0].IsVisited)
            {
                currentBlock.Walls[nextDirection].gameObject.SetActive(false);
            }

            return true;
        }
        return false;
    }

    public bool ResolveNextBlockDepthFirst()
    {
        _tempDirectionList.Clear();
        MarkVisitedBlock(_lastBlock);
        
        Debug.Log("Adjust block : " + _adjustBlockList.Count);
        
        if (_adjustBlockList.Count > 0)
        {
            
            var current = _adjustBlockList[^1];
            _lastBlock = current;
            MarkVisitingBlock(current);
            for (int direction = 0; direction < MazeConfig.DirectionCount; direction++)
            {
                if (current.NeighbourBlocks[direction] != null && !current.NeighbourBlocks[direction].IsVisited)
                {
                    MarkCandidateBlock(current.NeighbourBlocks[direction]);
                    _tempDirectionList.Add(direction);
                }
            }

            if (_tempDirectionList.Count > 0)
            {
                var nextBlock = Random.Range(0, _tempDirectionList.Count);
                _adjustBlockList.Add(current.NeighbourBlocks[_tempDirectionList[nextBlock]]);
                current.Walls[_tempDirectionList[nextBlock]].gameObject.SetActive(false);
            }
            else
            {
                while (_adjustBlockList.Count > 0 && _adjustBlockList[^1].IsAllNeighbourVisited())
                {
                    _adjustBlockList.RemoveAt(_adjustBlockList.Count - 1);
                }
                
            }
            

            return true;
        }

        return false;
    }


    public bool ResolveNextBlockPrim()
    {
        _tempDirectionList.Clear();
        MarkVisitedBlock(_lastBlock);
        if (_adjustBlockList.Count == 0)
        {
            return false;
        }
        
        var randomIndex = Random.Range(0, _adjustBlockList.Count);
        var block = _adjustBlockList[randomIndex];
        _adjustBlockList.RemoveAt(randomIndex);
        for (int direction = 0; direction < MazeConfig.DirectionCount; direction++)
        {
            if (block.NeighbourBlocks[direction] != null && block.NeighbourBlocks[direction].IsVisited && block.Walls[direction].gameObject.activeSelf)
            {
                _tempDirectionList.Add(direction);
            }

            if (block.NeighbourBlocks[direction] != null && !block.NeighbourBlocks[direction].IsVisited && !_adjustBlockList.Contains(block.NeighbourBlocks[direction]))
            {
                MarkCandidateBlock(block.NeighbourBlocks[direction]);
                _adjustBlockList.Add(block.NeighbourBlocks[direction]);
            }
        }

        if (_tempDirectionList.Count > 0)
        {
            var wallDirection = _tempDirectionList[Random.Range(0, _tempDirectionList.Count)];
            block.Walls[wallDirection].gameObject.SetActive(false);
        }

        _lastBlock = block;
        MarkVisitingBlock(block);
        
        //Debug.Log(_adjustBlockList.Count);
        return true;
    }

    private void MarkCandidateBlock(MazeBlock block)
    {
        if (block == null)
            return;
        block.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void MarkVisitingBlock(MazeBlock block)
    {
        if (block == null)
            return;
        block.gameObject.GetComponent<Renderer>().material.color = Color.red;
        block.IsVisited = true;
    }

    private void MarkVisitedBlock(MazeBlock block)
    {
        if (block == null)
            return;
        block.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        block.IsVisited = true;
    }

}