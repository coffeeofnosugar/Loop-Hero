using UnityEngine;
using UnityEngine.InputSystem;

namespace Coffee.Player
{
    public class PlayerBrain : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private CharacterState idleState;

        private void Update()
        {
            var speed = Vector2.zero;
            if (Keyboard.current.digit1Key.isPressed)
            {
                speed = character.MovementStats.walkSpeed;
            }
            else if (Keyboard.current.digit2Key.isPressed)
            {
                speed = character.MovementStats.runSpeed;
            }
            else if (Keyboard.current.digit3Key.isPressed)
            {
                speed = character.MovementStats.turnSpeed;
            }

            character.MovementStats.Movement.TargetValue = speed;
        }
    }
}