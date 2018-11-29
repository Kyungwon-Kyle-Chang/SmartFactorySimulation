using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorSpecification {

    public string machineName;
    public SensorTypes sensorType;
    public string sensorName;
    public string sensorModel;
    public Specification spec1;
    public Specification spec2;
    //======================================================================================
    public SensorSpecification(string machineName,
                               SensorTypes sensorType,
                               string sensorName,
                               string sensorModel,
                               Specification spec1,
                               Specification spec2)
    {
        this.machineName = machineName;
        this.sensorType = sensorType;
        this.sensorName = sensorName;
        this.sensorModel = sensorModel;
        this.spec1 = spec1;
        this.spec2 = spec2;
    }
    
    public class Specification
    {
        public string name;
        public string value;
        public string unit;

        public Specification(string name, string value, string unit)
        {
            this.name = name;
            this.value = value;
            this.unit = unit;
        }
    }
}
