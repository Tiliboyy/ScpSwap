// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System;
    using Exiled.API.Features;
    using PlayerHandlers = Exiled.Events.Handlers.Player;
    using ServerHandlers = Exiled.Events.Handlers.Server;

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
        public override string Author => "Build";

        /// <inheritdoc />
        public override string Name => "ScpSwap";

        /// <inheritdoc />
        public override string Prefix => "ScpSwap";

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(4, 2, 2);

        /// <inheritdoc />
        public override Version Version { get; } = new Version(1, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;

            eventHandlers = new EventHandlers(this);
            PlayerHandlers.ChangingRole += eventHandlers.OnChangingRole;
            ServerHandlers.ReloadedConfigs += eventHandlers.OnReloadedConfigs;
            ServerHandlers.RestartingRound += eventHandlers.OnRestartingRound;
            ServerHandlers.WaitingForPlayers += eventHandlers.OnWaitingForPlayers;

            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            PlayerHandlers.ChangingRole -= eventHandlers.OnChangingRole;
            ServerHandlers.ReloadedConfigs -= eventHandlers.OnReloadedConfigs;
            ServerHandlers.RestartingRound -= eventHandlers.OnRestartingRound;
            ServerHandlers.WaitingForPlayers -= eventHandlers.OnWaitingForPlayers;
            eventHandlers = null;

            Instance = null;

            base.OnDisabled();
        }
    }
}