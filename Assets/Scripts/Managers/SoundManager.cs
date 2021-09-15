using System;
using System.Collections.Generic;
using Data;
using Types;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public static class SoundManager 
    {
        private static SoundData _soundData;
        private static bool _init;

        private static Dictionary<SoundType, float> _soundTimerDictionary;
        private static AudioSource _spatialOneShootSource;
        private static AudioSource _flatOneShootSource;
        
        public static void Init(SoundData soundData)
        {
            _soundData = soundData;
            _spatialOneShootSource = Object.Instantiate(_soundData.spatialOneShootSourcePrefab);
            _flatOneShootSource = Object.Instantiate(_soundData.flatOneShootSourcePrefab);
            _soundTimerDictionary = new Dictionary<SoundType, float>();
            
            _init = true;
        }

        public static void PlayOneShoot(SoundType soundType, bool spatialSource = false, Vector3 spatialPosition = default)
        {
            if (!_init) throw new Exception("Manager must be initialized");

            AudioSource oneShootSource;
            if (spatialSource)
            {
                oneShootSource = _spatialOneShootSource;
                oneShootSource.transform.position = spatialPosition;
            }
            else
            {
                oneShootSource = _flatOneShootSource;
            }
            
            oneShootSource.PlayOneShot(GetSound(soundType));
        }

        public static void Play(SoundType soundType, bool spatialSource = false, Vector3 spatialPosition = default)
        {
            if (!_init) throw new Exception("Manager must be initialized");

            AudioSource musicSource;
            if (spatialSource)
            {
                musicSource = Object.Instantiate(_soundData.spatialMusicSourcePrefab);
                musicSource.transform.position = spatialPosition;
            }
            else
            {
                musicSource = Object.Instantiate(_soundData.flatMusicSourcePrefab);
            }
            musicSource.clip = GetSound(soundType);
            musicSource.Play();
        }

        private static AudioClip GetSound(SoundType soundType)
        {
            return _soundData.soundList.Find(sound => sound.soundType == soundType).audioClip;
        }
    }
}
