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
            ref var forwardMovableComponent =  ref player.Get<ForwardMovableComponent>();
            ref var diveMovableComponent = ref player.Get<DiveMovableComponent>();

            var playerInformation = Object.Instantiate(_playerData.playerInformationPrefab);
            
            movableComponent.Rigidbody = playerInformation.playerRigidBody;
            forwardMovableComponent.ForwardMoveData = _playerData.playerForwardMoveData;
            diveMovableComponent.DiveMoveData = _playerData.playerDiveMoveData;
            
            playerInformation.gameObject.SetActive(true);
        }
    }
}
