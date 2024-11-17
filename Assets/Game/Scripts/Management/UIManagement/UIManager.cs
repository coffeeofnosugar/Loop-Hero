using System;
using System.Linq;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Coffee.Core.Management.UIManagement
{
    public abstract class BaseUI : MonoBehaviour
    {
        public virtual void Initialized() {  }
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