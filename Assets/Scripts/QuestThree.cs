using UnityEngine;

public class QuestThree : MonoBehaviour
{
    public static int hitCount = 0; // shared across all 3 objects
    public GameObject QuestCompleteNotifier;

    private bool alreadyHit = false;

    void OnTriggerEnter2D(Collider2D collision)
{
    
    if (collision.gameObject.CompareTag("Arrow") && !alreadyHit && GameManager.Instance.questNo ==2 && GameManager.Instance.questState == 1)
    {
        Destroy(collision.gameObject); 
        SoundManager.Instance.PlayTotemNoise();
        hitCount++;
        alreadyHit = true;

        if(hitCount == 3)
            {
                GameManager.Instance.questState = 2; // Mark complete
                SoundManager.Instance.PlayQuestProgress();
                QuestCompleteNotifier.SetActive(true);
            }

        
    }
}

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
}