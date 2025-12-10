using Unity.Cinemachine;
using UnityEngine;

public abstract class MyCamera : MonoBehaviour
{
    
    public virtual void Activate(Transform f, CinemachineCamera c) { }
    public virtual void LogicUpdate() { }
}
