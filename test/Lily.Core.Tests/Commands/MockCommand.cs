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

namespace Lily.Core.Tests.Commands;

/// <summary>
/// Represents a mock <see cref="Command"/> for testing purposes.
/// </summary>
/// <typeparam name="TCategory">Specifies the category the mock command should use for its logger.</typeparam>
internal class MockCommand<TCategory>
    : Command
{
    /// <summary>
    /// Creates a new <see cref="MockCommand{TCategory}"/> instance.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger{TCategory}"/> to use for logging.</param>
    public MockCommand(ILogger<TCategory> logger)
        : base(logger)
    { }
}