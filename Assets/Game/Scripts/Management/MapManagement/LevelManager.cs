using System.Collections.Generic;
using System.Linq;
using Coffee.Core.CharacterManagement;
using Coffee.Core.FightManagement;
using Sirenix.OdinInspector;
using Tools;
using Tools.PoolModule;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

namespace Coffee.Core.MapManagement
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private GameObject heroReferce;
        public Hero Hero { get; private set; }
        [SerializeField] private float spawnEnemyInterval = 1f;

        public SimpleObjectPooler enemyPooler;
        public SplineContainer splineContainer;

        private float timer;
        [ShowInInspector, ReadOnly] public HashSet<int> Sites;

        protected override void Awake()
        {
            base.Awake();
            Sites = new HashSet<int>(splineContainer.Spline.Count);
            List<int> numbers = new List<int>();
            for (int i = 0; i < splineContainer.Spline.Count; i++)
            {
                numbers.Add(i);
            }
            for (int i = 0; i < splineContainer.Spline.Count; i++)
            {
                int index = Random.Range(0, numbers.Count);
                Sites.Add(numbers[index]);
                numbers.RemoveAt(index);
            }
            Hero = Instantiate(heroReferce).GetComponent<Hero>();
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

        #region Enemy

        private void UpdateSpawnEnemy()
        {
            if (Hero == null)
                Hero = FindObjectOfType<Hero>();
            
            if (FightManager.Instance.IsFighting) return;   // 如果正在战斗停止
            if (Sites.Count == 0) return;
            if (Sites.Count == 1 && Sites.Contains(Hero.Site)) return;
            timer += Time.deltaTime;
            if (timer >= spawnEnemyInterval)
            {
                timer = 0;
                int site = Sites.First(b => b != Hero.Site);
                Sites.Remove(site);
                var enemy = enemyPooler.GetPooledGameObject();
                enemy.GetComponent<SplineAnimate>().StartOffset = site/(float)(splineContainer.Spline.Count);
                enemy.GetComponent<Character>().Site = site;
                enemy.SetActive(true);
            }
        }

        public void ResetSite(int site)
        {
            Sites.Add(site);
        }

        #endregion

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
