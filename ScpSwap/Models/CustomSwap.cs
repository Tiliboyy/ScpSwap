// -----------------------------------------------------------------------
// <copyright file="CustomSwap.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Models
{
    using System;
    using System.Collections.Generic;
    using Exiled.API.Features;

    /// <summary>
    /// A container to aid in the swapping of custom classes.
    /// </summary>
    public class CustomSwap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomSwap"/> class.
        /// </summary>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="spawnMethod"><inheritdoc cref="SpawnMethod"/></param>
        /// <param name="verificationMethod"><inheritdoc cref="VerificationMethod"/></param>
        public CustomSwap(string name, Action<Player> spawnMethod, Func<Player, bool> verificationMethod)
        {
            Name = name;
            SpawnMethod = spawnMethod;
            VerificationMethod = verificationMethod;
        }

        /// <summary>
        /// Gets a <see cref="List{T}"/> of all created <see cref="CustomSwap"/> instances.
        /// </summary>
        public static HashSet<CustomSwap> Registered { get; } = new HashSet<CustomSwap>();

        /// <summary>
        /// Gets the identifying name of the swappable class.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the method to spawn the custom class.
        /// </summary>
        public Action<Player> SpawnMethod { get; }

        /// <summary>
        /// Gets the method to check if a player is the custom class.
        /// </summary>
        public Func<Player, bool> VerificationMethod { get; }

        /// <summary>
        /// Attempts to register a <see cref="CustomSwap"/>.
        /// </summary>
        /// <returns>A value indicating whether the <see cref="CustomSwap"/> registered successfully.</returns>
        public bool TryRegister()
        {
            foreach (CustomSwap swap in Registered)
            {
                if (string.Equals(Name, swap.Name, StringComparison.OrdinalIgnoreCase))
                {
                    Log.Warn($"Attempted to register a {nameof(CustomSwap)} with a duplicate name of {Name}.");
                    return false;
                }
            }

            Registered.Add(this);
            return true;
        }

        /// <summary>
        /// Attempts to unregister a <see cref="CustomSwap"/>.
        /// </summary>
        /// <returns>A value indicating whether the <see cref="CustomSwap"/> registered successfully.</returns>
        public bool TryUnregister()
        {
            if (Registered.Remove(this))
                return true;

            Log.Warn($"Attempted to remove an unregistered {nameof(CustomSwap)} with a name of {Name}.");
            return false;
        }
    }
}