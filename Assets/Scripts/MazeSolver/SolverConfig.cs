public static class SolverConfig
{
    public static int[] RemapDirections = new[]
        { MazeConfig.DirectionLeft, MazeConfig.DirectionUp, MazeConfig.DirectionRight, MazeConfig.DirectionDown };
    
    public enum SolveAlgorithmName
    {
        WallFollower,
        DepthFirst
    }
}
