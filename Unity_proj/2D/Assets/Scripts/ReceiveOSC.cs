using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using extOSC.UI;
using extOSC.Examples;
using System;

public class ReceiveOSC : MonoBehaviour
{
    public OSCReceiver Receiver;
    private const string _lookingAt = "/gaze";
    private const string _movement = "/rotation";

    private const string _tracker = "/args";

    private string gazeType = "";
    private string prevGazeType = "";
    public string getGaze = "";

    private string nowTime = "";
    private string whereToSaveCSV = "";

    float timer_1 = 0;
    float timer_2 = 0;
    float timeElapsed = 0;

    public delegate void OnGaze(float timeNow, string gazeType);
    public static event OnGaze Gaze;

    public delegate void OnGazeChange(float gazeType);
    public static event OnGazeChange GazeChange;

    // Start is called before the first frame update
    void Start()
    {
        //Receiver.Bind(_lookingAt, ReceiveGaze);
        //Receiver.Bind(_movement, ReceiveGazeMovement);

        Receiver.Bind(_tracker, Gettracker);
    }

    // Update is called once per frame
    void Update()
    {
        if(gazeType == prevGazeType)
        {
            timer_1 = Time.deltaTime * 1000f; //milliseconds
        }
        if (gazeType != prevGazeType)
        {
            timer_2 = Time.deltaTime * 1000f; //milliseconds

            if (timer_2 < timer_1)
            {
                getGaze = gazeType;
                timeElapsed = (timer_1 - timer_2);
            }
            else
            {
                getGaze = gazeType;
                timeElapsed = (timer_2 - timer_1);
            }

            Gaze(timeElapsed, getGaze);
        }

        prevGazeType = gazeType;
    }

    public void ReceiveGaze(OSCMessage message)
    {
      
        //string[] gazeTypeString = message.ToString().Split(':',')','"');
        //Debug.Log(gazeTypeString[7]);
        //gazeType = gazeTypeString[7];

        if (message.ToString(out var value))
        {
            gazeType = value;
        }

    }

    float a = 0;

    public void ReceiveGazeMovement(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            GazeChange(value);
        }
    }

    public void  Gettracker(OSCMessage message)
    {
         Debug.Log(message);
     
    }


    public struct PointerEventArgs
    {
        public string _gazeDirection;
    }
}
