using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    public void Reset()
    {
        GameManager.Instance.questNo = 0;
        GameManager.Instance.questState = 0;
        SceneManager.LoadScene("StartScene");
    }




}
