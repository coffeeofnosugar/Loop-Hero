using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class MoveState : CharacterState
    {
        [SerializeField] public TransitionAsset _animation;

        private void OnEnable()
        {
            character.Brain.movement.TargetValue = character.SplineAnimate.MaxSpeed;
            character.Animancer.Play(_animation);
            character.SplineAnimate.Play();
        }

        private void OnDisable()
        {
            character.Brain.movement.TargetValue = 0f;
            character.SplineAnimate.Pause();
        }
    }
}