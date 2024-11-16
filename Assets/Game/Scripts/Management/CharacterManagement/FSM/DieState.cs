using System;
using Animancer;
using Coffee.Core.MapManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class DieState : CharacterState
    {
        [SerializeField] private TransitionAsset _animation;
        [SerializeField] private float dissolveDealy = 1f;

        private async void OnEnable()
        {
            await character.Animancer.Play(_animation);
            if (dissolveDealy == -1f) return;
            await UniTask.Delay(TimeSpan.FromSeconds(dissolveDealy));
            character.gameObject.SetActive(false);
            MapManager.Instance.sites.Add(character.Site);
            character.StateMachine.ForceSetDefaultState.Invoke();
        }
    }
}