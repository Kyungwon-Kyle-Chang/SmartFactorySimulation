using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public GameObject ppVolume;
    public InfoPanel infoPanel;

    private int _currentLine = 1;
    private int _currentMachine = 1;
    private int _wholeSpeed = 1;
    private Dictionary<int, GameObject> _lineDict;

    [DllImport("__Internal")]
    private static extern void Initialize();
    //======================================================================================
    private void Awake()
    {
        _lineDict = new Dictionary<int, GameObject>();

        var alarmControllers = GetComponentsInChildren<AlarmController>();
        for (int i = 0; i < alarmControllers.Length; i++)
        {
            _lineDict.Add(alarmControllers[i].lineNum, alarmControllers[i].gameObject);
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Initialize();
#endif
    }
    //======================================================================================
    public void SetCurrentLine(int lineNum) { _currentLine = lineNum; }
    public void SetCurrentMachine(int machineNum) { _currentMachine = machineNum; }

    public void SetMachineStatus(int status)
    {
        _lineDict[_currentLine].GetComponent<AlarmController>().SetAlarmStatus((MachineStatus)status, _currentMachine);
    }

    public void SetPipeLength(float length)
    {
        _lineDict[_currentLine].GetComponent<PipeCreator>().pipeLength = length;
    }

    public void SetPipeSpeed(float speed)
    {
        _lineDict[_currentLine].GetComponent<PipeCreator>().speed = speed;
    }

    public int AdjustWholeLineSpeed(bool increase)
    {
        if (increase)
        {
            if (_wholeSpeed >= 30)
                return _wholeSpeed;
            _wholeSpeed += 1;
        }
        else
        {
            if (_wholeSpeed <= 1)
                return _wholeSpeed;
            _wholeSpeed -= 1;
        }

        foreach (int key in _lineDict.Keys)
        {
            _lineDict[key].GetComponent<PipeCreator>().speed = _wholeSpeed;
        }

        return _wholeSpeed;
    }

    public void SetPipeColor(string hexColor)
    {
        _lineDict[_currentLine].GetComponent<PipeCreator>().pipeColor = ColorConverter.HexToColor(hexColor);
    }

    public void StartLineOperation(int lineNum)
    {
        _lineDict[lineNum].GetComponent<PipeCreator>().StartLineOperation();
    }

    public void FinishLineOperation(int lineNum)
    {
        _lineDict[lineNum].GetComponent<PipeCreator>().FinishLineOperation();
    }

    public void StartLineWithSettings(string query)
    {
        string[] values = query.Split(new char[] { ' ' });

        switch (values.Length)
        {
            case 1:
                StartLineOperation(Convert.ToInt32(values[0]));
                break;
            case 2:
                SetCurrentLine(Convert.ToInt32(values[0]));
                SetPipeLength(Convert.ToSingle(values[1]));
                StartLineOperation(Convert.ToInt32(values[0]));
                break;
            case 3:
                SetCurrentLine(Convert.ToInt32(values[0]));
                SetPipeLength(Convert.ToSingle(values[1]));
                SetPipeSpeed(Convert.ToSingle(values[2]));
                StartLineOperation(Convert.ToInt32(values[0]));
                break;
            case 4:
                SetCurrentLine(Convert.ToInt32(values[0]));
                SetPipeLength(Convert.ToSingle(values[1]));
                SetPipeSpeed(Convert.ToSingle(values[2]));
                SetPipeColor(values[3]);
                StartLineOperation(Convert.ToInt32(values[0]));
                break;
            default:
                break;
        }
    }

    public void SetQuality(int quality)
    {
        if (quality == 0)
        {
            ppVolume.SetActive(false);
            QualitySettings.shadows = ShadowQuality.Disable;
        }
        else if (quality == 1)
        {
            ppVolume.SetActive(true);
            QualitySettings.shadows = ShadowQuality.All;
        }
    }

    public void SetPipeCountLimit(int limit)
    {
        foreach (int key in _lineDict.Keys)
        {
            _lineDict[key].GetComponent<PipeCreator>().pipeCountLimit = limit;
        }
    }

    public void SetErrorGeneratingTime(string query)
    {
        string[] values = query.Split(' ');

        foreach (int key in _lineDict.Keys)
        {
            _lineDict[key].GetComponent<AlarmController>()
                .GetSensorDataManager()
                .GetErrorGenerator()
                .ChangeErrorGeneratingTime(Convert.ToInt32(values[0]), Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
        }
    }

    public void SetPhotoSlidingTime(string query)
    {
        string[] values = query.Split(' ');
        
        infoPanel.ChangePhotoSlidingTime(Convert.ToSingle(values[0]), Convert.ToSingle(values[1]));
    }
}
