using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraNavigation : MonoBehaviour {

    public MeshCollider worldBorder;
    public MeshCollider orbitPlane;

    private RaycastHit _raycast;
    private Coroutine _moveCoroutine;
    private Coroutine _orbitCoroutine;

    private void Awake()
    {
        _raycast = new RaycastHit();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(MoveCoroutine());
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = null;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (_orbitCoroutine != null)
            {
                StopCoroutine(_orbitCoroutine);
            }
            _orbitCoroutine = StartCoroutine(OrbitCoroutine());
        }
        else if(Input.GetMouseButtonUp(1))
        {
            if(_orbitCoroutine != null)
            {
                StopCoroutine(_orbitCoroutine);
            }
            _orbitCoroutine = null;
        }
        else if(Input.mouseScrollDelta.magnitude > 0)
        {
            Zoom();
        }
    }

    private IEnumerator MoveCoroutine()
    {
        Vector3 prevPos = Input.mousePosition;
        Vector3 planarForward = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 planarRight = new Vector3(transform.right.x, 0, transform.right.z);
        Vector3 moveDelta;
        
        while(true)
        {
            if (worldBorder.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _raycast, Camera.main.farClipPlane))
            {
                moveDelta = (Input.mousePosition - prevPos) * 0.01f * (1 + transform.position.y * 0.3f); 
                transform.position -= planarForward * moveDelta.y + planarRight * moveDelta.x;
                prevPos = Input.mousePosition;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator OrbitCoroutine()
    {
        if (!orbitPlane.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _raycast, Camera.main.farClipPlane))
        {
            yield break;
        }

        Vector3 prevPos = Input.mousePosition;
        Vector3 targetPos = _raycast.point;
        Vector3 moveDelta;

        while (true)
        {
            moveDelta = (Input.mousePosition - prevPos) * 0.002f;
            transform.RotateAround(targetPos, Vector3.up, moveDelta.x * 90);
            transform.RotateAround(targetPos, transform.right, -moveDelta.y * 90);
            prevPos = Input.mousePosition;
            yield return new WaitForFixedUpdate();
        }
    }

    private void Zoom()
    {
        if (worldBorder.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _raycast, Camera.main.farClipPlane))
        {
            transform.position += transform.forward * Input.mouseScrollDelta.y * (1 + transform.position.y * 0.1f);
        }
    }
}
