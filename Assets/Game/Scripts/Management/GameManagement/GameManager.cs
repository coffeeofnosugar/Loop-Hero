using System;
using Coffee.Core.MapManagement;
using Coffee.Management.UIManagement;
using Cysharp.Threading.Tasks;
using Tools;
using Tools.EventBus;

namespace Coffee.Core.FightManagement
{
    public enum GameEventTypes
    {
        ContinueGame,
        NewGame,
        LevelStart,
        LevelEnd
    }

    public struct GameEvent
    {
        public GameEventTypes EventType;
        
        public GameEvent(GameEventTypes eventType)
        {
            EventType = eventType;
        }

        private static GameEvent e;
        public static void Trigger(GameEventTypes eventType)
        {
            e.EventType = eventType;
            EventBus.TriggerEvent(e);
        }
    }
    
    public class GameManager : PersistentSingleton<GameManager>,
        IEventListener<GameEvent>
    {
        private void OnEnable()
        {
            this.EventStartListening<GameEvent>();
        }

        private void OnDisable()
        {
            this.EventStopListening<GameEvent>();
        }

        public void OnEvent(GameEvent fightEvent)
        {
            switch (fightEvent.EventType)
            {
                case GameEventTypes.ContinueGame:
                    // LevelManager.Instance.ContinueGame();
                    break;
                case GameEventTypes.NewGame:
                    LoadManager.Instance.LoadGameScene().Forget();
                    break;
                case GameEventTypes.LevelStart:
                    LevelManager.Instance.InitLevel().Forget();
                    break;
                case GameEventTypes.LevelEnd:
                    LevelManager.Instance.LevelEnd();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}