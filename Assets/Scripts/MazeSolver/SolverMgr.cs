using System.Collections.Generic;

public class SolverMgr
{
    private static SolverMgr _instance;
    private MazeBlock _finishBlock;

    public static SolverMgr Instance()
    {
        return _instance ??= new SolverMgr();
    }

    private SolverMgr()
    {
    }

    public void Prepare()
    {
        _currentBlock = MazeMgr.Instance().GetMaze()[0];
        _finishBlock = MazeMgr.Instance().GetMaze()[^1];
    }

    public bool ResolveNextMaze(SolverConfig.SolveAlgorithmName name)
    {
        switch (name)
        {
            case SolverConfig.SolveAlgorithmName.WallFollower:
                return ResolveNextMazeWallFollower();
            case SolverConfig.SolveAlgorithmName.DepthFirst:
                return ResolveNextMazeDepthFirst();
        }

        return false;
    }

    private int _currentDirectionIndex = 0;
    private MazeBlock _currentBlock;
    private bool ResolveNextMazeWallFollower()
    {
        if (_currentBlock == _finishBlock)
        {
            return false;
        }

        for (int index = 0; index < MazeConfig.DirectionCount; index++)
        {
            int checkDirection = (_currentDirectionIndex + index + MazeConfig.DirectionCount - 1) % MazeConfig.DirectionCount;
            if (_currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[checkDirection]] != null && !_currentBlock.Walls[SolverConfig.RemapDirections[checkDirection]].gameObject.activeSelf)
            {
                SolverUtils.MarkSolvedBlock(_currentBlock);
                SolverUtils.MarkSolvingBlock(_currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[checkDirection]]);
                _currentBlock = _currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[checkDirection]];
                _currentDirectionIndex = checkDirection;
                return true;
            }
        }

        return false;
    }

    private List<MazeBlock> _visitedStack = new List<MazeBlock>();
    private bool ResolveNextMazeDepthFirst()
    {
        if (_currentBlock == _finishBlock)
        {
            return false;
        }
        for (int directionIndex = 0; directionIndex < MazeConfig.DirectionCount; directionIndex++)
        {
            if(_currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[directionIndex]] != null && !_currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[directionIndex]].IsSolved
               && !_currentBlock.Walls[SolverConfig.RemapDirections[directionIndex]].gameObject.activeSelf)
            {
                _visitedStack.Add(_currentBlock);
                SolverUtils.MarkSolvedBlock(_currentBlock);
                _currentBlock = _currentBlock.NeighbourBlocks[SolverConfig.RemapDirections[directionIndex]];
                SolverUtils.MarkSolvingBlock(_currentBlock);
                return true;
            }
        }

        if (_visitedStack.Count > 0)
        {
            SolverUtils.MarkSolvedBlock(_currentBlock);
            _currentBlock = _visitedStack[^1];
            SolverUtils.MarkSolvingBlock(_currentBlock);
            _visitedStack.RemoveAt(_visitedStack.Count - 1);
            return true;
        }
        else
        {
            return false;
        }
    }

    
}
