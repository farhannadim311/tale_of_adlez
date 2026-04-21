using UnityEngine;
using UnityEngine.SceneManagement;

//reset all quest progress on GameOver or GameEnd
public class ResetGame : MonoBehaviour
{

    public void Reset()
    {
        GameManager.Instance.questNo = 0;
        GameManager.Instance.questState = 0;
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
    }




}
