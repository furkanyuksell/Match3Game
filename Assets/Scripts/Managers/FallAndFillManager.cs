using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAndFillManager : MonoBehaviour, IProvidable
{
    private Board _board;
    void Awake()
    {
        Services.Register(this);
    }
    public void Init(Board board)
    {
        _board = board;
    }

    public void DoFalls()
    {
        for (var y = 0; y < _board.Rows; y++)
        {
            for (var x = 0; x < _board.Cols; x++)
            {
                var cell = _board.Cells[x, y];
                if (cell.Cube != null && cell.FirstCellBelow != null && cell.FirstCellBelow.Cube == null)
                    cell.Cube.Fall();
                if(y == _board.Rows-1 && cell.Cube == null)
                    DoFills();
            }
        }
    }

    public void DoFills()
    {
        for(var i = 0; i < _board.Cols; i++)
        {
            var cell = _board.Cells[i, _board.Rows - 1];
            if (cell.Cube == null)
            {
                cell.Cube = Services.GetCube.CreateCubeBase(_board.CubesParent);
                var targetCell = cell.GetFallTarget().FirstCellBelow;

                var newCubeOffsetY = 0f;
                if (targetCell != null)
                {
                    if(targetCell.Cube != null)
                    {
                        newCubeOffsetY = targetCell.Cube.transform.position.y + 1;
                    }
                }
                
                var tempCell = cell.transform.position;
                tempCell.y +=3;
                tempCell.y = tempCell.y > newCubeOffsetY ? tempCell.y : newCubeOffsetY;

                if(!cell.HasCube()) continue;
                cell.Cube.transform.position = tempCell;
                cell.Cube.Fall();
                DoFills();
            }
        }
    }
}
