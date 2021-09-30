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
    using Exiled.API.Features;
    using ScpSwap.Models;

    public static class ValidSwaps
    {
        private static readonly Dictionary<string, RoleType> DefaultSwaps = new Dictionary<string, RoleType>
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
        /// Attempts to get a <see cref="RoleType"/> from <see cref="DefaultSwaps"/> or from directly parsing a request.
        /// </summary>
        /// <param name="request">The query to get the <see cref="RoleType"/>.</param>
        /// <returns>The found <see cref="RoleType"/>.</returns>
        public static RoleType Get(string request)
        {
            if (!DefaultSwaps.TryGetValue(request, out var roleType))
                Enum.TryParse(request, true, out roleType);

            return roleType;
        }
    }
}