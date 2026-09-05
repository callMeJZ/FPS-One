using System.Collections;
using UnityEngine;

public class GunMechanics : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public Transform muzzlePoint;
    public ParticleSystem muzzleFlash;
    public ParticleSystem muzzleSmoke;
    public Transform gunTransform;
    public GameObject crosshair;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 30f;

    [Header("Zoom")]
    public float defaultFOV = 60f;
    public float zoomedFOV = 35f;
    public float zoomSpeed = 8f;

    [Header("Gun Recoil")]
    public float recoilDistance = 0.08f;
    public float recoilRotation = 4f;
    public float recoilReturnSpeed = 12f;

    [Header("Ammo UI")]
    public AmmoUI ammoUI;

    private Vector3 gunOriginalPosition;
    private Quaternion gunOriginalRotation;

    void Start()
    {
        // Find camera automatically if not assigned
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            cam.fieldOfView = defaultFOV;
        }

        // Save the gun's starting position and rotation
        if (gunTransform != null)
        {
            gunOriginalPosition = gunTransform.localPosition;
            gunOriginalRotation = gunTransform.localRotation;
        }
    }

    void Update()
    {
        HandleZoom();

        // LEFT MOUSE BUTTON = FIRE
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void HandleZoom()
    {
        if (cam == null)
            return;

        // Right mouse button = scope
        bool isScoped = Input.GetMouseButton(1);

        // Normal FOV or zoomed FOV
        float targetFOV =
            isScoped
                ? zoomedFOV
                : defaultFOV;

        // Smooth camera zoom
        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );

        // Show or hide crosshair
        if (crosshair != null)
        {
            crosshair.SetActive(isScoped);
        }
    }

    void Shoot()
    {
        if (cam == null)
            return;

        // Check ammo before shooting
        if (ammoUI != null &&
            ammoUI.GetCurrentAmmo() <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        // Check bullet prefab
        if (bulletPrefab == null)
        {
            Debug.LogError(
                "Bullet Prefab is not assigned in GunMechanics."
            );

            return;
        }

        // Check muzzle point
        if (muzzlePoint == null)
        {
            Debug.LogError(
                "Muzzle Point is not assigned in GunMechanics."
            );

            return;
        }

        // ==========================================
        // 1. MUZZLE FLASH
        // ==========================================

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            muzzleFlash.Play();
        }

        // ==========================================
        // 2. MUZZLE SMOKE
        // ==========================================

        if (muzzleSmoke != null)
        {
            muzzleSmoke.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            muzzleSmoke.Play();
        }

        // ==========================================
        // 3. GUN RECOIL
        // ==========================================

        if (gunTransform != null)
        {
            StartCoroutine(GunRecoil());
        }

        // ==========================================
        // 4. CALCULATE AIM DIRECTION
        // ==========================================

        Ray cameraRay = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 targetPoint;

        RaycastHit cameraHit;

        if (Physics.Raycast(
            cameraRay,
            out cameraHit,
            1000f
        ))
        {
            targetPoint = cameraHit.point;
        }
        else
        {
            targetPoint =
                cameraRay.origin +
                cameraRay.direction * 1000f;
        }

        // ==========================================
        // 5. AIM FROM GUN MUZZLE TO TARGET
        // ==========================================

        Vector3 shootDirection =
            (targetPoint - muzzlePoint.position).normalized;

        // ==========================================
        // 6. CREATE BULLET
        // ==========================================

        GameObject bullet = Instantiate(
            bulletPrefab,
            muzzlePoint.position,
            Quaternion.LookRotation(shootDirection)
        );

        // ==========================================
        // 7. CONSUME AMMO
        // ==========================================

        if (ammoUI != null)
        {
            ammoUI.ConsumeAmmo();
        }

        // ==========================================
        // 8. SET BULLET SPEED
        // ==========================================

        Rigidbody bulletRb =
            bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.linearVelocity =
                shootDirection * bulletSpeed;
        }
    }

    IEnumerator GunRecoil()
    {
        // Push weapon backward
        gunTransform.localPosition =
            gunOriginalPosition +
            Vector3.back * recoilDistance;

        // Rotate weapon slightly upward
        gunTransform.localRotation =
            gunOriginalRotation *
            Quaternion.Euler(
                -recoilRotation,
                0f,
                0f
            );

        yield return new WaitForSeconds(0.05f);

        // Smoothly return
        while (
            Vector3.Distance(
                gunTransform.localPosition,
                gunOriginalPosition
            ) > 0.001f
        )
        {
            gunTransform.localPosition =
                Vector3.Lerp(
                    gunTransform.localPosition,
                    gunOriginalPosition,
                    Time.deltaTime *
                    recoilReturnSpeed
                );

            gunTransform.localRotation =
                Quaternion.Slerp(
                    gunTransform.localRotation,
                    gunOriginalRotation,
                    Time.deltaTime *
                    recoilReturnSpeed
                );

            yield return null;
        }

        gunTransform.localPosition =
            gunOriginalPosition;

        gunTransform.localRotation =
            gunOriginalRotation;
    }
}