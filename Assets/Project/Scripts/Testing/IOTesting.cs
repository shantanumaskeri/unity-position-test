using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Testing
{
    public class IOTesting : MonoBehaviour
    {
        private readonly string[] _testingOptions = {"rock", "paper", "scissors"};
        private const string _testingInput = "rock";

        public bool Compare()
        {
            var list = new List<string>();
            
            for (var i = 0; i < _testingOptions.Length; i++)
            {
                list.Add(_testingOptions[i]);
            }
            
            return list.Contains(_testingInput);
        }
    }
}
