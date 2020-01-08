using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel, LevelsPenel, UpgradePanel;
    private UpgradePointManager Upgrades;
    private void Start()
    {
        Upgrades = GetComponent<UpgradePointManager>();
    }
    public void ChooseLevel()
    {
        MainMenuPanel.SetActive(false);
        LevelsPenel.SetActive(true);
    }
    public void BackToMenu()
    {
        MainMenuPanel.SetActive(true);
        LevelsPenel.SetActive(false);
    }
    public void BackToMenuFromUpgrades()
    {
        MainMenuPanel.SetActive(true);
        UpgradePanel.SetActive(false);
        Upgrades.HideTurrets();
    }
    public void OpenUpgrades()
    {
        UpgradePanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        Upgrades.BasicTurret();
    }

    public void OpenStuLevel() { SceneManager.LoadScene("Stu"); Player.Instance.Canvas.SetActive(true); Player.Instance.WaveBox.SetActive(true); }
    public void OpenLeeLevel() { SceneManager.LoadScene("Lee"); Player.Instance.Canvas.SetActive(true); Player.Instance.WaveBox.SetActive(true); }
        
}
