using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBoxManager : MonoBehaviour
{
    public BounceBox[] bounceBoxes;
    public GameObject bounceObjectPrefab;

    public void OnBounceButtonClick()
    {
        foreach (BounceBox box in bounceBoxes)
        {
            if (box.IsEmpty())
            {
                GameObject obj = Instantiate(bounceObjectPrefab, box.GetPosition(), Quaternion.identity);

                ScoreObject score = obj.GetComponent<ScoreObject>();
                if (score != null)
                {
                    if (Random.value > 0.5f)
                    {
                        score.isMultiplying = true;
                        score.scoreValue = 1.1f;
                    }
                    else
                    {
                        score.isMultiplying = false;
                        score.scoreValue = 1f;
                    }
                }

                box.SetOccupied();
                return;
            }
        }

        Debug.Log("Ç∑Ç◊ÇƒÇÃBounceBoxÇ™ñÑÇ‹Ç¡ÇƒÇ¢Ç‹Ç∑ÅB");
    }
}