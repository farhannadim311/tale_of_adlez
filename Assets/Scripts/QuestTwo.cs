using TMPro;
using UnityEngine;

public class QuestTwo : MonoBehaviour
{
    public int batsKilled = 0;
    public TextMeshProUGUI batsSlainText;
    public GameObject QuestCompleteNotifier;

    void Update()
    {
        // UI visibility logic (owns itself)
        if (GameManager.Instance.questNo == 1 && GameManager.Instance.questState == 1)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
        else if (GameManager.Instance.questNo >= 2)
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

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

    public void AddKill()
    {
        batsKilled++;
        batsSlainText.text = "Foes Slain: " + batsKilled;

        if (batsKilled >= 7)
        {
            SoundManager.Instance.PlayQuestProgress();
            GameManager.Instance.questState = 2;
            QuestCompleteNotifier.SetActive(true);
        }
    }
}