using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.LimitedEdition.Domain
{
    public class LimitedTimeSettings : ISettings
    {
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string CustomMessage { get; set; }
        public TimerLayoutType TimerLayout { get; set; }
        public bool HideProductWhenExpired { get; set; }
    }
}