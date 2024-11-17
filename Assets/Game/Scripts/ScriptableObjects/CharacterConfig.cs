using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee
{
    [CreateAssetMenu(menuName = "ScriptableObjects/CharacterConfig")]
    [InlineEditor]
    public class CharacterConfig : SerializedScriptableObject
    {
        public int MaxHealth;
    }
}