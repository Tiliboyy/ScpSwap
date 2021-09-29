// -----------------------------------------------------------------------
// <copyright file="CustomSwap.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Models
{
    using System;
    using Exiled.API.Features;

    public class CustomSwap
    {
        public CustomSwap(string name, Action<Player> spawnMethod, Func<Player, bool> verify)
        {
            Name = name;
            SpawnMethod = spawnMethod;
            Verify = verify;
        }

        public string Name { get; set; }

        public Action<Player> SpawnMethod { get; set; }

        public Func<Player, bool> Verify { get; set; }
    }
}