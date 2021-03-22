using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneIndicator : MonoBehaviour
{
    ARRaycastManager rayCastManager;
    GameObject placeDetector;
    bool poseIsValid;
    
    public GameObject buttonBearObj;
    
    void Start()
    {
        rayCastManager = FindObjectOfType<ARRaycastManager>();
        placeDetector = transform.GetChild(0).gameObject;

        placeDetector.SetActive(false);
        buttonBearObj.SetActive(false);
    }

    void Update()
    {
        placeDetection();
        spawnIndicator();
    }

    private void placeDetection()
    {
        List<ARRaycastHit> rayHits = new List<ARRaycastHit>();
        rayCastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), rayHits, TrackableType.Planes);

        poseIsValid = rayHits.Count > 0;

        if (poseIsValid)
        {
            transform.position = rayHits[0].pose.position;
            transform.rotation = rayHits[0].pose.rotation;
        }
    }

    private void spawnIndicator()
    {
        if (poseIsValid && !placeDetector.activeInHierarchy)
        {
            placeDetector.SetActive(true);
            buttonBearObj.SetActive(true);

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
            transform.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
