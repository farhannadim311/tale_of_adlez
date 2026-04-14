using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    bool playerDetect = false;

    public bool debugCompleteAllQuest;
    public int questNumber;


    public GameObject PressButton;
    public GameObject DialougeCanvas; //REGULAR DIALOUGE
    public GameObject GiveQuestCanvas; //Give a quest
    public GameObject QuestCompleteCanvas; //complete a quest dialouge

    void Start()
    {
        PressButton.SetActive(false);
        DialougeCanvas.SetActive(false);
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
        }
    }

    void Update()
    {
        // If dialogue is open
        if (DialougeCanvas.activeSelf || GiveQuestCanvas.activeSelf || QuestCompleteCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                GiveQuestCanvas.SetActive(false);
                QuestCompleteCanvas.SetActive(false);
                Time.timeScale = 1f;

                // COMPLETE QUEST ONCE
                if (debugCompleteAllQuest)
                {
                    GameManager.Instance.CompleteQuest(questNumber);
                }

            }
            return;
        }

        // Player nearby
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space)){
                if(questNumber != GameManager.Instance.questNo) //normal dialogue
                {
                PressButton.SetActive(false);
                DialougeCanvas.SetActive(true);
                Time.timeScale = 0f;
                }

                if(questNumber == GameManager.Instance.questNo && GameManager.Instance.questState == 0) //give quest
                {
                    PressButton.SetActive(false);
                    GiveQuestCanvas.SetActive(true);
                    Time.timeScale = 0f;
                    GameManager.Instance.questState = 1; //quest accepted
                }
                if(questNumber == GameManager.Instance.questNo && GameManager.Instance.questState == 2) //complete quest
                {
                    PressButton.SetActive(false);
                    QuestCompleteCanvas.SetActive(true);
                    Time.timeScale = 0f;
                    GameManager.Instance.CompleteQuest(questNumber);
                }

            }
        }
    }
}