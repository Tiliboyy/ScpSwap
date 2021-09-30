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
    using ScpSwap.Models;

    /// <summary>
    /// Handles queries that expose the names of swappable classes.
    /// </summary>
    public class ValidSwaps
    {
        private readonly Plugin plugin;
        private readonly List<string> names = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidSwaps"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public ValidSwaps(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Gets a collection of all available swap names.
        /// </summary>
        public ReadOnlyCollection<string> Names => names.AsReadOnly();

        /// <summary>
        /// Attempts to get a <see cref="RoleType"/> from <see cref="Translations.DefaultSwaps"/> or from directly parsing a request.
        /// </summary>
        /// <param name="request">The query to get the <see cref="RoleType"/>.</param>
        /// <returns>The found <see cref="RoleType"/>.</returns>
        public RoleType Get(string request)
        {
            if (plugin.Translation.DefaultSwaps == null || !plugin.Translation.DefaultSwaps.TryGetValue(request, out var roleType))
                Enum.TryParse(request, true, out roleType);

            return roleType;
        }

        /// <summary>
        /// Clears and adds all available names of roles to swap to the cache.
        /// </summary>
        public void Refresh()
        {
            names.Clear();
            if (plugin.Translation.DefaultSwaps != null)
            {
                names.AddRange(plugin.Translation.DefaultSwaps.Keys);
            }

            foreach (CustomSwap customSwap in CustomSwap.Registered)
            {
                names.Add(customSwap.Name);
            }

            foreach (string role in Enum.GetNames(typeof(RoleType)))
            {
                if (role.Contains("Scp") && !names.Contains(role))
                    names.Add(role);
            }
        }
    }
}