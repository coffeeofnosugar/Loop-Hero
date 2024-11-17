using System.Collections.Generic;
using System.Linq;
using Coffee.Core.CharacterManagement;
using Coffee.Core.MapManagement;
using Sirenix.OdinInspector;
using Tools;
using Tools.EventBus;
using UnityEngine;

namespace Coffee.Core.FightManagement
{
    public struct EnterFightEvent
    {
        private static EnterFightEvent e;
        public static void Trigger()
        {
            EventBus.TriggerEvent(e);
        }
    }

    public class FightManager : Singleton<FightManager>
    {
        public bool IsFighting;
        private Hero Hero => LevelManager.Instance.Hero;

        [ReadOnly] public Character fightEnemy;
        [ReadOnly] public List<GameObject> hideEnemies;
        
        
        private void Update()
        {
            IsFightingHandler();
        }

        private void IsFightingHandler()
        {
            if (!LevelManager.Instance.Sites.Contains(Hero.Site) && !IsFighting)
            {
                EnterFightEvent.Trigger();
                Hero.state = State.Idle;
                Hero.fightCamera.SetActive(true);
            }
        }

        public void EnterFight(int heroSite, List<GameObject> pooledGameObjects)
        {
            IsFighting = true;
            foreach (GameObject pooledGame in pooledGameObjects.Where(pooledGame => pooledGame.activeInHierarchy))
            {
                if (pooledGame.TryGetComponent(out Character character) && character.Site == heroSite)
                    fightEnemy = character;
                else
                {
                    pooledGame.SetActive(false);
                    hideEnemies.Add(pooledGame);
                }
            }
        }
    }
}