using System.Collections;
using UnityEngine;

public class TouchDraggableLimited : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private int draggingFingerId = -1;
    private Transform originalParent;
    private Vector3 originalPosition;

    private Coroutine scaleCoroutine;
    private Vector3 originalScale;

    private Collider2D col;

    private void Awake()
    {
        originalScale = transform.localScale;
        col = GetComponent<Collider2D>();
    }

    private IEnumerator ScaleTo(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float time = 0f;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }
    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    void Update()
    {
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

                        // ぬるっと拡大
                        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
                        scaleCoroutine = StartCoroutine(ScaleTo(originalScale * 1.5f, 0.2f));

                        if (col != null) col.enabled = false; // ← コライダーを無効化
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
                        TryPlaceOrReturn();

                        isDragging = false;
                        draggingFingerId = -1;

                        // ぬるっと元に戻す
                        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
                        scaleCoroutine = StartCoroutine(ScaleTo(originalScale, 0.2f));

                        if (col != null) col.enabled = true; // ← コライダーを再び有効化
                    }
                }
            }
        }
    }

    private Vector3 lastValidPosition;

    void TryPlaceOrReturn()
    {
        SpriteRenderer bounceSR = GetComponent<SpriteRenderer>();
        Vector2 bounceSize = bounceSR.bounds.size;

        Collider2D[] placeHits = Physics2D.OverlapBoxAll(transform.position, bounceSize, 0f);

        foreach (var hit in placeHits)
        {
            if (hit.CompareTag("PlacePoint"))
            {
                // すでに他の BounceObject がいたら無視
                Collider2D[] objectsAtPlace = Physics2D.OverlapBoxAll(hit.transform.position, bounceSize, 0f);
                bool occupied = false;
                foreach (var obj in objectsAtPlace)
                {
                    if (obj.CompareTag("BounceObject") && obj.gameObject != gameObject)
                    {
                        occupied = true;
                        break;
                    }
                }

                if (occupied)
                {
                    transform.position = lastValidPosition;
                }
                else
                {
                    transform.position = hit.transform.position;
                    lastValidPosition = transform.position;
                }

                return;
            }
        }

        // PlacePoint に当たってなかった場合
        transform.position = lastValidPosition;
    }
}