using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FallAnimation : MonoBehaviour
{
    public Cube Cube;
    public void StartFall(Cell targetCell)
    {
        Cube.Cell.IsClickeable = false;
        Cube.Cell = targetCell;
        Cube.transform.DOMove(targetCell.transform.position, 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
             {
                 Cube.Cell.IsClickeable = true;
             });
    }
}
