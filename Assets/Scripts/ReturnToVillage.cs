using UnityEngine;
using Unity.Cinemachine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform spawnPoint;
    public CinemachineCamera vcam;
    public TestMove player;

    public GameObject ReturnToVillagePanel;

    public GameObject PuaseMain;

    public void TeleportToSpawn()
    {
        Debug.Log("Teleport button clicked");

        if (player == null || spawnPoint == null)
        {
            Debug.LogError("Missing references");
            return;
        }

        Vector3 oldPos = player.transform.position;

        player.transform.position = spawnPoint.position;

        // UNPAUSE GAME
        Time.timeScale = 1f;

        ReturnToVillagePanel.SetActive(false);
        PuaseMain.SetActive(false);
        SoundManager.Instance.SetMusicState(0);

        // CAMERA FIX (same logic as your DoorTeleport script)
        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(
                player.transform,
                player.transform.position - oldPos
            );
        }
    }
}