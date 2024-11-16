using Coffee.Core.MapManagement;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private Character character;

        private void Start()
        {
            character.state = State.Walk;
        }

        private void Update()
        {
            UpdateSite();
        }


        private void UpdateSite()
        {
            var spline = MapManager.Instance.splineContainer.Spline;
            int nextSite = character.Site + 1 >= spline.Count ? 0 : character.Site + 1;
            if (Vector3.SqrMagnitude(transform.position - (Vector3)spline[nextSite].Position) < .75f *.75f)
                character.Site = nextSite;
        }
    }
}