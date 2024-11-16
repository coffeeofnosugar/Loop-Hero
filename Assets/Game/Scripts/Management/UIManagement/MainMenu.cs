using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Coffee.Management.UIManagement
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button exitButton;
        
        private void Awake()
        {
            continueButton.onClick.AddListener(Continue);
            startButton.onClick.AddListener(StartGame);
            settingsButton.onClick.AddListener(Settings);
            exitButton.onClick.AddListener(Exit);
        }

        private void Continue()
        {
            
        }

        private void StartGame()
        {
            SceneManager.LoadScene("Game");
        }

        private void Settings()
        {
            
        }

        private void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}