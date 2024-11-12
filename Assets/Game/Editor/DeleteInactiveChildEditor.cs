using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DeleteInactiveChildEditor : OdinEditorWindow
    {
        [MenuItem("Tools/Object/DeleteInactiveObj")]
        private static void OpenWindow()
        {
            GetWindow<DeleteInactiveChildEditor>().Show();
        }
        
        [SceneObjectsOnly, Required]
        [SerializeField] private GameObject targetObj;
        
        [Button]
        private void DeleteInactiveChild()
        {
            if (targetObj == null) return;

            DeleteInactiveChild(targetObj);
        }
        
        private void DeleteInactiveChild(GameObject obj)
        {
            if (obj == null) return;
            for (int i = obj.transform.childCount - 1; i >= 0; i--)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                if (!child.activeInHierarchy)
                {
                    DestroyImmediate(child);
                }
                else
                {
                    DeleteInactiveChild(child);
                }
            }
        }
    }
}