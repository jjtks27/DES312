using UnityEngine;
using UnityEngine.InputSystem;
public class WeaponController : MonoBehaviour
{
    [Header("References")]
    public PlayerController player;
    public Transform cameraTransform;
    public Transform muzzle;
    public GameObject projectilePrefab;

    [Header("Settings")]
    public float shootForce = 25f;
    [Tooltip("How high do you spawn after teleport")]
    public float teleportHeightOffset = 1f;
    [Tooltip("After teleport, the object dissapears")]
    public bool consumeOnTeleport = true;
    private GameObject currentProjectile;
    private Collider playerCollider;

    void Awake()
    {
        if (player == null) player = GetComponent < PlayerController>() ;
        playerCollider = GetComponent<CharacterController>();

    }
    void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null) return;
        if (Cursor.lockState != CursorLockMode.Locked) return;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
        if (mouse.rightButton.wasPressedThisFrame)
        {
            TeleportToProjectile();
        }
    }
    void Shoot()
    {
        if (currentProjectile != null) {
            Destroy(currentProjectile);
        }
    currentProjectile=Instantiate(projectilePrefab, muzzle.position, muzzle.rotation);

    Collider projCol = currentProjectile.GetComponent<Collider>();
    if(projCol !=null && playerCollider !=null){
            Physics.IgnoreCollision(projCol, playerCollider);
        }

        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
    rb.linearVelocity=cameraTransform.forward* shootForce;
}
    void TeleportToProjectile()
{
    if (currentProjectile == null) return;
    Vector3 target = currentProjectile.transform.position + Vector3.up * teleportHeightOffset;
    player.TeleportTo(target);
    if (consumeOnTeleport)
    {
        Destroy(currentProjectile);
        currentProjectile = null;
    }
}
}
