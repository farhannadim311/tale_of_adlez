using UnityEngine;

//must shoot all three different totems

public class QuestThree : MonoBehaviour
{
    public static int hitCount = 0; //so hit count will be shared amoungth the three totems. Must find all and hit them.

    public GameObject QuestCompleteNotifier;

    // NEW UI
    public GameObject PressButton;
    public GameObject DialogueCanvas;

    private bool alreadyHit = false;
    private bool playerDetect = false;

    void Start()
    {
        PressButton.SetActive(false);
       DialogueCanvas.SetActive(false);
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
            DialogueCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ArrowShot") && !alreadyHit &&
            GameManager.Instance.questNo == 2 && GameManager.Instance.questState == 1)
        {
            Destroy(collision.gameObject);
            SoundManager.Instance.PlayTotemNoise();

            hitCount++;
            alreadyHit = true;

            if (hitCount == 3)
            {
                GameManager.Instance.questState = 2;
                SoundManager.Instance.PlayQuestProgress();
                QuestCompleteNotifier.SetActive(true);
            }
        }
    }

    void Update()
    {
        //interact with
        if (playerDetect)
        {
            if (PressButton != null) PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PressButton != null) PressButton.SetActive(false);
                if (DialogueCanvas != null) DialogueCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        
        if (DialogueCanvas != null && DialogueCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialogueCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        // quest completed
        if (QuestCompleteNotifier.activeSelf)
        {
            Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.B))
            {
                QuestCompleteNotifier.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}