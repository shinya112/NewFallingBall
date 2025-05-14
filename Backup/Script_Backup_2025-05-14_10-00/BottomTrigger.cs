using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FallingObject falling = other.GetComponent<FallingObject>();
        if (falling != null)
        {
            GameManager.Instance.AddScore(falling.CurrentScore);
            Destroy(other.gameObject);
        }
    }
}
