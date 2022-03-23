using Project.Scripts.Core;
using Project.Scripts.Game;
using Project.Scripts.Output;
using Project.Scripts.Player;
using Project.Scripts.Result;
using Project.Scripts.Sprites;
using Project.Scripts.Timer;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Installers
{
    internal sealed class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private Sprite[] playerInputOptions;
        
        [SerializeField] private TMP_InputField playerInput;
        
        [SerializeField] private Image playerInputFieldImage;
        [SerializeField] private Image playerSymbol;
        [SerializeField] private Image opponentSymbol;
        
        [SerializeField] private TMP_Text countdownText;
        [SerializeField] private TMP_Text resultText;
        
        [SerializeField] private SpriteAtlas imageAtlas;
        
        private CoroutineUpdater _updater;
        
        public override void InstallBindings()
        {
            // Make the updater
            _updater = new CoroutineUpdater {MonoBehaviour = this};
            
            Container.BindInterfacesAndSelfTo<PlayerInputPresenter>().AsSingle().WithArguments(playerInput, playerInputFieldImage, _updater).NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerOutputPresenter>().AsSingle().WithArguments(playerSymbol).NonLazy();
            Container.BindInterfacesAndSelfTo<AIOutputPresenter>().AsSingle().WithArguments(opponentSymbol).NonLazy();
            
            Container.BindInterfacesAndSelfTo<LevelTimerPresenter>().AsSingle().WithArguments(countdownText, _updater).NonLazy();
            Container.BindInterfacesAndSelfTo<LevelResultPresenter>().AsSingle().WithArguments(resultText).NonLazy();
            
            Container.BindInterfacesAndSelfTo<ImageAtlasPresenter>().AsSingle().WithArguments(imageAtlas).NonLazy();
            
            Container.BindInterfacesAndSelfTo<GamePresenter>().AsSingle().WithArguments(playerInputOptions, playerSymbol, opponentSymbol).NonLazy();
        }

        private void OnApplicationQuit()
        {
            UnsubscribeEvents();
        }

        private void UnsubscribeEvents()
        {
            if (_updater != null)
            {
                _updater.OnUpdate -= PlayerInputPresenter.HandleUpdate;
                _updater.OnUpdate -= LevelTimerPresenter.HandleUpdate;

                _updater = null;    
            }
        }
    }
}