using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CSV_data", menuName ="ScriptableObjects/CSVtools", order =1)]
public class CSVdata : ScriptableObject
{
    [SerializeField] public string fileExtension;
    [SerializeField] public string csvPath;
    [SerializeField] public string userCode;

    [SerializeField] public string XR_or_Desktop;
    [SerializeField] public string songType;

    [SerializeField] public string HeadMovementAVG;
    [SerializeField] public string HeadMovementSTD;
    [SerializeField] public string Watching;
    [SerializeField] public string WhoIsWatching;

}
