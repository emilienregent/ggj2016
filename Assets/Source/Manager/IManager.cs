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
        public ManagerType type { get; set;}
    }
}

