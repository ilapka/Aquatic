using System.Collections.Generic;
using Components;
using Leopotam.Ecs;
using UnityComponents;
using UnityEngine;

namespace Systems.PipeRing
{
    public sealed class ClearRubbishSystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter  = null;
        private readonly EcsFilter<RubbishComponent> _locationFilter  = null;

        public void PreInit()
        {
            _world.NewEntity().Get<RubbishComponent>().RubbishList = new List<RubbishInformation>();
        }
        
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var playerTransform = _playerFilter.Get1(i).PlayerInformation.transform;
                
                foreach (var j in _locationFilter)
                {
                    var rubbishList = _locationFilter.Get1(j).RubbishList;
                    ClearRubbish(playerTransform, ref rubbishList);
                }
            }
        }
        
        private void ClearRubbish(Transform playerTransform, ref List<RubbishInformation> rubbishList)
        {
            for (int i = 0; i < rubbishList.Count; i++)
            {
                if (rubbishList[i] == null)
                {
                    rubbishList.Remove(rubbishList[i]);
                    return;
                }

                if(playerTransform.position.x < rubbishList[i].transform.position.x - rubbishList[i].distanceFromPlayerToRemove)
                {
                    Object.Destroy(rubbishList[i].gameObject);
                    rubbishList.Remove(rubbishList[i]);
                }
            }
        }
    }
}
