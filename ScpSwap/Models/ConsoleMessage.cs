// -----------------------------------------------------------------------
// <copyright file="ConsoleMessage.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Models
{
    using Exiled.API.Features;

    /// <summary>
    /// Container to make translations for console messages more fun.
    /// </summary>
    public struct ConsoleMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMessage"/> struct.
        /// </summary>
        /// <param name="message"><inheritdoc cref="Message"/></param>
        /// <param name="color"><inheritdoc cref="Color"/></param>
        public ConsoleMessage(string message, string color)
        {
            Message = message;
            Color = color;
        }

        /// <summary>
        /// Gets the message to send.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the color to use.
        /// </summary>
        public string Color { get; }

        /// <summary>
        /// Sends the message to a given player.
        /// </summary>
        /// <param name="player">The player to send the message to.</param>
        public void SendTo(Player player) => player.SendConsoleMessage(Message, Color);
    }
}