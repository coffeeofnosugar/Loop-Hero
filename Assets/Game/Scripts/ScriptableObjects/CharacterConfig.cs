using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee
{
    /// <summary>
    /// 角色配置文件
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterConfig")]
    [InlineEditor]
    public class CharacterConfig : SerializedScriptableObject
    {
        public int MaxHealth;
        public int AttackBaseSpeed;
    }
}