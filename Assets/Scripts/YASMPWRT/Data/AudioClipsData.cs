using System;
using System.Collections.Generic;
using UnityEngine;
using YASMPWRT.Enums;

namespace YASMPWRT.Data
{
    public class AudioClipsData
    {
        private static readonly Lazy<AudioClipsData> _instance = new Lazy<AudioClipsData>(() => new AudioClipsData());

        public static AudioClipsData Instance => _instance.Value;
        public AudioClip MusicLoop { get; private set; }
        public Dictionary<SoundType, AudioClip> Sounds { get; private set; }

        public AudioClipsData()
        {
            MusicLoop = Resources.Load<AudioClip>("Sounds/music_loop");

            Sounds = new Dictionary<SoundType, AudioClip>
            {
                {SoundType.Coin, Resources.Load<AudioClip>("Sounds/coin")},
                {SoundType.Key, Resources.Load<AudioClip>("Sounds/key")},
                {SoundType.Jump, Resources.Load<AudioClip>("Sounds/jump")},
                {SoundType.Death, Resources.Load<AudioClip>("Sounds/death")},
                {SoundType.Success, Resources.Load<AudioClip>("Sounds/success")}
            };
        }
    }
}