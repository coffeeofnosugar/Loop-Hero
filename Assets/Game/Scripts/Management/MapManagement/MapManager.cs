using System.Collections.Generic;
using Coffee.Core.CharacterManagement;
using Coffee.Core.FightManagement;
using Sirenix.OdinInspector;
using Tools;
using Tools.EventBus;
using Tools.PoolModule;
using UnityEngine;
using UnityEngine.Splines;

namespace Coffee.Core.MapManagement
{
    public class MapManager : Singleton<MapManager>,
        IEventListener<FightEvent>
    {
        [SerializeField] private float spawnEnemyInterval = 1f;
        
        [SerializeField] private SimpleObjectPooler enemyPooler;
        public SplineContainer splineContainer;
        
        private float timer;
        public List<int> sites;

        protected override void Awake()
        {
            base.Awake();
            sites = new List<int>(splineContainer.Spline.Count);
            for (int i = 0; i < splineContainer.Spline.Count; i++)
                sites.Add(i);
        }
        
        private void Update()
        {
            if (FightManager.Instance.IsFighting) return;
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

        public void OnEvent(FightEvent animationEvent)
        {
            
        }
    }
}
