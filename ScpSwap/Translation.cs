// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets a collection of custom names with their correlating <see cref="RoleType"/>.
        /// </summary>
        [Description("A collection of custom names with their correlating RoleType.")]
        public Dictionary<string, RoleType> TranslatableSwaps { get; set; } = new Dictionary<string, RoleType>
        {
            ["173"] = RoleType.Scp173,
            ["peanut"] = RoleType.Scp173,
            ["939"] = RoleType.Scp93953,
            ["dog"] = RoleType.Scp93953,
            ["079"] = RoleType.Scp079,
            ["79"] = RoleType.Scp079,
            ["computer"] = RoleType.Scp079,
            ["106"] = RoleType.Scp106,
            ["larry"] = RoleType.Scp106,
            ["096"] = RoleType.Scp096,
            ["96"] = RoleType.Scp096,
            ["shyguy"] = RoleType.Scp096,
            ["049"] = RoleType.Scp049,
            ["49"] = RoleType.Scp049,
            ["doctor"] = RoleType.Scp049,
            ["0492"] = RoleType.Scp0492,
            ["492"] = RoleType.Scp0492,
            ["zombie"] = RoleType.Scp0492,
        };

        /// <summary>
        /// Gets or sets the message to be displayed to all Scp subjects at the start of the round.
        /// </summary>
        [Description("The message to be displayed to all Scp subjects at the start of the round.")]
        public Broadcast StartMessage { get; set; } = new Broadcast("<color=yellow><b>Did you know you can swap classes with other SCP's?</b></color> Simply type <color=orange>.scpswap (role number)</color> in your in-game console (not RA) to swap!", 15);

        /// <summary>
        /// Gets or sets the broadcast to display to the receiver of a swap request.
        /// </summary>
        public Broadcast RequestBroadcast { get; set; } = new Broadcast("<i>You have an SCP Swap request!\nCheck your console by pressing [`] or [~]</i>", 5);

        /// <summary>
        /// Gets or sets the console message to send to the receiver of a swap request.
        /// </summary>
        public string RequestConsoleMessage { get; set; } = $"You have received a swap request from $SenderName who is SCP-$RoleName. Would you like to swap with them? Type \".scpswap accept\" to accept or \".scpswap decline\" to decline.";
    }
}