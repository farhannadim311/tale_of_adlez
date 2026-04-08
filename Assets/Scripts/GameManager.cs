using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int questState = 0; // 0 = none, 1 = accepted, 2 = completed
    public int questNo = 0;

    [Header("Post Processing")]
    public VolumeProfile volumeProfile;

    private ColorAdjustments colorAdjustments;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Try to get ColorAdjustments from profile
        if (volumeProfile != null)
        {
            volumeProfile.TryGet(out colorAdjustments);
        }

        colorAdjustments.saturation.value = -100f;;
    }

    public void CompleteQuest()
    {
        questState = 2;
        questNo++;

        IncreaseSaturation(50f);
    }

    void IncreaseSaturation(float amount)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value += amount;
        }
    }
}//