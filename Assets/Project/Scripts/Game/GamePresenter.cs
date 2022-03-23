using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Project.Scripts.Output;
using Project.Scripts.Player;
using Project.Scripts.Result;
using Project.Scripts.Sprites;
using Project.Scripts.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game
{
    [UsedImplicitly]
    public class GamePresenter
    {
        /// <summary>
        /// Static variables (private/public)
        /// </summary>
        public static Sprite[] spriteOptions;
        private static Image _playerImage;
        private static Image _opponentImage;
        
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="_spriteOptions"></param>
        /// <param name="playerImage"></param>
        /// <param name="opponentImage"></param>
        public GamePresenter(Sprite[] _spriteOptions, Image playerImage, Image opponentImage)
        {
            spriteOptions = _spriteOptions;
            _playerImage = playerImage;
            _opponentImage = opponentImage;
            
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private static void Init()
        {
            // Load default images on game start
            ImageAtlasPresenter.LoadAtlas(_playerImage, null, "question_mark");
            ImageAtlasPresenter.LoadAtlas(_opponentImage, null, "question_mark");
        }

        /// <summary>
        /// Reset the game to initial state
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResetGame()
        {
            // Reset all the components on each object i.e. player, AI, timer, result, to their initial states
            PlayerInputPresenter.ResetPlayerInput();
            PlayerOutputPresenter.ResetPlayerOutput();
            AIOutputPresenter.ResetAIOutput();
            LevelTimerPresenter.ResetLevelTimer();
            LevelResultPresenter.ResetLevelResult();
        }
    }
}
