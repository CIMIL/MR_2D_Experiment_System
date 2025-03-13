using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    public Camera cam;

    float startVal = 1;
    float modifiedVal = 10;

    // Start is called before the first frame update
    void Start()
    {
        cam.fieldOfView = startVal;
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = modifiedVal;
    }
}
