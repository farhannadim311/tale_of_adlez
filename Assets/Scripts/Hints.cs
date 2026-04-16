using UnityEngine;
using TMPro;

public class Hints : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    void OnEnable()
    {
        UpdateHint(); 
    }

    void UpdateHint()
    {
        if (GameManager.Instance == null) return;

        string newText = "";



        //QUEST 1 HINTS
        if (GameManager.Instance.questNo == 0 && GameManager.Instance.questState == 0)
        {
            newText = "Get started by talking to villagers to see what they need. \n\nTry checking inside one of the wooden houses in the center of Town.";
        }
        else if (GameManager.Instance.questNo == 0 && GameManager.Instance.questState == 1)
        {
            newText = "The Twisted Forest is SOUTH of the village in the Wilderness. \n\n Follow down the main path until you hit the four-way intersection, then head WEST. \n\n Try to find a pumpkin!";
        }
        else if (GameManager.Instance.questNo == 0 && GameManager.Instance.questState == 2)
        {
            newText = "Great Job! \n\n Return the pumpkin to the lady to complete the quest!";
        }






        //QUEST 2 HINTS
        else if (GameManager.Instance.questNo == 1 && GameManager.Instance.questState == 0)
        {
            newText = "A man gardening near the edge of Town has a problem!";
        }
        else if (GameManager.Instance.questNo == 1 && GameManager.Instance.questState == 1)
        {
            newText = "Go back to the Wilderness and slay 7 bats with your WEAPONS for the gardner!";
        }
        else if (GameManager.Instance.questNo == 1 && GameManager.Instance.questState == 2)
        {
            newText = "Great Job! \n\n Return to the gardener to complete the quest!";
        }

        hintText.text = newText;
    }
}