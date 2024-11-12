using System;
using Coffee.Management;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Coffee
{
    public class GroundGenerator : MonoBehaviour
    {
        public GameObject groundPrefab;
        
        private void Awake()
        {
            LuaManager.Instance.DoLuaFile("Assets/Game/LuaScripts/Main.lua.txt");
            // groundPrefab = Addressables.LoadAssetAsync<GameObject>("Assets/Game/Prefabs/Ground.prefab").Result;
            // Debug.Log(groundPrefab);
        }

        private void Update()
        {
            // Debug.Log(groundPrefab);
        }
    }
}