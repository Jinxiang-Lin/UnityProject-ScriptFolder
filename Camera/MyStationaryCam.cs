using Unity.Cinemachine;
using UnityEngine;

public class MyStationaryCam : MyCamera
{
    
    public override void Activate(Transform f, CinemachineCamera c)
    {
        // Implementation for activating the stationary camera
        Debug.Log("Stationary camera activated.");
    }
   
}
