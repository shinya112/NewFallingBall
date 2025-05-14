using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDraggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private int draggingFingerId = -1;

    void Update()
    {
#if UNITY_EDITOR
        // エディタではマウス操作も可能にする（デバッグ用）
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - mouseWorldPos;
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;
            transform.position = mouseWorldPos + offset;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
#endif
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchWorldPos.z = 0f;

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero);
                    if (hit.collider != null && hit.collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        draggingFingerId = touch.fingerId;
                        offset = transform.position - touchWorldPos;
                    }
                }
                else if (touch.fingerId == draggingFingerId)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        transform.position = touchWorldPos + offset;
                    }
                    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        isDragging = false;
                        draggingFingerId = -1;
                    }
                }
            }
        }
    }
}
