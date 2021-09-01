using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Components.Events;
using Data;
using UnityComponents;

namespace Systems
{
    public sealed class PlayerSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PlayerBoatData _playerBoatData = null;
        private readonly EcsFilter<LocationSpawnEvent> _locationSpawnEvent = null;

        private bool _playerSpawned;

        public void Run()
        {
            if(_playerSpawned) return;

            foreach (var i in _locationSpawnEvent)
            {
                var spawnPosition = _locationSpawnEvent.Get1(i).LocationInformation.playerSpawnPoint.position;
                var playerInformation = Object.Instantiate(_playerBoatData.playerInformationPrefab, spawnPosition, Quaternion.identity);
            
                CreatePlayerContainerEntity(playerInformation);
                CreatePlayerBoatEntity(playerInformation);
            
                playerInformation.gameObject.SetActive(true);

                _playerSpawned = true;
            }
        }

        private void CreatePlayerContainerEntity(PlayerInformation playerInformation)
        {
            var playerContainer = _world.NewEntity();
            var playerComponent = new PlayerComponent()
            {
                PlayerInformation = playerInformation
            };
            var playerContainerMovable = new MovableComponent()
            {
                Transform = playerInformation.playerContainerTransform
            };
            var playerContainerForwardMovable = new ForwardMovableComponent()
            {
                ForwardMoveData = _playerBoatData.playerForwardMoveData
            };
            playerContainer
                .Replace(playerContainerMovable)
                .Replace(playerContainerForwardMovable)
                .Replace(playerComponent);
        }
        
        private void CreatePlayerBoatEntity(PlayerInformation playerInformation)
        {
            var playerBoat = _world.NewEntity();
            var playerBoatMovable = new MovableComponent()
            {
                Transform = playerInformation.playerBoatTransform,
                LocalSpaceMoving = true
            };
            var playerBoatDiveMovable = new DiveMovableComponent()
            {
                DiveMoveData = _playerBoatData.playerDiveMoveData,
                StartYPosition = playerInformation.transform.position.y
            };
            playerBoat
                .Replace(playerBoatMovable)
                .Replace(playerBoatDiveMovable);
        }
    }
}
