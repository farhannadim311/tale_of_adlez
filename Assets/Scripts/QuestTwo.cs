using TMPro;
using UnityEngine;

public class QuestTwo : MonoBehaviour
{
    public int batsKilled = 0;
    public TextMeshProUGUI batsSlainText;
    public GameObject QuestCompleteNotifier;

    void Update()
    {
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
        batsSlainText.text = "Bats Slain: " + batsKilled;

        if (batsKilled >= 7)
        {
            SoundManager.Instance.PlayQuestProgress();
            GameManager.Instance.questState = 2;
            QuestCompleteNotifier.SetActive(true);
        }
    }
}