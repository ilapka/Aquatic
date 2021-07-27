using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Components;
using Data;

namespace Systems
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private EcsWorld _world = null;
        private PlayerData _playerData = null;

        public void Init()
        {
            var player = _world.NewEntity();

            ref var movableComponent = ref player.Get<MovableComponent>();
            player.Get<InputEventComponent>();

            var playerPrefab = Object.Instantiate(_playerData.playerPrefab, _playerData.spawnPosition, Quaternion.identity);

            movableComponent.transform = playerPrefab.transform;
            movableComponent.rigidbody = playerPrefab.GetComponent<Rigidbody>();
            movableComponent.maxMoveSpeed = _playerData.maxSpeed;
            movableComponent.acceleration = _playerData.acceleration;
            
            playerPrefab.SetActive(true);
        }
    }
}
