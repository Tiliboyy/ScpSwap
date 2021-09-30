// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System;
    using Exiled.API.Enums;
    using Exiled.API.Features;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config, Translation>
    {
        private EventHandlers eventHandlers;

        /// <summary>
        /// Gets the only existing instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc />
        public override string Author { get; } = "Build";

        /// <inheritdoc />
        public override string Name { get; } = "ScpSwap";

        /// <inheritdoc />
        public override string Prefix { get; } = "scpswap";

        /// <inheritdoc />
        public override PluginPriority Priority { get; } = PluginPriority.Higher;

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;
            eventHandlers = new EventHandlers(this);
            Exiled.Events.Handlers.Server.ReloadedConfigs += eventHandlers.OnReloadedConfigs;
            Exiled.Events.Handlers.Server.RestartingRound += eventHandlers.OnRestartingRound;
            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Server.ReloadedConfigs -= eventHandlers.OnReloadedConfigs;
            Exiled.Events.Handlers.Server.RestartingRound -= eventHandlers.OnRestartingRound;
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
            eventHandlers = null;
            Instance = null;
            base.OnDisabled();
        }
    }
}