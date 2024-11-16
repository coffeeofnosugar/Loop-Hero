using Tools;
using Tools.EventBus;

namespace Coffee.Core.FightManagement
{
    public struct FightEvent
    {
        public bool IsEnter;

        private static FightEvent e;
        public static void Trigger(bool isEnter)
        {
            e.IsEnter = isEnter;
            EventBus.TriggerEvent(e);
        }
    }

    public class FightManager : Singleton<FightManager>,
        IEventListener<FightEvent>
    {
        public bool IsFighting;

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

        public void OnEvent(FightEvent animationEvent)
        {
            IsFighting = animationEvent.IsEnter;
            if (animationEvent.IsEnter)
            {
                
            }
        }
    }
}