using UnityEngine;


namespace Coffee.Core.Management.UIManagement
{
    public class FightTimeLine : BaseUI
    {
        [SerializeField] private RectTransform timeLine;
        [SerializeField] private RectTransform heroIcon;
        [SerializeField] private RectTransform enemyIcon;
        
        [SerializeField] private float multiplier = 1f;
        
        private void Awake()
        {
            Initialized();
        }

        public override void Initialized()
        {
            multiplier = timeLine.rect.width / 100f;
            heroIcon.anchoredPosition = new Vector2(0f, heroIcon.anchoredPosition.y);
            enemyIcon.anchoredPosition = new Vector2(0f, enemyIcon.anchoredPosition.y);
        }
        
        public void UpdateHeroIcon(float progress)
        {
            heroIcon.anchoredPosition = new Vector2(progress * multiplier, heroIcon.anchoredPosition.y);
        }
        
        public void UpdateEnemyIcon(float progress)
        {
            enemyIcon.anchoredPosition = new Vector2(progress * multiplier, enemyIcon.anchoredPosition.y);
        }


        private void OnValidate()
        {
            multiplier = timeLine.rect.width / 100f;
        }
    }
}