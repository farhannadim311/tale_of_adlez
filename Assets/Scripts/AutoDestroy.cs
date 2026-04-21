using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float duration = 10f;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}