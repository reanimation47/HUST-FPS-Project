using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBackGround : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
