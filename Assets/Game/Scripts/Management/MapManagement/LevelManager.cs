using System.Collections.Generic;
using Coffee.Core.CharacterManagement;
using Coffee.Core.FightManagement;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Tools;
using Tools.EventBus;
using Tools.PoolModule;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Coffee.Core.MapManagement
{
    public class LevelManager : Singleton<LevelManager>,
        IEventListener<FightEvent>
    {
        [SerializeField] private GameObject heroReferce;
        public Character Hero { get; private set; }
        [SerializeField] private float spawnEnemyInterval = 1f;

        private SimpleObjectPooler enemyPooler;
        public SplineContainer splineContainer { get; private set; }

        private float timer;
        private List<int> sites;

        protected override void Awake()
        {
            base.Awake();
            enemyPooler = GetComponent<SimpleObjectPooler>();
            splineContainer = GetComponent<SplineContainer>();
            sites = new List<int>(splineContainer.Spline.Count);
            for (int i = 0; i < splineContainer.Spline.Count; i++)
                sites.Add(i);
        }

        public async UniTask InitLevel()
        {
            await SpawnHero();
        }

        public void LevelEnd()
        {
            enabled = false;
        }


        public void StartGame()
        {
            Hero.state = State.Walk;
            enabled = true;
        }
        
        private void Update()
        {
            UpdateSpawnEnemy();
        }

        #region Hero

        private async UniTask SpawnHero()
        {
            GameObject[] obj = await InstantiateAsync(heroReferce).ToUniTask();
            Hero = obj[0].GetComponent<Character>();
        }

        #endregion

        #region Enemy

        private void UpdateSpawnEnemy()
        {
            if (FightManager.Instance.IsFighting) return;   // 如果正在战斗停止
            if (sites.Count == 0) return;
            timer += Time.deltaTime;
            if (timer >= spawnEnemyInterval)
            {
                timer = 0;
                var site = sites[Random.Range(0, sites.Count)];
                sites.Remove(site);
                var enemy = enemyPooler.GetPooledGameObject();
                enemy.GetComponent<SplineAnimate>().StartOffset = site/(float)(splineContainer.Spline.Count - 1);
                enemy.GetComponent<Character>().Site = site;
                enemy.SetActive(true);
            }
        }

        public void ResetSite(int site)
        {
            sites.Add(site);
        }

        #endregion

        public void OnEvent(FightEvent animationEvent)
        {
            
        }

        [Button(ButtonSizes.Gigantic)]
        private void Refresh()
        {
            var _transform = transform.Find("Ground");
            splineContainer.Spline.Clear();
            for (int i = 0; i < _transform.childCount; i++)
            {
                splineContainer.Spline.Add(_transform.GetChild(i).position, TangentMode.AutoSmooth);
            }
        }
    }
}
