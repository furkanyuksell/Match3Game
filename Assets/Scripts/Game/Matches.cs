using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches
{
    private bool[,] _visitedCells;

    public void SetVisitedCells(int rows, int cols) => _visitedCells = new bool[rows, cols];

    public List<Cell> FindMatches(Cell cell, CubeType cubeType)
    {
        var resultCells = new List<Cell>();
        ClearVisitedCells();
        FindMatches(cell, cubeType, resultCells);

        return resultCells;
    }

    private void FindMatches(Cell cell, CubeType cubeType, List<Cell> resultCells)
    {
        if (cell == null) return;

        var x = cell.X;
        var y = cell.Y;
        if (_visitedCells[x, y]) return;

        if (cell.HasCube() && cell.Cube.GetCubeType() == cubeType && cell.Cube.GetCubeType() != CubeType.None)
        {
            _visitedCells[x, y] = true;
            resultCells.Add(cell);

            var neighbours = cell.Neighbours;
            if (neighbours.Count == 0) return;

            for (var i = 0; i < neighbours.Count; i++)
            {
                FindMatches(neighbours[i], cubeType, resultCells);
            }
        }

    }

    private void ClearVisitedCells()
    {
        for (var x = 0; x < _visitedCells.GetLength(0); x++)
        {
            for (var y = 0; y < _visitedCells.GetLength(1); y++)
            {
                _visitedCells[x, y] = false;
            }
        }
    }
}
