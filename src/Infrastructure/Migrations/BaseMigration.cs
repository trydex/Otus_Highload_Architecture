using FluentMigrator;

namespace Infrastructure.Migrations;

public abstract class BaseMigration : Migration
{
    public override void Up() => Execute.Sql(UpSql);

    public override void Down() => Execute.Sql(DownSql);

    protected abstract string UpSql { get; }
    protected abstract string DownSql{ get; }
}