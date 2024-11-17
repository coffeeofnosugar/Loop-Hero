using System.Linq;
using Tools;
using UnityEngine;

namespace Coffee.Core.Management.UIManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseUI : MonoBehaviour
    {
        [SerializeField] private bool _awakeActive;      // 是否在Awake时激活
        public virtual void Initialized() {  }
        public virtual BaseUI Show() { gameObject.SetActive(true); return this; }
        public virtual BaseUI Hide() { gameObject.SetActive(false); return this; }
        
        protected virtual void Awake()
        {
            if (_awakeActive) Show(); else Hide();
        }
    }

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private BaseUI[] uiElements;
        
        public static T GetUI<T>() where T : BaseUI
        {
            return Instance.uiElements.First(ui => ui.GetType() == typeof(T)) as T;
        }

        private void OnValidate()
        {
            uiElements = GetComponentsInChildren<BaseUI>();
        }
    }
}