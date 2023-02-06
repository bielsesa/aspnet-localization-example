using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace LocalizationExampleMSDN
{
    public sealed class ParameterizedMessageService
    {
        private readonly IStringLocalizer _localizer;

        //The IStringLocalizerFactory isn't required. Instead, it is preferred for consuming services to require the IStringLocalizer<T>.
        public ParameterizedMessageService(IStringLocalizerFactory factory) =>
            _localizer = factory.Create(typeof(ParameterizedMessageService));

        [return: NotNullIfNotNull(nameof(_localizer))]
        public string? GetFormattedMessage(DateTime dateTime, double dinnerPrice)
        {
            LocalizedString localizedString = _localizer["DinnerPriceFormat", dateTime, dinnerPrice];
            return localizedString;
        }
    }
}
