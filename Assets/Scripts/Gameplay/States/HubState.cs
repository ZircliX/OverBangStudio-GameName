using System;
using LTX.ChanneledProperties;
using LTX.ChanneledProperties.Priorities;
using OverBang.GameName.Core.GameMode;
using OverBang.GameName.Core.Scene;
using OverBang.GameName.Core.Services;
using OverBang.GameName.Gameplay.Gameplay.StateMachine;
using OverBang.GameName.Managers;
using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace OverBang.GameName.Gameplay.States
{
    public class HubState : IGameState
    {
        public string Name => "Hub";
        
        private readonly ICharacterSelectionService selectionService;
        private readonly ICharacterSpawnService spawnService;
        private readonly StateMachine<IGameState> stateMachine;

        private ChannelKey key;

        public HubState(StateMachine<IGameState> stateMachine,
            ICharacterSelectionService selectionService,
            ICharacterSpawnService spawnService)
        {
            this.stateMachine = stateMachine;
            this.selectionService = selectionService;
            this.spawnService = spawnService;
        }

        public async void Enter()
        {
            try
            {
                key = ChannelKey.GetUniqueChannelKey();
            
                GameController.CursorLockModePriority.AddPriority(key, PriorityTags.High);
                GameController.CursorVisibleStatePriority.AddPriority(key, PriorityTags.High);
            
                GameController.CursorLockModePriority.Write(key, CursorLockMode.Locked);
                GameController.CursorVisibleStatePriority.Write(key, false);
            
                if (SceneManager.GetActiveScene().name != "Hub")
                {
                    await SceneLoader.LoadSceneAsync("Hub");
                }
            
                spawnService.SpawnCharacter();
            
                // Subscribe to hub input (press "Start" to begin game)
                //HubController.OnGameStartRequested += HandleGameStart;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void Exit()
        {
            //HubController.OnGameStartRequested -= HandleGameStart;
            
            GameController.CursorLockModePriority.RemovePriority(key);
            GameController.CursorVisibleStatePriority.RemovePriority(key);
        }

        private void HandleGameStart()
        {
            stateMachine.ChangeState(new GameplayState(stateMachine, selectionService, spawnService));
        }
    }
}