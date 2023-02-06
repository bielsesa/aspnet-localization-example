using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace Localization.Example
{
    public sealed class MessageService
    {
        private readonly IStringLocalizer<MessageService> _localizer;

        public MessageService(IStringLocalizer<MessageService> localizer) =>
            _localizer = localizer;

        [return: NotNullIfNotNull(nameof(_localizer))]
        public string? GetGreetingMessage()
        {
            LocalizedString localizedString = _localizer["GreetingMessage"];
            return localizedString;
        }
    }
}
