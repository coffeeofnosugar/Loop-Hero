using System;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.ScriptableObjects
{
    // [CreateAssetMenu(fileName = "MovementSO", menuName = "ScriptableObjects/MovementSO", order = 1)]
    [InlineEditor]
    public class MovementStatsSO : ScriptableObject
    {
        public StringAsset parameterSpeed;
        public float smoothTime = .1f;
        public SmoothedFloatParameter Movement;
    }
}