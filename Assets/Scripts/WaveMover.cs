using System.Collections;
using Logic;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    void Start()
    {
        Vector3 startPos = new Vector3(FindObjectOfType<GlobalWave>().leftXWave, FindObjectOfType<GlobalWave>().YWave, FindObjectOfType<WaveHolderReferencedContent>().DistanceFromCamera);
        Vector3 finishPos = new Vector3(FindObjectOfType<GlobalWave>().rightXWave, FindObjectOfType<GlobalWave>().YWave, FindObjectOfType<WaveHolderReferencedContent>().DistanceFromCamera);
        StartCoroutine(DeleteObject(gameObject, FindObjectOfType<GlobalWave>().durationWave));
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
            float distCovered = (Time.time - startTime) * FindObjectOfType<GlobalWave>().speedWave;
            float fracJourney = distCovered / journeyLength;
            targetObject.position = Vector3.Lerp(startPos, toPosition, fracJourney);
            if (fracJourney >= 1)
                yield break;
            yield return null;
        }
    }
}
