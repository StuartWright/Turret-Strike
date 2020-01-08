using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePoints : MonoBehaviour
{
    public bool selected;
    public Vector3 MousePos;



    private void OnMouseDown()
    {
        selected = true;
    }

    private void OnMouseUp()
    {
        selected = false;
    }

    void Update()
    {
        if(selected)
        {
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            transform.position = new Vector2(MousePos.x, MousePos.y);
        }
    }
}
