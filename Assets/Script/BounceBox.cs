using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBox : MonoBehaviour
{
    private bool hasObject = false;

    public bool IsEmpty()
    {
        return !hasObject;
    }

    public void SetOccupied()
    {
        hasObject = true;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}