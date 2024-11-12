using Coffee.Management;
using UnityEngine;

namespace Coffee
{
    public class Test : MonoBehaviour
    {
        private void Awake()
        {
            var a = LuaManager.Instance.Global.Get<float>("a");
            Debug.Log(a);
        }
    }
}