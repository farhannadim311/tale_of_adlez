using UnityEngine;
using Unity.Cinemachine;

public class DoorTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public CinemachineCamera vcam;

    public GameObject spaceIconPrefab; // prefab, NOT scene object
    public float iconYOffset = 1.5f;

    private TestMove pDetected;
    private GameObject spawnedIcon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        pDetected = other.GetComponent<TestMove>();

        if (pDetected != null && spaceIconPrefab != null)
        {
            spawnedIcon = Instantiate(
                spaceIconPrefab,
                pDetected.transform.position + Vector3.up * iconYOffset,
                Quaternion.identity
            );

            spawnedIcon.GetComponent<SpriteRenderer>().sortingOrder = 10;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.GetComponent<TestMove>() == pDetected)
        {
            pDetected = null;

            if (spawnedIcon != null)
                Destroy(spawnedIcon);
        }
    }

    void Update()
    {
        if (pDetected == null) return;

        {
            spawnedIcon.transform.position =
                pDetected.transform.position + Vector3.up * iconYOffset;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 oldPos = pDetected.transform.position;

            pDetected.transform.position = teleportTarget.position;

       
                vcam.OnTargetObjectWarped(
                    pDetected.transform,
                    pDetected.transform.position - oldPos
                );
        }
    }
}