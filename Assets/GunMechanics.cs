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

    [Header("Shooting")]
    public float maxShootDistance = 100f;
    public float impulseForce = 12f;

    [Header("Zoom")]
    public float defaultFOV = 60f;
    public float zoomedFOV = 35f;
    public float zoomSpeed = 8f;

    [Header("Gun Recoil")]
    public float recoilDistance = 0.08f;
    public float recoilRotation = 4f;
    public float recoilReturnSpeed = 12f;

    [Header("Camera Recoil")]
    public float cameraRecoilAmount = 2f;
    public float cameraRecoilReturnSpeed = 10f;

    private Vector3 gunOriginalPosition;
    private Quaternion gunOriginalRotation;

    private float currentCameraRecoil = 0f;

    void Start()
    {
        // Automatically find the camera if not assigned
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            cam.fieldOfView = defaultFOV;
        }

        // If gunTransform isn't assigned, use the weapon model
        if (gunTransform == null)
        {
            Debug.LogWarning(
                "Gun Transform is not assigned in GunMechanics."
            );
        }
        else
        {
            gunOriginalPosition = gunTransform.localPosition;
            gunOriginalRotation = gunTransform.localRotation;
        }
    }

    void Update()
    {
        HandleZoom();

        // LEFT CLICK = SHOOT
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        HandleCameraRecoil();
    }

    void HandleZoom()
    {
        if (cam == null)
            return;

        float targetFOV =
            Input.GetMouseButton(1)
                ? zoomedFOV
                : defaultFOV;

        cam.fieldOfView = Mathf.Lerp(
            cam.fieldOfView,
            targetFOV,
            Time.deltaTime * zoomSpeed
        );
    }

    void Shoot()
    {
        if (cam == null)
            return;

        // ==========================================
        // MUZZLE FLASH
        // ==========================================

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            muzzleFlash.Play();
        }
        if (muzzleSmoke != null)
        {
            muzzleSmoke.Stop(
                true,
                ParticleSystemStopBehavior.StopEmittingAndClear
            );

            muzzleSmoke.Play();
        }
        // ==========================================
        // GUN RECOIL
        // ==========================================

        if (gunTransform != null)
        {
            StartCoroutine(GunRecoil());
        }

        // ==========================================
        // CAMERA RECOIL
        // ==========================================

        currentCameraRecoil = cameraRecoilAmount;

        // ==========================================
        // FIND TARGET USING CROSSHAIR
        // ==========================================

        Ray cameraRay = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 targetPoint;

        RaycastHit cameraHit;

        if (Physics.Raycast(
            cameraRay,
            out cameraHit,
            maxShootDistance
        ))
        {
            targetPoint = cameraHit.point;
        }
        else
        {
            targetPoint =
                cameraRay.origin +
                cameraRay.direction *
                maxShootDistance;
        }

        // ==========================================
        // SHOOT FROM MUZZLE
        // ==========================================

        Vector3 shootOrigin;

        if (muzzlePoint != null)
        {
            shootOrigin = muzzlePoint.position;
        }
        else
        {
            shootOrigin = cam.transform.position;
        }

        Vector3 shootDirection =
            (targetPoint - shootOrigin).normalized;

        Ray gunRay = new Ray(
            shootOrigin,
            shootDirection
        );

        RaycastHit hit;

        if (Physics.Raycast(
            gunRay,
            out hit,
            maxShootDistance
        ))
        {
            HandleHit(hit);
        }
    }

    IEnumerator GunRecoil()
    {
        // Push gun backwards
        gunTransform.localPosition =
            gunOriginalPosition +
            Vector3.back * recoilDistance;

        // Kick gun upward
        gunTransform.localRotation =
            gunOriginalRotation *
            Quaternion.Euler(
                -recoilRotation,
                0f,
                0f
            );

        // Wait briefly
        yield return new WaitForSeconds(0.05f);

        // Return to original position
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

    void HandleCameraRecoil()
    {
        if (cam == null)
            return;

        if (currentCameraRecoil > 0f)
        {
            currentCameraRecoil =
                Mathf.Lerp(
                    currentCameraRecoil,
                    0f,
                    Time.deltaTime *
                    cameraRecoilReturnSpeed
                );

            // Small upward camera kick
            // The FirstPersonController handles
            // normal camera rotation.
        }
    }

    void HandleHit(RaycastHit hit)
    {
        // =====================================
        // CHECK FOR HUMAN TARGET
        // =====================================
        Debug.Log("HIT: " + hit.collider.name);
        TargetHealth humanTarget =
            hit.collider.GetComponentInParent<TargetHealth>();

        if (humanTarget != null)
        {
            humanTarget.TakeDamage(
                humanTarget.damagePerShot
            );

            return;
        }


        // =====================================
        // NORMAL COLOR PROPAGATION TARGET
        // =====================================

        ColorPropagation colorObject =
            hit.collider.GetComponentInParent<ColorPropagation>();

        if (colorObject != null)
        {
            colorObject.ApplyRandomColor();
        }


        // =====================================
        // PHYSICS
        // =====================================

        Rigidbody rb =
            hit.collider.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = true;

            Vector3 bounceDirection =
                (hit.point - transform.position).normalized
                + Vector3.up;

            rb.AddForce(
                bounceDirection * impulseForce,
                ForceMode.Impulse
            );
        }
    }
}