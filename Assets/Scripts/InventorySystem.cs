using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public int bombCount = 0;
    public int arrowCount = 0;
    public int potionCount = 0;

    public TextMeshProUGUI bombCountText;
    public TextMeshProUGUI arrowCountText;
    public TextMeshProUGUI potionCountText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddBomb(int amount = 1)
    {
        bombCount += amount;
        UpdateUI();
    }

    public void AddArrow(int amount = 1)
    {
        arrowCount += amount;
        UpdateUI();
    }

    public void AddPotion(int amount = 1)
    {
        potionCount += amount;
        UpdateUI();
    }

    public bool UseBomb()
    {
        if (bombCount > 0)
        {
            bombCount--;
            UpdateUI();
            return true;
        }
        Debug.Log("No bombs left!");
        return false;
    }

    public bool UseArrow()
    {
        if (arrowCount > 0)
        {
            arrowCount--;
            UpdateUI();
            return true;
        }
        Debug.Log("No arrows left!");
        return false;
    }

    public bool UsePotion()
    {
        if (potionCount > 0)
        {
            potionCount--;
            UpdateUI();
            return true;
        }
        Debug.Log("No potions left!");
        return false;
    }

    void UpdateUI()
    {
        bombCountText.text = "x" + bombCount;
        arrowCountText.text = "x" + arrowCount;
        potionCountText.text = "x" + potionCount;
    }
}