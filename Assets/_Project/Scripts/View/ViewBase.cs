using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame.View
{
    public class ViewBase : MonoBehaviour, IDisposable
    {
        protected readonly List<IDisposable> Disposables = new List<IDisposable>();
        
        public void Dispose()
        {
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}