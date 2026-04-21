using UnityEngine;

public class SoundCooldownPlayer : MonoBehaviour
{

    public float cooldown = 2f;

    private float lastPlayTime = -500f;

    public void PlaySound()
    {
        if (Time.time < lastPlayTime + cooldown)
            return;

        SoundManager.Instance.PlayQuestObtained();
        lastPlayTime = Time.time;
    }
}