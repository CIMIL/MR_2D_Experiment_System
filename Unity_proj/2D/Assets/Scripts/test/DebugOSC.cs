using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOSC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.value;
    }

    float rand;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test(Vector4 a)
    {
        Debug.Log(a + " - " + rand);
    }
}
