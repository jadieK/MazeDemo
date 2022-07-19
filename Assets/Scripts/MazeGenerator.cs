using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public GameObject MazeBlock;
    public GameObject MazeWall;
    public int MazeTotalBlockX = 20;
    public int MazeTotalBlockY = 20;
    
    private const float _mazeBlockSizeX = 1f;
    private const float _mazeBlockSizeY = 1f;
    
    private const float _mazeBlockCenterOffsetX = _mazeBlockSizeX / 2.0f;
    private const float _mazeBlockCenterOffsetY = _mazeBlockSizeY / 2.0f;
    private MazeBlock[] _mazeBlocks;
    private List<int> _mazeBlockIndexList = null;

    private MazeWall[] _mazeWalls;
    private List<int> _mazeWallIndexList = null;

    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateOriginalMaze();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateOriginalMaze()
    {
        _mazeBlocks = new MazeBlock[MazeTotalBlockX * MazeTotalBlockY];
        _mazeBlockIndexList = new List<int>();
        for (int blockIndex = 0; blockIndex < _mazeBlocks.Length; blockIndex++)
        {
            (float, float) newPos = GetBlockPos(blockIndex);
            var obj = Instantiate(MazeBlock, new Vector3(newPos.Item1, 0, newPos.Item2), Quaternion.identity);
            _mazeBlocks[blockIndex] = new MazeBlock(obj);
            _mazeBlockIndexList.Add(blockIndex);
        }

        _mazeWalls = new MazeWall[(MazeTotalBlockX + 1) * (MazeTotalBlockY + 1)];
        _mazeWallIndexList = new List<int>();
        for (int wallIndex = 0; wallIndex < _mazeWalls.Length; wallIndex++)
        {
            
        }

        int randomStartBlockIndex = Random.Range(0, MazeTotalBlockX * MazeTotalBlockY);
        ResolveBlock(randomStartBlockIndex);

    }

    private void ResolveBlock(int blockIndex)
    {
        var block = _mazeBlocks[blockIndex];
        MarkBlock(block);
        var leftBlock = GetNeighbourBlock(blockIndex, Direction.LEFT);
        
    }

    private (float, float) GetBlockPos(int blockIndex)
    {
        float resX = (blockIndex % MazeTotalBlockX) * _mazeBlockSizeX - (MazeTotalBlockX * _mazeBlockSizeX / 2.0f);
        float resY = (int)(blockIndex / MazeTotalBlockY) * _mazeBlockSizeY - (MazeTotalBlockY * _mazeBlockSizeY / 2.0f);
        return (resX, resY);
    }

    private (Vector3, Quaternion) GetWallPosAndRot(int wallIndex)
    {
        if (wallIndex < MazeTotalBlockX)
        {
            //the upside wall
        }
        else if (wallIndex < MazeTotalBlockX * (MazeTotalBlockY + 1))
        {
            //the downside wall
        }
        else if (wallIndex < MazeTotalBlockX * (MazeTotalBlockY + 1) + MazeTotalBlockY)
        {
            //the left side wall
        }
        else
        {
            //the right side wall
        }
        
    }

    private MazeBlock GetNeighbourBlock(int currentIndex, Direction direction)
    {
        int neighbourIndex = -1;
        switch (direction)
        {
            case Direction.UP:
                neighbourIndex = currentIndex - MazeTotalBlockX;
                break;
            case Direction.DOWN:
                neighbourIndex = currentIndex + MazeTotalBlockX;
                break;
            case Direction.LEFT:
                neighbourIndex = currentIndex - 1;
                break;
            case Direction.RIGHT:
                neighbourIndex = currentIndex + 1;
                break;
        }

        if (neighbourIndex < 0 || neighbourIndex >= MazeTotalBlockX * MazeTotalBlockY)
        {
            return null;
        }
        return _mazeBlocks[neighbourIndex];
    }

    private void MarkBlock(MazeBlock block)
    {
        block.IsVisited = true;
        block.GameObjectInstance.GetComponent<Renderer>().material.color = Color.red;
    }



}
