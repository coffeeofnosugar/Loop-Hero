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
            float sceneLoadWeight = .7f;
            float levelStartWeight = .3f;
            
            float totalProgress = 0f;
            
            AsyncOperation sceneLoadTask = SceneManager.LoadSceneAsync("Game");

            while (!sceneLoadTask.isDone)
            {
                float sceneProgress = sceneLoadTask.progress;
                totalProgress = sceneProgress * sceneLoadWeight;
                UpdateProgressBar(totalProgress);
                if (sceneLoadTask.progress >= .9f)
                    sceneLoadTask.allowSceneActivation = true;
                await UniTask.Yield();
            }

            UniTask levelStartTask = LevelManager.Instance.InitLevel();
            while (true)
            {
                if (levelStartTask.AsValueTask().IsCompleted)
                {
                    float levelStartProgress = 1f;
                    totalProgress = sceneLoadWeight + levelStartProgress * levelStartWeight;
                    UpdateProgressBar(totalProgress);
                    break;
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