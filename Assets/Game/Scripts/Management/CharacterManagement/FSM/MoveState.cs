using System;
using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class MoveState : CharacterState
    {
        [SerializeField] public TransitionAsset _animation;
        public SmoothedFloatParameter movement;
        
        private void Awake()
        {
            StringAsset moveParameter = ScriptableObject.CreateInstance<StringAsset>();
            moveParameter.name = "movement";
            movement = new SmoothedFloatParameter(character.Animancer, moveParameter, 0.1f);
            movement.TargetValue = character.SplineAnimate.MaxSpeed;
            if (_animation.Transition is LinearMixerTransition linearMixerTransition)
            {
                linearMixerTransition.ParameterName = moveParameter;
            }
        }

        private void OnEnable()
        {
            character.Animancer.Play(_animation);
            character.SplineAnimate.Play();
        }

        private void OnDisable()
        {
            character.SplineAnimate.Pause();
        }
    }
}