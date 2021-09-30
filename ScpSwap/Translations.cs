// -----------------------------------------------------------------------
// <copyright file="Translations.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translations : ITranslation
    {
        /// <summary>
        /// Gets or sets a collection of custom names with their correlating <see cref="RoleType"/>.
        /// </summary>
        [Description("A collection of custom names with their correlating RoleType.")]
        public Dictionary<string, RoleType> DefaultSwaps { get; set; } = new Dictionary<string, RoleType>
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
    }
}