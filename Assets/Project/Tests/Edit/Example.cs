using System.Collections;
using Cysharp.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Zenject;

namespace Project.Tests.Edit
{
    internal sealed class Example : ZenjectUnitTestFixture
    {
        [Inject] private IBoundary boundary;

        [SetUp]
        public void CommonInstall()
        {
            Container.BindInstance(Substitute.For<IBoundary>());
            Container.Inject(this);
        }

        [Test]
        public void Boundary_Receives_Call()
        {
            boundary.Do();
            boundary.Received(1).Do();
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

        public interface IBoundary
        {
            void Do();
        }
    }
}
