using System;
using Animancer;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public enum State { Idle, Walk, Attack, Die }

    public class Brain : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private CharacterState idleState;
        [SerializeField] private CharacterState moveState;
        [SerializeField] private CharacterState attackState;
        [SerializeField] private CharacterState dieState;

        private void Awake()
        {
            character.StateMachine.AddRange(
                new [] { State.Idle, State.Walk, State.Attack, State.Die },
                new [] { idleState, moveState, attackState, dieState });
            character.StateMachine.ForceSetDefaultState += () => character.state = State.Idle;
        }

        private void Update()
        {
            switch (character.state)
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