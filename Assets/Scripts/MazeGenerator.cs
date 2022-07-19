using System;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    
    public GameObject MazeBlock;



    // Start is called before the first frame update
    void Start()
    {
        GenerateOriginalMaze();
    }

    private void GenerateMaze()
    {
        for (int blockIndex = 0; blockIndex < MazeConfig.MazeTotalBlock; blockIndex++)
        {
            Instantiate(MazeBlock).GetComponent<MazeBlock>().InitMazeBlock(blockIndex);
        }
    }

    private void Update()
    {
        MazeMgr.Instance().ResolveNextBlock();
    }


    private void GenerateOriginalMaze()
    {
        GenerateMaze();
        MazeMgr.Instance().InitBlockNeighbours();
        MazeMgr.Instance().ResolveNextBlock();
        
    }


}
