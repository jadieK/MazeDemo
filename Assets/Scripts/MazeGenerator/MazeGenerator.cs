using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MazeGenerator : MonoBehaviour
{
    
    public GameObject MazeBlock;
    public GameObject MazeWall;
    [FormerlySerializedAs("Algorithm")] public MazeConfig.GenerateAlgorithmName generateAlgorithm;
    public int Speed;

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

    private int count = 0;

    private IEnumerator DoGenerateMazeStep()
    {
        bool isContinue = true;
        while (isContinue)
        {
            if (_isAutoGenerate && (count++ % Speed == 0))
            {
                isContinue = MazeMgr.Instance().ResolveNextBlock(generateAlgorithm);
            }

            yield return null;
        }
        
    }

    private void GenerateOriginalMaze()
    {
        GenerateMaze();
        MazeMgr.Instance().InitBlockNeighbours();
        MazeMgr.Instance().LinkWallsAndBlocks();
        MazeMgr.Instance().Prepare();
        SolverMgr.Instance().Prepare();
        StartCoroutine(DoGenerateMazeStep());
    }


    public void ResolveNextBlock()
    {
        MazeMgr.Instance().ResolveNextBlock(generateAlgorithm);
    }
}
