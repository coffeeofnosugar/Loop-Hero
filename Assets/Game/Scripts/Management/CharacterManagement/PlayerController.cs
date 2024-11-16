using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character character;

        private void Start()
        {
            character.Brain.state = State.Walk;
        }
    }
}