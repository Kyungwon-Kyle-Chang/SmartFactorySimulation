using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour {

    public int lineNum;
    public List<Alarm> alarms;

    private SensorDataManager _sensorDataManager;
    private CameraFocusController _focusController;
    private InfoPanel _infoPanel;
    //======================================================================================
    private void Awake()
    {
        for(int i=0; i<alarms.Count; i++)
        {
            alarms[i].SetAlarmID(i+1, lineNum);
            alarms[i].SetOnClickEvent(OnAlarmClick);
        }

        _sensorDataManager = new SensorDataManager(lineNum);
        _focusController = FindObjectOfType<CameraFocusController>();
        _infoPanel = FindObjectOfType<CanvasManager>().infoPanel;
    }

    private void Start()
    {
        SetAllAlarmStatus(MachineStatus.NORMAL);
        SwitchAllAlarms(true);
        StartCoroutine(OscilateSensorValues());
    }
    //======================================================================================
    public void SetAllAlarmStatus(MachineStatus status)
    {
        alarms.ForEach(x => x.SetStatus(status));
    }

    public void SetAlarmStatus(MachineStatus status, int index)
    {
        if (alarms.Count <= index)
            return;

        alarms[index].SetStatus(status);
    }

    public void SwitchAllAlarms(bool state)
    {
        if(state)
        {
            alarms.ForEach(x => x.StartEmission());
        }
        else
        {
            alarms.ForEach(x => x.FinishEmission());
        }
    }

    public void SwitchAlarm(bool state, int index)
    {
        if(alarms.Count <= index)
            return;

        if(state)
        {
            alarms[index].StartEmission();
        }
        else
        {
            alarms[index].FinishEmission();
        }
    }

    public IEnumerator OnAlarmClick(int machineNum)
    {
        yield return StartCoroutine(_focusController.SetCameraFocused(alarms[machineNum-1].transform));
        StartCoroutine(_infoPanel.OpenPanel(machineNum, _sensorDataManager.GetSensorData));
    }

    public SensorDataManager GetSensorDataManager()
    {
        return _sensorDataManager;
    }
    //======================================================================================
    private IEnumerator OscilateSensorValues()
    {
        int errorMachineNum;
        while(true)
        {
            errorMachineNum = _sensorDataManager.GenerateCurrentValues();
            if (errorMachineNum == 0)
                SetAllAlarmStatus(MachineStatus.NORMAL);
            else
                ChangeAlarmsIfError(errorMachineNum);

            yield return new WaitForSeconds(1);
        }
    }

    private void ChangeAlarmsIfError(int errorMachineNum)
    {
        SensorData[] sensorDataArray = _sensorDataManager.GetSensorData(errorMachineNum);
        for(var i=0;i<sensorDataArray.Length;i++)
        {
            if(sensorDataArray[i].status != MachineStatus.NORMAL)
            {
                SetAlarmStatus(sensorDataArray[i].status, errorMachineNum-1);
                break;
            }
        }
    }
}
