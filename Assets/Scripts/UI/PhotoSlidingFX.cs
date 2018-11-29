using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoSlidingFX {

    private Image _imageObject1;
    private Image _imageObject2;
    //======================================================================================
    public PhotoSlidingFX(Image imageObject1, Image imageObject2)
    {
        _imageObject1 = imageObject1;
        _imageObject2 = imageObject2;
    }
    //======================================================================================
    public IEnumerator PlayFX(Sprite[] photoItems, float stayTime, float slideTime)
    {
        if (photoItems.Length < 2)
            yield break;

        int fps = (int)(slideTime / Time.fixedDeltaTime);
        int currentIndex = 0;
        int nextIndex = 1;
        while(true)
        {
            _imageObject2.sprite = photoItems[currentIndex];
            _imageObject1.sprite = photoItems[nextIndex];
            _imageObject2.fillAmount = 1;

            yield return new WaitForSeconds(stayTime);

            for(int i=0; i<fps; i++)
            {
                _imageObject2.fillAmount -= 1f / fps;  
                yield return null;
            }

            currentIndex = nextIndex;
            nextIndex = currentIndex >= photoItems.Length - 1 ? 0 : currentIndex + 1;
        }
    }
}
