using UnityEngine;
using UnityEngine.Rendering;

public class SceneUIBinder : MonoBehaviour
{
    //retanining object references on scene change
    public GameObject pauseMenu;
    public GameObject inventoryPanel;
    public GameObject returnToVillagePanel;
    public GameObject hintPanel;
    public GameObject controlsPanel;

    public QuestTwo questTwoUI;  
    public Volume volume;

    void Start()
    {
        // Pause UI
        GameManager.Instance.pauseMenu = pauseMenu;
        GameManager.Instance.InventoryPanel = inventoryPanel;
        GameManager.Instance.ReturnToVillagePanel = returnToVillagePanel;
        GameManager.Instance.HintPanel = hintPanel;
        GameManager.Instance.controlsPanel = controlsPanel;

        
        GameManager.Instance.questTwoUI = questTwoUI;

        // Volume
        GameManager.Instance.BindVolume(volume);
    }
}