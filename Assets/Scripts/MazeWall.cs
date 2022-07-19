using System;
using UnityEngine;

public class MazeWall : MonoBehaviour
{
    [NonSerialized]
    public bool isActive;
    
    public void InitMazeWall(int index)
    {
        isActive = true;
    }

}