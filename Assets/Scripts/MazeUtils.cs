using UnityEngine;

public static class MazeUtils
{
    public static Vector3 GetBlockPosByIndex(int index)
    {
        float x = (index % MazeConfig.MazeTotalBlockX - MazeConfig.MazeTotalBlockX / 2) * MazeConfig.MazeBlockSizeX;
        float z = (MazeConfig.MazeTotalBlockY / 2 - index / MazeConfig.MazeTotalBlockX) * MazeConfig.MazeBlockSizeY;
        return new Vector3(x, 0, z);
    }
}
