using System;
using Data;
using Systems;
using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    private EcsWorld _world;
    
    private EcsSystems _updateSystems;
    private EcsSystems _fixedUpdateSystem;

    [Header("Data")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private MaterialData materialData;

    private void Start()
    {
        _world = new EcsWorld();

        _updateSystems = new EcsSystems(_world);
        _fixedUpdateSystem = new EcsSystems(_world);

        #region Update systems
        _updateSystems.Add(new PlayerSpawnSystem());
        _updateSystems.Add(new InputSystem());
        _updateSystems.Add(new MaterialSystem());
        #endregion

        #region FixedUpdate systems
        _fixedUpdateSystem.Add(new MoveSystem());
        #endregion
        
        #region Data
        _updateSystems.Inject(playerData);
        _updateSystems.Inject(materialData);
        #endregion

        #region Init
        _updateSystems.Init();
        _fixedUpdateSystem.Init();
        #endregion
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
