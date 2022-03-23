using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Project.Scripts.Output;
using Project.Scripts.Player;
using Project.Scripts.Result;
using Project.Scripts.Testing;
using Project.Scripts.Timer;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Zenject;

namespace Project.Tests.Play
{
    internal sealed class Example : ZenjectIntegrationTestFixture
    {
        [Inject] private PlayerInputPresenter playerInputPresenter;
        
        [Inject] private PlayerOutputPresenter playerSymbolPresenter;
        [Inject] private AIOutputPresenter opponentSymbolPresenter;

        [Inject] private LevelTimerPresenter countdownPresenter;
        [Inject] private LevelResultPresenter resultPresenter;
        
        [SetUp]
        public void CommonInstall()
        {
            PreInstall();
            GameInstaller.Install(Container);
            PostInstall();
            Container.Inject(this);
        }

        [UnityTest]
        public IEnumerator Installation_Succeeds()
        {
            yield break;
        }

        [UnityTest]
        public IEnumerator Load_URL_Success() => UniTask.ToCoroutine(async () =>
        {
            var request = await UnityWebRequest.Get("http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1").SendWebRequest();
            Assert.That(request.result, Is.EqualTo(UnityWebRequest.Result.Success));
        });
        
        [UnityTest]
        public IEnumerator Get_Correct_Random_Numbers() => UniTask.ToCoroutine(async () =>
        {
            var request = await UnityWebRequest.Get("http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1").SendWebRequest();
            
            var urlData = request.downloadHandler.text;
            
            // Convert the text output into string
            var tempDataArray = urlData.Split('[');
            var tempData = tempDataArray[1];
            var finalDataArray = tempData.Split(']');
                
            // Parse integer value from string and store it in variable
            var result = int.Parse(finalDataArray[0]);

            Assert.That(result, Is.LessThanOrEqualTo(2));
            Assert.That(result, Is.GreaterThanOrEqualTo(0));
        });
        
        [UnityTest]
        public IEnumerator Return_Correct_Symbol_For_Input_Values()
        {
            var ioTesting = new GameObject().AddComponent<IOTesting>();
            var options = ioTesting.Compare();

            yield return options;
            
            Assert.AreEqual(1, Convert.ToInt32(options));
        }
    }
}