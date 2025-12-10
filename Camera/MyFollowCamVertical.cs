using UnityEngine;
using Unity.Cinemachine;

public class MyFollowCamVertical : MyCamera
{
    Transform followTarget;
    CinemachineCamera c_cam;
    public int myIndex;
    public override void Activate(Transform f, CinemachineCamera c)
    {
        followTarget = f;
        Debug.Log("Follow camera vertical activated for target: " + f.name);
        c_cam = c;
        c_cam.Priority = 20; // Set the priority of this camera to activate it
    }
    public override void LogicUpdate()
    {
        Vector3 cameraPosition = c_cam.transform.position;
        cameraPosition.y = followTarget.position.y; // Follow the target vertically
        c_cam.transform.position = cameraPosition;
      
    }
}