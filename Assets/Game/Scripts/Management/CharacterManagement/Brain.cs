using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public enum State { Idle, Walk, Attack, Die }

    public class Brain : MonoBehaviour
    {
        [SerializeField, EnumToggleButtons, HideLabel] private State currentState;
        
        [SerializeField] private Character character;
        [SerializeField] private CharacterState idleState;
        [SerializeField] private CharacterState walkState;
        [SerializeField] private CharacterState attackState;
        [SerializeField] private CharacterState dieState;

        private void Awake()
        {
            character.StateMachine.AddRange(
                new [] { State.Idle, State.Walk, State.Attack, State.Die },
                new [] { idleState, walkState, attackState, dieState });
            character.StateMachine.ForceSetDefaultState += () => currentState = State.Idle;
        }

        private void Update()
        {
            switch (currentState)
            {
                case State.Idle:
                    character.StateMachine.TrySetDefaultState();
                    break;
                case State.Walk:
                    character.StateMachine.TrySetState(State.Walk);
                    break;
                case State.Attack:
                    character.StateMachine.TrySetState(State.Attack);
                    break;
                case State.Die:
                    character.StateMachine.TrySetState(State.Die);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}