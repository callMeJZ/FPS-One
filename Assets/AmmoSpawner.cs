using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [Header("Ammo")]
    public GameObject ammoPrefab;

    [Header("Spawn Area")]
    public BoxCollider spawnArea;

    [Header("Spawn Timing")]
    public float spawnInterval = 30f;

    private GameObject currentAmmo;

    private void Start()
    {
        // Spawn the first ammo immediately.
        TrySpawnAmmo();

        // Then check every 30 seconds.
        InvokeRepeating(nameof(TrySpawnAmmo), spawnInterval, spawnInterval);
    }

    private void TrySpawnAmmo()
    {
        // Only one ammo object can exist at a time.
        if (currentAmmo != null)
            return;

        if (ammoPrefab == null)
        {
            Debug.LogError("Ammo Prefab is not assigned.");
            return;
        }

        if (spawnArea == null)
        {
            Debug.LogError("Spawn Area is not assigned.");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();

        currentAmmo = Instantiate(
            ammoPrefab,
            spawnPosition,
            Quaternion.identity
        );

        Debug.Log("Ammo spawned at: " + spawnPosition);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Bounds bounds = spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(
            randomX,
            bounds.min.y + 0.20f,
            randomZ
        );
    }
}