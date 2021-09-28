// -----------------------------------------------------------------------
// <copyright file="ScpSwap.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

    [CommandHandler(typeof(ClientCommandHandler))]
    public class ScpSwap : ParentCommand, IUsageProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScpSwap"/> class.
        /// </summary>
        public ScpSwap() => LoadGeneratedCommands();

        /// <inheritdoc />
        public override string Command { get; } = "scpswap";

        /// <inheritdoc />
        public override string[] Aliases { get; } = Array.Empty<string>();

        /// <inheritdoc />
        public override string Description { get; } = "Base command for ScpSwap.";

        /// <inheritdoc />
        public string[] Usage { get; } = { "Scp Number" };

        /// <inheritdoc />
        public sealed override void LoadGeneratedCommands()
        {
            RegisterCommand(new Cancel());
        }

        /// <inheritdoc />
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Round.IsStarted)
            {
                response = "The round has not yet started.";
                return false;
            }

            if (Round.ElapsedTime.TotalSeconds > Plugin.Instance.Config.SwapTimeout)
            {
                response = "The Scp swap period has ended.";
                return false;
            }

            if (arguments.IsEmpty())
            {
                response = $"Usage: .{Command} {string.Join(" ", Usage)}";
                return false;
            }

            Player playerSender = Player.Get(sender as CommandSender);
            if (!playerSender.IsScp)
            {
                response = "Take a good, long look at yourself then try to swap again.";
                return false;
            }

            if (Swap.FromSender(playerSender) != null)
            {
                response = "You already have a pending swap request!";
                return false;
            }

            Player receiver = Player.Get(arguments.At(0));
            if (receiver == null)
            {
                response = "";
                return false;
            }

            Swap.Send(playerSender, receiver);
            response = "Request sent!";
            return true;
        }
    }
}