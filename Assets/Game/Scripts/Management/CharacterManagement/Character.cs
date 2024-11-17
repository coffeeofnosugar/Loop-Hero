using Animancer;
using Animancer.FSM;
using Coffee.Core.MapManagement;
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

        public CharacterData Data;
        public CharacterConfig Config;
        public AnimancerComponent Animancer;
        public StateMachine<State, CharacterState>.WithDefault StateMachine;
        public SplineAnimate SplineAnimate;
        public int Site;

        protected virtual void Awake()
        {
            Initialized();
            StateMachine.InitializeAfterDeserialize();
            if (SplineAnimate.Container == null)
                SplineAnimate.Container = LevelManager.Instance.splineContainer;
        }

        protected virtual void Initialized()
        {
            Data.health = Config.MaxHealth;
        }
    }
}