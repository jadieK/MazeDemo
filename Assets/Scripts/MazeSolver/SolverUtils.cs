using UnityEngine;

public static class SolverUtils
{
    public static void MarkSolvingBlock(MazeBlock block)
    {
        block.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        block.IsSolved = true;
    }

    public static void MarkSolvedBlock(MazeBlock block)
    {
        block.gameObject.GetComponent<Renderer>().material.color = Color.green;
        block.IsSolved = true;
    }
}
