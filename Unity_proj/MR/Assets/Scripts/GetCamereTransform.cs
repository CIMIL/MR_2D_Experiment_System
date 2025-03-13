using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCamereTransform : MonoBehaviour
{
    public GameObject cameraReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = cameraReference.transform.position;
        this.transform.rotation = cameraReference.transform.rotation;
    }
}
