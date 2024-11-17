using System.Collections.Generic;
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

    [DefaultExecutionOrder(-10000)]
    public class UIManager : Singleton<UIManager>
    {
        private Dictionary<string, UIBase> uiElements;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            UIBase[] uis = GetComponentsInChildren<UIBase>();
            uiElements = new Dictionary<string, UIBase>(uis.Length);
            foreach (var ui in uis)
            {
                uiElements.Add(ui.GetType().Name, ui);
            }
        }
        
        public static T GetUI<T>() where T : UIBase
        {
            var ui = Instance.uiElements[typeof(T).Name] as T;
            Debug.Assert(ui.gameObject.activeInHierarchy, $"{typeof(T)} 未激活");
            return ui;
        }
        
        public static T ShowUI<T>(bool withAnim = true, float duration = .2f) where T : UIBase
        {
            var ui = Instance.uiElements[typeof(T).Name] as T;
            ui.gameObject.SetActive(true);
            if (withAnim)
            {
                var canvasGroup = ui.GetComponent<CanvasGroup>();
                canvasGroup.alpha = 0f;
                canvasGroup.DOFade(1f, duration);
            }
            return ui;
        }
        
        public static T HideUI<T>(bool withAnim = true, float duration = 1f) where T : UIBase
        {
            var ui = Instance.uiElements[typeof(T).Name] as T;
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
    }
}