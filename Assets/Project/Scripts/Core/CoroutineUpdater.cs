using System;
using System.Collections;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    /// <summary>
    /// Dispatches an event every frame when a MonoBehaviour's coroutines are resumed
    /// </summary>
    public class CoroutineUpdater : IUpdater
    {
        private MonoBehaviour monoBehaviour;
        private Coroutine coroutine;
 
        /// <summary>
        /// Dispatched every frame
        /// </summary>
        public event Action OnUpdate;
 
        /// <summary>
        /// The MonoBehaviour to run the coroutine with.
        /// Setting this stops any previous coroutine.
        /// </summary>
        public MonoBehaviour MonoBehaviour
        {
            set
            {
                Stop();
                monoBehaviour = value;
                Start();
            }
        }
 
        /// <summary>
        /// Start dispatching OnUpdate every frame
        /// </summary>
        public void Start()
        {
            if (coroutine == null && monoBehaviour)
                coroutine = monoBehaviour.StartCoroutine(DispatchOnUpdate());
        }
 
        /// <summary>
        /// Stop dispatching OnUpdate every frame
        /// </summary>
        public void Stop()
        {
            if (coroutine != null && monoBehaviour)
                monoBehaviour.StopCoroutine(coroutine);
            
            coroutine = null;
        }
 
        private IEnumerator DispatchOnUpdate()
        {
            while (true)
            {
                if (OnUpdate != null)
                    OnUpdate();
                
                yield return null;
            }
        }
    }
}