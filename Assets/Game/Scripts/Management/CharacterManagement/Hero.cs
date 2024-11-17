using Coffee.Core.MapManagement;
using Tools.EventBus;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public struct LevelUpEvent
    {
        public int Level;
        
        public LevelUpEvent(int level)
        {
            Level = level;
        }

        private static LevelUpEvent e;
        public static void Trigger(int level)
        {
            EventBus.TriggerEvent(e);
        }
    }
    
    [SelectionBase]
    public class Hero : Character
    {
        public GameObject fightCamera;
        public Transform fightPoint;

        private void Awake()
        {
            fightCamera.SetActive(false);
        }

        private void Update()
        {
            UpdateSite();
        }
        
        private void UpdateSite()
        {
            var spline = LevelManager.Instance.splineContainer.Spline;
            int nextSite = Site + 1 >= spline.Count ? 0 : Site + 1;
            if (Vector3.SqrMagnitude(transform.position - (Vector3)spline[nextSite].Position) < .75f *.75f)
                Site = nextSite;
        }

        protected override void Initialized()
        {
            base.Initialized();
        }
    }
}