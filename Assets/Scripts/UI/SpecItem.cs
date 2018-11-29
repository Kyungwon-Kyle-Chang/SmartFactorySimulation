using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecItem : MonoBehaviour {

    public TextMeshProUGUI type;
    public TextMeshProUGUI sensorName;
    public TextMeshProUGUI sensorModel;
    public RawImage modelImage;
    public TextMeshProUGUI spec1Type;
    public TextMeshProUGUI spec2Type;
    public TextMeshProUGUI spec1Value;
    public TextMeshProUGUI spec2Value;
    public TextMeshProUGUI spec1Unit;
    public TextMeshProUGUI spec2Unit;
    public TextMeshProUGUI currentValue;
    //======================================================================================
    public void Register(SensorData sensorData, RenderTexture[] sensorObjectRTs)
    {
        string type;
        RenderTexture sensorObjectRT = null;

        // sensorObjects :
        // 0 - TEMPERATURE
        // 1 - HUMIDITY
        // 2 - PRESSURE
        // 3 - MOTOR
        // 4 - PUMP
        // 5 - COOLER
        switch (sensorData.spec.sensorType)
        {
            case SensorTypes.TEMPERATURE:
                type = "온도";
                sensorObjectRT = sensorObjectRTs[1];
                break;
            case SensorTypes.ATMOSPHERE_TEMPERATURE:
                type = "대기온도";
                sensorObjectRT = sensorObjectRTs[1];
                break;
            case SensorTypes.HUMIDITY:
                type = "습도";
                sensorObjectRT = sensorObjectRTs[1];
                break;
            case SensorTypes.ATMOSPHERE_PRESSURE:
                type = "기압";
                sensorObjectRT = sensorObjectRTs[2];
                break;
            case SensorTypes.MOTOR:
                type = "모터";
                sensorObjectRT = sensorObjectRTs[3];
                break;
            case SensorTypes.EXTRUSION_TEMPERATURE:
                type = "압출온도";
                sensorObjectRT = sensorObjectRTs[0];
                break;
            case SensorTypes.PUMP:
                type = "펌프";
                sensorObjectRT = sensorObjectRTs[4];
                break;
            case SensorTypes.COOLER_TEMPERATURE:
                type = "냉각온도";
                sensorObjectRT = sensorObjectRTs[5];
                break;
            default:
                type = "-";
                break;
        }

       SetInfo(type, sensorData.spec.sensorName,
               sensorData.spec.sensorModel, sensorObjectRT,
               sensorData.spec.spec1.name, sensorData.spec.spec2.name,
               sensorData.spec.spec1.value, sensorData.spec.spec2.value,
               sensorData.spec.spec1.unit, sensorData.spec.spec2.unit,
               sensorData.currentValue);
    }
    //======================================================================================
    private void SetInfo(string type, string sensorName, string sensorModel, RenderTexture modelImage, 
                        string spec1Type, string spec2Type, string spec1Value, string spec2Value, 
                        string spec1Unit, string spec2Unit, string currentValue)
    {
        this.type.text = type;
        this.sensorName.text = sensorName;
        this.sensorModel.text = sensorModel;
        this.modelImage.texture = modelImage;
        this.spec1Type.text = spec1Type;
        this.spec2Type.text = spec2Type;
        this.spec1Value.text = spec1Value;
        this.spec2Value.text = spec2Value;
        this.spec1Unit.text = spec1Unit;
        this.spec2Unit.text = spec2Unit;
        this.currentValue.text = currentValue;
    }
}