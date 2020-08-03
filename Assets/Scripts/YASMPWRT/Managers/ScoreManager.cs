using System;
using UnityEngine;

namespace YASMPWRT.Managers
{
    public sealed class ScoreManager : IDisposable
    {
        private int _score;

        public int Score => _score;

        public ScoreManager()
        {
            Director.Instance.Set(this);
            
            _score = PlayerPrefs.GetInt("Score", 0);
        }
        
        public void Dispose()
        {
            Director.Instance.Remove(this);
            
            PlayerPrefs.SetInt("Score", _score);
        }

        public void AddScore(int value)
        {
            _score += value;
        }

        public void ResetScore()
        {
            _score = 0;
        }
    }
}