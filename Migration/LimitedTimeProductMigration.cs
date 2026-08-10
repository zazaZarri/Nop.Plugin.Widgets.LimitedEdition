using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Migration
{
    [NopMigration("2026-08-05 09:00:00", "Widgets.LimitedEdition base schema", MigrationProcessType.Installation)]
    public class LimitedTimeProductMigration : AutoReversingMigration
    {
        public override void Up()
        {
            Create.TableFor<LimitedTimeProduct>();

        }
    }
}

