using Animancer;
using Animancer.FSM;
using Coffee.Core.FightManagement;
using Tools.EventBus;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Splines;

namespace Coffee.Core.CharacterManagement
{
    [DefaultExecutionOrder(-10000)]
    [SelectionBase]
    public class Character : MonoBehaviour,
        IEventListener<FightEvent>
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
            if (SplineAnimate.Container == null)
            {
                SplineAnimate.Container = FindObjectOfType<SplineContainer>();
            }
        }

        private void OnEnable()
        {
            this.EventStartListening<FightEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<FightEvent>();
        }

        public void OnEvent(FightEvent fightEvent)
        {
            Debug.Log("chuaf");
            // if (fightEvent.IsEnter)
            //     Animancer.Graph.PauseGraph();
            // else
            //     Animancer.Graph.UnpauseGraph();
        }
    }
}