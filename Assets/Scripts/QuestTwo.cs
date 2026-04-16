using TMPro;
using UnityEngine;

public class QuestTwo : MonoBehaviour
{
    public int batsKilled = 0;
    public TextMeshProUGUI batsSlainText;

    public void AddKill()
    {
        batsKilled++;
        batsSlainText.text = "Bats Slain: " + batsKilled;

        if (batsKilled >= 7)
        {
            SoundManager.Instance.PlayQuestProgress();
            GameManager.Instance.questState = 2; //mark as completed
            batsSlainText.text = "Bats Slain: " + batsKilled + "\nQuest Complete! Return back to the man!";
        }

    }
}