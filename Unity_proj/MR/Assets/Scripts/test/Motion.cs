using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ONE OF CAMERAS
public class Motion : MonoBehaviour
{
    private float headsetVelocity;
    private Vector3 lastHeadsetPosition;
    private Transform headset;

    public delegate void HMDspeed(float timeNow);
    public static event HMDspeed HmdSpeed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        headset = this.gameObject.GetComponent<Transform>();

        headsetVelocity = (headset.position - lastHeadsetPosition).magnitude / Time.deltaTime;
        HmdSpeed(headsetVelocity);
        lastHeadsetPosition = headset.position;
    }

    

}
