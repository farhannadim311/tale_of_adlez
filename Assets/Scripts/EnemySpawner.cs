using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject batPrefab;
    public GameObject slimePrefab;

    public float checkInterval = 2f;

    private GameObject currentEnemy;

    void Start()
    {
        Spawn();
        InvokeRepeating(nameof(CheckEnemy), checkInterval, checkInterval);
    }

    void CheckEnemy()
    {
        if (currentEnemy == null)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject prefabToSpawn = GetEnemyForQuest();

        currentEnemy = Instantiate(
            prefabToSpawn,
            transform.position,
            transform.rotation
        );
    }

    GameObject GetEnemyForQuest()
    {
        switch (GameManager.Instance.questNo)
        {
            case 0:
                return batPrefab;

            case 1:
                return slimePrefab;

            default:
                return batPrefab;
        }
    }
}