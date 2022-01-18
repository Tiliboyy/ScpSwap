// -----------------------------------------------------------------------
// <copyright file="List.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Commands
{
    using System;
    using System.Text;
    using CommandSystem;
    using NorthwoodLib.Pools;

    /// <summary>
    /// Lists all valid swappable roles.
    /// </summary>
    public class List : ICommand
    {
        /// <inheritdoc />
        public string Command { get; } = "list";

        /// <inheritdoc />
        public string[] Aliases { get; } = { "l" };

        /// <inheritdoc />
        public string Description { get; } = "Lists all valid swappable roles.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            StringBuilder stringBuilder = StringBuilderPool.Shared.Rent();
            stringBuilder.AppendLine("Available Roles:");
            stringBuilder.Append(string.Join(Environment.NewLine, ValidSwaps.Names));
            response = StringBuilderPool.Shared.ToStringReturn(stringBuilder);
            return true;
        }
    }
}