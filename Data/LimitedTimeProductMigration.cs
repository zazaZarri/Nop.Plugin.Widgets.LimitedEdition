using FluentMigrator;
using Nop.Data.Extensions;
using Nop.Data.Migrations;
using Nop.Plugin.Widgets.LimitedEdition.Domain;

namespace Nop.Plugin.Widgets.LimitedEdition.Data
{
    [NopMigration("2026-08-05 09:00:00", "Widgets.LimitedEdition base schema", MigrationProcessType.Installation)]
    public class LimitedTimeProductMigration : AutoReversingMigration
    {
        public override void Up()
        {
            if (!Schema.Table(nameof(LimitedTimeProduct)).Exists())
                Create.TableFor<LimitedTimeProduct>();

            if (!Schema.Table(nameof(CustomerTable)).Exists())
                Create.TableFor<CustomerTable>();

            if (!Schema.Table(nameof(SocialProofEvent)).Exists())
                Create.TableFor<SocialProofEvent>();
        }
    }

    /// <summary>
    /// Aggiunge colonne scarsità + social proof su install già esistenti.
    /// </summary>
    [NopMigration("2026-08-13 12:00:00", "Widgets.LimitedEdition scarcity columns v2", MigrationProcessType.Update)]
    public class LimitedTimeScarcityMigration : Migration
    {
        private const string Table = "LimitedTimeProduct";

        public override void Up()
        {
            if (!Schema.Table(Table).Exists())
            {
                Create.TableFor<LimitedTimeProduct>();
            }
            else
            {
                AddInt(Table, "InitialQuantity", 0);
                AddInt(Table, "RemainingQuantity", 0);
                AddInt(Table, "SoldCount", 0);
                AddBool(Table, "ShowRemainingStock", false);
                AddBool(Table, "ShowSoldCount", false);
                AddBool(Table, "ShowProgressBar", false);
                AddInt(Table, "ProgressBarMode", 0);
                AddDecimal(Table, "DiscountPercentage");
                AddBool(Table, "BlockPurchaseWhenExpired", true);
            }

            if (!Schema.Table(nameof(SocialProofEvent)).Exists())
                Create.TableFor<SocialProofEvent>();

            if (!Schema.Table(nameof(CustomerTable)).Exists())
                Create.TableFor<CustomerTable>();
        }

        public override void Down()
        {
        }

        private void AddInt(string table, string column, int defaultValue)
        {
            if (!Schema.Table(table).Column(column).Exists())
                Alter.Table(table).AddColumn(column).AsInt32().NotNullable().WithDefaultValue(defaultValue);
        }

        private void AddBool(string table, string column, bool defaultValue)
        {
            if (!Schema.Table(table).Column(column).Exists())
                Alter.Table(table).AddColumn(column).AsBoolean().NotNullable().WithDefaultValue(defaultValue);
        }

        private void AddDecimal(string table, string column)
        {
            if (!Schema.Table(table).Column(column).Exists())
                Alter.Table(table).AddColumn(column).AsDecimal(18, 4).NotNullable().WithDefaultValue(0);
        }
    }
}
