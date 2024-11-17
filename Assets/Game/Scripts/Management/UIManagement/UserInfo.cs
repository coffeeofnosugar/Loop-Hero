using System;
using Coffee.Core.CharacterManagement;
using TMPro;
using Tools.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace Coffee.Management.UIManagement
{
    public class UserInfo : MonoBehaviour,
        IEventListener<LevelUpEvent>
    {
        [SerializeField] private HealthBar healthSlider;
        [SerializeField] private Slider expSlider;
        [SerializeField] private TextMeshProUGUI levelText;

        private void Awake()
        {
            // healthSlider.Initialized();
            expSlider.value = 0;
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
    }
}