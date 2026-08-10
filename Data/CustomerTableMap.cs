using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;
using Nop.Plugin.Misc.Notes.Domain;

namespace Nop.Plugin.Misc.Notes.Data;

public class NoteBuilder : NopEntityBuilder<Note>
{
    public override void MapEntity(CreateTableExpressionBuilder table)
    {
        table
            .WithColumn(nameof(Note.Id)).AsInt32().PrimaryKey().Identity()
            .WithColumn(nameof(Note.Title)).AsString(200).NotNullable()
            .WithColumn(nameof(Note.Text)).AsString(int.MaxValue).NotNullable();
    }
}
