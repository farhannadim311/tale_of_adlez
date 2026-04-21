using UnityEngine;

public class bombDrop : MonoBehaviour
{
    public GameObject explosionPrefab;

    private float timer = 3f;
    private bool exploded = false;

    void Update()
    {
        if (exploded) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SoundManager.Instance.PlayBombExplode();

            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            exploded = true;
        }
    }

}