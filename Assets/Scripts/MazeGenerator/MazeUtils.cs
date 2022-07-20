using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public static class MazeUtils
{
    public static Vector3 GetBlockPosByIndex(int index)
    {
        float x = (index % MazeConfig.MazeTotalBlockX - MazeConfig.MazeTotalBlockX / 2) * MazeConfig.MazeBlockSizeX;
        float z = (MazeConfig.MazeTotalBlockY / 2 - index / MazeConfig.MazeTotalBlockX) * MazeConfig.MazeBlockSizeY;
        return new Vector3(x, 0, z);
    }
}

public static class Extensions
{
    private static Random rand = new Random();
 
    public static void Shuffle<T>(this IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--) 
        {
            int k = rand.Next(i + 1);
            (values[k], values[i]) = (values[i], values[k]);
        }
    }
}