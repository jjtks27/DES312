using UnityEngine;
[RequireComponent(typeof(Collider))]
public class KillZone : MonoBehaviour
{
    public Transform respawnPoint;
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;
        Telemetry.Death(player.transform.position);
        foreach(Projectile p in FindObjectsByType<Projectile>(FindObjectsSortMode.None))
        {
            Destroy(p.gameObject);
        }
        player.TeleportTo(respawnPoint.position);
        player.transform.rotation = respawnPoint.rotation;
    }
}
