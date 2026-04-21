using UnityEngine;
using Unity.Cinemachine;
//similar to door teleport snap camera back to position
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

        
        Time.timeScale = 1f;

        ReturnToVillagePanel.SetActive(false);
        PuaseMain.SetActive(false);
        SoundManager.Instance.SetMusicState(0);

        
        if (vcam != null)
        {
            vcam.OnTargetObjectWarped(
                player.transform,
                player.transform.position - oldPos
            );
        }
    }
}