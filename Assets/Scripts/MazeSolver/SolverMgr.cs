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
            if (_currentBlock.NeighbourBlocks[SolverConfig.WallFollowerDirections[checkDirection]] != null && !_currentBlock.Walls[SolverConfig.WallFollowerDirections[checkDirection]].gameObject.activeSelf)
            {
                SolverUtils.MarkSolvedBlock(_currentBlock);
                SolverUtils.MarkSolvingBlock(_currentBlock.NeighbourBlocks[SolverConfig.WallFollowerDirections[checkDirection]]);
                _currentBlock = _currentBlock.NeighbourBlocks[SolverConfig.WallFollowerDirections[checkDirection]];
                _currentDirectionIndex = checkDirection;
                return true;
            }
        }

        return false;
    }
}
