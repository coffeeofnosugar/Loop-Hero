using Tools;
using Tools.EventBus;

namespace Coffee.Core.FightManagement
{
    public struct EnterFightEvent
    {
        private static EnterFightEvent e;
        public static void Trigger()
        {
            EventBus.TriggerEvent(e);
        }
    }

    public class FightManager : Singleton<FightManager>,
        IEventListener<EnterFightEvent>
    {
        public bool IsFighting;
        
        private void OnEnable()
        {
            this.EventStartListening<EnterFightEvent>();
        }
        
        private void OnDisable()
        {
            this.EventStopListening<EnterFightEvent>();
        }

        private void Update()
        {
            IsFightingHandler();
        }

        private void IsFightingHandler()
        {
            // if ()
            {
                
            }
        }

        public void OnEvent(EnterFightEvent enterFightEvent)
        {
            IsFighting = true;
        }
    }
}