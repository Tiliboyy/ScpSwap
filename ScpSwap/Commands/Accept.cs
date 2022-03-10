// -----------------------------------------------------------------------
// <copyright file="Accept.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using ScpSwap.Models;

    /// <summary>
    /// Accepts an active swap request.
    /// </summary>
    public class Accept : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "accept";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = { "yes", "y" };

        /// <inheritdoc />
        public string Description { get; set; } = "Accepts an active swap request.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player playerSender = Player.Get(sender);
            if (playerSender == null)
            {
                response = "This command must be from the game level.";
                return false;
            }

            Swap swap = Swap.FromReceiver(playerSender);
            if (swap == null)
            {
                response = "You do not have a pending swap request.";
                return false;
            }

            swap.Run();
            response = "Swap successful!";
            return true;
        }
    }
}