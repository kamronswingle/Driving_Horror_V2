using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private Canvas endScreenCanvas;    
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private Button restartButton;
    
    void Start()
    {
        endScreenCanvas.enabled = false;
        endScreenPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.endKey.wasPressedThisFrame)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        endScreenCanvas.enabled = true;
        endScreenPanel.SetActive(true);
    }

    public void RestartClicked()
    {
        ResetScene();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
