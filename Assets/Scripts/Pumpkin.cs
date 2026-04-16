using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    bool playerDetect = false;

    public GameObject PressButton;
    public GameObject DialougeCanvas;

        public GameObject Description;

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
            Description.SetActive(false);
        }
    }

    void Update()
    {
        // If dialogue is open
        if (DialougeCanvas.activeSelf || Description.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                Description.SetActive(false);
                Time.timeScale = 1f;

                if (GameManager.Instance.questState == 2)
                {
                    SoundManager.Instance.PlayQuestProgress();
                    GameObject.Destroy(gameObject);
                }
            }
            return;
        }

        // Player nearby
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (GameManager.Instance.questNo == 0 && GameManager.Instance.questState == 1)
            {

                if (Input.GetKeyDown(KeyCode.Space)) //progress quest
                {
                    PressButton.SetActive(false);
                    DialougeCanvas.SetActive(true);
                    Time.timeScale = 0f;
                    GameManager.Instance.questState = 2;
                  
                }
                    
            } //if first quest and accepted:

            else if (Input.GetKeyDown(KeyCode.Space)) //if quest not accepted, just descibe pumpkin
                {
                   PressButton.SetActive(false); 
                   Description.SetActive(true);
                   Time.timeScale = 0f;
                }

            
        }
    }
}