using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace S1API.Internal.Abstraction
{
    /// <summary>
    /// INTERNAL: This static class provides us an easy wrapper for subscribing and unsubscribing unity actions.
    /// </summary>
    internal static class EventHelper
    {
        /// <summary>
        /// INTERNAL: Tracking for subscribed actions.
        /// </summary>
        internal static readonly Dictionary<Action, UnityAction> SubscribedActions = new Dictionary<Action, UnityAction>();

        /// <summary>
        /// INTERNAL: Adds a listener to the event, as well as the subscription list.
        /// </summary>
        /// <param name="listener">The action / method you want to subscribe.</param>
        /// <param name="unityEvent">The event you want to subscribe to.</param>
        internal static void AddListener(Action listener, UnityEvent unityEvent)
        {
            if (SubscribedActions.ContainsKey(listener))
            {
	            return;
            }

            UnityAction wrappedListener = (UnityAction)listener.Invoke;
            unityEvent.AddListener(wrappedListener);
            SubscribedActions.Add(listener, wrappedListener);
        }
        
        /// <summary>
        /// INTERNAL: Removes a listener to the event, as well as the subscription list.
        /// </summary>
        /// <param name="listener">The action / method you want to unsubscribe.</param>
        /// <param name="unityEvent">The event you want to unsubscribe from.</param>
        internal static void RemoveListener(Action listener, UnityEvent unityEvent)
        {
            SubscribedActions.TryGetValue(listener, out UnityAction? wrappedAction);
            SubscribedActions.Remove(listener);
            unityEvent.RemoveListener(wrappedAction);
        }
    }
}