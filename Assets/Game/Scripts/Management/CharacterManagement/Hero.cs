using System.Collections;
using System.Collections.Generic;
using Coffee.Core.CharacterManagement;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private Character character;

    private void Start()
    {
        character.state = State.Walk;
    }
}
