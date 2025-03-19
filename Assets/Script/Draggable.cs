using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset; // 鼠标点击时的偏移量
    private bool isDragging = false; // 是否正在拖拽

    void OnMouseDown()
    {
        // 当鼠标按下时，计算偏移量
        offset = transform.position - GetMouseAsWorldPosition();
        isDragging = true;
    }

    void OnMouseUp()
    {
        // 鼠标释放时停止拖拽
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            // 更新位置
            transform.position = GetMouseAsWorldPosition() + offset;
        }
    }

    // 将鼠标位置转换为世界坐标
    private Vector3 GetMouseAsWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
