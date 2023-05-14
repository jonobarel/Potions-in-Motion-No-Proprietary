using System;
using MoreMountains.Tools;
using UnityEngine;
using Zenject;
using ZeroPrep.UI;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(MMCountdown))]
    public class GamePlayStartTrigger : MonoBehaviour, IDisplayable
    {
       
        private MMCountdown _timer;
        
        
        private PlayerConfigManagement _playerConfigManagement;

        void Awake()
        {
            _timer.CountdownCompleteEvent.AddListener(OnCountdownFinished);
            _playerConfigManagement.OnPlayersReady.AddListener(OnReadyPlayers);
        }

        [Inject]
        void Init(PlayerConfigManagement playerConfigManagement, GameSettings gameSettings)
        {
            _playerConfigManagement = playerConfigManagement;
            _timer = GetComponent<MMCountdown>();
            _timer.CountdownFrom = gameSettings.PlayerReadyDelay;
        }
        void OnReadyPlayers(bool isReady)
        {
            if (isReady)
            {
                _timer.StartCountdown();
            }
            else
            {
                _timer.StopCountdown();
                _timer.ResetCountdown();
            }
        }

        void OnCountdownFinished()
        {
            _timer.StopCountdown();
            _timer.ResetCountdown();
            GetComponent<MMLoadScene>().LoadScene();
        }

        void OnDestroy()
        {
            _timer.CountdownCompleteEvent.RemoveListener(OnCountdownFinished);
            if (_playerConfigManagement != null)
            {
                _playerConfigManagement.OnPlayersReady.RemoveListener(OnReadyPlayers);
            }
        }


        public float Value()
        {
            return _timer.CurrentTime;
        }

        public string ValueString()
        {
            return $"{_timer.CurrentTime: 0}";
        }

        public event Action<float> ValueChanged;
    }
}