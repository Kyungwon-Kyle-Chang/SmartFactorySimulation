using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusController : MonoBehaviour {

    public Camera cameraObject;

    private const float TARGET_TIME = 0.5f;
    //======================================================================================
    public IEnumerator SetCameraFocused(Transform tf)
    {
        Vector3 targetPos = tf.position + tf.right * 0.5f;
        Quaternion targetRot = Quaternion.LookRotation(-tf.right, tf.up);

        yield return StartCoroutine(FocusTween(targetPos, targetRot));
    }
    //======================================================================================
    private IEnumerator FocusTween(Vector3 position, Quaternion rotation)
    {
        int fps = (int)(TARGET_TIME / Time.fixedDeltaTime);
        Vector3 startPos = cameraObject.transform.position;
        Quaternion startRot = cameraObject.transform.rotation;

        for(int i=0; i<fps; i++)
        {
            cameraObject.transform.position = Vector3.Lerp(startPos, position, (float)(i + 1) / fps);
            cameraObject.transform.rotation = Quaternion.Lerp(startRot, rotation, (float)(i + 1) / fps);
            yield return null;
        }
    }
}
