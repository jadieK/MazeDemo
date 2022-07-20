public static class MazeConfig
{
    public const int DirectionUp = 0;
    public const int DirectionDown = 1;
    public const int DirectionLeft = 2;
    public const int DirectionRight = 3;
    public const int DirectionCount = 4;

    public const int ConnectionUp = 0;
    public const int ConnectionLeft = 0;
    public const int ConnectionDown = 1;
    public const int ConnectionRight = 1;

    public static readonly int[] DirectionReverse = new[] { DirectionDown, DirectionUp, DirectionRight, DirectionLeft };

    public const int MazeTotalBlockX = 30;
    public const int MazeTotalBlockY = 30;

    public static readonly int MazeTotalBlock = MazeTotalBlockX * MazeTotalBlockY;

    public static readonly int MazeTotalWall =
        MazeTotalBlockX * (MazeTotalBlockY + 1) + (MazeTotalBlockX + 1) * MazeTotalBlockY;

    public const float MazeBlockSizeX = 1f;
    public const float MazeBlockSizeY = 1f;

    public const float MazeBlockCenterOffsetX = MazeBlockSizeX / 2.0f;
    public const float MazeBlockCenterOffsetY = MazeBlockSizeY / 2.0f;
    
    public const float  WallBaseX = -(MazeTotalBlockX * MazeBlockSizeX / 2.0f);
    public const float  WallBaseY = (MazeTotalBlockY * MazeBlockSizeY / 2.0f);
    
    public enum AlgorithmName
    {
        Prim,
        DepthFirst,
        AldousBroder 
    }
}