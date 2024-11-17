using Coffee.Core.FightManagement;
using Coffee.Core.MapManagement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Coffee.Core.CharacterManagement
{
    public class Hero : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] public bool isFighting => FightManager.Instance.IsFighting;
        [SerializeField] private Character character;
        [SerializeField] private GameObject fightCamera;

        private void Awake()
        {
            fightCamera.SetActive(false);
        }

        private void Update()
        {
            UpdateSite();
            UpdateEnterFight();
        }


        private void UpdateSite()
        {
            var spline = LevelManager.Instance.splineContainer.Spline;
            int nextSite = character.Site + 1 >= spline.Count ? 0 : character.Site + 1;
            if (Vector3.SqrMagnitude(transform.position - (Vector3)spline[nextSite].Position) < .75f *.75f)
                character.Site = nextSite;
        }

        private void UpdateEnterFight()
        {
            if (!LevelManager.Instance.Sites.Contains(character.Site) && !isFighting)
            {
                EnterFightEvent.Trigger();
                character.state = State.Idle;
                fightCamera.SetActive(true);
            }
        }
    }
}