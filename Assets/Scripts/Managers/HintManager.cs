using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HintManager : MonoBehaviour
{
    [SerializeField] private Board _board;
    int hintLevel;
    bool isDeadlock;
    public void FindHints()
    {
        isDeadlock = true;
        for (var y = 0; y < _board.Rows; y++)
        {
            for (var x = 0; x < _board.Cols; x++)
            {
                if (_board.Cells[x, y].Cube == null) continue;

                var cells = _board.Matches.FindMatches(_board.Cells[x, y], _board.Cells[x, y].Cube.GetCubeType());
                if (cells.Count >= 2)
                    isDeadlock = false;

                if (cells.Count <= 4)
                    SetDefault(cells);
                else
                    SetHints(cells);
            }
        }
        if (isDeadlock)
        {
            DOVirtual.DelayedCall(0.7f, ChangeBoard);
        }
    }

    public void ChangeBoard()
    {
        for (var y = 0; y < _board.Rows; y += UnityEngine.Random.Range(1, _board.Rows))
        {
            for (var x = 0; x < _board.Cols; x += UnityEngine.Random.Range(1, _board.Cols))
            {
                _board.Cells[x, y].Cube.transform.DOScale(0, 0.2f)
                    .OnComplete(() => _board.Cells[x, y].Cube.RemoveCube());
                _board.Cells[x, y].Cube = Services.GetCube.CreateCubeBase(_board.CubesParent);
                _board.Cells[x, y].Cube.transform.position = _board.Cells[x, y].transform.position;
                _board.Cells[x, y].Cube.transform.localScale = Vector3.zero;
                _board.Cells[x, y].Cube.transform.DOScale(1, 0.2f);

            }
        }
        FindHints();
    }


    public void SetHints(List<Cell> cells)
    {
        if (cells.Count > 4 && cells.Count <= 7)
            hintLevel = 1;
        else if (cells.Count >= 8 && cells.Count <= 9)
            hintLevel = 2;
        else if (cells.Count >= 10)
            hintLevel = 3;

        for (var i = 0; i < cells.Count; i++)
        {
            cells[i].Cube.SetSpriteAccordingTo(hintLevel);
        }
    }
    public void SetDefault(List<Cell> cells)
    {
        for (var i = 0; i < cells.Count; i++)
        {
            cells[i].Cube.SetSpriteAccordingTo(0);
        }
    }
    void OnEnable()
    {
        Board.OnHint += FindHints;
    }

    void OnDisable()
    {
        Board.OnHint -= FindHints;
    }
}
