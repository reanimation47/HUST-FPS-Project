using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFxUI : MonoBehaviour
{
    private Image SoundOn;
    private Image SoundOff;

    // Start is called before the first frame update
    void Start()
    {
        /*if ()
        {

        }*/
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ON()
    {
        SoundOff.gameObject.SetActive(true);
        SoundOn.gameObject.SetActive(false);
    }

    public void OFF()
    {
        SoundOn.gameObject.SetActive(true);
        SoundOff.gameObject.SetActive(false);
    }
}
