using System;

namespace Project.Scripts.Interfaces
{
    /// <summary>
    /// Periodically dispatches an event
    /// </summary>
    public interface IUpdater
    {
        /// <summary>
        /// Dispatched periodically
        /// </summary>
        event Action OnUpdate;
 
        /// <summary>
        /// Start dispatching OnUpdate events
        /// </summary>
        void Start();
 
        /// <summary>
        /// Stop dispatching OnUpdate events
        /// </summary>
        void Stop();
    }
}