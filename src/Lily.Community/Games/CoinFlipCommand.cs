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

/// <summary>
/// Represents a command that allows a user to flip a coin.
/// </summary>
public sealed class CoinFlipCommand
    : Command
{
    private readonly Random _random;

    /// <summary>
    /// Creates a new <see cref="CoinFlipCommand"/> instance.
    /// </summary>
    /// <param name="random">The <see cref="Random"/> instance to use for generating random numbers.</param>
    /// <param name="logger">The <see cref="ILogger{TCategoryName}"/> instance to use for logging.</param>
    public CoinFlipCommand(Random random, ILogger<CoinFlipCommand> logger)
        : base(logger)
    {
        if (random is null)
            throw new ArgumentNullException(nameof(random));

        _random = random;
    }

    /// <summary>
    /// Flips a coin.
    /// </summary>
    [Command("coinflip", "Flip a coin.")]
    public async Task<string> Flip()
    {
        Logger.LogInformation("Received invocation for the `coinflip` command.");
        
        int flipNumber = _random.Next(0, 101);
        CoinFlipResult result = GetFlipResult(flipNumber);
        string response = result switch
        {
            CoinFlipResult.Heads => "Heads!",
            CoinFlipResult.Tails => "Tails!",
            CoinFlipResult.Side => "Sweet mother of monkey milk, the coin landed on its side!",
            _ => throw new InvalidOperationException($"The coin flip result `{result}` is not supported.")
        };
        
        await ReplyAsync(response);
        return response;
    }

    /// <summary>
    /// Gets the result of a coin flip.
    /// </summary>
    /// <param name="flipNumber">A random number between <c>0</c> and <c>100<c> for determining the flip result.</param>
    /// <returns>The result of the coin flip.</returns>
    public CoinFlipResult GetFlipResult(int flipNumber) => flipNumber switch
    {
        < 0 => throw new ArgumentOutOfRangeException(nameof(flipNumber), "The flip number must be non-negative."),
        > 100 => throw new ArgumentOutOfRangeException(nameof(flipNumber), "The flip number must be less than or equal to 100."),
        0 => CoinFlipResult.Side,
        >= 1 and <= 49 => CoinFlipResult.Heads,
        >= 50 and <= 100 => CoinFlipResult.Tails
    };
}
