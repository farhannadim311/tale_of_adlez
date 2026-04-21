using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip totemNoise;
    public AudioClip ArrowShoot;
    public AudioClip TitleScreenMusic;
    public AudioClip WeirdHouseMusic;
    public AudioClip GameOverMusic;
    public AudioClip DrinkPotion;
    public AudioClip BombExplode;
    public AudioClip BossMusic;
    public AudioClip Win;

    public AudioClip ItemGet;

    public AudioClip SwordSwing;
    public AudioClip DoorOpen;
    public AudioClip EnemyHit; 
    public AudioClip QuestCompleted;
    public AudioClip QuestProgress;
    public AudioClip QuestObtained;
    public AudioClip TakeDamage;

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

       void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    switch (scene.name)
    {
        case "StartScene":
            SetMusicState(2);
            break;

        case "GameOver":
            SetMusicState(3);
            break;
        
        case "WinScene":
            SetMusicState(6);
            break;

        case "Scene_VillageOverworld":
        default:
            SetMusicState(0);
            break;
    }
}
public void SetMusicState(int state)
{

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
            default: newClip = questMusic1; break;
        }
    }
    else if (state == 2)
    {
        newClip = TitleScreenMusic; // Title screen music
    }
    else if (state == 3)
    {
        newClip = GameOverMusic; // Game over music (swap later if you want)
    }
    else if (state ==4)
    {
        newClip = WeirdHouseMusic;
    }
    
    else if (state == 5)
        {
            newClip = BossMusic;
        }
    else if (state == 6)
        {
            newClip = Win;
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

    public void PlayTotemNoise()
    {
        FXSource.PlayOneShot(totemNoise);
    }

    public void PlayArrowShoot()
    {
        FXSource.PlayOneShot(ArrowShoot);
    }
    public void PlayTakeDamage()
    {
        FXSource.PlayOneShot(TakeDamage);
    }

    public void PlayItemGet()
    {
        FXSource.PlayOneShot(ItemGet);
    }

    public void PlayDrinkPotion()
    {
        FXSource.PlayOneShot(DrinkPotion);
    }

    public void PlayBombExplode()
    {
        FXSource.PlayOneShot(BombExplode);
    }
}