// -----------------------------------------------------------------------
// <copyright file="SwapData.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Enums;
using PlayerRoles;

namespace ScpSwap.Models
{
    using Exiled.API.Features;
    using UnityEngine;

    /// <summary>
    /// A container to swap data between players.
    /// </summary>
    public class SwapData
    {
        private readonly CustomSwap customSwap;
        private readonly RoleTypeId role;
        private readonly Vector3 position;
        private readonly float health;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwapData"/> class.
        /// </summary>
        /// <param name="player">The player to generate the data from.</param>
        public SwapData(Player player)
        {
            role = player.Role;
            position = player.Position;
            health = player.Health;
            customSwap = ValidSwaps.GetCustom(player);
        }

        /// <summary>
        /// Spawns a player with the contained swap data.
        /// </summary>
        /// <param name="player">The player to swap.</param>
        public void Swap(Player player)
        {
            if (customSwap == null)
                player.Role.Set(role, SpawnReason.None);
            else
                customSwap.SpawnMethod(player);

            player.Position = position;
            player.Health = health;
        }
    }
}