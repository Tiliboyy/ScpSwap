// -----------------------------------------------------------------------
// <copyright file="CommandTranslations.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Configs
{
    using ScpSwap.Commands;

    /// <summary>
    /// Contains configs for the various command instances to be translated.
    /// </summary>
    public class CommandTranslations
    {
        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Commands.Accept"/> command.
        /// </summary>
        public Accept Accept { get; set; } = new Accept();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Commands.Cancel"/> command.
        /// </summary>
        public Cancel Cancel { get; set; } = new Cancel();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Commands.Decline"/> command.
        /// </summary>
        public Decline Decline { get; set; } = new Decline();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="Commands.List"/> command.
        /// </summary>
        public List List { get; set; } = new List();
    }
}