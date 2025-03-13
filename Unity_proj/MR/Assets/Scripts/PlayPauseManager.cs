using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class PlayPauseManager : MonoBehaviour
{
    public ZEDManager zm1;
    public ZEDManager zm2;
    public ZEDManager zm3;

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    [Header("keys")]

    // 0 - SET AUDIO SOURCES
    public KeyCode positionControllers = KeyCode.Keypad4;

    // 1 - RESET and OPEN CAMERAS
    public KeyCode toggleStart = KeyCode.Keypad0;
    public KeyCode resetSVOs = KeyCode.Keypad0;
    // 2 - UNPAUSE and START
    public KeyCode togglePause1 = KeyCode.Keypad1;
   
    // END - CLOSE SESSION
    public KeyCode toggleClose = KeyCode.Keypad3;

    // HIDE REFERENCE OBJECTS 
    public KeyCode hideObj = KeyCode.A;

    // SHOW REFERENCE OBJECTS
    public KeyCode showObj = KeyCode.S;

    private bool pause1 = true;
    private bool pause2 = true;
    private bool pause3 = true;

    public float a, b;

    public GameObject fixpointcloud_Right;
    public GameObject fixpointcloud_Center;
    public GameObject fixpointcloud_Left;

    //public UnityEvent openCSV;
    //public UnityEvent closeCSV;

    public List<GameObject> markerObjects;

    public GameObject curtain;

    // Pause the SVO at the beginning (if those vals are set to false, it will start sequentially)

    private void Start()
    {
        Color cc = curtain.GetComponent<Renderer>().material.color;
        cc.a = 1;
        curtain.GetComponent<Renderer>().material.color = cc;

        audio1.Pause();
        audio2.Pause();
        audio3.Pause();

        zm1.pauseSVOReading = pause1;
        zm2.pauseSVOReading = pause1;
        zm3.pauseSVOReading = pause3;

    }

    private void Update()

    {

        //SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/qy", a);
        //SendOSC.GetComponent<sendData>().MandaMessaggio1("/StereoEncoder/qy", b);
        // Pause or play the SVO

        if (Input.GetKeyDown(toggleClose))
        {
            //SendOSC.GetComponent<sendData>().MandaMessaggio("t/1/stop", 1);
            //SendOSC.GetComponent<sendData>().MandaMessaggio("b/1/rewind", 1);

        

            zm1.pauseSVOReading = true;
            zm2.pauseSVOReading = true;
            zm3.pauseSVOReading = true;


            audio1.Stop();
            audio2.Stop();
            audio3.Stop();

            //closeCSV.Invoke();

            EditorApplication.isPlaying = false;

            

        }

        if (Input.GetKeyDown(toggleStart))
        {
            //SendOSC.GetComponent<sendData>().MandaMessaggio("t/1/play", 1);
            //SendOSC.GetComponent<sendData>().MandaMessaggio("t/1/stop", 1);
            //SendOSC.GetComponent<sendData>().MandaMessaggio("b/1/rewind", 1);

            

        }

        if (Input.GetKeyDown(togglePause1))
        {
            pause1 = !pause1;
            pause2 = !pause2;
            pause3 = !pause3;

            zm1.pauseSVOReading = pause1;
            zm2.pauseSVOReading = pause1;
            zm3.pauseSVOReading = pause1;
            
            audio1.Play();
            audio2.Play();
            audio3.Play();

            

            //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio2/", 2);
            //SendOSC.GetComponent<sendData>().MandaMessaggio("t/1/play", 1);

            /*
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/qx", 0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/left/qy", 0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/left/qz", 0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/left/qw", 0.5f);

            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/right/qx", -0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/right/qy", -0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/right/qz", -0.5f);
            SendOSC.GetComponent<sendData>().MandaMessaggio("/StereoEncoder/right/qw", -0.5f);
            */

            //openCSV.Invoke();

           

            StartCoroutine(Fading());


        }

        //if (Input.GetKeyDown(togglePause2)) { pause2 = !pause2; zm2.pauseSVOReading = pause2; }

        //if (Input.GetKeyDown(togglePause3)) { pause3 = !pause3; zm3.pauseSVOReading = pause3; }

        // Reset / Synchronize SVO

        if (Input.GetKeyDown(resetSVOs)) { 
            
            zm1.zedCamera.SetSVOPosition(0); 
            zm2.zedCamera.SetSVOPosition(0);
            zm3.zedCamera.SetSVOPosition(0);

            //fixpointcloud_Right.GetComponent<FixTrackersHeight>().FixPositions();
            //fixpointcloud_Left.GetComponent<FixTrackersHeight>().FixPositions();
        }

        if (Input.GetKeyDown(positionControllers))
        {
           
            fixpointcloud_Right.GetComponent<FixTrackersHeight>().FixPositions();
            fixpointcloud_Center.GetComponent<CentralTrackerPositioning>().FixCentralVirtualTracker();
            fixpointcloud_Left.GetComponent<FixTrackersHeight>().FixPositions();
        }

        if (Input.GetKeyDown(hideObj))
        {
            foreach (var mobj in markerObjects)
            {
                mobj.SetActive(false);
            }
        }

        if (Input.GetKeyDown(showObj))
        {
            foreach (var mobj in markerObjects)
            {
                mobj.SetActive(true);
            }
        }



    }

    IEnumerator Fading()
    {
        Color c = curtain.GetComponent<Renderer>().material.color;
        float finishTime = Time.time + 3f;
        while (Time.time < finishTime)
        {
            for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
            {
                c.a = alpha;
                curtain.GetComponent<Renderer>().material.color = c;
                if(alpha < 0.1)
                {
                    curtain.SetActive(false);
                }
                //Debug.Log(c.a);
                //yield return new WaitForSeconds(.5f);
                yield return null;
            }
        }
        
    }

    IEnumerator Close()
    {
        curtain.SetActive(false);
        yield return null;
    }


}
