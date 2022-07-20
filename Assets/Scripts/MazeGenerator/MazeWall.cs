using System;
using UnityEngine;

public class MazeWall : MonoBehaviour
{
    [NonSerialized] public MazeBlock[] ConnectedBlocks = new MazeBlock[2];
    [NonSerialized] public int WallIndex;
    public void InitMazeWall(int index)
    {
        WallIndex = index;
        MazeMgr.Instance().RegisterWall(this);
        UpdatePosAndRot();
    }

    private void UpdatePosAndRot()
    {
        if (WallIndex < MazeConfig.MazeTotalBlockX * (MazeConfig.MazeTotalBlockY + 1))
        {//up/down side wall
            Quaternion rot = Quaternion.Euler(0,90,0);
            Vector3 pos =
                new Vector3(MazeConfig.WallBaseX + (WallIndex % MazeConfig.MazeTotalBlockX) * MazeConfig.MazeBlockSizeX,
                    0.5f, MazeConfig.WallBaseY + MazeConfig.MazeBlockCenterOffsetY - (WallIndex / MazeConfig.MazeTotalBlockX) * MazeConfig.MazeBlockSizeY);
            transform.SetPositionAndRotation(pos, rot);
        }
        else
        {//left/right side wall
            int realIndex = WallIndex - MazeConfig.MazeTotalBlockX * (MazeConfig.MazeTotalBlockY + 1);
            Vector3 pos = new Vector3(
                MazeConfig.WallBaseX - MazeConfig.MazeBlockCenterOffsetX +
                (realIndex % (MazeConfig.MazeTotalBlockX + 1)) * MazeConfig.MazeBlockSizeX, 0.5f,
                MazeConfig.WallBaseY - (realIndex / (MazeConfig.MazeTotalBlockX + 1)) * MazeConfig.MazeBlockSizeY);
            transform.position = pos;
        }
    }

}