using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MazeSolver : MonoBehaviour
{
    private bool _isAutoSolve = false;

    public SolverConfig.SolveAlgorithmName solveAlgorithmName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAutoSolve)
        {
            DoSolveMazeStep();
        }
    }

    public void DoSolveMazeStep()
    {
        SolverMgr.Instance().ResolveNextMaze(solveAlgorithmName);
    }

    public void SwitchAutoSolve()
    {
        _isAutoSolve = !_isAutoSolve;
    }
}
