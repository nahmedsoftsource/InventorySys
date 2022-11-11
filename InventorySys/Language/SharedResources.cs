using Microsoft.Extensions.Localization;

namespace Language
{
    public class SharedResources : IResources
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SharedResources(IStringLocalizer<SharedResources> localizer)
        {
            this._localizer = localizer;
        }

        public string GetResource(string key)
        {
            return _localizer[key];
        }
    }
}
