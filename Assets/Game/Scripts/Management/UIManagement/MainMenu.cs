using Coffee.Core.FightManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.Core.Management.UIManagement
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
            startButton.onClick.AddListener(NewGame);
            settingsButton.onClick.AddListener(Settings);
            exitButton.onClick.AddListener(Exit);
        }

        private void Continue()
        {
            GameEvent.Trigger(GameEventTypes.ContinueGame);
        }

        private void NewGame()
        {
            GameEvent.Trigger(GameEventTypes.NewGame);
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