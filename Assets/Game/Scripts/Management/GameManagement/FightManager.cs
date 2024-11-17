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
        public bool StartFighting;
        private Hero Hero => LevelManager.Instance.Hero;

        [ReadOnly] public Character fightEnemy;
        [ReadOnly] public List<GameObject> hideEnemies;
        
        
        private void Update()
        {
            IsFightHandler();
            FightingHandler();
        }

        private void IsFightHandler()
        {
            if (!LevelManager.Instance.Sites.Contains(Hero.Site) && !IsFighting)
            {
                EnterFight(LevelManager.Instance.enemyPooler._objectPool.PooledGameObjects).Forget();
            }
        }

        private async UniTask EnterFight(List<GameObject> pooledGameObjects)
        {
            IsFighting = true;
            Hero.state = State.Idle;
            Hero.fightCamera.SetActive(true);
            // 隐藏其他敌人
            foreach (GameObject pooledGame in pooledGameObjects.Where(pooledGame => pooledGame.activeInHierarchy))
            {
                if (pooledGame.TryGetComponent(out Character character) && character.Site == Hero.Site)
                {
                    fightEnemy = character;
                }
                else
                {
                    // pooledGame.SetActive(false);
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
            timerHero = totle;
            timerEnemy = totle;
            StartFighting = true;
        }

        #region Fighting

        
        
        public float totle = 100;
        public float timerHero;
        public float timerEnemy;
        
        private void FightingHandler()
        {
            if (!IsFighting || !StartFighting) return;
            timerHero -= Time.deltaTime * Hero.Data.attackSpeed;
            timerEnemy -= Time.deltaTime * fightEnemy.Data.attackSpeed;

            if (timerHero <= 0)
            {
                Hero.state = State.Attack;
                timerHero = totle;
            }

            if (timerEnemy <= 0)
            {
                fightEnemy.state = State.Attack;
                timerEnemy = totle;
            }
        }

        #endregion
    }
}