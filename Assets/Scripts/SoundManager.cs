using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource FXSource;

    public AudioClip townMusic;
    public AudioClip questMusic1;
    public AudioClip questMusic2;
    public AudioClip questMusic3;
    public AudioClip questMusic4;
    public AudioClip questMusic5;

    public AudioClip SwordSwing;
    public AudioClip DoorOpen;
    public AudioClip EnemyHit; 
    public AudioClip QuestCompleted;
    public AudioClip QuestProgress;
    public AudioClip QuestObtained;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource.clip = townMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetMusicState(int state)
    {
        Debug.Log("Music state: " + state + " QuestNo: " + GameManager.Instance.questNo);

        AudioClip newClip = null;

        if (state == 0)
        {
            newClip = townMusic;
        }
        else if (state == 1)
        {
            int q = GameManager.Instance.questNo;

            switch (q)
            {
                case 0: newClip = questMusic1; break;
                case 1: newClip = questMusic2; break;
                case 2: newClip = questMusic3; break;
                case 3: newClip = questMusic4; break;
                case 4: newClip = questMusic5; break;
                default: newClip = questMusic1; break;
            }
        }

        if (newClip == null) return;
        if (musicSource.clip == newClip) return;

        musicSource.clip = newClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySwordSwing()
    {
        FXSource.PlayOneShot(SwordSwing);
    }

    public void PlayDoorOpen()
    {
        FXSource.PlayOneShot(DoorOpen);
    }

    public void PlayEnemyHit()
    {
        FXSource.PlayOneShot(EnemyHit);
    }

    public void PlayQuestCompleted()
    {
        FXSource.PlayOneShot(QuestCompleted);
    }
    public void PlayQuestProgress()
    {
        FXSource.PlayOneShot(QuestProgress);
    }
    public void PlayQuestObtained()
    {
        FXSource.PlayOneShot(QuestObtained);
    }
}