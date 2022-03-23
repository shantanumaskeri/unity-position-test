using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Game;
using Project.Scripts.Output;
using TMPro;

namespace Project.Scripts.Result
{
    [UsedImplicitly]
    public class LevelResultPresenter
    {
        /// <summary>
        /// Static variables
        /// </summary>
        private static TextMeshProUGUI _resultText;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="resultText"></param>
        public LevelResultPresenter(TextMeshProUGUI resultText)
        {
            _resultText = resultText;
        }
        
        /// <summary>
        /// Calculate the result of level based on players and AI's output
        /// </summary>
        /// <param name="opponentId"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async void CalculateResult(int opponentId)
        {
            // Add delay of 0.5 seconds
            await Task.Delay(500);
            
            // Store player input id in a variable
            var playerId = PlayerOutputPresenter.inputId;
            
            // Check both id's and based on their values and comparing with the other display the result
            var playerOutput = GamePresenter.spriteOptions[playerId].name;
            var opponentOutput = GamePresenter.spriteOptions[opponentId].name;

            _resultText.text = playerOutput switch
            {
                "rock" => opponentOutput switch
                {
                    "paper" => "AI Win!",
                    "scissors" => "Player Win!",
                    "rock" => "Tie!",
                    _ => _resultText.text
                },
                "paper" => opponentOutput switch
                {
                    "scissors" => "AI Win!",
                    "rock" => "Player Win!",
                    "paper" => "Tie!",
                    _ => _resultText.text
                },
                "scissors" => opponentOutput switch
                {
                    "rock" => "AI Win!",
                    "paper" => "Player Win!",
                    "scissors" => "Tie!",
                    _ => _resultText.text
                },
                _ => _resultText.text
            };

            // Add delay of 2.5 seconds
            await Task.Delay(2500);
            
            // reset the game
            GamePresenter.ResetGame();
        }
        
        /// <summary>
        /// Reset results text
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetLevelResult()
        {
            // assign default value to results text field
            _resultText.text = "Result";
        }
    }
}
