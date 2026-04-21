using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject InventoryPanel;
    public GameObject ReturnToVillagePanel;
    public GameObject controlsPanel;

    
    public GameObject HintPanel;

    bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        if (InventoryPanel.activeSelf ||
            ReturnToVillagePanel.activeSelf ||
            HintPanel.activeSelf || controlsPanel.activeSelf)
            return;

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}