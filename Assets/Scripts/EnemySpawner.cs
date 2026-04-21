using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject batPrefab;
    public GameObject crabPrefab;
    public GameObject golemPrefab;
    public GameObject slimePrefab;

    public bool town = false;
    public bool proj = false;

    public float checkInterval = 5f;

    private GameObject current;
    private float timer;

    void Start()
    {
        Spawn();
        timer = checkInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            CheckEnemy();
            timer = checkInterval;
        }
    }

    void CheckEnemy() //spawn enemy if none
    {
        if (current == null)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemyToSpawn = GetEnemy();

        current = Instantiate(
            enemyToSpawn,
            transform.position,
            transform.rotation
        );
    }

    GameObject GetEnemy()
    {
        if (!town && !proj)
            return batPrefab;

        if (town && !proj)
            return crabPrefab;

        if (!town && proj)
            return golemPrefab;

        return slimePrefab;
    }
}