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
using Microsoft.Extensions.Logging;

namespace Lily.Community.Games;

public sealed class EightBallCommand
    : Command
{
    private readonly Random _random;

    public EightBallCommand(Random random, ILogger<EightBallCommand> logger)
        : base(logger)
    {
        if (random is null)
            throw new ArgumentNullException(nameof(random));

        _random = random;
    }

    [Command("8ball", "Ask the magic 8-ball a question.")]
    public async Task Identify()
    {
        Logger.LogInformation("Received invocation for the identify command.");
        
        string message = "TODO: Implement 8ball command.";

        await ReplyAsync(message);
    }
}
