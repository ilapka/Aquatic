using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Systems.Input;
using Systems.Location;
using Systems.Movement;
using Systems.PipeContent;
using Systems.PipeRing;
using Systems.Player;
using Systems.Saving;
using Systems.UI;
using Components;
using Components.Events;
using Extension;
using Managers;
using UnityComponents;
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
    [SerializeField] private SceneLoadData sceneLoadData;
    [SerializeField] private SavingSettings savingSettings;
    [SerializeField] private EncryptionData encryptionData;

    [Header("Saved Data")]
    [SerializeField] private GameProgressSavedData gameProgressData; 

    private void Start()
    {
        InitializeAssistants();
        
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
            .Add(new GenerationPipeRingSystem())
            .Add(new ClearRubbishSystem())
            .Add(new DestroyableObjectsSystem())
            .Add(new BoatStateSystem())
            .Add(new LevelProgressSystem())
            .Add(new GameStateSystem())
            .Add(new PlayerParticlesSystem())
            .Add(new SceneLoadSystem())

            .Inject(playerBoatData)
            .Inject(levelList)
            .Inject(sceneLoadData)
            
            .Init();

        _savedDataSystem
            .Add(new LevelValueSystem())
            .Add(new WalletSystem())
            .Add(new SaveFileSystem())
            .Add(new LoadFileSystem())

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
            
            .Inject(uiData)
            
            .OneFrame<StartGameEvent>()
            .OneFrame<LocationSpawnEvent>()
            .OneFrame<AddNewDestroyableObjectEvent>()
            .OneFrame<ExplosionDestroyableObjectEvent>()
            .OneFrame<LevelCompleteEvent>()
            .OneFrame<PlayConfettiEvent>()
            .OneFrame<LoadSceneEvent>()
            
            .OneFrame<SaveDataEvent>()
            .OneFrame<LevelUpEvent>()
            .OneFrame<UpdateLevelValueEvent>()
            .OneFrame<UpdateMoneyValueEvent>()
            .OneFrame<MoneyTransactionEvent>()
            
            .Init();
    }

    private void InitializeAssistants()
    {
        GlobalObjectsContainer.Instance.Init(uiData);
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
