using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Game;
using Project.Scripts.Sprites;
using Project.Scripts.Timer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
// using Random = UnityEngine.Random;

namespace Project.Scripts.Output
{
    /// <summary>
    /// Abstract base class
    /// </summary>
    public abstract class OutputPresenter
    {
        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="symbol"></param>
        protected OutputPresenter(Image symbol)
        {
        }
    }
    
    /// <summary>
    /// Class to present the players symbol output
    /// </summary>
    [UsedImplicitly]
    public sealed class PlayerOutputPresenter : OutputPresenter
    {
        /// <summary>
        /// Static variables (private/public)
        /// </summary>
        public static int inputId;
        private static Image _playerSymbol;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="playerSymbol"></param>
        public PlayerOutputPresenter(Image playerSymbol): base(playerSymbol)
        {
            _playerSymbol = playerSymbol;
        }
        
        /// <summary>
        /// Select player output
        /// </summary>
        /// <param name="input"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SelectPlayerOutput(string output)
        {
            // Assign an id to the player output based on input provided
            inputId = Array.FindIndex(GamePresenter.spriteOptions, s => s.name == output);
        }
        
        /// <summary>
        /// Show the players output
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShowPlayerOutput()
        {
            // Based on the input id set above show the final symbol
            var spriteName = GamePresenter.spriteOptions[inputId].name;
            ImageAtlasPresenter.LoadAtlas(_playerSymbol, null, spriteName);
        }
        
        /// <summary>
        /// Reset the players output
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetPlayerOutput()
        {
            // Assign default sprite to player image
            ImageAtlasPresenter.LoadAtlas(_playerSymbol, null, "question_mark");
        }
    }
    
    /// <summary>
    /// Class to present AI's symbol output
    /// </summary>
    [UsedImplicitly]
    public sealed class AIOutputPresenter : OutputPresenter
    {
        /// <summary>
        /// Constants
        /// </summary>
        private const string url = "http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1";
        
        /// <summary>
        /// Static variables (private/public)
        /// </summary>
        public static int aiFinalResult;
        private static Image _opponentSymbol;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="opponentSymbol"></param>
        public AIOutputPresenter(Image opponentSymbol):base(opponentSymbol)
        {
            _opponentSymbol = opponentSymbol;
        }
        
        /// <summary>
        /// Select AI's output randomly
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void SelectAIRandomOutput()
        {
            // Read the url and store it in a web request
            var www = UnityWebRequest.Get(url);
            
            // Begin communication with remote server and wait till operation is completed
            var operation = www.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();
            
            // Check if there are are any errors or not
            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse data as text
                var urlData = www.downloadHandler.text;
                
                // Convert the text output into string
                var tempDataArray = urlData.Split('[');
                var tempData = tempDataArray[1];
                var finalDataArray = tempData.Split(']');
                
                // Parse integer value from string and store it in variable
                aiFinalResult = int.Parse(finalDataArray[0]);
                
                // Start the timer
                LevelTimerPresenter.StartTimer();
            }
            else
                Debug.LogError(www.error); // Show error message in console
        }
        
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // public static void SelectAIRandomOutput()
        // {
        //     // Select the AI output from a range of numbers from 0 to length of the options array
        //     aiFinalResult = Random.Range(0, GamePresenter.spriteOptions.Length);
        //     
        //     // Start the timer
        //     LevelTimerPresenter.StartTimer();
        // }
        
        /// <summary>
        /// Show the AI's output
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ShowAIOutput()
        {
            // Based on the result found above show the final symbol
            var spriteName = GamePresenter.spriteOptions[aiFinalResult].name;
            ImageAtlasPresenter.LoadAtlas(_opponentSymbol, null, spriteName);
        }
        
        /// <summary>
        /// Reset the AI's output
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetAIOutput()
        {
            // Assign default sprite to AI image
            ImageAtlasPresenter.LoadAtlas(_opponentSymbol, null, "question_mark");
        }
    }
}
