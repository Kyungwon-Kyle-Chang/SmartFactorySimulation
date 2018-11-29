using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDataManager {

    private int _lineNum;
    private SensorData[][] _sensorDataArray;
    private ErrorGenerator _errorGenerator;
    //======================================================================================
    public SensorDataManager(int lineNum)
    {
        _lineNum = lineNum;
        Initialize(lineNum);

        _errorGenerator = new ErrorGenerator(lineNum);
    }

    public int GenerateCurrentValues()
    {
        for (var i = 0; i < _sensorDataArray.Length; i++)
        {
            for (var j = 0; j < _sensorDataArray[i].Length; j++)
            {
                GenerateCurrentValue(_sensorDataArray[i][j]);
            }
        }
        return _errorGenerator.InjectError(_sensorDataArray);
    }

    public SensorData[] GetSensorData(int machineNum)
    {
        return _sensorDataArray[machineNum-1];
    }

    public ErrorGenerator GetErrorGenerator()
    {
        return _errorGenerator;
    }
    //======================================================================================
    private void Initialize(int lineNum)
    {
        UnityEngine.Random.InitState(lineNum);
        _sensorDataArray = new SensorData[SensorSpecHolder.specArray.Length][];

        for (var i = 0; i < _sensorDataArray.Length; i++)
        {
            if (SensorSpecHolder.specArray[i] == null)
                continue;

            _sensorDataArray[i] = new SensorData[SensorSpecHolder.specArray[i].Length];
            for (var j = 0; j < _sensorDataArray[i].Length; j++)
            {
                _sensorDataArray[i][j] = new SensorData();
                _sensorDataArray[i][j].spec = SensorSpecHolder.specArray[i][j];
                GenerateRandomValue(_sensorDataArray[i][j]);
            }
        }
    }

    private void GenerateCurrentValue(SensorData sensorData)
    {
        switch (sensorData.spec.sensorType)
        {
            case SensorTypes.TEMPERATURE:
                sensorData.currentValue = Math.Round(Mathf.Lerp(
                    Convert.ToSingle(sensorData.randomValue) - 1, 
                    Convert.ToSingle(sensorData.randomValue) + 1, 
                    Mathf.PingPong(Time.time * 0.3f, 1)), 2).ToString("0.0");
                break;
            case SensorTypes.ATMOSPHERE_TEMPERATURE:
                sensorData.currentValue = Math.Round(Mathf.Lerp(
                    Convert.ToSingle(sensorData.randomValue) - 0.2f,
                    Convert.ToSingle(sensorData.randomValue) + 0.2f,
                    Mathf.PingPong(Time.time * 0.3f, 1)), 2).ToString("0.0");
                break;
            case SensorTypes.HUMIDITY:
                sensorData.currentValue = Math.Round(Mathf.Lerp(
                    Convert.ToSingle(sensorData.randomValue) - 10,
                    Convert.ToSingle(sensorData.randomValue) + 10,
                    Mathf.PingPong(Time.time * 0.3f, 1)), 4).ToString("0.0000");
                break;
            case SensorTypes.ATMOSPHERE_PRESSURE:
                sensorData.currentValue = Math.Round(Mathf.Lerp(
                    Convert.ToSingle(sensorData.randomValue) - 5,
                    Convert.ToSingle(sensorData.randomValue) + 5,
                    Mathf.PingPong(Time.time * 0.3f, 1)), 2).ToString("0.00");
                break;
            case SensorTypes.MOTOR:
                sensorData.currentValue = Mathf.Round(Mathf.Lerp(
                    Convert.ToInt32(sensorData.randomValue) - 5,
                    Convert.ToInt32(sensorData.randomValue) + 5,
                    Mathf.PingPong(Time.time * 0.3f, 1))).ToString();
                break;
            case SensorTypes.EXTRUSION_TEMPERATURE:
                sensorData.currentValue = string.Format($"{sensorData.randomValue}.0");
                break;
            case SensorTypes.PUMP:
                sensorData.currentValue = Mathf.Round(Mathf.Lerp(
                    Convert.ToInt32(sensorData.randomValue) - 5,
                    Convert.ToInt32(sensorData.randomValue) + 5,
                    Mathf.PingPong(Time.time * 0.3f, 1))).ToString();
                break;
            case SensorTypes.COOLER_TEMPERATURE:
                sensorData.currentValue = Math.Round(Mathf.Lerp(
                    Convert.ToSingle(sensorData.randomValue) - 0.5f,
                    Convert.ToSingle(sensorData.randomValue) + 0.5f,
                    Mathf.PingPong(Time.time * 0.3f, 1)), 2).ToString("0.0");
                break;
        }
    }

    private void GenerateRandomValue(SensorData sensorData)
    {
        switch (sensorData.spec.sensorType)
        {
            case SensorTypes.TEMPERATURE:
                sensorData.randomValue = UnityEngine.Random.Range(
                        Convert.ToSingle(sensorData.spec.spec1.value) + 1,
                        Convert.ToSingle(sensorData.spec.spec2.value) - 1
                    ).ToString();
                break;
            case SensorTypes.ATMOSPHERE_TEMPERATURE:
                sensorData.randomValue = "33.5";
                break;
            case SensorTypes.HUMIDITY:
                sensorData.randomValue = UnityEngine.Random.Range(
                        Convert.ToSingle(sensorData.spec.spec1.value) + 10,
                        Convert.ToSingle(sensorData.spec.spec2.value) - 10
                    ).ToString();
                break;
            case SensorTypes.ATMOSPHERE_PRESSURE:
                sensorData.randomValue = UnityEngine.Random.Range(
                        Convert.ToSingle(sensorData.spec.spec1.value) + 5,
                        Convert.ToSingle(sensorData.spec.spec2.value) - 5
                    ).ToString();
                break;
            case SensorTypes.MOTOR:
                if (sensorData.spec.machineName == "배합기")
                    sensorData.randomValue = "1730";
                else if (sensorData.spec.machineName == "압출기")
                    sensorData.randomValue = "1710";
                else if (sensorData.spec.machineName == "절단기")
                    sensorData.randomValue = "1730";
                break;
            case SensorTypes.EXTRUSION_TEMPERATURE:
                sensorData.randomValue = _lineNum != 8 ? (130 + (_lineNum - 1) * 10).ToString() : "190";
                break;
            case SensorTypes.PUMP:
                sensorData.randomValue = "1750";
                break;
            case SensorTypes.COOLER_TEMPERATURE:
                sensorData.randomValue = UnityEngine.Random.Range(
                        Convert.ToSingle(sensorData.spec.spec1.value) + 1,
                        Convert.ToSingle(sensorData.spec.spec2.value) - 16
                    ).ToString();
                break;
        }
    }
}
