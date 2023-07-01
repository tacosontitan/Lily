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

using Discord.WebSocket;
using Lily.Core.Commands;
using DiscordCommandService = Discord.Commands.CommandService;

namespace Lily.Services;

/// <summary>
/// Defines a <see cref="BackgroundService"/> for handling incoming commands.
/// </summary>
internal sealed class CommandService
    : BackgroundService
{
    private readonly ILogger _logger;
    private readonly DiscordSocketClient _client;
    private readonly DiscordCommandService _commandService;
    private readonly IServiceProvider _serviceProvider;

    public CommandService(
        DiscordSocketClient client,
        DiscordCommandService commandService,
        IServiceProvider serviceProvider,
        ILogger<CommandService> logger)
    {
        _client = client;
        _commandService = commandService;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("Starting the command service.");
        IEnumerable<Type> commandTypes = GetCommandTypes();
        if (commandTypes?.Any() != true)
        {
            _logger.LogError("Unable to identify commands.");
            return;
        }

        _logger.LogDebug("Registering types.");
        int commandCount = 0;
        foreach (Type commandType in commandTypes)
        {
            try
            {
                await _commandService.AddModuleAsync(commandType, _serviceProvider);
                commandCount++;
            } catch (Exception e)
            {
                _logger.LogError($"Unable to register command of type `{commandType.Name}`. {e.Message}");
            }

        }

        _logger.LogDebug($"Successfully registered `{commandCount}/{commandTypes.Count()}` commands.");
        _logger.LogInformation("Command service started successfully.");
    }

    private IEnumerable<Type> GetCommandTypes() =>
        from type in GetType().Assembly.GetTypes()
        where typeof(Command).IsAssignableFrom(type)
        select type;
}
