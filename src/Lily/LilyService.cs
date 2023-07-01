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
using Discord.Commands;
using Discord.WebSocket;

namespace Lily;

// Disabled because this class is injected into the DI container.
#pragma warning disable CA1812

internal sealed class LilyService
    : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commandService;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public LilyService(
        IConfiguration configuration,
        DiscordSocketClient client,
        CommandService commandService,
        ILogger<LilyService> logger)
    {
        _client = client;
        _commandService = commandService;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Information, $"Starting Lily.");
        _client.Connected += () =>
        {
            _logger.Log(LogLevel.Information, "Connected to Discord.");
            return Task.CompletedTask;
        };

        _client.Disconnected += (exception) =>
        {
            _logger.Log(LogLevel.Information, "Disconnected from Discord.");
            _logger.Log(LogLevel.Information, exception.Message);
            return Task.CompletedTask;
        };

        _logger.Log(LogLevel.Information, "Attempting to load authentication information.");
        string token = _configuration["discordToken"] ?? throw new InvalidOperationException("Discord token is null or empty.");

        _logger.Log(LogLevel.Information, "Attempting to login to Discord.");
        await _client.LoginAsync(TokenType.Bot, token).ConfigureAwait(continueOnCapturedContext: false);

        _logger.Log(LogLevel.Information, "Attempting to start Discord client.");
        await _client.StartAsync().ConfigureAwait(continueOnCapturedContext: false);
    }
}
