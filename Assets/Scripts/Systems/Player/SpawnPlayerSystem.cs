using Leopotam.Ecs;
using UnityEngine;
using Components;
using Components.Events;
using Data;
using UnityComponents;

namespace Systems
{
    public sealed class SpawnPlayerSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PlayerBoatData _playerBoatData = null;
        private readonly EcsFilter<LocationComponent, LocationSpawnEvent> _locationSpawnEvent = null;
        
        public void Run()
        {
            foreach (var i in _locationSpawnEvent)
            {
                var spawnPosition = _locationSpawnEvent.Get1(i).LocationInformation.playerSpawnPoint.position;
                var playerInformation = Object.Instantiate(_playerBoatData.playerInformationPrefab, spawnPosition, Quaternion.identity);
            
                CreatePlayerContainerEntity(playerInformation);
                CreatePlayerBoatEntity(playerInformation);
            
                playerInformation.gameObject.SetActive(true);
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
            };
            var playerBoatDiveMovable = new DiveMovableComponent()
            {
                DiveMoveData = _playerBoatData.playerDiveMoveData,
                StartLocalPosition = playerInformation.transform.localPosition
            };
            playerBoat
                .Replace(playerBoatMovable)
                .Replace(playerBoatDiveMovable);
        }
    }
}
