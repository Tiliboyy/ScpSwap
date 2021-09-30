// -----------------------------------------------------------------------
// <copyright file="ValidSwaps.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using ScpSwap.Models;

    /// <summary>
    /// Handles queries that expose the names of swappable classes and config blacklists.
    /// </summary>
    public static class ValidSwaps
    {
        private static readonly List<string> NamesValue = new List<string>();
        private static readonly List<CustomSwap> CustomSwapsValue = new List<CustomSwap>();
        private static readonly List<RoleType> DefaultSwapsValue = new List<RoleType>();
        private static readonly Dictionary<string, RoleType> TranslatableSwapsValue = new Dictionary<string, RoleType>();

        /// <summary>
        /// Gets a collection of all available swap names.
        /// </summary>
        public static ReadOnlyCollection<string> Names => NamesValue.AsReadOnly();

        /// <summary>
        /// Gets a collection of all available custom swaps.
        /// </summary>
        public static ReadOnlyCollection<CustomSwap> CustomSwaps => CustomSwapsValue.AsReadOnly();

        /// <summary>
        /// Gets a collection of all default swaps.
        /// </summary>
        public static ReadOnlyCollection<RoleType> DefaultSwaps => DefaultSwapsValue.AsReadOnly();

        /// <summary>
        /// Gets a collection of all translatable swaps.
        /// </summary>
        public static ReadOnlyDictionary<string, RoleType> TranslatableSwaps => new ReadOnlyDictionary<string, RoleType>(TranslatableSwapsValue);

        /// <summary>
        /// Attempts to get a <see cref="RoleType"/> from <see cref="Translation.TranslatableSwaps"/> or from directly parsing a request.
        /// </summary>
        /// <param name="request">The query to get the <see cref="RoleType"/>.</param>
        /// <returns>The found <see cref="RoleType"/>.</returns>
        public static RoleType Get(string request)
        {
            if (TranslatableSwaps.TryGetValue(request, out RoleType roleType))
                return roleType;

            if (Enum.TryParse(request, true, out RoleType parsedRole) && DefaultSwaps.Contains(parsedRole))
                return parsedRole;

            return RoleType.None;
        }

        /// <summary>
        /// Returns a <see cref="CustomSwap"/> with a matching name to the provided one.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>The found <see cref="CustomSwap"/> or null if one is not found.</returns>
        public static CustomSwap GetCustom(string name)
        {
            foreach (CustomSwap customSwap in CustomSwaps)
            {
                if (string.Equals(name, customSwap.Name, StringComparison.OrdinalIgnoreCase))
                    return customSwap;
            }

            return null;
        }

        /// <summary>
        /// Checks a <see cref="Player"/> to see if they have a custom class.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> to check.</param>
        /// <returns>The found <see cref="CustomSwap"/> or null if one is not found.</returns>
        public static CustomSwap GetCustom(Player player)
        {
            foreach (CustomSwap customSwap in CustomSwaps)
            {
                if (customSwap.VerificationMethod(player))
                    return customSwap;
            }

            return null;
        }

        /// <summary>
        /// Clears and adds all available names of roles to swap to the cache.
        /// </summary>
        public static void Refresh()
        {
            NamesValue.Clear();

            RefreshCustomSwaps();
            RefreshTranslatableSwaps();
            RefreshDefaultSwaps();
        }

        private static void RefreshCustomSwaps()
        {
            CustomSwapsValue.Clear();
            foreach (CustomSwap customSwap in CustomSwap.Registered)
            {
                if (Plugin.Instance.Config.BlacklistedNames.Contains(customSwap.Name, StringComparison.OrdinalIgnoreCase))
                    continue;

                CustomSwapsValue.Add(customSwap);
                NamesValue.Add(customSwap.Name);
            }
        }

        private static void RefreshTranslatableSwaps()
        {
            TranslatableSwapsValue.Clear();
            if (Plugin.Instance.Translation.TranslatableSwaps == null)
                return;

            foreach (KeyValuePair<string, RoleType> kvp in Plugin.Instance.Translation.TranslatableSwaps)
            {
                if (Plugin.Instance.Config.BlacklistedScps.Contains(kvp.Value) || kvp.Value.GetTeam() != Team.SCP)
                    continue;

                TranslatableSwapsValue.Add(kvp.Key, kvp.Value);
                NamesValue.Add(kvp.Key);
            }
        }

        private static void RefreshDefaultSwaps()
        {
            DefaultSwapsValue.Clear();
            foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
            {
                if (Plugin.Instance.Config.BlacklistedScps.Contains(role) || role.GetTeam() != Team.SCP)
                    continue;

                DefaultSwapsValue.Add(role);
                NamesValue.Add(role.ToString());
            }
        }
    }
}