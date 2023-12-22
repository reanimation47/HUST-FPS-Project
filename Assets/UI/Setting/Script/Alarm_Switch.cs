using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm_Switch : MonoBehaviour
{
    public Image On;
    public Image Off;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ON()
    {
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
    }

    public void OFF()
    {
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
    }
}
