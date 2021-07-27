using Components;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class MaterialSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private MaterialData _materialData = null;
        
        private EcsFilter<InputEventComponent, MaterialComponent> _playerMoveFilter = null;
        private static readonly int NormalStrength2 = Shader.PropertyToID("_NormalStrength2");

        public void Init()
        {
            var waterMaterial = _world.NewEntity();

            waterMaterial.Get<MaterialComponent>();
            waterMaterial.Get<InputEventComponent>();
        }
        
        
        public void Run()
        {
            foreach (var i in _playerMoveFilter)
            {
                ref var inputComponent = ref _playerMoveFilter.Get1(i);
                ref var materialComponent = ref _playerMoveFilter.Get2(i);

                var endValue = inputComponent.isTouching ? 0.8f : 0f;
                var currentValue = _materialData.waterMaterial.GetFloat(NormalStrength2);
                currentValue = Mathf.Lerp(currentValue, endValue, Time.deltaTime * _materialData.speedChangeWaterStrength);
                _materialData.waterMaterial.SetFloat(NormalStrength2, currentValue);
            }
        }
    }
}

