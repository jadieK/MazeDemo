using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSolver : MonoBehaviour
{
    private bool _isAutoSolve = false;
    
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
        SolverMgr.Instance().ResolveNextMaze(SolverConfig.SolveAlgorithmName.WallFollower);
    }

    public void SwitchAutoSolve()
    {
        _isAutoSolve = !_isAutoSolve;
    }
}
