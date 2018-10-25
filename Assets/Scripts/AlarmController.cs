using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour {

    public int lineNum;
    public List<Alarm> alarms;
    //======================================================================================
    private void Awake()
    {
        for(int i=0; i<alarms.Count; i++)
        {
            alarms[i].SetAlarmID(i+1, lineNum);
        }
    }

    private void Start()
    {
        SetAllAlarmStatus(MachineStatus.NORMAL);
        SwitchAllAlarms(true);
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
}
