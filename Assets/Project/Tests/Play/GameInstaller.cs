using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Output;
using Project.Scripts.Player;
using Project.Scripts.Result;
using Project.Scripts.Timer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Tests.Play
{
    [UsedImplicitly]
    internal sealed class GameInstaller : Installer<GameInstaller>
    {
        private CoroutineUpdater _updater;
        
        public override void InstallBindings()
        {
            var playerInput = new GameObject("PlayerInput").AddComponent<TMP_InputField>();
            var playerInputFieldImage = new GameObject("PlayerInputField").AddComponent<Image>();
            
            var playerSymbol = new GameObject("PlayerOutput").AddComponent<Image>();
            var opponentSymbol = new GameObject("AIOutput").AddComponent<Image>();
            
            var countdownText = new GameObject("LevelTimer").AddComponent<TextMeshProUGUI>();
            var resultText = new GameObject("LevelResult").AddComponent<TextMeshProUGUI>();
            
            // Make the updater
            _updater = new CoroutineUpdater();
            
            Container.BindInterfacesAndSelfTo<PlayerInputPresenter>().AsSingle().WithArguments(playerInput, playerInputFieldImage, _updater).NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerOutputPresenter>().AsSingle().WithArguments(playerSymbol).NonLazy();
            Container.BindInterfacesAndSelfTo<AIOutputPresenter>().AsSingle().WithArguments(opponentSymbol).NonLazy();
            
            Container.BindInterfacesAndSelfTo<LevelTimerPresenter>().AsSingle().WithArguments(countdownText, _updater).NonLazy();
            Container.BindInterfacesAndSelfTo<LevelResultPresenter>().AsSingle().WithArguments(resultText).NonLazy();
        }
    }
}