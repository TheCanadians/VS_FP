using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

public class Log : MonoBehaviour
{

    [SerializeField] private String path;
    [SerializeField] private String fileName;

    //[SerializeField] private Transform player;

    private List<String[]> rowData = new List<String[]>();

    private int listIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        string[] rowDataTemp = new String[3];
        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "ID";
        rowDataTemp[2] = "Sickness Score";
        rowData.Add(rowDataTemp);
    }

    public void AddRow(string SS)
    {
        string[] rowDataTemp = new String[3];
        rowDataTemp[0] = Mathf.Floor(Time.realtimeSinceStartup).ToString();
        rowDataTemp[1] = listIndex.ToString();
        rowDataTemp[2] = SS;
        rowData.Add(rowDataTemp);
        listIndex++;
    }

    public void Save()
    {
        string[][] output = new String[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string filePath = GetPath();

        Debug.Log(filePath);

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private string GetPath()
    {
        if (path == "")
        {
#if UNITY_EDITOR
            return Application.dataPath + "/CSV/" + fileName + ".csv";
#else
        return Application.dataPath + "/" + fileName + ".csv";
#endif
        }
        else
        {
            return path + fileName + ".csv";
        }
    }
}
