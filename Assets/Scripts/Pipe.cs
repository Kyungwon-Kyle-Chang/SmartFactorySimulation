using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
    
    public void SetColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
