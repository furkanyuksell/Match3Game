using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMesh _cellText;

    [HideInInspector] public int X;
    [HideInInspector] public int Y;
    [HideInInspector] public Cell FirstCellBelow;
    [HideInInspector] public bool IsClickeable;
    public List<Cell> Neighbours { get; private set; }

    private Cube _cube;
    public Cube Cube
    {
        get { return _cube; }
        set
        {
            if (_cube == value) return;
            _cube = value;
            if (value != null)
            {
                value.Cell = this;
            }
        }
    }

    public void Prepare(Board board)
    {
        transform.localPosition = new Vector3(X, Y);

        SetLabel();
        SetNeighbours(board);
    }

    private void SetLabel()
    {
        var cellName = X + ":" + Y;
        _cellText.text = cellName;
        gameObject.name = "Cell " + cellName;
        IsClickeable = true;
    }
    private void SetNeighbours(Board board)
    {
        Neighbours = new List<Cell>();
        var up = board.FindNeighbour(this, Direction.Up);
        var down = board.FindNeighbour(this, Direction.Down);
        var left = board.FindNeighbour(this, Direction.Left);
        var right = board.FindNeighbour(this, Direction.Right);

        if (up != null) Neighbours.Add(up);
        if (down != null) Neighbours.Add(down);
        if (left != null) Neighbours.Add(left);
        if (right != null) Neighbours.Add(right);

        if (down != null) FirstCellBelow = down;
    }

    public bool HasCube()
    {
        return Cube != null;
    }

    public Cell GetFallTarget()
    {
        var targetCell = this;
        while (targetCell.FirstCellBelow != null && targetCell.FirstCellBelow.Cube == null)
        {
            targetCell = targetCell.FirstCellBelow;
        }
        return targetCell;
    }
}
