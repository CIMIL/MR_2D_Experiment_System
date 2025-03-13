using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTrackersHeight : MonoBehaviour
{
    public Transform myTracker;
    private float x, y, z;
    public float offsetHorizontal, offsetVertical, offsetDepth;

    public float ax,ay,az,aw;
    //private Transform pointCloudtransform;

    public ZEDControllerTracker controllerTracker;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(myTracker.position.x, myTracker.position.y + offsetVertical, myTracker.position.z);
        //this.transform.GetComponentInChildren<Transform>().position = new Vector3(this.transform.GetComponentInChildren<Transform>().position.x,
        //                                                                          this.transform.GetComponentInChildren<Transform>().position.y - offsetVertical,
        //                                                                          this.transform.GetComponentInChildren<Transform>().position.z);

        //this.transform.position = new Vector3(myTracker.position.x, myTracker.position.y + offsetVertical, myTracker.position.z);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.transform.rotation = new Quaternion(myTracker.rotation.x - ax,
                                                 myTracker.rotation.y - ay,
                                                 myTracker.rotation.z - az,
                                                 myTracker.rotation.w - aw);
        }
        

    }

    public void FixPositions()
    {
        this.transform.position = new Vector3(myTracker.position.x + offsetHorizontal, myTracker.position.y + offsetVertical, myTracker.position.z + offsetDepth);
        

        
        //this.transform.rotation = new Quaternion(myTracker.rotation.x, myTracker.rotation.y, myTracker.rotation.z, myTracker.rotation.w);

        /*
        this.transform.GetComponentInChildren<Transform>().position = new Vector3(this.transform.GetComponentInChildren<Transform>().position.x,
                                                                                  this.transform.GetComponentInChildren<Transform>().position.y - offsetVertical,
                                                                                  this.transform.GetComponentInChildren<Transform>().position.z);
        
        this.transform.GetComponentInChildren<Transform>().rotation = new Quaternion(this.transform.GetComponentInChildren<Transform>().rotation.x,
                                                                                  this.transform.GetComponentInChildren<Transform>().rotation.y,
                                                                                  this.transform.GetComponentInChildren<Transform>().rotation.z,
                                                                                  this.transform.GetComponentInChildren<Transform>().rotation.w);
       */
    }
}
