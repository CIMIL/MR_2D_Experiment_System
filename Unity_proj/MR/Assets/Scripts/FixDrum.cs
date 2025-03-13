using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDrum : MonoBehaviour
{
    public GameObject drum;

    // Start is called before the first frame update
    void Start()
    {
        drum.GetComponent<Transform>().position = new Vector3(0f, -0.24f, -2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
