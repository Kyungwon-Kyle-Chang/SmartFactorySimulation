using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public TextMeshProUGUI title;
    public Image photo;
    public Image mainInfoMask;
    public Image detailInfoMask;
    public Transform horizontalLayout;
    public GameObject specItemPrefab;

    private RectTransform _rectTfComp;
    private Vector2 _normalSize;
    private const float TARGET_TIME = 0.3f;
    //======================================================================================
    private void Awake()
    {
        _rectTfComp = GetComponent<RectTransform>();
    }
    //======================================================================================
    public void Initialize()
    {
        _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        _normalSize = _rectTfComp.sizeDelta;
        mainInfoMask.fillAmount = 0;
        detailInfoMask.fillAmount = 0;
    }

    public IEnumerator OpenPanel()
    {
        gameObject.SetActive(true);

        Initialize();
        int fps = (int)(TARGET_TIME / Time.fixedDeltaTime);
        float width = 0, height = 0;
        for(int i=0; i<fps; i++)
        {
            width += -_normalSize.x / fps;
            height += -_normalSize.y / fps;
            _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _rectTfComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            yield return null;
        }

        for(int i=0; i<fps; i++)
        {
            mainInfoMask.fillAmount += 1.0f / fps;
            detailInfoMask.fillAmount += 1.0f / fps;
            yield return null;
        }
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void AddSpecItem(string type, string sensorName, string sensorModel, string maxValue, string minValue, string unit, string currentValue)
    {
        var newItem = Instantiate(specItemPrefab);
        newItem.transform.SetParent(horizontalLayout);
        newItem.transform.SetAsLastSibling();

        newItem.GetComponent<SpecItem>().SetInfo(type, sensorName, sensorModel, maxValue, minValue, unit, currentValue);
    }
}
