using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject pauseMenu;
    public GameObject InventoryPanel;
    public GameObject ReturnToVillagePanel;
    public GameObject HintPanel;
    public bool isPaused = false;

    public int questState = 0; // 0 = none, 1 = accepted, 2 = completed
    public int questNo = 0;

    public GameObject batsSlainPanel;
    public VolumeProfile volumeProfile;

    private ColorAdjustments colorAdjustments;

    public int enemiesKilled = 0;

    public QuestTwo questTwoUI;

   public void RegisterEnemyKill()
{
    enemiesKilled++;

    if (questNo == 1 && questState == 1)
    {
        if (questTwoUI != null)
            questTwoUI.AddKill();
    }
}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Try to get ColorAdjustments from profile
        if (volumeProfile != null)
        {
            volumeProfile.TryGet(out colorAdjustments);
        }

        colorAdjustments.saturation.value = -80f;;
    }

    void Update()
{

    if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) Resume();
            else Pause();
        }

    if (batsSlainPanel == null) return;

    // show panel during quest 1 while it's active
    if (questNo == 1 && questState == 1)
    {
        batsSlainPanel.SetActive(true);
    }
    // hide when quest advances past 1
    else if (questNo >= 2)
    {
        batsSlainPanel.SetActive(false);
    }
}

    public void CompleteQuest(float questNo)
    {
        questState = 0; //start new quest cycle
        this.questNo++; //increment quest number

        IncreaseSaturation(20f);
    }

    void IncreaseSaturation(float amount)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value += amount;
        }
    }

     void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
{
    if (InventoryPanel.activeSelf ||
        ReturnToVillagePanel.activeSelf ||
        HintPanel.activeSelf)
    {
        return; 
    }

    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
}
    
}