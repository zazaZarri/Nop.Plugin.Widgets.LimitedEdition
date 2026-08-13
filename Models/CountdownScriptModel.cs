using System;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    public class CountdownScriptModel
    {
        public string ElementId { get; set; }
        public DateTime EndDateUtc { get; set; }
        public string ExpiredText { get; set; }
    }
}
