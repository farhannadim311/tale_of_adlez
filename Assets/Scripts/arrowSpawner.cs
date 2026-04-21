using UnityEngine;

//arrowSpawners for the boss battle
public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public float respawnTimer = 20f;

    private GameObject currentArrow;
    private float timer;

    void Start()
    {
        SpawnArrow();
    }

    void Update()
    {
        // if arrow exists, do nothing
        if (currentArrow != null)
            return;

        // count when gone
        timer += Time.deltaTime;

        if (timer >= respawnTimer)
        {
            SpawnArrow();
            timer = 0f;
        }
    }

    void SpawnArrow()
    {
        if (arrowPrefab == null) return;

        currentArrow = Instantiate(
            arrowPrefab,
            transform.position,
            Quaternion.identity
        );
    }
}