using System;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee
{
    public class MoveState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;
        [SerializeField] private StringAsset inputX;
        [SerializeField] private StringAsset inputY;
        [SerializeField, Unit(Units.Meter)] private Vector2 moveSpeed = new Vector2(1.919599f, 1.570797f);
        // [SerializeField, Unit(Units.Meter)] private float runSpeed = 3.839193f;
        [SerializeField] private float smoothTime = .1f;
        private SmoothedVector2Parameter movement;

        private void Awake()
        {
            movement = new SmoothedVector2Parameter(character.Animancer, inputX, inputY, smoothTime);
        }

        private void OnEnable()
        {
            character.Animancer.Play(animation);
        }

        private void Update()
        {
            Vector2 speed = moveSpeed;
            // if (Input.GetButton("Horizontal"))
            // {
            //     var leftWeight = character.Animancer.States.Current.GetEnumerator()[^1].Weight;
            //     var rightWeight = character.Animancer.States.Current.GetEnumerator()[^2].Weight;
            //     
            //     if (leftWeight != 0)
            //     {
            //         speed.y /= leftWeight;
            //     }
            //     else if (rightWeight != 0)
            //     {
            //         speed.y /= rightWeight;
            //     }
            // }
            // Debug.Log(speed);
            movement.TargetValue = new Vector2(Input.GetAxis("Horizontal") * speed.x, Input.GetAxis("Vertical") * speed.y);
        }
    }
}