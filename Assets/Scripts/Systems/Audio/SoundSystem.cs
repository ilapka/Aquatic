using System;
using System.Collections.Generic;
using System.Data;
using Components;
using Components.Events;
using Data;
using Leopotam.Ecs;
using Types;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Systems.Audio
{
    public class SoundSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SoundData _soundData = null;

        private readonly EcsFilter<AudioSourceComponent> _audioSourceFilter = null;
        private readonly EcsFilter<PlayOneShootSpatialEvent> _playOneShootSpatialEvent = null;
        private readonly EcsFilter<PlayOneShootFlatEvent> _playOneShootFlatEvent = null;
        
        public void Init()
        {
            var audioSourceComponent = new AudioSourceComponent()
            {
                SpatialOneShootSource = Object.Instantiate(_soundData.spatialOneShootSourcePrefab),
                FlatOneShootSource = Object.Instantiate(_soundData.flatOneShootSourcePrefab),
            };
            _world.NewEntity().Replace(audioSourceComponent);
        }

        public void Run()
        {
            foreach (var i in _playOneShootSpatialEvent)
            {
                var playOneShootFlatEvent = _playOneShootSpatialEvent.Get1(i);

                foreach (var j in _audioSourceFilter)
                {
                    var spatialSource = _audioSourceFilter.Get1(j).SpatialOneShootSource;
                    spatialSource.transform.position = playOneShootFlatEvent.Position;
                    spatialSource.PlayOneShot(GetSound(playOneShootFlatEvent.SoundType));
                }
            }
            
            foreach (var i in _playOneShootFlatEvent)
            {
                var playOneShootFlatEvent = _playOneShootFlatEvent.Get1(i);

                foreach (var j in _audioSourceFilter)
                {
                    var flatSource = _audioSourceFilter.Get1(j).FlatOneShootSource;
                    flatSource.PlayOneShot(GetSound(playOneShootFlatEvent.SoundType));
                }
            }
        }
        
        private AudioClip GetSound(SoundType soundType)
        {
            return _soundData.soundList.Find(sound => sound.soundType == soundType).audioClip;
        }
    }
}
