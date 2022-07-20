using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    
    public GameObject MazeBlock;
    public GameObject MazeWall;


    private bool _isAutoGenerate = false;
    // Start is called before the first frame update
    void Start()
    {
        GenerateOriginalMaze();
    }

    public void SwitchAutoGenerate()
    {
        _isAutoGenerate = !_isAutoGenerate;
    }
    
    private void GenerateMaze()
    {
        for (int blockIndex = 0; blockIndex < MazeConfig.MazeTotalBlock; blockIndex++)
        {
            Instantiate(MazeBlock).GetComponent<MazeBlock>().InitMazeBlock(blockIndex);
        }

        for (int wallIndex = 0; wallIndex < MazeConfig.MazeTotalWall; wallIndex++)
        {
            Instantiate(MazeWall).GetComponent<MazeWall>().InitMazeWall(wallIndex);
        }
    }
    
    private void Update()
    {
        if (_isAutoGenerate)
        {
            MazeMgr.Instance().ResolveNextBlockPrim();
        }
    }


    private void GenerateOriginalMaze()
    {
        GenerateMaze();
        MazeMgr.Instance().InitBlockNeighbours();
        MazeMgr.Instance().LinkWallsAndBlocks();
        MazeMgr.Instance().Prepare();
    }


    public void ResolveNextBlock()
    {
        MazeMgr.Instance().ResolveNextBlockPrim();
    }
}
