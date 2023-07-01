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

using Discord;
using Discord.WebSocket;

namespace Lily.Services;

internal sealed class AnimationService
    : BackgroundService
{
    private readonly int _totalNicknames;
    private readonly ILogger _logger;
    private readonly DiscordSocketClient _client;
    private readonly string[] _nicknames;
    private CancellationToken _cancellationToken;

    public AnimationService(
        DiscordSocketClient client,
        ILogger<AnimationService> logger)
    {
        _client = client;
        _logger = logger;
        _nicknames = new string[] {
            "    HI    ",
            "    HI    ",
            "    HI    ",
            "   H  I   ",
            "  H    I  ",
            " H      I ",
            "H        I",
            "          ",
            "    👋    ",
            "          ",
            "    👋    ",
            "          ",
            "    IM    ",
            "    IM    ",
            "    IM    ",
            "   I  M   ",
            "  I    M  ",
            " I      M ",
            "I        M",
            "          ",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "🔥Hazel💧",
            "💧Hazel🔥",
            "          ",
            "🤜        ",
            " 🤜       ",
            "  🤜      ",
            "   🤜     ",
            "    🤜    ",
            "     🤜   ",
            "      🤜  ",
            "       🤜 ",
            "        🤜",
            "        💥",
            "        👍",
            "        👍",
            "        👍",
            "           ",
            "           ",
            "           ",
            "           ",
        };

        _totalNicknames = _nicknames.Count();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Starting the animation service.");
        _client.Connected += StartAnimation;
        _cancellationToken = stoppingToken;

        _logger.LogInformation("Command service started successfully.");
        return Task.CompletedTask;
    }

    private async Task StartAnimation()
    {
        IEnumerable<IGuildUser> users = GetUsers();
        if (users?.Any() != true)
        {
            _logger.LogError("Unable to load user profiles.");
            return;
        }

        byte currentIndex = 0;
        do
        {
            string currentNickname = _nicknames[currentIndex++];
            foreach (IGuildUser user in users)
                await user.ModifyAsync(x => x.Nickname = currentNickname).ConfigureAwait(continueOnCapturedContext: false);

            if (currentIndex >= _totalNicknames)
                currentIndex = 0;

            await Task.Delay(TimeSpan.FromMilliseconds(250)).ConfigureAwait(continueOnCapturedContext: false);
        } while (!_cancellationToken.IsCancellationRequested);
    }

    private IEnumerable<IGuildUser> GetUsers()
    {
        List<IGuildUser> users = new();
        ulong currentUserId = _client.CurrentUser.Id;
        foreach (var guild in _client.Guilds)
        {
            var user = guild.GetUser(currentUserId);
            if (user is not null)
                users.Add(user);
        }

        return users;
    }
}