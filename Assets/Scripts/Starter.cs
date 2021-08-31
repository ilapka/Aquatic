﻿using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Systems.Movement;
using Systems.Saving;
using Components;
using Components.Events;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystem;
    private EcsSystems _savedDataSystem;
    
    [Header("General Data")]
    [SerializeField] private PlayerBoatData playerBoatData;
    [SerializeField] private LevelListData levelList;
    [SerializeField] private SavingSettings savingSettings;

    [Header("Saved Data")]
    [SerializeField] private GameProgressData gameProgressData; 

    private void Start()
    {
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystem = new EcsSystems(_world);
        _savedDataSystem = new EcsSystems(_world);
        

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedUpdateSystem);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_savedDataSystem);
#endif

        _updateSystems
            .Add(new PlayerSpawnSystem())
            .Add(new InputSystem())
        
            .Inject(playerBoatData)
            .Inject(levelList)
        
            .Init();

        _fixedUpdateSystem
            .Add(new DiveMoveSystem())
            .Add(new ForwardMoveSystem())
            .Add(new MoveSystem())

            .Init();

        _savedDataSystem
            .Add(new SaveFileSystem())
            .Add(new LoadFileSystem())
            
            .Inject(savingSettings)
            .Inject(gameProgressData)
            
            .OneFrame<SaveDataEvent>()
            
            .Init();
    }
    
    private void Update()
    {
        _updateSystems.Run();
        _savedDataSystem.Run();
    }

    private void FixedUpdate()
    {
        _fixedUpdateSystem.Run();
    }

    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _fixedUpdateSystem.Destroy();
        _savedDataSystem.Destroy();
        _world.Destroy();
    }
}
