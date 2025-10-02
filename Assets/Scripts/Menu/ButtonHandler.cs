using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmSettingsButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void StartClick()
    {
        // Currently kind of broken
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void QuitClick()
    {
        
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    public void SettingsClick()
    {
        settingsPanel.SetActive(true);
    }

    public void ConfirmSettingsClick()
    {
        settingsPanel.SetActive(false);
    }
}
