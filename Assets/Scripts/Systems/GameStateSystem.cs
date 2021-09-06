﻿using Components;
using Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class GameStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<GameStateComponent> _gameStateFilter = null;
        private readonly EcsFilter<StartGameEvent> _startGameFilter = null;
        private readonly EcsFilter<LevelCompleteEvent> _levelCompleteFilter = null;
        
        public void Init()
        {
            _world.NewEntity().Get<GameStateComponent>();
        }
        
        public void Run()
        {
            foreach (var i in _startGameFilter)
            {
                SetGameState(true);
            }

            foreach (var i in _levelCompleteFilter)
            {
                SetGameState(false);
            }
        }

        private void SetGameState(bool isGamePlayProcess)
        {
            foreach (var j in _gameStateFilter)
            {
                _gameStateFilter.Get1(j).IsGamePlayProcess = isGamePlayProcess;
            }
        }
    }
}