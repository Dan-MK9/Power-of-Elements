using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject magicProjectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 50f;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetDirection;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetDirection = (hit.point - firePoint.position).normalized;
        }
        else
        {
            targetDirection = ray.direction;
        }

        GameObject projectile = Instantiate(magicProjectilePrefab, firePoint.position, Quaternion.LookRotation(targetDirection));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = targetDirection * projectileSpeed;
        }
    }
}


