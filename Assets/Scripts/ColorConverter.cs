using UnityEngine;
using System.Collections;
using System;

public class ColorConverter
{
    //----------------------------------------------------------------------------------------------------------
    public static Color HexToColor(string hex, int alpha = 255)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = Convert.ToByte(alpha);//assume fully visible unless specified in hex
        return new Color32(r, g, b, a);
    }
    //----------------------------------------------------------------------------------------------------------

}