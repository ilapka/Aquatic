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

            ref var movableComponent = ref player.Get<ForwardMovableComponent>();
            player.Get<InputComponent>();

            var playerPrefab = Object.Instantiate(_playerData.playerPrefab, _playerData.spawnPosition, Quaternion.identity);

            /*
            movableComponent.Transform = playerPrefab.transform;
            movableComponent.Rigidbody = playerPrefab.GetComponent<Rigidbody>();
            movableComponent.MaxMoveSpeed = _playerData.maxSpeed;
            movableComponent.Acceleration = _playerData.acceleration;*/
            
            playerPrefab.SetActive(true);
        }
    }
}
