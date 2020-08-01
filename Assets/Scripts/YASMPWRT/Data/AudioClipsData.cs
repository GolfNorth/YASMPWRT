using System;
using UnityEngine;

namespace YASMPWRT.Data
{
    public class AudioClipsData
    {
        private static Lazy<AudioClipsData> _instance = new Lazy<AudioClipsData>(() => new AudioClipsData());

        public static AudioClipsData Instance => _instance.Value;
        public AudioClip MusicLoop { get; private set; }

        public AudioClipsData()
        {
            MusicLoop = Resources.Load<AudioClip>("Sounds/music_loop");
        }
    }
}