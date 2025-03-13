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
        //paramName.Add("Gaze"); //Column 4
        //paramName.Add("Gaze Time"); //Column 6

    }

    public void CreateFile(string path)
    {
        writer = new StreamWriter(path, true);
        writer.Dispose();
    }


    public void WriteDataToFile(string inputType,
                                float headMovementX,
                                float headMovementY,
                                float headMovementZ,
                                float headDiff,
                                float InstrMovementX,
                                float InstrMovementY,
                                float InstrMovementZ,
                                float instrDfiff,
                                String exportPath)
    {
        exportPath = String.Format("{0}.csv", exportPath);

        if (File.Exists(exportPath))
        {
            using (StreamWriter stream = File.AppendText(exportPath))
            {
                //Modify depending on type to save and in which column
                stream.Write(String.Format("\n"));
                stream.Write(String.Format("{0}{1}", 0 > 0 ? "," : "", inputType));
                stream.Write(String.Format("{0}{1}", 1 > 0 ? "," : "", headMovementX));
                stream.Write(String.Format("{0}{1}", 2 > 0 ? "," : "", headMovementY));
                stream.Write(String.Format("{0}{1}", 3 > 0 ? "," : "", headMovementZ));
                stream.Write(String.Format("{0}{1}", 4 > 0 ? "," : "", headDiff));
                stream.Write(String.Format("{0}{1}", 5 > 0 ? "," : "", InstrMovementX));
                stream.Write(String.Format("{0}{1}", 6 > 0 ? "," : "", InstrMovementY));
                stream.Write(String.Format("{0}{1}", 7 > 0 ? "," : "", InstrMovementZ));
                stream.Write(String.Format("{0}{1}", 8 > 0 ? "," : "", instrDfiff));
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
                stream.Write(String.Format("{0},", paramName[1], headMovementX));
                stream.Write(String.Format("{0},", paramName[2], headMovementY));
                stream.Write(String.Format("{0},", paramName[3], headMovementZ));
                stream.Write(String.Format("{0},", paramName[4], headDiff));
                stream.Write(String.Format("{0},", paramName[5], InstrMovementX));
                stream.Write(String.Format("{0},", paramName[6], InstrMovementY));
                stream.Write(String.Format("{0},", paramName[7], InstrMovementZ));
                stream.Write(String.Format("{0},", paramName[8], instrDfiff));

                stream.Write(String.Format("\n"));

                stream.Write(String.Format("{0}{1}", 0 > 0 ? "," : "", inputType));
                stream.Write(String.Format("{0}{1}", 1 > 0 ? "," : "", headMovementX));
                stream.Write(String.Format("{0}{1}", 2 > 0 ? "," : "", headMovementY));
                stream.Write(String.Format("{0}{1}", 3 > 0 ? "," : "", headMovementZ));
                stream.Write(String.Format("{0}{1}", 4 > 0 ? "," : "", headDiff));
                stream.Write(String.Format("{0}{1}", 5 > 0 ? "," : "", InstrMovementX));
                stream.Write(String.Format("{0}{1}", 6 > 0 ? "," : "", InstrMovementY));
                stream.Write(String.Format("{0}{1}", 7 > 0 ? "," : "", InstrMovementZ));
                stream.Write(String.Format("{0}{1}", 8 > 0 ? "," : "", instrDfiff));

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
