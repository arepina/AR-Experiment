using System.Collections;
using UnityEngine;

public class WaveMover : MonoBehaviour
{
    public float speed;
    public float duration;

    void Start()
    {
        Vector3 startPos = new Vector3(-15, 18.5f, 65);
        Vector3 finishPos = new Vector3(15, 18.5f, 65);
        StartCoroutine(DeleteObject(gameObject, duration));
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
            //Move to B and wait for the Move to finish
            yield return moveToX(transform, PosB);
            //Move to A and wait for the Move to finish
            yield return moveToX(transform, PosA);
        }
    }

    IEnumerator moveToX(Transform targetObject, Vector3 toPosition)
    {
        float startTime;
        // Total distance between the markers.
        float journeyLength;
        startTime = Time.time;

        //Get the current position of the object to be moved
        Vector3 startPos = targetObject.position;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPos, toPosition);


        if (startPos == toPosition)
            yield break;

        while (true)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            targetObject.position = Vector3.Lerp(startPos, toPosition, fracJourney);

            //Exit if lerp time reaches 1
            if (fracJourney >= 1)
                yield break;

            yield return null;
        }
    }

    void Update()
    {
       
    }
}
