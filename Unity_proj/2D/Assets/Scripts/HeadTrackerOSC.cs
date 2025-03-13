using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTrackerOSC : MonoBehaviour
{
    public delegate void GetHeadVals(Vector4 headVal);
    public static event GetHeadVals headRotationVals;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetValues(Vector4 getHeadVals)
    {
        headRotationVals(getHeadVals);
    }


}
