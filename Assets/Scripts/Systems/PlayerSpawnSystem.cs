using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Data;
using Information;

namespace Systems
{
    public sealed class PlayerSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly PlayerBoatData _playerBoatData = null;

        public void Init()
        {
            var playerInformation = Object.Instantiate(_playerBoatData.playerInformationPrefab);
            
            CreatePlayerContainerEntity(playerInformation);
            CreatePlayerBoatEntity(playerInformation);
            
            playerInformation.gameObject.SetActive(true);
        }

        private void CreatePlayerContainerEntity(PlayerInformation playerInformation)
        {
            var playerContainer = _world.NewEntity();
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
                .Replace(playerContainerForwardMovable);
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
