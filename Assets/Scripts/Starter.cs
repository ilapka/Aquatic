using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystem;

    [Header("Data")]
    [SerializeField] private PlayerData playerData;

    private void Start()
    {
        _world = new EcsWorld();

        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystem = new EcsSystems(_world);

        _updateSystems
            .Add(new PlayerSpawnSystem())
            .Add(new InputSystem());

        _fixedUpdateSystem
            .Add(new DiveMoveSystem())
            .Add(new ForwardMoveSystem())
            .Add(new MoveSystem());


        _updateSystems.Inject(playerData);

        _updateSystems.Init();
        _fixedUpdateSystem.Init();
    }
    
    private void Update()
    {
        _updateSystems.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystem.Run();
    }

    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _fixedUpdateSystem.Destroy();
        _world.Destroy();
    }
}
