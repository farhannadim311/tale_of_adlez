using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    bool playerDetect = false;

    public bool debugCompleteAllQuest;
    public int questNumber;

    public GameObject PressButton;
    public GameObject DialougeCanvas;
    public GameObject GiveQuestCanvas;
    public GameObject QuestCompleteCanvas;

    void Start()
    {
        PressButton.SetActive(false);
        DialougeCanvas.SetActive(false);
        GiveQuestCanvas.SetActive(false);
        QuestCompleteCanvas.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetect = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetect = false;
            PressButton.SetActive(false);
            DialougeCanvas.SetActive(false);
            GiveQuestCanvas.SetActive(false);
            QuestCompleteCanvas.SetActive(false);
        }
    }

    void Update()
    {
        // =========================
        // CLOSE DIALOGUE
        // =========================
        if (DialougeCanvas.activeSelf || GiveQuestCanvas.activeSelf || QuestCompleteCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                GiveQuestCanvas.SetActive(false);
                QuestCompleteCanvas.SetActive(false);
                Time.timeScale = 1f;

                if (debugCompleteAllQuest)
                {
                    GameManager.Instance.CompleteQuest(questNumber);
                }
            }
            return;
        }

        // =========================
        // INTERACTION
        // =========================
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressButton.SetActive(false);

                // =========================
                // NORMAL DIALOGUE
                // =========================
                if (questNumber != GameManager.Instance.questNo)
                {
                    DialougeCanvas.SetActive(true);
                    Time.timeScale = 0f;
                }

                // =========================
                // QUEST ACCEPT / ACTIVE
                // =========================
                else if (GameManager.Instance.questState == 0 || GameManager.Instance.questState == 1)
                {
                    GiveQuestCanvas.SetActive(true);
                    Time.timeScale = 0f;

                    // ONLY ON FIRST ACCEPT
                    if (GameManager.Instance.questState == 0)
                    {
                        GameManager.Instance.questState = 1;
                        SoundManager.Instance.PlayQuestObtained();
                    }
                }

                // =========================
                // QUEST COMPLETE
                // =========================
                else if (GameManager.Instance.questState == 2)
                {
                    QuestCompleteCanvas.SetActive(true);
                    Time.timeScale = 0f;

                    SoundManager.Instance.PlayQuestCompleted();
                    GameManager.Instance.CompleteQuest(questNumber);
                }
            }
        }
    }
}