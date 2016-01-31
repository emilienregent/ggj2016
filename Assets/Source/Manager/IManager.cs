using System;
using UnityEngine;

namespace Manager
{
    public enum ManagerType
    {
        MAP,
        AUDIO,
        CONTROL
    }

    public abstract class IManager : MonoBehaviour
    {
        public IManager()
        {
        }

        abstract public void Initialize();
        abstract public void StartGame();
        public ManagerType type { get; set;}
    }
}

