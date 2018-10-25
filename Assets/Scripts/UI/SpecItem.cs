using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecItem : MonoBehaviour {

    public TextMeshProUGUI type;
    public TextMeshProUGUI sensorName;
    public TextMeshProUGUI sensorModel;
    public TextMeshProUGUI maxValue;
    public TextMeshProUGUI minValue;
    public TextMeshProUGUI maxUnit;
    public TextMeshProUGUI minUnit;
    public TextMeshProUGUI currentValue;

    public void SetInfo(string type, string sensorName, string sensorModel, string maxValue, string minValue, string unit, string currentValue)
    {
        this.type.text = type;
        this.sensorName.text = sensorName;
        this.sensorModel.text = sensorModel;
        this.maxValue.text = maxValue;
        this.minValue.text = minValue;
        this.maxUnit.text = unit;
        this.minUnit.text = unit;
        this.currentValue.text = currentValue;
    }
}