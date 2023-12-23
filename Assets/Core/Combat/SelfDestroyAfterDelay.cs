using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyAfterDelay : MonoBehaviour
{
    public float delayInSeconds = 4;
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(delayInSeconds);
        Destroy(gameObject);
    }
}
