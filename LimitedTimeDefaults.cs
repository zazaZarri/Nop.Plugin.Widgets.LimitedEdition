using Nop.Core.Caching;

namespace Nop.Plugin.Widgets.LimitedEdition
{
    public static class LimitedTimeDefaults
    {
        public const string SystemName = "Widgets.LimitedEdition";

        public const string WIDGET_VIEW_COMPONENT_NAME = "WidgetsLimitedEdition";

        public const string ConfigurationRouteName = "Plugin.Widgets.LimitedEdition.Configure";

        public const string ProductListRouteName = "Plugin.Widgets.LimitedEdition.ProductList";

        public const string ProductUpdateRouteName = "Plugin.Widgets.LimitedEdition.ProductUpdate";

        public const string ProductDeleteRouteName = "Plugin.Widgets.LimitedEdition.ProductDelete";

        public static CacheKey ProductByProductIdCacheKey => new("Nop.plugins.widgets.limitededition.productid.{0}");
    }
}
