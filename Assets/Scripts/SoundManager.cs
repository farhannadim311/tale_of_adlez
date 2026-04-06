using UnityEngine;
using UnityEngine.SceneManagement;

public class soundManager : MonoBehaviour
{
    public static soundManager smInstance;

    public AudioSource musicSource; 
    public AudioSource fxSource;

    public AudioClip titleScreenMusic;

    public AudioClip VillageOverWorldMusic;


    private AudioClip currentSong;

    void Awake()
    {
        if (smInstance == null)
        {
            smInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (smInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip newClip = null;

        switch (scene.name)
        {
            
            case "Scene_VillageOverworld":
                newClip = VillageOverWorldMusic;
                break;
        }

        if (newClip != currentSong)
        {
            currentSong = newClip;
            musicSource.clip = currentSong;
            musicSource.Play();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}