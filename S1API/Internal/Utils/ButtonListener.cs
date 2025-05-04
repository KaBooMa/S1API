using System;
using S1API.Internal.Abstraction;
using UnityEngine;
using UnityEngine.UI;

namespace S1API.Internal.Utils
{
    /// <summary>
    /// Utility class for managing UI Buttons.
    /// TODO (@omar-akermi): Is this intended to be internal instead of public??
    /// </summary>
    public static class ButtonUtils
    {
        /// <summary>
        /// Adds a click listener to the specified button, ensuring compatibility with IL2CPP and Mono.
        /// </summary>
        public static void AddListener(Button button, Action action)
        {
            if (button == null || action == null)
            {
	            return;
            }

            EventHelper.AddListener(action, button.onClick);
        }

        /// <summary>
        /// Removes a previously added click listener from the specified button.
        /// </summary>
        public static void RemoveListener(Button button, Action action)
        {
            if (button == null || action == null)
            {
	            return;
            }

            EventHelper.RemoveListener(action, button.onClick);
        }

        /// <summary>
        /// Removes all listeners from the specified button safely.
        /// </summary>
        public static void ClearListeners(Button button)
        {
            if (button == null)
            {
	            return;
            }

            button.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// Enables the button and optionally updates the label.
        /// </summary>
        public static void Enable(Button button, Text? label = null, string? text = null)
        {
            if (button != null)
            {
	            button.interactable = true;
            }

            if (label != null && !string.IsNullOrEmpty(text))
            {
	            label.text = text;
            }
        }

        /// <summary>
        /// Disables the button and optionally updates the label.
        /// </summary>
        public static void Disable(Button button, Text? label = null, string? text = null)
        {
            if (button != null)
            {
	            button.interactable = false;
            }

            if (label != null && !string.IsNullOrEmpty(text))
            {
	            label.text = text;
            }
        }

        /// <summary>
        /// Sets the label text of a button with a known Text child.
        /// </summary>
        public static void SetLabel(Text label, string text)
        {
            if (label != null)
            {
	            label.text = text;
            }
        }
        /// <summary>
        /// Sets the button label and background color.
        /// </summary>
        public static void SetStyle(Button button, Text label, string text, Color bg)
        {
            if (button == null || label == null)
            {
	            return;
            }

            label.text = text;
            button.image.color = bg;
        }
    }
}