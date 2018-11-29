using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SensorSpecHolder {

    public static string[] machineName =
    {
        "크레인",
        "배합기",
        "압출기",
        "냉각기",
        "절단기"
    };

    public static SensorSpecification[][] specArray =
    {
        new SensorSpecification[] { },
        //----------------------------------------------------------------------
        new SensorSpecification[]
        {
             new SensorSpecification
             (
                machineName[1],
                SensorTypes.TEMPERATURE,
                "mix_temp_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "16.1", "℃"),
                new SensorSpecification.Specification("Max", "38.3", "℃")
             ),
             new SensorSpecification
             (
                machineName[1],
                SensorTypes.HUMIDITY,
                "mix_humi_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "14.0195", "%"),
                new SensorSpecification.Specification("Max", "99.8197", "%")
             ),
             new SensorSpecification
             (
                machineName[1],
                SensorTypes.ATMOSPHERE_PRESSURE,
                "mix_atmp_01",
                "BMP180",
                new SensorSpecification.Specification("Min", "955.01", "hPa"),
                new SensorSpecification.Specification("Max", "1007.48", "hPa")
             ),
             new SensorSpecification
             (
                machineName[1],
                SensorTypes.MOTOR,
                "mix_moto_01",
                "SW420",
                new SensorSpecification.Specification("Freq", "60", "Hz"),
                new SensorSpecification.Specification("Rpm", "1730", "RPM")
             )
        },
        //----------------------------------------------------------------------
        new SensorSpecification[]
        {
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.EXTRUSION_TEMPERATURE,
                "ext_util_01",
                "",
                new SensorSpecification.Specification("Min", "130", "℃"),
                new SensorSpecification.Specification("Max", "190", "℃")
            ),
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.ATMOSPHERE_TEMPERATURE,
                "ext_temp_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "16.1", "℃"),
                new SensorSpecification.Specification("Max", "38.3", "℃")
            ),
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.HUMIDITY,
                "ext_humi_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "14.0195", "%"),
                new SensorSpecification.Specification("Max", "99.8197", "%")
            ),
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.ATMOSPHERE_PRESSURE,
                "ext_atmp_01",
                "BMP180",
                new SensorSpecification.Specification("Min", "955.01", "hPa"),
                new SensorSpecification.Specification("Max", "1007.48", "hPa")
            ),
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.MOTOR,
                "ext_moto_01",
                "SW420",
                new SensorSpecification.Specification("Freq", "60", "Hz"),
                new SensorSpecification.Specification("Rpm", "1710", "RPM")
            ),
            new SensorSpecification
            (
                machineName[2],
                SensorTypes.PUMP,
                "ext_pump_01",
                "SW420",
                new SensorSpecification.Specification("Freq", "60", "Hz"),
                new SensorSpecification.Specification("Rpm", "1750", "RPM")
            )
        },
        //----------------------------------------------------------------------
        new SensorSpecification[]
        {
            new SensorSpecification
            (
                machineName[3],
                SensorTypes.ATMOSPHERE_TEMPERATURE,
                "con_temp_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "16.1", "℃"),
                new SensorSpecification.Specification("Max", "38.3", "℃")
            ),
            new SensorSpecification
            (
                machineName[3],
                SensorTypes.HUMIDITY,
                "con_humi_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "14.0195", "%"),
                new SensorSpecification.Specification("Max", "99.8197", "%")
            ),
            new SensorSpecification
            (
                machineName[3],
                SensorTypes.ATMOSPHERE_PRESSURE,
                "con_atmp_01",
                "BMP180",
                new SensorSpecification.Specification("Min", "955.01", "hPa"),
                new SensorSpecification.Specification("Max", "1007.48", "hPa")
            ),
            new SensorSpecification
            (
                machineName[3],
                SensorTypes.COOLER_TEMPERATURE,
                "con_cool_01",
                "DS18B20",
                new SensorSpecification.Specification("Min", "12.062", "℃"),
                new SensorSpecification.Specification("Max", "37.875", "℃")
            ),
            new SensorSpecification
            (
                machineName[3],
                SensorTypes.PUMP,
                "con_pump_01",
                "SW420",
                new SensorSpecification.Specification("Freq", "60", "Hz"),
                new SensorSpecification.Specification("Rpm", "1750", "RPM")
            )
        },
        //----------------------------------------------------------------------
        new SensorSpecification[]
        {
            new SensorSpecification
            (
                machineName[4],
                SensorTypes.ATMOSPHERE_TEMPERATURE,
                "cut_temp_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "16.1", "℃"),
                new SensorSpecification.Specification("Max", "38.3", "℃")
            ),
            new SensorSpecification
            (
                machineName[4],
                SensorTypes.HUMIDITY,
                "cut_humi_01",
                "HTU21D",
                new SensorSpecification.Specification("Min", "14.0195", "%"),
                new SensorSpecification.Specification("Max", "99.8197", "%")
            ),
            new SensorSpecification
            (
                machineName[4],
                SensorTypes.ATMOSPHERE_PRESSURE,
                "cut_atmp_01",
                "BMP180",
                new SensorSpecification.Specification("Min", "955.01", "hPa"),
                new SensorSpecification.Specification("Max", "1007.48", "hPa")
            ),
            new SensorSpecification
            (
                machineName[4],
                SensorTypes.MOTOR,
                "cut_moto_01",
                "SW420",
                new SensorSpecification.Specification("Freq", "60", "Hz"),
                new SensorSpecification.Specification("Rpm", "1730", "RPM")
            )
        }
    };
}
