/*
* Copyright 2023 tacosontitan and contributors
* 
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
*       http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace Lily.Core.Commands;

/// <summary>
/// Represents a command for execution by Lily.
/// </summary>
public abstract class Command
    : ModuleBase<SocketCommandContext>
{
    /// <summary>
    /// Creates a new <see cref="Command"/> instance.
    /// </summary>
    /// <param name="logger">The logger the command should use.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="logger"/> is <see langword="null"/>.</exception>
    public Command(ILogger logger)
    {
        if (logger is null)
            throw new ArgumentNullException(nameof(logger));

        Logger = logger;
    }

    /// <summary>
    /// Gets the <see cref="ILogger"/> utilized by this command instance.
    /// </summary>
    protected ILogger Logger { get; private set; }
}
