using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BounceObjectDisplay : MonoBehaviour
{
    void Start()
    {
        var data = GetComponent<BounceObjectData>();
        var label = GetComponentInChildren<TextMeshPro>();
        if (data != null && label != null)
        {
            label.text = data.GetDisplayText();
        }
    }
}