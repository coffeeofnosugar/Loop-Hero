using Sirenix.OdinInspector;

namespace Coffee.Core.CharacterManagement
{
    [System.Serializable]
    [HideLabel, FoldoutGroup("Character Data")]
    public class HeroData : CharacterData
    {
        public int Level;
        public int Experience;
    }
}