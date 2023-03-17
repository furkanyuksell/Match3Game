using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Board : MonoBehaviour
{
    public const int MinMatch = 2;
    
    [Header("Set Border Size")]
    public int Rows = 10;
    public int Cols = 10;

    
    [Header("Border Elements")]
    [SerializeField] private Transform _cellParent;
    [SerializeField] public Transform CubesParent;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private SpriteRenderer _border;
    public Cell[,] Cells;
    public readonly Matches Matches = new Matches();
    public static Action OnHint;
    void Start()
    {
        Cells = new Cell[Cols, Rows];
        Matches.SetVisitedCells(Cols, Rows);
        Services.GetFallAndFill.Init(this);
        SetGameBoard();
        CreateCells();
        SetCells();
        CreateCubes();      
        OnHint?.Invoke();
    }
    
    private void SetGameBoard()
    {
        _cellParent.transform.position = new Vector3((-Cols / 2f) + .5f, (-Rows / 2f) + .5f, 0);
        _border.size = new Vector2(Cols + .2f, Rows + .4f);
        _border.sortingOrder = 1;
    }

    private void CreateCells()
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                Cell cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, _cellParent);
                cell.X = x;
                cell.Y = y;
                Cells[x, y] = cell;
            }
        }
    }
    private void SetCells()
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                Cells[x, y].Prepare(this);
            }
        }
    }

    private void CreateCubes()
    {
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Cols; x++)
            {
                var cell = Cells[x, y];
                var cube = Services.GetCube.CreateCubeBase(CubesParent.transform);
                cell.Cube = cube;
                cube.transform.position = cell.transform.position;
            }
        }
    }

    public void CellClicked(Cell cell)
    {
        if (!cell.HasCube()) return;
        if (!cell.IsClickeable) return;

        var cells = Matches.FindMatches(cell, cell.Cube.GetCubeType());
        if (cells.Count < MinMatch) return;

        for(var i = 0; i < cells.Count; i++)
        {
            cell.IsClickeable = false;
            var explodedCube = cells[i].Cube;
            explodedCube.Blast();
        }
        Services.GetFallAndFill.DoFalls();
        OnHint?.Invoke();        
    }

    public Cell FindNeighbour(Cell cell, Direction direction)
    {
        var x = cell.X;
        var y = cell.Y;
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Up:
                y += 1;
                break;
            case Direction.Right:
                x += 1;
                break;
            case Direction.Down:
                y -= 1;
                break;
            case Direction.Left:
                x -= 1;
                break;
            default:
                Debug.Log("Unknown direction");
                break;
        }

        if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

        return Cells[x, y];
    }
}
