using JetBrains.Annotations;
using Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Tests.Play
{
    [UsedImplicitly]
    internal sealed class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            var playerInput = new GameObject("PlayerInput").AddComponent<TMP_InputField>();
            
            var playerSymbol = new GameObject("PlayerSymbol").AddComponent<Image>();
            var opponentSymbol = new GameObject("OpponentSymbol").AddComponent<Image>();
            
            var countdownText = new GameObject("CountdownText").AddComponent<TextMeshProUGUI>();
            var resultText = new GameObject("ResultText").AddComponent<TextMeshProUGUI>();
            
            Container.BindInterfacesAndSelfTo<PlayerInputPresenter>().AsSingle().WithArguments(playerInput).NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlayerSymbolPresenter>().AsSingle().WithArguments(playerSymbol).NonLazy();
            Container.BindInterfacesAndSelfTo<OpponentSymbolPresenter>().AsSingle().WithArguments(opponentSymbol).NonLazy();
            
            Container.BindInterfacesAndSelfTo<CountdownPresenter>().AsSingle().WithArguments(countdownText).NonLazy();
            Container.BindInterfacesAndSelfTo<ResultTextPresenter>().AsSingle().WithArguments(resultText).NonLazy();
        }
    }
}