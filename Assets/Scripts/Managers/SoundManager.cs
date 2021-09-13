using System;
using Data;
using Types;
using UnityEngine;
using Object = System.Object;

namespace Managers
{
    public static class SoundManager 
    {
        private static SoundData _soundData;
        private static bool _init;

        private static AudioSource _oneShotAudioSource;

        public static void Init(SoundData soundData)
        {
            _soundData = soundData;
            _oneShotAudioSource = new GameObject().AddComponent<AudioSource>();
            _oneShotAudioSource.outputAudioMixerGroup = _soundData.audioMixer.outputAudioMixerGroup;
            
            _init = true;
        }

        public static void PlaySound(SoundType soundType)
        {
            if (!_init) throw new Exception("Manager must be initialized");
            
            _oneShotAudioSource.PlayOneShot(GetSound(soundType));
            
            //Это все нужно будет разложить на 3 сисетмы !!!
            //Out put'ы всех сурсов выставлять на миксер из даты
            
            //Play one shot - все что нужно проиграть 1 раз - засовывать в один и тот же сурс и двигать его на координаты из ивента
            
            //Play - если дело касается долгих циклических звуков (музыка, амбиент и тд)
            //- для него создать новый сурс и запихать под родителя из ивента
            
            //Subscribe button - надо подумать делать ли (чекнуть еще раз видос) (или сабскрайб в целом на ивент если так можно)
            //Для звуков меню выставлять игнор паузы листенера !)))
        }

        private static AudioClip GetSound(SoundType soundType)
        {
            return _soundData.soundList.Find(sound => sound.soundType == soundType).audioClip;
        }
    }
}
