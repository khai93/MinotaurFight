using System;
using UnityEngine;

namespace MinotaurFight.Core
{
    public abstract class BaseState
    {
        protected GameObject gameObject;
        protected Transform transform;

        public BaseState(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public abstract Type Tick();
    }
}
