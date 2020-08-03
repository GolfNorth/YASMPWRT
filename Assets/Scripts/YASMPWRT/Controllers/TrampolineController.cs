using System.Collections;
using UnityEngine;
using YASMPWRT.Interfaces;
using YASMPWRT.Managers;
using YASMPWRT.Models;
using YASMPWRT.Views;

namespace YASMPWRT.Controllers
{
    public sealed class TrampolineController : IController<TrampolineController>
    {
        private readonly TrampolineModel _model;
        private readonly LevelManager _levelManager;

        public bool IsReloading => _model.Reloading;

        public float Power
        {
            set => _model.Power = value;
        }
        
        public bool Flipped
        {
            set => _model.Flipped = value;
        }

        public float ReloadSpeed
        {
            set => _model.ReloadSpeed = value;
        }
        
        public TrampolineController(TrampolineView view)
        {
            _model = new TrampolineModel();
            _levelManager = Director.Instance.Get<LevelManager>();
        }
        
        public void Dispose()
        {
        }

        public void Throw()
        {
            if (_model.Reloading) return;

            _model.Reloading = true;
            
            var direction = _model.Flipped
                ? - _model.Power
                : _model.Power;
            
            _levelManager.ThrowPlayer(direction);
            
            Director.Instance.RunCoroutine(Reloading());
        }
        
        private IEnumerator Reloading()
        {
            var duration = 0f;
            
            while(duration < _model.ReloadSpeed)
            {
                duration += Time.deltaTime;
                
                yield return null;
            }
            
            _model.Reloading = false;
        }
    }
}