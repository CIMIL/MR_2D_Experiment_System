using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Test1 : MonoBehaviour
{
    private float timeNow = 0;
    private float elapsedTime = 0;
    private string intersected = "";

    public UnityEvent m_MyEvent;
    public UnityEvent m_MyEvent1;

    private void OnEnable()
    {
        RaycastFromGaze.GazeIn += GazeIntersection;
        RaycastFromGaze.GazeOut += GazeIntersectionOut;
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_MyEvent.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_MyEvent1.Invoke();
        }
    }

    public void GazeIntersection(float timeNow, string targetName)
    {
        Debug.Log("1: " + timeNow + " 2: " + targetName);
    }

    public void GazeIntersectionOut(float timeNow, float timeElapsed, string targetName)
    {
        Debug.Log("1: " + timeNow + " 2: " + timeElapsed + " 3: " + targetName);
    }

    private void OnDisable()
    {
        RaycastFromGaze.GazeIn -= GazeIntersection;
        RaycastFromGaze.GazeOut -= GazeIntersectionOut;
    }
}
