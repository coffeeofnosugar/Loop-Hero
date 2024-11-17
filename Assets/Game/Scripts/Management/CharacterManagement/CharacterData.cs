using Sirenix.OdinInspector;

namespace Coffee.Core.CharacterManagement
{
    /// <summary>
    /// 会随着游戏进程变化的角色数据
    /// </summary>
    [System.Serializable]
    [HideLabel, FoldoutGroup("Character Data")]
    public class CharacterData
    {
        public int health;
        public float attackSpeed;
    }
}