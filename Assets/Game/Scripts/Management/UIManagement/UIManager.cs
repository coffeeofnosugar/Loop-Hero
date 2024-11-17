using System.Linq;
using DG.Tweening;
using Tools;
using UnityEngine;

namespace Coffee.Core.Management.UIManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIBase : MonoBehaviour
    {
        [SerializeField] private bool _awakeActive;      // 是否在Awake时激活
        public virtual void Initialized() {  }
        
        protected virtual void Awake()
        {
            gameObject.SetActive(_awakeActive);
        }
    }

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private UIBase[] uiElements;
        
        public static T GetUI<T>() where T : UIBase
        {
            var ui = Instance.uiElements.First(ui => ui.GetType() == typeof(T)) as T;
            Debug.Assert(ui.gameObject.activeInHierarchy, $"{typeof(T)} 未激活");
            return ui;
        }
        
        public static T ShowUI<T>(bool withAnim = false, float duration = .2f) where T : UIBase
        {
            T ui = Instance.uiElements.First(ui => ui.GetType() == typeof(T)) as T;
            ui.gameObject.SetActive(true);
            if (withAnim)
            {
                var canvasGroup = ui.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, duration);
            }
            return ui;
        }
        
        public static T HideUI<T>(bool withAnim = false, float duration = 1f) where T : UIBase
        {
            T ui = Instance.uiElements.First(ui => ui.GetType() == typeof(T)) as T;
            if (withAnim)
            {
                var canvasGroup = ui.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1f;
                canvasGroup.DOFade(0f, duration).OnComplete(() => ui.gameObject.SetActive(false));
            }
            else
            {
                ui.gameObject.SetActive(false);
            }
            return ui;
        }

        private void OnValidate()
        {
            uiElements = GetComponentsInChildren<UIBase>();
        }
    }
}