#if (IL2CPPMELON)
using S1Messaging = Il2CppScheduleOne.Messaging;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1Messaging = ScheduleOne.Messaging;
#endif
using System;

namespace S1API.Messaging
{
    /// <summary>
    /// Represents a message response displayed for the player.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// INTERNAL: The instance of the response in-game.
        /// </summary>
        internal readonly S1Messaging.Response S1Response;

        /// <summary>
        /// Creates a new response for displaying in-game.
        /// </summary>
        public Response() => S1Response = new S1Messaging.Response();

        /// <summary>
        /// INTERNAL: Creates a response from the in-game response instance.
        /// </summary>
        /// <param name="response"></param>
        internal Response(S1Messaging.Response response) => S1Response = response;

        /// <summary>
        /// A callback for when the response is triggered.
        /// </summary>
        public Action? OnTriggered
        {
            get => S1Response.callback == null ? null : new Action(() => S1Response.callback.Invoke());
            set => S1Response.callback = value;
        }

        /// <summary>
        /// The unique identifier for this response.
        /// </summary>
        public string Label
        {
            get => S1Response.label;
            set => S1Response.label = value;
        }

        /// <summary>
        /// The text displayed in-game for the player.
        /// </summary>
        public string Text
        {
            get => S1Response.text;
            set => S1Response.text = value;
        }
    }
}
