using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public KeyCode pipeSpeedUp;
    public KeyCode pipeSpeedDown;
    public KeyCode setQualityHigh;
    public KeyCode setQualityLow;

    private FactoryManager _factoryManager;
    private SpeedText _speedText;

    private void Awake()
    {
        _factoryManager = FindObjectOfType<FactoryManager>();
        _speedText = FindObjectOfType<CanvasManager>().speedText;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(pipeSpeedUp))
        {
            _speedText.SetSpeedText(_factoryManager.AdjustWholeLineSpeed(true));
        }
        else if (Input.GetKeyDown(pipeSpeedDown))
        {
            _speedText.SetSpeedText(_factoryManager.AdjustWholeLineSpeed(false));
        }
        else if(Input.GetKeyDown(setQualityHigh))
        {
            _factoryManager.SetQuality(1);
        }
        else if(Input.GetKeyDown(setQualityLow))
        {
            _factoryManager.SetQuality(0);
        }
	}
}
