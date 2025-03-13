using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PausePlay : MonoBehaviour
{
    public ZEDManager zm1;

    public ZEDManager zm2;

    public ZEDManager zm3;

    //public ZEDManager zm3;

    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

   // public GameObject SendOSC;


    [Header("keys")]

    public KeyCode togglePause1 = KeyCode.Keypad1;
    public KeyCode toggleStart = KeyCode.Keypad0;
    public KeyCode toggleClose = KeyCode.Keypad3;

    //public KeyCode togglePause2 = KeyCode.Keypad2;

    //public KeyCode togglePause3 = KeyCode.Keypad3;

    public KeyCode resetSVOs = KeyCode.Keypad0;



    private bool pause1 = true;

    private bool pause2 = true;

    private bool pause3 = true;

    public float a, b;

    // Pause the SVO at the beginning (if those vals are set to false, it will start sequentially)

    private void Start()

    {

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
            zm1.pauseSVOReading = pause1;
            //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio1/", 3);
            audio1.Play();

            pause2 = !pause2;
            zm2.pauseSVOReading = pause1;
            audio2.Play();
            //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio2/", 2);

            pause3 = !pause3;
            zm3.pauseSVOReading = pause3;
            //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio1/", 3);
            audio3.Play();
         

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

        }

        //if (Input.GetKeyDown(togglePause2)) { pause2 = !pause2; zm2.pauseSVOReading = pause2; }

        //if (Input.GetKeyDown(togglePause3)) { pause3 = !pause3; zm3.pauseSVOReading = pause3; }

        // Reset / Synchronize SVO

        if (Input.GetKeyDown(resetSVOs)) { zm1.zedCamera.SetSVOPosition(0); zm2.zedCamera.SetSVOPosition(0); zm3.zedCamera.SetSVOPosition(0); }

    }
    public void ResetCam()
    {
        zm1.zedCamera.SetSVOPosition(0); zm2.zedCamera.SetSVOPosition(0); zm3.zedCamera.SetSVOPosition(0);
    }
    public void StartPlay()
    {
        pause1 = !pause1;
        zm1.pauseSVOReading = pause1;
        //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio1/", 3);
        audio1.Play();

        pause2 = !pause2;
        zm2.pauseSVOReading = pause1;
        audio2.Play();
        //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio2/", 2);

        pause3 = !pause3;
        zm3.pauseSVOReading = pause3;
        //SendOSC.GetComponent<sendData>().MandaMessaggio("/audio1/", 3);
        audio3.Play();
    }
}