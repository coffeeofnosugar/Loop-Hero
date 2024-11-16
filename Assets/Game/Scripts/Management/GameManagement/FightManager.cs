using Tools;
using Tools.EventBus;
using UnityEngine.InputSystem;

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

    public class FightManager : Singleton<FightManager>
    {
        public bool IsFighting;

        private void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                IsFighting = !IsFighting;
                FightEvent.Trigger(IsFighting);
            }
        }
    }
}