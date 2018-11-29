using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ErrorGenerator {

    private int _lineNum;
    private int _errorFrequencyInSeconds;
    private int _errorMachineNum;
    private int _errorSensorOrder;
    private Stopwatch _sw;

    private int _errorFreqMin = 18;
    private int _errorFreqMax = 36;
    private int _duration = 60000;
    //======================================================================================
    public ErrorGenerator(int lineNum)
    {
        _lineNum = lineNum;
        UnityEngine.Random.InitState(lineNum);
        _errorFrequencyInSeconds = UnityEngine.Random.Range(_errorFreqMin, _errorFreqMax);

        _sw = new Stopwatch();
        _sw.Start();
    }
    //======================================================================================
    public int InjectError(SensorData[][] sensorDataArray)
    {
        if (_sw.ElapsedMilliseconds < _errorFrequencyInSeconds * 1000)
        {
            SelectErrorSensor(sensorDataArray);
            return 0;
        }

        SetErrorValue(sensorDataArray);
        //UnityEngine.Debug.Log($"Error Generated in Line{_lineNum} Machine{_errorMachineNum} Sensor{_errorSensorOrder}");

        if (_sw.ElapsedMilliseconds > _errorFrequencyInSeconds * 1000 + _duration)
        {
            sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.NORMAL;
            _sw.Restart();
        }
        return _errorMachineNum;
    }

    public void ChangeErrorGeneratingTime(int min, int max, int duration)
    {
        _duration = duration;
        _errorFrequencyInSeconds = UnityEngine.Random.Range(min, max);
    }
    //======================================================================================
    private void SelectErrorSensor(SensorData[][] sensorDataArray)
    {
        _errorMachineNum = UnityEngine.Random.Range(2, sensorDataArray.Length + 1);
        _errorSensorOrder = UnityEngine.Random.Range(0, sensorDataArray[_errorMachineNum - 1].Length);
    }

    private void SetErrorValue(SensorData[][] sensorDataArray)
    {
        float errorFloat;
        int errorInt;
        SensorData target = sensorDataArray[_errorMachineNum - 1][_errorSensorOrder];
        switch (target.spec.sensorType)
        {
            case SensorTypes.TEMPERATURE:
                errorFloat = UnityEngine.Random.Range(0.1f, 2.0f);
                sensorDataArray[_errorMachineNum-1][_errorSensorOrder].currentValue = (Convert.ToSingle(target.spec.spec2.value) + errorFloat).ToString("0.0");

                if (errorFloat > 0.4f)
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.ERROR;
                else
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.WARNING;
                break;
            case SensorTypes.ATMOSPHERE_PRESSURE:
                errorFloat = UnityEngine.Random.Range(0.1f, 10.0f);
                sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].currentValue = (Convert.ToSingle(target.spec.spec2.value) + errorFloat).ToString("0.00");

                if (errorFloat > 2.0f)
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.ERROR;
                else
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.WARNING;
                break;
            case SensorTypes.MOTOR:
                errorInt = UnityEngine.Random.Range(6, 16);
                sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].currentValue = (Convert.ToInt32(target.spec.spec2.value) + errorInt).ToString();

                if (errorInt > 8)
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.ERROR;
                else
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.WARNING;
                break;
            case SensorTypes.PUMP:
                errorInt = UnityEngine.Random.Range(6, 16);
                sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].currentValue = (Convert.ToInt32(target.spec.spec2.value) + errorInt).ToString();

                if (errorInt > 8)
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.ERROR;
                else
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.WARNING;
                break;
            case SensorTypes.COOLER_TEMPERATURE:
                errorFloat = UnityEngine.Random.Range(0.1f, 1.0f);
                sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].currentValue = (Convert.ToSingle(target.spec.spec2.value) + errorFloat).ToString("0.0");

                if (errorFloat > 0.2f)
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.ERROR;
                else
                    sensorDataArray[_errorMachineNum - 1][_errorSensorOrder].status = MachineStatus.WARNING;
                break;
            default:
                break;
        }
    }
}
