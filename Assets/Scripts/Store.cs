using UnityEngine;

public class Store : MonoBehaviour
{
    

    public void BuyBomb()
    {
        int coins = InventoryManager.Instance.coins;
        if (coins < 1)
        {
            SoundManager.Instance.PlayTakeDamage();//temp prohibit sound
            return;
        }
        else
        {
            InventoryManager.Instance.AddBomb(1);
            InventoryManager.Instance.TakeCoins(1);
            SoundManager.Instance.PlayItemGet();
            return;
        }

    }

    public void BuyArrow()
    {
        int coins = InventoryManager.Instance.coins;
        if (coins < 1)
        {
            SoundManager.Instance.PlayTakeDamage();
            return;
        }
        else
        {
            InventoryManager.Instance.AddArrow(1);
            InventoryManager.Instance.TakeCoins(1);
            SoundManager.Instance.PlayItemGet();
            return;
        }

    }

    public void BuyPotion()
    {
        int coins = InventoryManager.Instance.coins;
        if (coins < 1)
        {
            SoundManager.Instance.PlayTakeDamage();
            return;
        }
        else
        {
            InventoryManager.Instance.AddPotion(1);
            InventoryManager.Instance.TakeCoins(1);
            SoundManager.Instance.PlayItemGet();
            return;
        }

    }




}
