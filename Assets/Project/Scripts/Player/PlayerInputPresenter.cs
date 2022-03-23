using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Project.Scripts.Interfaces;
using Project.Scripts.Output;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Player
{
    [UsedImplicitly]
    public sealed class PlayerInputPresenter
    {
        /// <summary>
        /// Constants
        /// </summary>
        private const string rockInput = "rock";
        private const string paperInput = "paper";
        private const string scissorsInput = "scissors";
        private const string empty = "";

        /// <summary>
        /// Static/read-only variables
        /// </summary>
        private static TMP_InputField _playerOptionInputField;
        private static Image _playerOptionInputFieldImage;
        private static IUpdater _updater;
        private static readonly List<string> _playersInput = new List<string>();
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="field"></param>
        /// <param name="playerOptionInputFieldImage"></param>
        /// <param name="updater"></param>
        public PlayerInputPresenter(TMP_InputField field, Image playerOptionInputFieldImage, IUpdater updater)
        {
            _playerOptionInputField = field;
            _playerOptionInputFieldImage = playerOptionInputFieldImage;
            _updater = updater;
            
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private static void Init()
        {
            // Subscribe to update event
            _updater.OnUpdate += HandleUpdate;
            
            CreateOptionsList();
        }
        
        /// <summary>
        /// Handle the update loop every frame
        /// </summary>
        public static void HandleUpdate()
        {
            // Check if the enter key has been pressed
            if (Input.GetKeyDown(KeyCode.Return))
                ValidatePlayerInputOption();
        }
        
        /// <summary>
        /// Create list of options
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CreateOptionsList()
        {
            // Add input options to the list
            _playersInput.Add(rockInput);
            _playersInput.Add(paperInput);
            _playersInput.Add(scissorsInput);
        }
        
        /// <summary>
        /// Validate the input provided by player
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ValidatePlayerInputOption()
        {
            // Cache the input field text to a variable
            var input = _playerOptionInputField.text;
            
            // Check if input is blank and exit
            if (input == empty)
                return;
            
            // Check if the list contains the input text and exit if it doesn't
            if (!_playersInput.Contains(input))
                return;
            
            // Disable the input field text from additional input during game
            _playerOptionInputField.enabled = false;
            _playerOptionInputFieldImage.color = Color.gray;
            
            // Assign the output value based on the input given
            PlayerOutputPresenter.SelectPlayerOutput(input);
            
            // Start AI process to select output randomly 
            AIOutputPresenter.SelectAIRandomOutput();
            
            // Unsubscribe to update event
            _updater.OnUpdate -= HandleUpdate;
        }
        
        /// <summary>
        /// Reset players input
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetPlayerInput()
        {
            // Enable the input field text when game resets
            _playerOptionInputField.enabled = true;
            _playerOptionInputFieldImage.color = Color.white;
            
            // Set the input text to blank
            _playerOptionInputField.text = empty;
            
            _updater.OnUpdate += HandleUpdate;
        }
    }
}