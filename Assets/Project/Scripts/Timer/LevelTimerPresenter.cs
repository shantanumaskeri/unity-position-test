using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Project.Scripts.Interfaces;
using Project.Scripts.Output;
using Project.Scripts.Result;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Timer
{
    [UsedImplicitly]
    public sealed class LevelTimerPresenter
    {
        /// <summary>
        /// Constants
        /// </summary>
        private const string emptyString = "";
        
        /// <summary>
        /// Static variables
        /// </summary>
        private static TextMeshProUGUI _timerText;
        private static float _levelTime = 3;
        private static IUpdater _updater;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="timerText"></param>
        /// <param name="updater"></param>
        public LevelTimerPresenter(TextMeshProUGUI timerText, IUpdater updater)
        {
            _timerText = timerText;
            _updater = updater;
        }
        
        /// <summary>
        /// Handle the update loop every frame
        /// </summary>
        public static void HandleUpdate()
        {
            // Reduce level time by Time.deltaTime
            _levelTime -= Time.deltaTime;
            
            // Display the updated time in the text field
            _timerText.text = emptyString+Mathf.Round(_levelTime);
            
            // Check if time is less than 0
            if (_levelTime < 0f)
            {
                // Clamp value to 0
                _levelTime = 0f;
                
                // Show outputs of player and AI
                PlayerOutputPresenter.ShowPlayerOutput();
                AIOutputPresenter.ShowAIOutput();
                
                // Calculate the result
                LevelResultPresenter.CalculateResult(AIOutputPresenter.aiFinalResult);
                
                // Unsubscribe to update event
                _updater.OnUpdate -= HandleUpdate;
            }
        }

        /// <summary>
        /// Starts the countdown timer process
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartTimer()
        {
            // Set initial value of timer to the text field
            _timerText.text = emptyString+_levelTime;
            
            // Subscribe to update event
            _updater.OnUpdate += HandleUpdate;
        }

        /// <summary>
        /// Resets the level timer
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetLevelTimer()
        {
            // Reset value of timer to initial value
            _levelTime = 3f;
        }
    }
}
