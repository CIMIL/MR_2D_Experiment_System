using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class BuilderCSV : MonoBehaviour
{
    private static List<String> paramName;

    private StreamWriter writer;

    static BuilderCSV()
    {
        paramName = new List<string>();

        paramName.Add("Data type"); //Column 1
        paramName.Add("Head Rotation_x"); //Column 2
        paramName.Add("Head Rotation_y");
        paramName.Add("Head Rotation_z");
        paramName.Add("Head Rotation Difference");
        paramName.Add("Instr Rotation_x"); //Column 2
        paramName.Add("Instr Rotation_y");
        paramName.Add("Instr Rotation_z");
        paramName.Add("Instr Rotation Difference");

        paramName.Add("Pointing Who"); //Column 4
        paramName.Add("Pointing Time Now"); //Column 4
        paramName.Add("Pointing Elapsed Time"); //Column 5
        paramName.Add("other"); //Column 6

    }

    public void CreateFile(string path)
    {
        writer = new StreamWriter(path, true);
        writer.Dispose();
    }

    
    public void WriteDataToFile(string inputType,
                                float headRotx, float headRoty, float headRotz, float headRotdiff,
                                float instrRotx, float instrRoty, float instrRotz, float instrRotdiff,
                                string targetPointing, float targetPointingTimeNow, float targetPointElapsedTime,
                                float otherVal, String exportPath)
    {     
        exportPath = String.Format("{0}.csv", exportPath);

        if (File.Exists(exportPath))
        {
            using (StreamWriter stream = File.AppendText(exportPath))
            {
                //Modify depending on type to save and in which column
                stream.Write(String.Format("\n"));
                stream.Write(String.Format("{0}{1}", 0 > 0 ? "," : "", inputType));
                stream.Write(String.Format("{0}{1}", 1 > 0 ? "," : "", headRotx));
                stream.Write(String.Format("{0}{1}", 2 > 0 ? "," : "", headRoty));
                stream.Write(String.Format("{0}{1}", 3 > 0 ? "," : "", headRotz));
                stream.Write(String.Format("{0}{1}", 4 > 0 ? "," : "", headRotdiff));
                stream.Write(String.Format("{0}{1}", 5 > 0 ? "," : "", instrRotx));
                stream.Write(String.Format("{0}{1}", 6 > 0 ? "," : "", instrRoty));
                stream.Write(String.Format("{0}{1}", 7 > 0 ? "," : "", instrRotz));
                stream.Write(String.Format("{0}{1}", 8 > 0 ? "," : "", instrRotdiff));
                stream.Write(String.Format("{0}{1}", 9 > 0 ? "," : "", targetPointing));
                stream.Write(String.Format("{0}{1}", 10 > 0 ? "," : "", targetPointingTimeNow));
                stream.Write(String.Format("{0}{1}", 11 > 0 ? "," : "", targetPointElapsedTime));
                stream.Write(String.Format("{0}{1}", 12 > 0 ? "," : "", otherVal));

                stream.Close();
            }
        }
        else
        {
            String directorPath = new FileInfo(exportPath).Directory.FullName;
            if (!Directory.Exists(directorPath))
            {
                Directory.CreateDirectory(directorPath);
            }
            using (StreamWriter stream = File.CreateText(exportPath))
            {
                //Modify depending on type to save and in which column
                stream.Write(String.Format("{0},", paramName[0], inputType));
                stream.Write(String.Format("{0},", paramName[1], headRotx));
                stream.Write(String.Format("{0},", paramName[2], headRoty));
                stream.Write(String.Format("{0},", paramName[3], headRotz));
                stream.Write(String.Format("{0},", paramName[4], headRotdiff));
                stream.Write(String.Format("{0},", paramName[5], instrRotx));
                stream.Write(String.Format("{0},", paramName[6], instrRoty));
                stream.Write(String.Format("{0},", paramName[7], instrRotz));
                stream.Write(String.Format("{0},", paramName[8], instrRotdiff));
                stream.Write(String.Format("{0},", paramName[9], targetPointing));
                stream.Write(String.Format("{0},", paramName[10], targetPointingTimeNow));
                stream.Write(String.Format("{0},", paramName[11], targetPointElapsedTime));
                stream.Write(String.Format("{0},", paramName[12], otherVal));

                stream.Write(String.Format("\n"));

                stream.Write(String.Format("{0}{1}", 0 > 0 ? "," : "", inputType));
                stream.Write(String.Format("{0}{1}", 1 > 0 ? "," : "", headRotx));
                stream.Write(String.Format("{0}{1}", 2 > 0 ? "," : "", headRoty));
                stream.Write(String.Format("{0}{1}", 3 > 0 ? "," : "", headRotz));
                stream.Write(String.Format("{0}{1}", 4 > 0 ? "," : "", headRotdiff));
                stream.Write(String.Format("{0}{1}", 5 > 0 ? "," : "", instrRotx));
                stream.Write(String.Format("{0}{1}", 6 > 0 ? "," : "", instrRoty));
                stream.Write(String.Format("{0}{1}", 7 > 0 ? "," : "", instrRotz));
                stream.Write(String.Format("{0}{1}", 8 > 0 ? "," : "", instrRotdiff));
                stream.Write(String.Format("{0}{1}", 9 > 0 ? "," : "", targetPointing));
                stream.Write(String.Format("{0}{1}", 10 > 0 ? "," : "", targetPointingTimeNow));
                stream.Write(String.Format("{0}{1}", 11 > 0 ? "," : "", targetPointElapsedTime));
                stream.Write(String.Format("{0}{1}", 12 > 0 ? "," : "", otherVal));

                stream.Close();
            }
        }
    }

    public void CloseWriteFile()
    {
        writer.Dispose();
        writer.Close();
    }

}
