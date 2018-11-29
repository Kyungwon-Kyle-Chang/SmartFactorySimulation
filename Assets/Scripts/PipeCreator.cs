using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    public GameObject pipePrefab;
    public Transform startPos;
    public Transform endPos;
    public Color pipeColor;
    [Range(0.6f, 2)] public float pipeLength;
    [Range(  1, 30)] public float speed;
    public int pipeCountLimit;

    private Queue<Pipe> _pipeQueue;
    private Coroutine _coroutine;
    private bool _isWorking;
    //======================================================================================
    private void Awake()
    {
        _pipeQueue = new Queue<Pipe>();
        _isWorking = false;
    }
    //======================================================================================
    public void StartLineOperation()
    {
        _isWorking = true;
        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(OperateLine());
        }
    }

    public void FinishLineOperation()
    {
        _isWorking = false;
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    //======================================================================================
    private IEnumerator OperateLine()
    {
        while (_isWorking)
        {
            if(_pipeQueue.Count > pipeCountLimit)
            {
                //DestroyEarlyCreatedPipe();
                DestroyAllPipes();
            }

            GameObject pipe = CreatePipe();
            StartCoroutine(MovePipe(pipe));

            while (pipe.transform.localPosition.z >= startPos.localPosition.z - pipe.transform.localScale.y)
            {
                yield return null;
            }
        }
    }

    private GameObject CreatePipe()
    {
        GameObject newPipe = Instantiate(pipePrefab);
        Transform tf = newPipe.transform;

        tf.SetParent(transform);
        tf.localScale = new Vector3(tf.localScale.x, pipeLength, tf.localScale.z);
        tf.localPosition = startPos.localPosition + tf.localScale.y * Vector3.forward;

        Pipe pipeComp = newPipe.GetComponent<Pipe>();
        pipeComp.SetColor(pipeColor);

        return newPipe;
    }
    
    private IEnumerator MovePipe(GameObject pipe)
    {
        while(pipe.transform.localPosition.z >= endPos.localPosition.z - pipe.transform.localScale.y)
        {
            pipe.transform.Translate(Vector3.up * speed * 0.007f * Time.fixedDeltaTime, Space.Self);
            yield return new WaitForFixedUpdate();
        }

        Rigidbody rb = pipe.AddComponent<Rigidbody>();
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce(new Vector3(0.2f, 0, 0), ForceMode.Impulse);

        _pipeQueue.Enqueue(pipe.GetComponent<Pipe>());
    }

    private void DestroyEarlyCreatedPipe()
    {
        Destroy(_pipeQueue.Dequeue().gameObject);
    }

    private void DestroyAllPipes()
    {
        while(_pipeQueue.Count > 0)
        {
            DestroyEarlyCreatedPipe();
        }
    }
}
