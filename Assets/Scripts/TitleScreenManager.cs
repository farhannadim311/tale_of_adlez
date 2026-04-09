using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup titleGroup;       
    public CanvasGroup menuGroup;       
    public CanvasGroup pressAnyKeyGroup; 

    [Header("Settings")]
    public string gameSceneName = "Scene_VillageOverworld";
    public float fadeInDuration = 1.5f;
    public float titleDelay = 0.5f;
    public float pulseSpeed = 1.2f;

    private bool waitingForKey = true;
    private bool isTransitioning = false;

    void Start()
    {
        if (titleGroup != null) titleGroup.alpha = 0f;
        if (menuGroup != null)
        {
            menuGroup.alpha = 0f;
            menuGroup.interactable = false;
            menuGroup.blocksRaycasts = false;
        }
        if (pressAnyKeyGroup != null) pressAnyKeyGroup.alpha = 0f;

        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        // Wait a moment
        yield return new WaitForSeconds(titleDelay);

        // Fade in the title
        yield return StartCoroutine(FadeCanvasGroup(titleGroup, 0f, 1f, fadeInDuration));

        // Small pause
        yield return new WaitForSeconds(0.3f);

        // Fade in "Press Any Key"
        yield return StartCoroutine(FadeCanvasGroup(pressAnyKeyGroup, 0f, 1f, 0.8f));

        // Start pulsing the "Press Any Key" text
        StartCoroutine(PulseCanvasGroup(pressAnyKeyGroup));
    }

    void Update()
    {
        if (isTransitioning) return;

        if (waitingForKey)
        {
            if (Input.anyKeyDown)
            {
                waitingForKey = false;
                StartCoroutine(ShowMenu());
            }
        }
    }

    IEnumerator ShowMenu()
    {
        // Fade out "Press Any Key"
        StopCoroutine(nameof(PulseCanvasGroup));
        yield return StartCoroutine(FadeCanvasGroup(pressAnyKeyGroup, pressAnyKeyGroup.alpha, 0f, 0.3f));
        pressAnyKeyGroup.gameObject.SetActive(false);

        // Fade in the menu buttons
        menuGroup.interactable = true;
        menuGroup.blocksRaycasts = true;
        yield return StartCoroutine(FadeCanvasGroup(menuGroup, 0f, 1f, 0.6f));
    }

   

    public void OnStartGame()
    {
        if (isTransitioning) return;
        isTransitioning = true;
        StartCoroutine(TransitionToGame());
    }

    public void OnQuitGame()
    {
        if (isTransitioning) return;
        isTransitioning = true;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator TransitionToGame()
    {
        StartCoroutine(FadeCanvasGroup(titleGroup, 1f, 0f, 0.8f));
        yield return StartCoroutine(FadeCanvasGroup(menuGroup, 1f, 0f, 0.8f));

        yield return new WaitForSeconds(0.3f);

        SceneManager.LoadScene(gameSceneName);
    }

   

    IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
    {
        if (group == null) yield break;

        float elapsed = 0f;
        group.alpha = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        group.alpha = to;
    }

    IEnumerator PulseCanvasGroup(CanvasGroup group)
    {
        if (group == null) yield break;

        while (true)
        {
            float alpha = 0.4f + 0.6f * Mathf.Abs(Mathf.Sin(Time.time * pulseSpeed));
            group.alpha = alpha;
            yield return null;
        }
    }
}
