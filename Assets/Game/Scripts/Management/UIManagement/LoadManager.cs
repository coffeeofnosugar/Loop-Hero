using Coffee.Core.MapManagement;
using Cysharp.Threading.Tasks;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Coffee.Management.UIManagement
{
    public class LoadManager : PersistentSingleton<LoadManager>
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI text;
        
        private void UpdateProgressBar(float progress)
        {
            slider.value = progress;
            text.text = progress >= 1f ? "点击任意位置继续" : $"{progress * 100}%";
        }


        public async UniTask LoadGameScene()
        {
            panel.SetActive(true);
            slider.value = 0;
            
            AsyncOperation sceneLoadTask = SceneManager.LoadSceneAsync("Game");

            while (!sceneLoadTask.isDone)
            {
                UpdateProgressBar(sceneLoadTask.progress);
                if (sceneLoadTask.progress >= .9f)
                {
                    UpdateProgressBar(1f);
                    sceneLoadTask.allowSceneActivation = true;
                }
                await UniTask.Yield();
            }

            while (true)
            {
                if (Input.anyKey)
                    break;
                await UniTask.Yield();
            }
            
            panel.SetActive(false);
            LevelManager.Instance.StartGame();
        }
    }
}