using R3;
using UnityEngine;

namespace TowerDefence.Scripts.InGame.Model
{
    public class TimerModel : MonoBehaviour
    {
        private readonly Subject<float> _timer = new();
        public Observable<float> TimerObservable => _timer;
        
        private void Update()
        {
            _timer.OnNext(Time.deltaTime);
        }
    }
}
