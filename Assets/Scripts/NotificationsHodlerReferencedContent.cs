﻿using Logic;
using UnityEngine;

public class NotificationsHodlerReferencedContent : MonoBehaviour
{
    [Tooltip("The camera is needed to emulate a torso reference frame")]
    public GameObject Camera;

    [Tooltip("The distance from the camera that this object should be placed")]
    private float DistanceFromCamera = 10f;

    [Tooltip("If checked, makes objecta move smoothly")]
    public bool SimulateInertia = false;

    [Tooltip("The speed at which this object changes its position, if the inertia effect is enabled")]
    public float LerpSpeed = 1f;

    [Tooltip("Angle to the horizon")]
    public float AngleToTheHorizon = 0.01f;

    [Tooltip("Angle when tray should be shown")]
    public float TrayShowAngle = 0.02f;

    void OnEnable()
    {
        if (Camera == null)
        {
            Debug.LogError("Error: Camera is not set. Disabling the script.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        Vector3 posTo = Camera.transform.position;
        posTo.y = AngleToTheHorizon + 1;
        if (Camera.transform.position.y >= TrayShowAngle)
        {
            EventManager.Broadcast(EVENT.ShowTray);
            return;
        }

        if (transform.childCount == 0)
        {
            posTo = Camera.transform.position + Camera.transform.forward * DistanceFromCamera;
            Quaternion rotTo = Quaternion.LookRotation(transform.position - Camera.transform.position);
            transform.rotation = rotTo;
            transform.position = posTo;
            return;
        }

        if (SimulateInertia && posTo.x != transform.position.x)
        {
            float posSpeed = Time.deltaTime * LerpSpeed;
            transform.position = Vector3.SlerpUnclamped(transform.position, posTo, posSpeed);
        }
        else
        {
            transform.position = posTo;
        }
    }
}
