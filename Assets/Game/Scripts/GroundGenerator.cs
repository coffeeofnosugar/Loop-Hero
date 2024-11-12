using System;
using Coffee.Management;
using UnityEngine;

namespace Coffee
{
    public class GroundGenerator : MonoBehaviour
    {
        private void Awake()
        {
            LuaManager.Instance.DoLuaFile("Assets/Game/LuaScripts/HelloWorld.lua.txt");
        }
    }
}