using System;
using Animancer;
using Coffee.Core.MapManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class DieState : CharacterState
    {
        [SerializeField] private new TransitionAsset animation;
        [SerializeField] private float dissolve = 1f;

        private async void OnEnable()
        {
            await character.Animancer.Play(animation);
            if (dissolve == -1f) return;
            await UniTask.Delay(TimeSpan.FromSeconds(dissolve));
            character.gameObject.SetActive(false);
            MapManager.Instance.sites.Add(character.Site);
            character.StateMachine.ForceSetDefaultState.Invoke();
        }
    }
}