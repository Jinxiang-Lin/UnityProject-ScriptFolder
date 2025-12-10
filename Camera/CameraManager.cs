using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Collider2D camConfiner;
    public static CameraManager Instance { get; private set; }
    private List<CinemachineCamera> v_cams = new();
    MyCamera current_camera;
    Transform followTarget;
    private bool isAboveThreshold = false; // Flag to track if the target is above the threshold
    float yThreshold = 8f; // The y position threshold for switching cameras
    private void Awake()
    {
        Instance = this;
        v_cams.Clear();
        v_cams.AddRange(FindObjectsByType<CinemachineCamera>(FindObjectsSortMode.None));
        Debug.Log("Found " + v_cams.Count + " Cinemachine cameras in the scene.");
        if (v_cams.Count == 0)
        {
            Debug.LogError("No Cinemachine cameras found in the scene.");
        }
        if (camConfiner == null)
        {
            camConfiner = GameObject.FindWithTag("Confiner")?.GetComponent<Collider2D>();
            if (camConfiner == null)
            {
                Debug.LogError("No Collider2D with tag 'Confiner' found in the scene.");
            }
        }
        foreach (var cam in v_cams)
        {
            // Add CinemachineConfiner2D extension if not already present
            cam.Priority = 0;
            var confiner2D = cam.GetComponent<CinemachineConfiner2D>();
            if (confiner2D == null)
            {
                confiner2D = cam.gameObject.AddComponent<CinemachineConfiner2D>();
            }
            // Assign the confiner collider
            confiner2D.BoundingShape2D = camConfiner;
        }
    }
    private void Start()
    {
        foreach (var cam in v_cams)
        {
            if (cam.TryGetComponent<MyCamera>(out var myCam))
            {
                // Example: pick the first camera with myIndex == 1, regardless of type
                var myIndexField = myCam.GetType().GetField("myIndex");
                if (myIndexField != null && (int)myIndexField.GetValue(myCam) == 0)
                {
                    Debug.Log("Found MyCamera component in camera: " + cam.name);
                    current_camera = myCam;
                    Debug.Log($"curent camera is {current_camera}");
                    Debug.Log($"current follow target is {followTarget}");
                    if (followTarget != null)
                    {
                        current_camera.Activate(followTarget, cam);
                    }
                    else if (followTarget == null)
                    { 
                        Debug.LogWarning("Follow target is null, generating player.");
                        GameManager.Instance.GeneratePlayer();
                        GameManager.Instance.SetFollowTarget();
                    }
                }
            }
        }
    }
    private void Update()
    {
        //Debug.Log(followTarget.transform.position.y);
        if (current_camera is MyFollowCam) // only if it is horizontal follow cam
        {
            if (followTarget != null && followTarget.transform.position.y > yThreshold)
            {
                if (!isAboveThreshold)
                {
                    Debug.Log("Switching camera due to target position above y = 8: " + followTarget.transform.position.y);
                    SwitchCamera(1);
                    isAboveThreshold = true; // Set the flag to true after switching
                }
            }
            else if (followTarget != null && followTarget.transform.position.y <= 8f)
            {

                if (isAboveThreshold)
                {
                    Debug.Log("Switching camera due to target position below or equal to y = 8: " + followTarget.transform.position.y);
                    SwitchCamera(0);
                    isAboveThreshold = false; // Reset the flag after switching
                }
            }
        }
        
        if (current_camera != null)
        {
            current_camera.LogicUpdate();
        }
    }
    public void SwitchCamera(int index)
    {
        foreach (var cam in v_cams)
        {
            if (cam.TryGetComponent<MyCamera>(out var myCam))
            {
                // Example: pick the first camera with myIndex == index
                var myIndexField = myCam.GetType().GetField("myIndex");
                if (myIndexField != null && (int)myIndexField.GetValue(myCam) == index)
                {
                    Debug.Log("Switching to camera: " + cam.name);
                    current_camera = myCam;
                    current_camera.Activate(followTarget, cam);
                    cam.Priority = 20; // Set priority to activate this camera
                }
                else
                {
                    cam.Priority = 0; // Deactivate other cameras
                }
            }
        }
    }
    public void SetFollowTarget(Transform target)
    {
        followTarget = target;
        // Optionally update current_camera if needed
        if (current_camera != null)
        {
            current_camera.Activate(followTarget, current_camera.GetComponent<CinemachineCamera>());
        }
    }
}
