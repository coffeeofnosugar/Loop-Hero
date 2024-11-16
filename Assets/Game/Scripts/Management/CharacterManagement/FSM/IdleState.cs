using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class IdleState : CharacterState
    {
        [SerializeField] private TransitionAsset _animation;

        private void OnEnable()
        {
            character.Animancer.Play(_animation);
        }
    }
}