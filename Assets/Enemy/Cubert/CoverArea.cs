using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoverArea : MonoBehaviour
{
    private Cover[] covers;

    void Awake() {
        covers = GetComponentsInChildren<Cover>();
    }

    public Cover GetClosestCover(Vector3 agentLocation)
    {
        // Calculate distances to covers
        Dictionary<Cover, float> coverDistances = new Dictionary<Cover, float>();
        foreach (var cover in covers)
        {
            float distance = Vector3.Distance(agentLocation, cover.transform.position);
            coverDistances.Add(cover, distance);
        }

        // Sort covers by distance
        var closestCover = coverDistances.OrderBy(x => x.Value).FirstOrDefault();

        // Return the closest cover (or null if no covers exist)
        return closestCover.Key;
    }
}
