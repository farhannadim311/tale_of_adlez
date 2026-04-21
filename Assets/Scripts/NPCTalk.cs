using UnityEngine;

//normal NPCS should have quest of -1 to avoid conflict with quest giving NPCS

public class NPCTalk : MonoBehaviour
{
    bool playerDetect = false;

    public bool debugCompleteAllQuest;
    public int questNumber;

    public bool Boss = false;

    public GameObject BossFightObject;
    public GameObject Blocker;

    public GameObject PressButton;
    public GameObject DialougeCanvas;
    public GameObject GiveQuestCanvas;
    public GameObject QuestCompleteCanvas;

    private bool bossTriggered = false;

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
            playerDetect = true;
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
        //close dialouge
        if (DialougeCanvas.activeSelf || GiveQuestCanvas.activeSelf || QuestCompleteCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                GiveQuestCanvas.SetActive(false);
                QuestCompleteCanvas.SetActive(false);

                Time.timeScale = 1f;

                if (debugCompleteAllQuest)
                    GameManager.Instance.CompleteQuest(questNumber);

               //boss trigger once
                if (Boss && !bossTriggered)
                {
                    bossTriggered = true;

                    if (BossFightObject != null)
                        BossFightObject.SetActive(true);
                        Blocker.SetActive(true);

                    Destroy(gameObject);
                }
            }

            return;
        }

       //interact
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressButton.SetActive(false);

                if (questNumber != GameManager.Instance.questNo)
                {
                    DialougeCanvas.SetActive(true);
                    Time.timeScale = 0f;
                }
                else if (GameManager.Instance.questState == 0 || GameManager.Instance.questState == 1)
                {
                    GiveQuestCanvas.SetActive(true);
                    Time.timeScale = 0f;

                    if (GameManager.Instance.questState == 0)
                    {
                        GameManager.Instance.questState = 1;
                        SoundManager.Instance.PlayQuestObtained();
                    }
                }
                else if (GameManager.Instance.questState == 2)
                {
                    QuestCompleteCanvas.SetActive(true);
                    Time.timeScale = 0f;

                    SoundManager.Instance.PlayQuestCompleted();
                    GameManager.Instance.CompleteQuest(questNumber);
                }
            }
        }
        else
        {
            PressButton.SetActive(false);
        }
    }
}