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
using Discord.WebSocket;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Defines extension methods for easing the maintenance of dependency injection.
/// </summary>
internal static class DependencyInjectionExtensions
{
    /// <summary>
    /// Adds general dependencies to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the dependencies to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the dependencies added.</returns>
    internal static IServiceCollection AddGeneralDependencies(this IServiceCollection services) =>
        services.AddSingleton<Random>();
    
    /// <summary>
    /// Adds Discord dependencies to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the dependencies to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the dependencies added.</returns>
    internal static IServiceCollection AddDiscord(this IServiceCollection services)
    {
        var commandServiceConfiguration = new CommandServiceConfig {
            CaseSensitiveCommands = false,
            ThrowOnError = true,
            IgnoreExtraArgs = true
        };

        var commandService = new CommandService(commandServiceConfiguration);
        return services.AddSingleton(commandService)
            .AddSingleton<DiscordSocketClient>();
    }
}
