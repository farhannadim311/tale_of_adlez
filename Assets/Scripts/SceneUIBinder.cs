using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneUIBinder : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventoryPanel;
    public GameObject returnToVillagePanel;
    public GameObject hintPanel;

    public Volume volume; 

    void Start()
    {
        GameManager.Instance.pauseMenu = pauseMenu;
        GameManager.Instance.InventoryPanel = inventoryPanel;
        GameManager.Instance.ReturnToVillagePanel = returnToVillagePanel;
        GameManager.Instance.HintPanel = hintPanel;

       
        GameManager.Instance.BindVolume(volume);
    }
}