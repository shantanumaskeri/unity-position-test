using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Project.Scripts.Testing
{
    public class SymbolMatchTesting : MonoBehaviour
    {
        private const string url = "http://www.randomnumberapi.com/api/v1.0/random?min=0&max=2&count=1";
        
        public bool Compare()
        {
            return true;
        }
        
        public async Task<int> SelectAIRandomOutput()
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
                var result = int.Parse(finalDataArray[0]);

                return result;
            }
            else
                Debug.LogError(www.error); // Show error message in console
            
            return -1;
        }
    }
}
