using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class AttackState : CharacterState
    {
        [SerializeField] private TransitionAsset _animation;

        private void OnEnable()
        {
            var anim = character.Animancer.Play(_animation);
            anim.Events(this).OnEnd ??= character.StateMachine.ForceSetDefaultState;
        }
    }
}