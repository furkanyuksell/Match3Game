using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Board _board;
    private void Update()
    {
        InputListener();
    }
    private void InputListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown(Input.mousePosition);
        }
    }

    private void OnMouseDown(Vector3 position)
    {
        Collider2D cellCollider = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(position), LayerMask.GetMask("Cell"));
        if( cellCollider != null)
        {
            _board.CellClicked(cellCollider.GetComponent<Cell>());
        }
        
    }


}
