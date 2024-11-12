using System;
using Animancer;
using Sirenix.Utilities;
using UnityEngine;

namespace Coffee.Animation
{
    public class RedirectRootMotion : RedirectRootMotion<CharacterController>
    {
        [SerializeField] private Character character;
        [SerializeField] private TransitionAsset _state;

        public override Vector3 Position
        {
            get => Target.transform.position;
            set => Target.Move(value - Position);
        }

        public override Quaternion Rotation
        {
            get => Target.transform.rotation;
            set => Target.transform.rotation = value;
        }

        protected override void OnAnimatorMove()
        {
            if (!ApplyRootMotion)
                return;
            //
            // if ((TransitionAsset)character.Animancer.States.Current.DebugName == _state)
            // {
            //     var leftWeight = character.Animancer.States.Current.GetEnumerator()[^1].Weight;
            //     var rightWeight = character.Animancer.States.Current.GetEnumerator()[^2].Weight;
            //     
            //     if (leftWeight != 0)
            //     {
            //         Debug.Log(Animator.deltaPosition);
            //     }
            //     else if (rightWeight != 0)
            //     {
            //         Debug.Log(Animator.deltaPosition);
            //     }
            // }
            //
            Target.Move(Animator.deltaPosition);
            Target.transform.rotation *= Animator.deltaRotation;
        }
    }
}