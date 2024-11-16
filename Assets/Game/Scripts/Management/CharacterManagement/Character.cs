using Animancer;
using Animancer.FSM;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace Coffee.Core.CharacterManagement
{
    [DefaultExecutionOrder(-10000)]
    [SelectionBase]
    public class Character : MonoBehaviour
    {
        [EnumToggleButtons, HideLabel] public State state;

        [SerializeField] private AnimancerComponent animancer;
        public AnimancerComponent Animancer => animancer;
        
        [SerializeField] private StateMachine<State, CharacterState>.WithDefault stateMachine;
        public StateMachine<State, CharacterState>.WithDefault StateMachine => stateMachine;

        [SerializeField] private Brain brain;
        public Brain Brain => brain;
        
        [SerializeField] private SplineAnimate splineAnimate;
        public SplineAnimate SplineAnimate => splineAnimate;

        public int Site;

        private void Awake()
        {
            stateMachine.InitializeAfterDeserialize();
        }
    }
}