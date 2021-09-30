// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Config : IConfig
    {
        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug messages should be shown.
        /// </summary>
        [Description("Indicates whether debug messages should be shown.")]
        public bool ShowDebug { get; set; } = false;

        /// <summary>
        /// Gets or sets the duration, in seconds, before a swap request gets automatically deleted.
        /// </summary>
        [Description("The duration, in seconds, before a swap request gets automatically deleted.")]
        public float RequestTimeout { get; set; } = 20f;

        /// <summary>
        /// Gets or sets the duration, in seconds, after the round starts that swap requests can be sent.
        /// </summary>
        [Description("The duration, in seconds, after the round starts that swap requests can be sent.")]
        public float SwapTimeout { get; set; } = 60f;

        /// <summary>
        /// Gets or sets a value indicating whether a player can switch to a class if there is nobody playing as it.
        /// </summary>
        [Description("Indicates whether a player can switch to a class if there is nobody playing as it.")]
        public bool AllowNewScps { get; set; } = true;

        /// <summary>
        /// Gets or sets a collection of roles blacklisted from being swapped to.
        /// </summary>
        [Description("A collection of roles blacklisted from being swapped to.")]
        public RoleType[] BlacklistedScps { get; set; } =
        {
            RoleType.Scp0492,
        };

        /// <summary>
        /// Gets or sets a collection of the names of custom scps blacklisted from being swapped to. This must match the name the developer integrated the SCP into this plugin's API with.
        /// </summary>
        [Description("A collection of the names of custom scps blacklisted from being swapped to. This must match the name the developer integrated the SCP into this plugin's API with.")]
        public string[] BlacklistedNames { get; set; } = { };

        /// <summary>
        /// Gets or sets the message to be displayed to all Scp subjects at the start of the round.
        /// </summary>
        [Description("The message to be displayed to all Scp subjects at the start of the round.")]
        public Broadcast StartMessage { get; set; } = new Broadcast("<color=yellow><b>Did you know you can swap classes with other SCP's?</b></color> Simply type <color=orange>.scpswap (role number)</color> in your in-game console (not RA) to swap!", 15);
    }
}