using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LogCSV : MonoBehaviour
{
    public CSVdata dataCSV;
    public BuilderCSV csv;

    private string whereToSaveCSV = "";

    private string nowTime = "";

    private bool flag = false;

    private Vector3 head_values;
    private Vector3 instr_values;

    public Transform head_obj;
    public Transform instr_obj;

    Vector3 head_vals;
    Vector3 instr_vals;

    Vector3 lastPos_head;
    Vector3 lastPos_instr;

    float head_diff = 0f;
    float instr_diff = 0f;

    private void OnEnable()
    {
        RaycastFromGaze.GazeIn += GazeIntersectionIn;
        RaycastFromGaze.GazeOut += GazeIntersectionOut;

        Motion.HmdSpeed += SpeedOfHeadMovement;

        GetDataOSC.HeadRotationVals += GetHeadVals;
        GetDataOSC.InstrumentVals += GetInstrumentVals;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos_head = head_values;
        lastPos_instr = instr_values;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 instr_lastData = Vector3.zero;
        float instr_AccX = instr_values.y;
        float instr_AccY = instr_values.x;
        float instr_AccZ = instr_values.z;
        instr_lastData = new Vector3(-instr_AccX, instr_AccY, instr_AccZ);
        instr_obj.transform.rotation = Quaternion.Slerp(instr_obj.transform.rotation, Quaternion.Euler(instr_lastData), Time.deltaTime * 1.5f);

        Vector3 head_lastData = Vector3.zero;
        float head_AccX = instr_values.y;
        float head_AccY = instr_values.x;
        float head_AccZ = instr_values.z;
        head_lastData = new Vector3(-head_AccX, head_AccY, head_AccZ);
        instr_obj.transform.rotation = Quaternion.Slerp(instr_obj.transform.rotation, Quaternion.Euler(head_lastData), Time.deltaTime * 1.5f);

        /*
        x = inputValue
        average = (x + previous) / 2
        previous = average
        output = average
        */

        if (lastPos_instr != instr_values)
        {
            instr_vals = instr_values - lastPos_instr;
            instr_vals /= Time.deltaTime;
            lastPos_instr = instr_values;
        }

        float previous_instr = 0;
        float mag_instr = instr_vals.magnitude;
        float average_instr = ((mag_instr + previous_instr) / 2);
        previous_instr = average_instr;
        float output_instr = average_instr;

        float perc_instr = ((output_instr / 900) * 100);

        if ((perc_instr < 150f))
        {
            //INSTR
            //Debug.Log(perc_instr); //Output to Log
            head_diff = perc_instr;
        }

        //head
        if (lastPos_head != head_values)
        {
            head_vals = head_values - lastPos_head;
            head_vals /= Time.deltaTime;
            lastPos_head = head_values;
        }

        float previous_head = 0;
        float mag_head = head_vals.magnitude;
        float average_head = ((mag_head + previous_head) / 2);
        previous_head = average_instr;
        float output_head = average_instr;

        float perc_head = ((output_instr / 900) * 100);

        if ((perc_head < 150f))
        {
            //INSTR
            //Debug.Log(perc_head); //Output to Log
            instr_diff = perc_head;
        }

        //string targetPointing, float targetPointingTimeNow, float targetPointElapsedTime
        //WRITE
        csv.WriteDataToFile(dataCSV.userCode, head_values.x, head_values.y, head_values.z, perc_head, 
            instr_values.x, instr_values.y, instr_values.z, perc_instr, intersectOut_targetName, intersectOut_timeNow, intersectOut_timeElapsed, 0,
            whereToSaveCSV + ".csv");


    }

    public void StartRecordCSV()
    {
        nowTime = DateTime.Now.Hour.ToString() + DateTime.Now.Minute + DateTime.Now.Second.ToString();
        whereToSaveCSV = dataCSV.csvPath + "/" + nowTime + "_" + dataCSV.userCode + "_" + dataCSV.songType + "_" + dataCSV.XR_or_Desktop;

        csv = this.gameObject.GetComponent<BuilderCSV>();
        csv.CreateFile(whereToSaveCSV + dataCSV.fileExtension);
    }

    public void IsRecording()
    {
        flag = true;
    }

    public void CloseRecording()
    {
        flag = false;
        csv.CloseWriteFile();
    }

    float intersectIn_timeNow;
    string intersectIn_targetName;

    float intersectOut_timeNow;
    float intersectOut_timeElapsed;
    string intersectOut_targetName;

    public void GazeIntersectionIn(float timeNow, string targetName)
    {
        intersectIn_timeNow = timeNow;
        intersectIn_targetName = targetName;

        //csv.WriteDataToFile(dataCSV.WhoIsWatching, 0, 0, targetName, timeNow, 0, 0, whereToSaveCSV + ".csv");
        //Debug.Log("1: " + timeNow + " 2: " + targetName);
    }

    public void GazeIntersectionOut(float timeNow, float timeElapsed, string targetName)
    {
        intersectOut_timeNow = timeNow;
        intersectOut_timeElapsed = timeElapsed;
        intersectOut_targetName = targetName;
        //csv.WriteDataToFile(dataCSV.WhoIsWatching, 0, 0, targetName, timeNow, timeElapsed, 0, whereToSaveCSV + ".csv");
        //Debug.Log("1: " + timeNow + " 2: " + timeElapsed + " 3: " + targetName);
    }

    public void SpeedOfHeadMovement(float currSpeed)
    {
        //csv.WriteDataToFile(dataCSV.WhoIsWatching, (int)(currSpeed*1000f), 0,"",0,0,0, whereToSaveCSV + ".csv");
    }


    public void GetHeadVals(Vector3 headVals)
    {
        head_values = headVals;
        //csv.WriteDataToFile(dataCSV.userCode, headVals.x, headVals.y, headVals.z, headVals.x, , whereToSaveCSV + ".csv");
    }
    public void GetInstrumentVals(Vector3 instrVals)
    {
        instr_values = instrVals;
        //csv.WriteDataToFile(dataCSV.userCode, instrVals.x, instrVals.y, instrVals.z, instrVals.x, whereToSaveCSV + ".csv");
    }

    private void OnDisable()
    {
        RaycastFromGaze.GazeIn -= GazeIntersectionIn;
        RaycastFromGaze.GazeOut -= GazeIntersectionOut;
        
        Motion.HmdSpeed -= SpeedOfHeadMovement;

        GetDataOSC.HeadRotationVals -= GetHeadVals;
        GetDataOSC.InstrumentVals -= GetInstrumentVals;

    }
}
