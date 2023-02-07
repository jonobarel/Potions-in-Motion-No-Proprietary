using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace ZeroPrep.MineBuddies
{
    [RequireComponent(typeof(MMCountdown))]
    public class GamePlayStartTrigger : MonoBehaviour
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
            throw new NotImplementedException("Timer expired, scene transition should happen here");
        }

        void OnDestroy()
        {
            _timer.CountdownCompleteEvent.RemoveListener(OnCountdownFinished);
            if (_playerConfigManagement != null)
            {
                _playerConfigManagement.OnPlayersReady.RemoveListener(OnReadyPlayers);
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}