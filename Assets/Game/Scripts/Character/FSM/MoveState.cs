using System;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee
{
    public class MoveState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;

        private void OnEnable()
        {
            character.Animancer.Play(animation);
        }
    }
}