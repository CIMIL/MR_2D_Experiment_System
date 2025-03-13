using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeFrame : MonoBehaviour
{
    public Transform thisFrame;

    public Vector3 scaleVector; //0.8f, 0.5f, 0f
    public Vector3 posVector; //0.8f, 0.5f, 0f

    public bool set = false;

    // Start is called before the first frame update
    void Start()
    {
        //thisFrame.localPosition = new Vector3(0.7f, -0.001062738f, 0.15f);
        thisFrame.localScale = new Vector3(scaleVector.x, scaleVector.y, scaleVector.z);
        if (set)
        {
            thisFrame.localPosition = new Vector3(posVector.x, posVector.y, posVector.z);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
