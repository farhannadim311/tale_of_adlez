using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetupColorAdjustments();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetupColorAdjustments();
    }

    void SetupColorAdjustments()
    {
        if (volumeProfile != null)
        {
            volumeProfile.TryGet(out colorAdjustments);

            if (colorAdjustments != null)
            {
                colorAdjustments.saturation.value = -80f;
            }
        }
    }

    public void RegisterEnemyKill()
    {
        enemiesKilled++;

        if (questNo == 1 && questState == 1)
        {
            if (questTwoUI != null)
                questTwoUI.AddKill();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused) Resume();
            else Pause();
        }

        if (batsSlainPanel == null) return;

        if (questNo == 1 && questState == 1)
        {
            batsSlainPanel.SetActive(true);
        }
        else if (questNo >= 2)
        {
            batsSlainPanel.SetActive(false);
        }
    }

    public void CompleteQuest(float questNo)
    {
        questState = 0;
        this.questNo++;

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