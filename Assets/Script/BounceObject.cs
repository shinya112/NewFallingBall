using UnityEngine;

public class BounceObject : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private int draggingFingerId = -1;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = transform.position.z;
            transform.position = pos + offset;
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
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);
                pos.z = transform.position.z;

                if (touch.phase == TouchPhase.Began)
                {
                    HandleInput(touch.position, touch.fingerId);
                }
                else if (touch.fingerId == draggingFingerId)
                {
                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        transform.position = pos + offset;
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

    private void HandleInput(Vector3 inputPosition, int fingerId = -1)
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(inputPosition);
        worldPos.z = transform.position.z;

        Collider2D col = Physics2D.OverlapPoint(worldPos);
        if (col != null && col.gameObject == gameObject)
        {
            isDragging = true;
            draggingFingerId = fingerId;
            offset = transform.position - worldPos;
        }
    }
}