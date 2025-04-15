
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    public bool isDragging = false;
    [SerializeField] float YPosOffset = .01f;
    private void Start()
    {

    }
    void OnMouseDown()
    {
        Vector3 mouseWorldPos = GetMouseAsWorldPosition();
        offset = transform.position - mouseWorldPos;

        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 targetPos = GetMouseAsWorldPosition() + offset;
            targetPos.y = transform.position.y; // ����Y��,Ҳ���Ǹ߶ȣ���Ȼ����ת��ͷ������
            transform.position = targetPos;
            if (Input.GetKey(KeyCode.Mouse1)) 
            {
                Vector3 vec = transform.position;
                vec.y += YPosOffset;
                transform.position = vec;
            }
        }
    }

    private Vector3 GetMouseAsWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane dragPlane = new Plane(Vector3.up, Vector3.zero);

        if (dragPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return transform.position;
    }
}
