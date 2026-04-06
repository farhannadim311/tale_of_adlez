using UnityEngine;
using Unity.Cinemachine;

public class DoorTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public CinemachineCamera vcam;

    public GameObject spaceIconPrefab; // prefab, NOT scene object
    public float iconYOffset = 1.5f;

    private TestMove playerInside;
    private GameObject spawnedIcon;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInside = other.GetComponent<TestMove>();

        if (playerInside != null && spaceIconPrefab != null)
        {
            spawnedIcon = Instantiate(
                spaceIconPrefab,
                playerInside.transform.position + Vector3.up * iconYOffset,
                Quaternion.identity
            );
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.GetComponent<TestMove>() == playerInside)
        {
            playerInside = null;

            if (spawnedIcon != null)
                Destroy(spawnedIcon);
        }
    }

    void Update()
    {
        if (playerInside == null) return;

        // keep icon above player
        if (spawnedIcon != null)
        {
            spawnedIcon.transform.position =
                playerInside.transform.position + Vector3.up * iconYOffset;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 oldPos = playerInside.transform.position;

            playerInside.transform.position = teleportTarget.position;

            if (vcam != null)
                vcam.OnTargetObjectWarped(
                    playerInside.transform,
                    playerInside.transform.position - oldPos
                );
        }
    }
}