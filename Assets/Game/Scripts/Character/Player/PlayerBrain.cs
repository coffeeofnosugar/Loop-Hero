using System;
using UnityEngine;

namespace Coffee.Player
{
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private CharacterState idleState;
    }
}