using UnityEngine;
using Unity.Cinemachine;

public class DoorTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public CinemachineCamera vcam;

    public int MusicSignal;

    public bool isADoor;

    // bossroom stuff
    public bool BossRoom;
    public int requiredQuestNo = 3;
    public int requiredQuestState = 1;
    public GameObject lockedPanel;

    // interaction
    public GameObject spaceIconPrefab;
    public float iconYUp = 1.5f;

    private TestMove pDetected;
    private GameObject spawnedIcon;

    private bool uiOpen = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        pDetected = other.GetComponent<TestMove>();

        spawnedIcon = Instantiate(
            spaceIconPrefab,
            pDetected.transform.position + Vector3.up * iconYUp,
            transform.rotation
        );

        spawnedIcon.GetComponent<SpriteRenderer>().sortingOrder = 10;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.GetComponent<TestMove>() == pDetected)
        {
            pDetected = null;

            if (spawnedIcon != null)
                Destroy(spawnedIcon);

            CloseUI();
        }
    }

    void Update()
    {
        if (pDetected == null) return;

        spawnedIcon.transform.position =
            pDetected.transform.position + Vector3.up * iconYUp;

        // close with B
        if (uiOpen)
        {
            if (Input.GetKeyDown(KeyCode.B))
                CloseUI();

            return;
        }

        // interact
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // BOSS ROOM CHECK 
            if (BossRoom &&
                (GameManager.Instance.questNo != requiredQuestNo ||
                 GameManager.Instance.questState != requiredQuestState))
            {
                if (lockedPanel != null)
                    lockedPanel.SetActive(true);

                Time.timeScale = 0f;
                uiOpen = true;

                return;
            }

            if (isADoor)
                SoundManager.Instance.PlayDoorOpen();

            Vector3 oldPos = pDetected.transform.position;

            SoundManager.Instance.SetMusicState(MusicSignal);

            pDetected.transform.position = teleportTarget.position;

            vcam.OnTargetObjectWarped(
                pDetected.transform,
                pDetected.transform.position - oldPos
            );
        }
    }

    void CloseUI()
    {
        uiOpen = false;

        if (lockedPanel != null)
            lockedPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}