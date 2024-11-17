using Coffee.Core.CharacterManagement;
using Coffee.Core.MapManagement;
using DG.Tweening;
using TMPro;
using Tools;
using Tools.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.Core.Management.UIManagement
{
    public class UserInfo : BaseUI,
        IEventListener<LevelUpEvent>
    {
        public HealthBar healthSlider;
        [SerializeField] private Slider expSlider;
        [SerializeField] private TextMeshProUGUI levelText;

        public override void Initialized()
        {
            healthSlider.UpdateBar(LevelManager.Instance.Hero.Data.health, LevelManager.Instance.Hero.Config.MaxHealth);
            levelText.text = ((HeroData)LevelManager.Instance.Hero.Data).Level.ToString();
            expSlider.value = ((HeroData)LevelManager.Instance.Hero.Data).Experience;
        }

        private void OnEnable()
        {
            this.EventStartListening<LevelUpEvent>();
        }
        
        private void OnDisable()
        {
            this.EventStopListening<LevelUpEvent>();
        }
        
        public void OnEvent(LevelUpEvent eventType)
        {
            levelText.text = eventType.Level.ToString();
        }


        public void UpdateLevelText()
        {
            levelText.text = ((HeroData)LevelManager.Instance.Hero.Data).Level.ToString();
            levelText.transform.DOShakePosition(.2f, 5f);
        }
    }
}