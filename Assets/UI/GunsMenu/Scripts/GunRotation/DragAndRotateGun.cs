using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndRotateGun : MonoBehaviour
{
    public bool isActive = false;
    Color activeColor = new Color();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            activeColor = Color.red;
            if (Input.touchCount == 1)
            {
                Debug.LogWarning("aaa");
                Touch screenTouch = Input.GetTouch(0);
                if (screenTouch.phase == TouchPhase.Moved)
                {
                    transform.Rotate(0f, screenTouch.deltaPosition.x, 0f);
                }
                if (screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                }
            }
        }
        else
        {
            activeColor = Color.white;
        }
        //GetComponent<MeshRenderer>().material.color = activeColor;
    }
}
