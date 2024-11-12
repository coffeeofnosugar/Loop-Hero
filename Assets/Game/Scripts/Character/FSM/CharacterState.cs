using Animancer;
using Animancer.FSM;
using UnityEngine;
using UnityEngine.Serialization;

namespace Coffee
{
    public enum CharacterStatePriority { Low, Medium, High }
    public class CharacterState : StateBehaviour
    {
        [SerializeField]
        private Character _character;
        public Character character => _character;
        
        public virtual CharacterStatePriority Priority => CharacterStatePriority.Low;

        public virtual bool CanInterruptSelf => false;

        public override bool CanExitState
        {
            get
            {
                // There are several different ways of accessing the state change details:
                // CharacterState nextState = StateChange<CharacterState>.NextState;
                // CharacterState nextState = this.GetNextState();
                CharacterState nextState = _character.StateMachine.NextState;
                if (nextState == this)
                    return CanInterruptSelf;
                else if (Priority == CharacterStatePriority.Low)
                    return true;
                else
                    return nextState.Priority > Priority;
            }
        }
        
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            gameObject.GetComponentInParentOrChildren(ref _character);
        }
#endif
    }
}