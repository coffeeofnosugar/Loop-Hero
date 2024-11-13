using System;
using Animancer;
using Animancer.FSM;
using UnityEngine;

namespace Coffee
{
    [DefaultExecutionOrder(-10000)]
    public class Character : MonoBehaviour
    {
        [SerializeField] private AnimancerComponent animancer;
        public AnimancerComponent Animancer => animancer;
        
        [SerializeField] private StateMachine<CharacterState>.WithDefault stateMachine;
        public StateMachine<CharacterState>.WithDefault StateMachine => stateMachine;

        [SerializeField] private MovementStatsSO movementStats;
        public MovementStatsSO MovementStats => movementStats;

        private void Awake()
        {
            stateMachine.InitializeAfterDeserialize();
            MovementStats.Movement = new SmoothedVector2Parameter(Animancer, MovementStats.inputX, MovementStats.inputY, MovementStats.smoothTime);
        }
    }
}