using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(MMCountdown))]
    public class GamePlayStartTrigger : MonoBehaviour, IDisplayable
    {
       
        private MMCountdown _timer;

        private PlayerConfigManagement _playerConfigManagement;

        void Awake()
        {
            _timer = GetComponent<MMCountdown>();
            _timer.CountdownCompleteEvent.AddListener(OnCountdownFinished);
            _playerConfigManagement = GetComponentInParent<PlayerConfigManagement>();
            _playerConfigManagement.OnPlayersReady.AddListener(OnReadyPlayers);
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
            MMSceneLoadingManager.LoadScene("CorgiCarriage");
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