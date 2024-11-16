using Animancer;
using Animancer.FSM;
using Coffee.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace Coffee.Core.CharacterManagement
{
    [DefaultExecutionOrder(-10000)]
    [SelectionBase]
    public class Character : MonoBehaviour
    {
        [SerializeField] private bool isPlayer = false;
        
        [SerializeField] private AnimancerComponent animancer;
        public AnimancerComponent Animancer => animancer;
        
        [SerializeField] private StateMachine<State, CharacterState>.WithDefault stateMachine;
        public StateMachine<State, CharacterState>.WithDefault StateMachine => stateMachine;

        [SerializeField] private Brain brain;
        public Brain Brain => brain;

        [SerializeField, ShowIf("@isPlayer")] private MovementStatsSO movementStats;
        public MovementStatsSO MovementStats => movementStats;
        
        [SerializeField, ShowIf("@isPlayer")] private SplineAnimate splineAnimate;
        public SplineAnimate SplineAnimate => splineAnimate;

        public int Site;

        private void Awake()
        {
            if (isPlayer)
                MovementStats.Movement = new SmoothedFloatParameter(Animancer, MovementStats.parameterSpeed, MovementStats.smoothTime);
            stateMachine.InitializeAfterDeserialize();
        }
    }
}