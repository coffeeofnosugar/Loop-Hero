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

        private void Awake()
        {
            stateMachine.InitializeAfterDeserialize();
        }
    }
}