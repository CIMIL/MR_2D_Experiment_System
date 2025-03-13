using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralTrackerPositioning : MonoBehaviour
{
    public GameObject trackerRight;
    public GameObject trackerLeft;
    public GameObject referenceTracker;

    private Vector3 trackerLeft_pos;
    private Vector3 trackerRight_pos;

    public GameObject audioPointCloudCenter;

    private Vector3 virtualTrackerCenter;

    private Vector3 referenceTracker_pos;

    public Vector3 axisOffeset;

    //public float pointCloudOffesetZ;
  
    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject debugObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //debugObject.transform.position = new Vector3(virtualTrackerCenter.x - axisOffeset.x, referenceTracker_pos.y - axisOffeset.y, virtualTrackerCenter.z - axisOffeset.z);
    }

    public void FixCentralVirtualTracker()
    {
        trackerRight_pos = trackerLeft.GetComponent<Transform>().position;
        trackerLeft_pos = trackerLeft.GetComponent<Transform>().position;
        referenceTracker_pos = referenceTracker.GetComponent<Transform>().position;
        //Debug.DrawRay(trackerRight_pos, trackerLeft_pos, Color.red);
        virtualTrackerCenter = CentralVirtualTracker(trackerRight_pos, trackerLeft_pos, referenceTracker_pos, axisOffeset);
        audioPointCloudCenter.GetComponent<Transform>().position = virtualTrackerCenter;
        //audioPointCloudCenter.gameObject.transform.GetChild(0).gameObject.transform.position = new Vector3(0f, 0f, pointCloudOffesetZ); //manipulate pointclud itself
    }
    
    private Vector3 CentralVirtualTracker(Vector3 tracker1, Vector3 tracker2, Vector3 refTracker, Vector3 offset)
    {
 
        float virtualX = tracker1.x + (tracker2.x - tracker1.x) / 2;
        float virtualY = refTracker.y;
        float virtualZ = tracker1.z + (tracker2.z - tracker1.z) / 2;

        //Vector3 virtualPos = new Vector3(virtualX - offset.x, virtualY - offset.y, virtualZ-offset.z);
        Vector3 virtualPos = new Vector3(virtualX - offset.x, virtualY - offset.y, virtualZ);

        return virtualPos;
    }

    /*
    Vector3 directionCtoA = OtherObjectA.position - transform.position; // directionCtoA = positionA - positionC
    Vector3 directionCtoB = OtherObjectB.position - transform.position; // directionCtoB = positionB - positionC
    Vector3 directionAtoB = OtherObjectB.position - OtherObjectA.position; // directionAtoB = target.position - source.position
    Vector3 midpointAtoB = new Vector3((directionCtoA.x + directionCtoB.x) / 2.0f, (directionCtoA.y + directionCtoB.y) / 2.0f, (directionCtoA.z + directionCtoB.z) / 2.0f); // midpoint between A B
    Debug.DrawRay(OtherObjectA.position, directionAtoB, Color.red); // line between A and B
        Debug.DrawRay(transform.position, directionCtoA, Color.green); // line between C and A
        Debug.DrawRay(transform.position, directionCtoB, Color.green); // line between C and B
        Debug.DrawRay(transform.position, midpointAtoB, Color.blue); // line midway between C and B
    */
}
