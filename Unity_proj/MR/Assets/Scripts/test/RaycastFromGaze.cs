using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin;
using Valve.VR.Extras;
using Valve.VR;
using UnityEngine.Events;

public class RaycastFromGaze : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;
    public bool selected;

    float timer_1 = 0;
    float timer_2 = 0;
    float timeElapsed = 0;

    #region Delegates
    public delegate void OnGazeIn(float timeNow, string theName);
    public static event OnGazeIn GazeIn;

    public delegate void OnGazeOut(float timeNiw, float timeElapsed, string theName);
    public static event OnGazeOut GazeOut;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("SELECTED: " + selected);
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == this.gameObject.name && selected == false)
        {
            selected = true;
            timer_1 = Time.deltaTime * 1000f;

            GazeIn(timer_1, e.target.name);
            
            //Debug.Log("pointer is inside this object" + e.target.name + " " + timer_1);
            //e.target.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        
        if (e.target.name == this.gameObject.name && selected == true)
        {
            selected = false;
            timer_2 = Time.deltaTime * 1000f;

            //Debug.Log("pointer is outside this object" + e.target.name);

            if (timer_2 < timer_1)
            {
                timeElapsed = (timer_1 - timer_2);
            }
            else
            {
                timeElapsed = (timer_2 - timer_1);
            }

            GazeOut(timer_2, timeElapsed, e.target.name);

            //e.target.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public bool get_selected_value()
    {
        return selected;
    }

    
}
