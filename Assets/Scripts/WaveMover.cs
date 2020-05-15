﻿using System.Collections;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    public float speedWave;
    public float durationWave;

    void Start()
    {
        Vector3 startPos = transform.position;
        Vector3 finishPos = startPos;
        finishPos.x = startPos.x + 2;
        StartCoroutine(DeleteObject(gameObject, durationWave));
        StartCoroutine(PingPong(transform, startPos, finishPos));
    }

    IEnumerator DeleteObject(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }

    IEnumerator PingPong(Transform transform, Vector3 PosA, Vector3 PosB)
    {
        while (true)
        {
            yield return moveToX(transform, PosB);
            yield return moveToX(transform, PosA);
        }
    }

    IEnumerator moveToX(Transform targetObject, Vector3 toPosition)
    {
        float startTime;
        float journeyLength;
        startTime = Time.time;
        Vector3 startPos = targetObject.position;
        journeyLength = Vector3.Distance(startPos, toPosition);
        if (startPos == toPosition)
            yield break;
        while (true)
        { 
            float distCovered = (Time.time - startTime) * speedWave;
            float fracJourney = distCovered / journeyLength;
            Debug.Log(startPos + " " + toPosition);
            targetObject.position = Vector3.Lerp(startPos, toPosition, fracJourney);
            if (fracJourney >= 1)
                yield break;
            yield return null;
        }
    }
}
