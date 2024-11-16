using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class MoveState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;

        private void OnEnable()
        {
            character.MovementStats.Movement.TargetValue = character.SplineAnimate.MaxSpeed;
            character.Animancer.Play(animation);
            character.SplineAnimate.Play();
        }
    }
}