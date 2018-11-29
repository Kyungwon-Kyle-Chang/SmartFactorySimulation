using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public TextMeshProUGUI title;
    public Image photo;
    public Image photo2;
    public Image mainInfoMask;
    public Image detailInfoMask;
    public Transform horizontalLayout;
    public GameObject specItemPrefab;
    public Sprite[] machinePhotos;
    public RenderTexture[] sensorObjectRTs;

    private RectTransform _rectTfComp;
    private Vector2 _normalSize;
    private PhotoSlidingFX _photoSlider;
    private float _photoStayTime = 1f;
    private float _photoSlidingTime = 1.2f;
    private Coroutine _currentValueUpdateCoroutine;
    private Coroutine _photoSlidingAnimCoroutine;
    private Coroutine _photoLightingFXCoroutine;
    private Sprite[][] _machinePhotos;

    private const float TARGET_TIME = 0.3f;
    private readonly Color WARNING_COLOR = new Color(1, 0.5f, 0);
    private readonly Color ERROR_COLOR = new Color(1, 0, 0);
    //======================================================================================
    private void Awake()
    {
        _rectTfComp = GetComponent<RectTransform>();
        _photoSlider = new PhotoSlidingFX(photo, photo2);

        //--------------------- Hard Coded ----------------------------
        int count = 0;
        _machinePhotos = new Sprite[5][];

        _machinePhotos[0] = new Sprite[8];
        for (int i = 0; i < _machinePhotos[0].Length; i++)
            _machinePhotos[0][i] = machinePhotos[count++];

        _machinePhotos[1] = new Sprite[17];
        for (int i = 0; i < _machinePhotos[1].Length; i++)
            _machinePhotos[1][i] = machinePhotos[count++];

        _machinePhotos[2] = new Sprite[16];
        for (int i = 0; i < _machinePhotos[2].Length; i++)
            _machinePhotos[2][i] = machinePhotos[count++];

        _machinePhotos[3] = new Sprite[24];
        for (int i = 0; i < _machinePhotos[3].Length; i++)
            _machinePhotos[3][i] = machinePhotos[count++];

        _machinePhotos[4] = new Sprite[13];
        for (int i = 0; i < _machinePhotos[4].Length; i++)
            _machinePhotos[4][i] = machinePhotos[count++];
        //-------------------------------------------------------------
    }
    //======================================================================================
    public IEnumerator OpenPanel(int machineNum, Func<int, SensorData[]> getSensorDataFunc)
    {
        gameObject.SetActive(true);

        Initialize();
        SensorData[] sensorDataArray = getSensorDataFunc(machineNum);

        title.text = SensorSpecHolder.machineName[machineNum - 1];
        //photo.sprite = machinePhotos[machineNum - 1];
        for(var i=0; i<sensorDataArray.Length; i++)
            AddSpecItem(sensorDataArray[i]);

        _photoSlidingAnimCoroutine = StartCoroutine(_photoSlider.PlayFX(_machinePhotos[machineNum-1], _photoStayTime, _photoSlidingTime));
        yield return StartCoroutine(PanelOpenAnimation());
        _currentValueUpdateCoroutine = StartCoroutine(UpdateCurrentValueTexts(machineNum, getSensorDataFunc));
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);

        if(_currentValueUpdateCoroutine != null)
        {
            StopCoroutine(_currentValueUpdateCoroutine);
            _currentValueUpdateCoroutine = null;
        }
            
        if(_photoSlidingAnimCoroutine != null)
        {
            StopCoroutine(_photoSlidingAnimCoroutine);
            _photoSlidingAnimCoroutine = null;
        }
    }

    public void ChangePhotoSlidingTime(float stayTime, float slidingTime)
    {
        _photoStayTime = stayTime;
        _photoSlidingTime = slidingTime;
    }
    //======================================================================================
    private void Initialize()
    {
        _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        _normalSize = _rectTfComp.sizeDelta;
        mainInfoMask.fillAmount = 0;
        detailInfoMask.fillAmount = 0;

        if (_currentValueUpdateCoroutine != null)
            StopCoroutine(_currentValueUpdateCoroutine);

        if (_photoSlidingAnimCoroutine != null)
            StopCoroutine(_photoSlidingAnimCoroutine);

        for (var i = 0; i < horizontalLayout.childCount; i++)
        {
            Destroy(horizontalLayout.GetChild(i).gameObject);
        }
    }

    private void AddSpecItem(SensorData sensorData)
    {
        var newItem = Instantiate(specItemPrefab);
        newItem.transform.SetParent(horizontalLayout);
        newItem.transform.SetAsLastSibling();
        newItem.GetComponent<SpecItem>().Register(sensorData, sensorObjectRTs);
    }

    private IEnumerator PanelOpenAnimation()
    {
        int fps = (int)(TARGET_TIME / Time.fixedDeltaTime);
        float width = 0, height = 0;
        for (int i = 0; i < fps; i++)
        {
            width += -_normalSize.x / fps;
            height += -_normalSize.y / fps;
            _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < fps; i++)
        {
            mainInfoMask.fillAmount += 1.0f / fps;
            detailInfoMask.fillAmount += 1.0f / fps;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator UpdateCurrentValueTexts(int machineNum, Func<int, SensorData[]> getSensorDataFunc)
    {
        SensorData[] sensorData;
        while (true)
        {
            sensorData = getSensorDataFunc(machineNum);
            StartPhotoLighting(CheckIfError(sensorData));
            for(var i=0;i<horizontalLayout.childCount;i++)
            {
                horizontalLayout.GetChild(i).GetComponent<SpecItem>().currentValue.text = sensorData[i].currentValue;
                
                if(sensorData[i].status == MachineStatus.ERROR)
                    horizontalLayout.GetChild(i).GetComponent<SpecItem>().currentValue.color = ERROR_COLOR;
                else if(sensorData[i].status == MachineStatus.WARNING)
                    horizontalLayout.GetChild(i).GetComponent<SpecItem>().currentValue.color = WARNING_COLOR;
                else
                    horizontalLayout.GetChild(i).GetComponent<SpecItem>().currentValue.color = Color.green;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private bool CheckIfError(SensorData[] sensorDataArray)
    {
        for(var i=0; i<sensorDataArray.Length; i++)
        {
            if (sensorDataArray[i].status != MachineStatus.NORMAL)
                return true;
        }

        return false;
    }

    private void StartPhotoLighting(bool state)
    {
        if(state)
        {
            if (_photoLightingFXCoroutine == null)
            {
                _photoLightingFXCoroutine = StartCoroutine(PhotoLightingFX());
            }
        }
        else
        {
            if (_photoLightingFXCoroutine != null)
            {
                StopCoroutine(_photoLightingFXCoroutine);
                _photoLightingFXCoroutine = null;
                photo.color = Color.white;
                photo2.color = Color.white;
            }
        }
    }
    
    private IEnumerator PhotoLightingFX()
    {        
        while(true)
        {
            photo.color = Color.Lerp(Color.white, ERROR_COLOR, Mathf.Sqrt(Mathf.PingPong(Time.time, 1)));
            photo2.color = Color.Lerp(Color.white, ERROR_COLOR, Mathf.Sqrt(Mathf.PingPong(Time.time, 1)));
            yield return null;
        }

    }
}
