using UnityEngine;

public class MazeWall
{
    private bool _isActive;
    private int _wallIndex;
    public enum WallDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        COUNT
    }
    public MazeWall(int index)
    {
        _isActive = true;
        _wallIndex = index;
        UpdateWallPos();
    }

    private void UpdateWallPos()
    {
        
    }

}