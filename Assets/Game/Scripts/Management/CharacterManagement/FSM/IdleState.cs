using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class IdleState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;

        private void OnEnable()
        {
            character.MovementStats.Movement.TargetValue = 0f;
            character.Animancer.Play(animation);
            character.SplineAnimate.Pause();
        }
    }
}