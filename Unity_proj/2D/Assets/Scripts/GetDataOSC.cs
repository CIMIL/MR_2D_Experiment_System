using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDataOSC : MonoBehaviour
{
    public delegate void GetHeadVals(Vector3 headVal);
    public static event GetHeadVals HeadRotationVals;

    public delegate void GetInstrumentVals(Vector3 instrVal);
    public static event GetInstrumentVals InstrumentVals;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetHeadValues(Vector3 getHeadVals)
    {
        HeadRotationVals(getHeadVals);
    }

    public void GetinstrumentValues(Vector3 getInstrVals)
    {
        InstrumentVals(getInstrVals);
    }
}
