using Coffee.Core.MapManagement;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class Hero : Character
    {
        public GameObject fightCamera;

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
    }
}