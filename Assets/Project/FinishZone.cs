using UnityEngine;
[RequireComponent(typeof(Collider))]
public class FinishZone : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerController>() != null)
        {
            Telemetry.LevelComplete();
        }  
    }
}
