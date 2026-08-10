using System;

namespace Nop.Plugin.Widgets.LimitedEdition.Models
{
    /// <summary>
    /// Dati necessari al partial _CountdownScript per inizializzare
    /// il countdown JS di un singolo timer sulla pagina.
    /// </summary>
    public class CountdownScriptModel
    {
        public string ElementId { get; set; }

        public DateTime EndDateUtc { get; set; }

        public string ExpiredText { get; set; }
    }
}
