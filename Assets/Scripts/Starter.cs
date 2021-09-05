using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Systems.Location;
using Systems.Movement;
using Systems.PipeRing;
using Systems.Player;
using Systems.Saving;
using Components;
using Components.Events;
using Managers;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _savedDataSystem;    
    
    [Header("General Data")]
    [SerializeField] private PlayerBoatData playerBoatData;
    [SerializeField] private LevelListData levelList;
    [SerializeField] private SavingSettings savingSettings;
    [SerializeField] private EncryptionData encryptionData;

    [Header("Saved Data")]
    [SerializeField] private GameProgressData gameProgressData; 

    private void Start()
    {
        InitializeManagers();
        
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world);
        _savedDataSystem = new EcsSystems(_world);
        

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_savedDataSystem);
#endif

        _updateSystems
            .Add(new DiveMoveSystem())
            .Add(new ForwardMoveSystem())
            .Add(new InputSystem())
            .Add(new SpawnLocationSystem())
            .Add(new ExtendLocationSystem())
            .Add(new SpawnPlayerSystem())
            .Add(new GenerationPipeRingsSystem())
            .Add(new ClearRubbishSystem())
            .Add(new DestroyableObjectsSystem())
            .Add(new BoatStateSystem())

            .Inject(playerBoatData)
            .Inject(levelList)

            .OneFrame<StartGameEvent>()
            .OneFrame<LocationSpawnEvent>()
            .OneFrame<AddNewDestroyableObjectEvent>()
            .OneFrame<ExplosionDestroyableObjectEvent>()

            .Init();

        _savedDataSystem
            .Add(new SaveFileSystem())
            .Add(new LoadFileSystem())
            .Add(new LevelValueSystem())
            .Add(new WalletSystem())
            
            .Inject(savingSettings)
            .Inject(gameProgressData)
            
            .OneFrame<SaveDataEvent>()
            .OneFrame<LevelUpEvent>()
            .OneFrame<UpdateLevelValueEvent>()
            .OneFrame<AddMoneyEvent>()
            .OneFrame<UpdateMoneyValueEvent>()
            .OneFrame<SpendMoneyEvent>()
            
            .Init();
    }

    private void InitializeManagers()
    {
        EncryptionManager.Init(encryptionData);
    }

    private void Update()
    {
        _updateSystems.Run();
        _savedDataSystem.Run();
    }
    

    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _savedDataSystem.Destroy();
        _world.Destroy();
    }
}
