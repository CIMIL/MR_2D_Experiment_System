using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTest : MonoBehaviour
{
    // Start is called before the first frame update

    private ZEDPlaneDetectionManager zedPlane;

    private ZEDPlaneGameObject getPlane;

    bool HasDetectedFloor;

    void Awake()
    {
      zedPlane = FindObjectOfType<ZEDPlaneDetectionManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPlane = zedPlane.getFloorPlane;

        Debug.Log(getPlane);

        HasDetectedFloor = zedPlane.HasDetectedFloor;

        Debug.Log(HasDetectedFloor);

        //Clean up the list of detected planes.
        if (zedPlane.hitPlaneList.Count > 0)
        {
            Debug.Log("plane");

            for (int i = 0; i < zedPlane.hitPlaneList.Count; i++)
            {
                Debug.Log("plane");
            }
        }
    }
}
