using System;
using Components.Events;
using Data;
using GameAnalyticsSDK;
using GoogleMobileAds.Api;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.SDK
{
    public class GameAnalyticsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameAnalyticsData _gameAnalyticsData = null;

        private readonly EcsFilter<StartGameEvent> _startGameEvent = null;
        private readonly EcsFilter<LevelCompleteEvent> _levelCompleteEvent = null;

        public void Init()
        {
            GameAnalytics.Initialize();
        }

        public void Run()
        {
            foreach (var i in _startGameEvent)
            {
                OnLevelStart(_startGameEvent.Get1(i).CurrentLevel);
            }

            foreach (var i in _levelCompleteEvent)
            {
                OnLevelComplete(_levelCompleteEvent.Get1(i).CurrentLevel);
            }
        }
        
        private void OnLevelStart(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level: {level}");
            if(_gameAnalyticsData.debug) Debug.Log($"Level: {level} start");
        }

        private void OnLevelComplete(int level)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level: {level}");
            if(_gameAnalyticsData.debug) Debug.Log($"Level: {level} complete");
        }
    }
}
