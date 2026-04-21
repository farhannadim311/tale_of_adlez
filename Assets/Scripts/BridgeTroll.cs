using UnityEngine;
//pay him 5 coins then he dies
public class BridgeTroll : MonoBehaviour
{
    private bool playerDetect = false;

    public GameObject PressButton; //space bar icon
    public GameObject refusalPanel; 
    public GameObject acceptancePanel;

    public GameObject blockade; //invisble wall

    public int requiredCoins = 5; //toll

    private bool accepted = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetect = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetect = false;

            PressButton.SetActive(false);
            refusalPanel.SetActive(false);
            acceptancePanel.SetActive(false);

            Time.timeScale = 1f;
        }
    }

    void Update()
    {
        //unpause
        if (refusalPanel.activeSelf || acceptancePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                refusalPanel.SetActive(false);
                acceptancePanel.SetActive(false);

                Time.timeScale = 1f;

                
                //remove troll and blockade if you got 5 coins
                if (accepted)
                {
                    Destroy(gameObject);     
                    Destroy(blockade);      
                }
            }
            return;
        }



  
        if (playerDetect)
        {
            PressButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PressButton.SetActive(false);

                Time.timeScale = 0f;

                int coins = InventoryManager.Instance.coins;

                if (coins >= requiredCoins)
                {
                    acceptancePanel.SetActive(true); //oh yea u got 5 coins
                    refusalPanel.SetActive(false);
                    accepted = true; 
                    InventoryManager.Instance.TakeCoins(5);
                }
                else
                {
                    refusalPanel.SetActive(true); //you dont got no 5 coins
                    acceptancePanel.SetActive(false);
                    accepted = false;
                }
            }
        }
        else
        {
            PressButton.SetActive(false); 
        }
    }
}