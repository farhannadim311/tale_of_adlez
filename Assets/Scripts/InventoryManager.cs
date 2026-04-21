using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int bombs = 0;
    public int arrows = 0;
    public int potions = 0;
    public int coins = 0;

    private const int MAX_ITEMS = 99;

    public TMP_Text bombText;
    public TMP_Text arrowText;
    public TMP_Text potionText;
    public TMP_Text coinText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (bombText) bombText.text = bombs.ToString();
        if (arrowText) arrowText.text = arrows.ToString();
        if (potionText) potionText.text = potions.ToString();
        if (coinText) coinText.text = coins.ToString();
    }

    int Clamp(int value)
    {
        return Mathf.Clamp(value, 0, MAX_ITEMS);
    }

    public void AddBomb(int amount = 1)
    {
        bombs = Clamp(bombs + amount);
        UpdateUI();
    }

    public void AddCoin(int amount = 1) //it will always be 1
    {
        coins = Clamp(coins + amount);
        UpdateUI();
    }

    public void AddArrow(int amount = 1)
    {
        arrows = Clamp(arrows + amount);
        Debug.Log($"Arrows: {arrows}");
        UpdateUI();
    }

    public void AddPotion(int amount = 1)
    {
        potions = Clamp(potions + amount);
        UpdateUI();
    }

    public void TakeCoins(int amount) 
    {
        coins = coins - amount;
        UpdateUI();
    }

    public bool UseArrow()
    {
        if (arrows <= 0) return false;

        arrows--;
        UpdateUI();
        return true;
    }

    public bool UseBomb()
    {
        if (bombs <= 0) 
        {
        return false;
        }

        bombs--;
        UpdateUI();
        return true;
    }
}