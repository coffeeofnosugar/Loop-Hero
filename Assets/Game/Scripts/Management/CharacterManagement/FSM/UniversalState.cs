using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class UniversalState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;
        [SerializeField] private bool isOnce = false;

        private void OnEnable()
        {
            var anim = character.Animancer.Play(animation);
            if (isOnce)
                anim.Events(this).OnEnd ??= character.StateMachine.ForceSetDefaultState;
        }
    }
}