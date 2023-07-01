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

using Lily.Core.Commands;

namespace Lily.Commands;

/// <summary>
/// Represents a <see cref="Command"/> that prompts Lily to identify herself.
/// </summary>
internal sealed class IdentifyCommand
    : Command
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentifyCommand"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> to use for logging.</param>
    public IdentifyCommand(ILogger<IdentifyCommand> logger)
        : base(logger)
    { }

    /// <summary>
    /// Executes the <see cref="IdentifyCommand"/>.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [Command("identify", "Prompts Lily to identify herself.")]
    public async Task Identify()
    {
        Logger.LogInformation("Received invocation for the identify command.");
        string message = "👋 Hey there! I'm Lily, a simple but ambitious chatbot created by tacosontitan.";
        await ReplyAsync(message);
    }
}
