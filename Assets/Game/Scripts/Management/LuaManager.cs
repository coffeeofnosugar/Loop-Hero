using Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using XLua;

namespace Coffee.Management
{
    public class LuaManager : PersistentHumbleSingleton<LuaManager>
    {
        private LuaEnv luaEnv;

        public LuaTable Global => luaEnv.Global;

        protected override void Awake()
        {
            base.Awake();
            if (luaEnv != null) return;
            luaEnv = new LuaEnv();
            luaEnv.AddLoader(CustomLoader);
        }
        
        private byte[] CustomLoader(ref string filepath)
        {
            AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(filepath);
            handle.WaitForCompletion();
            return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result.bytes : null;
        }
        
        public void DoLuaFile(string fileName)
        {
            string str = $"require('{fileName}')";
            DoString(str);
        }

        public void DoString(string str)
        {
            if (luaEnv == null) return;
            luaEnv.DoString(str);
        }

        public void Tick()
        {
            if (luaEnv == null) return;
            luaEnv.Tick();
        }

        public void OnDestroy()
        {
            if (luaEnv == null) return;
            luaEnv.Dispose();
            luaEnv = null;
        }
    }
}