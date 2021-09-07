using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Systems.Input;
using Systems.Location;
using Systems.Movement;
using Systems.PipeRing;
using Systems.Player;
using Systems.Saving;
using Systems.UI;
using Components;
using Components.Events;
using Managers;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _updateSystems;
    private EcsSystems _savedDataSystem;
    private EcsSystems _uiSystems;
    
    [Header("General Data")]
    [SerializeField] private PlayerBoatData playerBoatData;
    [SerializeField] private LevelListData levelList;
    [SerializeField] private UIData uiData;
    [SerializeField] private SavingSettings savingSettings;
    [SerializeField] private EncryptionData encryptionData;

    [Header("Saved Data")]
    [SerializeField] private GameProgressSavedData gameProgressData; 

    private void Start()
    {
        InitializeManagers();
        
        _world = new EcsWorld();
        _updateSystems = new EcsSystems(_world);
        _savedDataSystem = new EcsSystems(_world);
        _uiSystems = new EcsSystems(_world);
        

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_savedDataSystem);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_uiSystems);
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
            .Add(new LevelProgressSystem())
            .Add(new GameStateSystem())
            .Add(new PlayerParticlesSystem())
            .Add(new SceneLoadSystem())

            .Inject(playerBoatData)
            .Inject(levelList)
            
            .Init();

        _savedDataSystem
            .Add(new SaveFileSystem())
            .Add(new LoadFileSystem())
            .Add(new LevelValueSystem())
            .Add(new WalletSystem())
            
            .Inject(savingSettings)
            .Inject(gameProgressData)
            
            .Init();
        
        _uiSystems
            .Add(new CanvasSystem())
            .Add(new LevelProgressUISystem())
            .Add(new MoneyUISystem())
            .Add(new PopUpRewardSystem())
            .Add(new StartPanelSystem())
            .Add(new CompletePanelSystem())
            //.Add(new DarkScreenSystem())
            
            .Inject(uiData)
            
            .OneFrame<StartGameEvent>()
            .OneFrame<LocationSpawnEvent>()
            .OneFrame<AddNewDestroyableObjectEvent>()
            .OneFrame<ExplosionDestroyableObjectEvent>()
            .OneFrame<LevelCompleteEvent>()
            .OneFrame<PlayConfettiEvent>()
            .OneFrame<PlayDarkScreenEvent>()
            .OneFrame<LoadSceneEvent>()
            
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
        _uiSystems.Run();
    }
    
    private void OnDestroy()
    {
        _updateSystems.Destroy();
        _savedDataSystem.Destroy();
        _uiSystems.Destroy();
        _world.Destroy();
    }
}
