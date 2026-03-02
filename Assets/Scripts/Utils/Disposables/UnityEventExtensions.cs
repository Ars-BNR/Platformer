using System;
using UnityEngine.Events;

namespace Platformer.Utils.Disposables
{
    public static class UnityEventExtensions
    {
        public static IDisposable Subsrcibe(this UnityEvent unityEvent, UnityAction call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveListener(call));
        }
        public static IDisposable Subscibe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveListener(call));
        }
    }
}
