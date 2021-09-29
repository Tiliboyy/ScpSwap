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
        private static readonly List<CustomSwap> CustomSwaps = new List<CustomSwap>();

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

        public static void Add(string name, Action<Player> spawnMethod, Func<Player, bool> verificationMethod)
        {
            CustomSwaps.Add(new CustomSwap(name, spawnMethod, verificationMethod));
        }

        public static CustomSwap GetCustom(string request)
        {
            foreach (CustomSwap customSwap in CustomSwaps)
            {
                if (string.Equals(request, customSwap.Name, StringComparison.OrdinalIgnoreCase))
                    return customSwap;
            }

            return null;
        }

        public static RoleType Get(string request)
        {
            if (!DefaultSwaps.TryGetValue(request, out var roleType))
                Enum.TryParse(request, true, out roleType);

            return roleType;
        }
    }
}