using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Alarm : MonoBehaviour, IPointerClickHandler
{
    public GameObject top;
    public GameObject mid;
    public GameObject bot;

    private int _machineNum;
    private int _lineNum;
    private MachineStatus _status;
    private bool _isWorking;

    private CameraFocusController _focusController;
    private InfoPanel _infoPanel;
    private Material[] _cachedMaterials;
    private Coroutine _coroutine;
    private Color _lightOff, _lightOn;
    private Color _baseColor;
    
    [DllImport("__Internal")]
    private static extern void MachineStatusClickEvent(int machineNum, int lineNum, int status);
    //======================================================================================
    private void Awake()
    {
        _focusController = FindObjectOfType<CameraFocusController>();
        _infoPanel = FindObjectOfType<CanvasManager>().infoPanel;

        _cachedMaterials = new Material[3];
        _cachedMaterials[0] = top.GetComponent<MeshRenderer>().material;
        _cachedMaterials[1] = mid.GetComponent<MeshRenderer>().material;
        _cachedMaterials[2] = bot.GetComponent<MeshRenderer>().material;

        _lightOff = new Color(0.23f, 0.23f, 0.23f);
        _lightOn = new Color(0.77f, 0.77f, 0.77f);
    }
    //======================================================================================
    public void SetAlarmID(int machineNum, int lineNum)
    {
        _machineNum = machineNum;
        _lineNum = lineNum;
    }

    public void StartEmission()
    {
        _isWorking = true;
        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(EmissionCoroutine());
        }
    }

    public void FinishEmission()
    {
        _isWorking = false;
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    
    public void SetStatus(MachineStatus status)
    {
        _status = status;

        switch (status)
        {
            case MachineStatus.NORMAL:
                //_cachedMaterial.color = Color.green;
                _baseColor = new Color(0, 1, 0.254f);
                break;
            case MachineStatus.WARNING:
                //_cachedMaterial.color = new Color(1, 0.58f, 0.21f);
                _baseColor = new Color(0.84f, 0.2f, 0);
                break;
            case MachineStatus.ERROR:
                //_cachedMaterial.color = Color.red;
                _baseColor = new Color(0.75f, 0, 0);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
#if UNITY_EDITOR
        Debug.Log($"External Function [MachineStatusClickEvent({_machineNum}, {_lineNum})]");
#else
        MachineStatusClickEvent(_machineNum, _lineNum, (int)_status);
#endif

        StartCoroutine(OnClickCoroutine());
    }
    //======================================================================================
    private IEnumerator EmissionCoroutine()
    {
        float emissionValue;

        while(_isWorking)
        {
            emissionValue = Mathf.PingPong(Time.time, 1f) * 0.9f + 0.05f;

            switch (_status)
            {
                case MachineStatus.NORMAL:
                    _cachedMaterials[0].color = _lightOff;
                    _cachedMaterials[1].color = _lightOff;
                    _cachedMaterials[2].color = _lightOn;
                    _cachedMaterials[0].SetColor("_EmissionColor", Color.black);
                    _cachedMaterials[1].SetColor("_EmissionColor", Color.black);
                    _cachedMaterials[2].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    break;
                case MachineStatus.WARNING:
                    _cachedMaterials[0].color = _lightOff;
                    _cachedMaterials[1].color = _lightOn;
                    _cachedMaterials[2].color = _lightOn;
                    _cachedMaterials[0].SetColor("_EmissionColor", Color.black);
                    _cachedMaterials[1].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    _cachedMaterials[2].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    break;
                case MachineStatus.ERROR:
                    _cachedMaterials[0].color = _lightOn;
                    _cachedMaterials[1].color = _lightOn;
                    _cachedMaterials[2].color = _lightOn;
                    _cachedMaterials[0].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    _cachedMaterials[1].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    _cachedMaterials[2].SetColor("_EmissionColor", _baseColor * Mathf.LinearToGammaSpace(emissionValue));
                    break;
            }
            yield return null;
        }
    }

    private IEnumerator OnClickCoroutine()
    {
        yield return StartCoroutine(_focusController.SetCameraFocused(transform));
        StartCoroutine(_infoPanel.OpenPanel());
    }
}
