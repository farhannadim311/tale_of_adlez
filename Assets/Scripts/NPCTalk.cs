using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    bool playerDetect = false;

    public bool debugCompleteQuest;

    public GameObject PressButton;
    public GameObject DialougeCanvas;

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
        if (DialougeCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                Time.timeScale = 1f;

                // COMPLETE QUEST ONCE
                if (debugCompleteQuest)
                {
                    GameManager.Instance.CompleteQuest();
                }
            }
            return;
        }

        // Player nearby
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressButton.SetActive(false);
                DialougeCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}