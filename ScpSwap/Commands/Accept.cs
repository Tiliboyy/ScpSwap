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

    public class Accept : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "accept";

        /// <inheritdoc />
        public string[] Aliases { get; } = { "yes" };

        /// <inheritdoc />
        public string Description { get; } = "Accepts an current swap request.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player playerSender = Player.Get(sender as CommandSender);
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