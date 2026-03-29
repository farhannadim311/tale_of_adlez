using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    bool playerDetect = false;

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
        if (DialougeCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                DialougeCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
            return;
        }

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