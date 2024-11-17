using System.Collections.Generic;
using System.Linq;
using Coffee.Core.CharacterManagement;
using Coffee.Core.MapManagement;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Coffee.Core.FightManagement
{
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
                EnterFight(LevelManager.Instance.enemyPooler._objectPool.PooledGameObjects).Forget();
            }
        }

        public async UniTask EnterFight(List<GameObject> pooledGameObjects)
        {
            IsFighting = true;
            Hero.state = State.Idle;
            Hero.fightCamera.SetActive(true);
            foreach (GameObject pooledGame in pooledGameObjects.Where(pooledGame => pooledGame.activeInHierarchy))
            {
                if (pooledGame.TryGetComponent(out Character character) && character.Site == Hero.Site)
                {
                    fightEnemy = character;
                }
                else
                {
                    pooledGame.SetActive(false);
                    hideEnemies.Add(pooledGame);
                }
            }

            fightEnemy.transform.DOMove(Hero.fightPoint.position, .5f);
            fightEnemy.transform.DOLookAt(Hero.transform.position, .5f);
            // 等待摄像机停止移动
            while (true)
            {
                Vector3 oldFightCameraPosition = Camera.main.transform.position;
                await UniTask.Yield();
                if (oldFightCameraPosition == Camera.main.transform.position)
                {
                    break;
                }
            }
            
            Debug.Log("Fight Start");
        }
    }
}