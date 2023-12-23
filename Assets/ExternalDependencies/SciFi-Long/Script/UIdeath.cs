using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UIdeath : MonoBehaviour
{
    public static UIdeath instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject deathScreen;
    public TMP_Text deathText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
