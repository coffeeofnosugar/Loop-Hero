using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee
{
    // [CreateAssetMenu(fileName = "MovementSO", menuName = "ScriptableObjects/MovementSO", order = 1)]
    [InlineEditor]
    public class MovementStatsSO : ScriptableObject
    {
        public Vector2 walkSpeed = new Vector2(0f, 1.81728f);
        public Vector2 runSpeed = new Vector2(0f, 2.719299f);
        public Vector2 turnSpeed = new Vector2(1.682996f, 2.267032f);
        public StringAsset inputX;
        public StringAsset inputY;
        public float smoothTime = .1f;

        public SmoothedVector2Parameter Movement;
    }
}