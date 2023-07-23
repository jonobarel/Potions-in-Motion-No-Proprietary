using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using ZeroPrep.MineBuddies;
using Random = UnityEngine.Random;
using Zenject;

namespace ZeroPrep.MineBuddies
{
    public class HazardSpawner
    {
        public float MinTime { get; }

        public float MaxTime { get; }
        protected float Speed => 0.5f;

        public bool IsPaused { get; }

        private bool _stopSpawning = false;

        private HazardTypesActiveInGame _validTypesActiveInGame;

        [Inject] private GameSettings _gameSettings;

        protected HazardManagerGO.InteractionType[] _interactions;
        
        protected HazardType[] Types => _validTypesActiveInGame.Types;

        /*
        Array values = Enum.GetValues(typeof(Bar));
        Random random = new Random();
        Bar randomBar = (Bar)values.GetValue(random.Next(values.Length));
        */
        #region Constructor

        [Inject]
        public HazardSpawner(float minTime, float maxTime, HazardTypesActiveInGame validTypesActiveInGame, MineBuddiesMultiplayerLevelManager levelManager)
        {
            MinTime = minTime;
            MaxTime = maxTime;
            _validTypesActiveInGame = validTypesActiveInGame;

            if (levelManager != null)
            {
                _interactions = levelManager.AvailableInteractions.ToArray();
            }
            else
            {
                _interactions = Enum.GetValues(typeof(HazardManagerGO.InteractionType)) as HazardManagerGO.InteractionType[];
            }
        }

        #endregion
        
        private IEnumerator SpawnNextHazardCoRoutine()
        {
            while (!_stopSpawning)
            {
                while (IsPaused)
                {
                    yield return null;
                }

                float delay = Random.Range(MinTime, MaxTime);
                yield return new WaitForSeconds(delay);
                
                //spawn!
                SpawnRandomTypeHazard();
            }

            // removed due to triviality, but make sure to test this
            //_stopSpawning = false;
        }

        public void StopSpawning()
        {
            _stopSpawning = true;
        }

        public void StartSpawning(MonoBehaviour caller)
        {
            _stopSpawning = false;
            caller.StartCoroutine(SpawnNextHazardCoRoutine());
        }
        
        public virtual HazardBase SpawnRandomTypeHazard()
        {
            var newType = Types[(Random.Range(0, Types.Length))];
            HazardExternal h = new HazardExternal(Speed, newType, _interactions[HazardBase.HazardClassID % _interactions.Length ], _gameSettings.HazardStartingHealth);
            return h;
        }

        public bool IsSpawning()
        {
            return !_stopSpawning;
        }
        
    }
}