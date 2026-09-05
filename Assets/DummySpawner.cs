using UnityEngine;
using System.Collections;

public class DummySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject dummyPrefab;
    public float spawnInterval = 5f;
    public Vector2 spawnAreaBounds = new Vector2(5f, 5f);
    public float spawnYOffset = 0.2f;
    
    [Header("Target Setting")]
    public Transform player; // Dito natin ilalagay ang Player mo

    private GameObject currentDummy;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnDummy(); 
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void SpawnDummy()
    {
        if (currentDummy != null)
        {
            Destroy(currentDummy);
        }

        if (dummyPrefab == null) return;

        float randomX = Random.Range(-spawnAreaBounds.x, spawnAreaBounds.x);
        float randomZ = Random.Range(-spawnAreaBounds.y, spawnAreaBounds.y);
        Vector3 spawnPosition = new Vector3(randomX, spawnYOffset, randomZ); 

        currentDummy = Instantiate(dummyPrefab, spawnPosition, Quaternion.identity);

        // --- BAGONG CODE: Paharapin ang dummy sa player ---
        if (player != null)
        {
            currentDummy.transform.LookAt(player.position);
            
            // Para hindi tumingala o yumuko ang dummy, i-lock natin ang Y axis lamang
            Vector3 currentRotation = currentDummy.transform.eulerAngles;
            currentRotation.x = 0f;
            currentRotation.z = 0f;
            currentDummy.transform.eulerAngles = currentRotation;
        }
    }
}