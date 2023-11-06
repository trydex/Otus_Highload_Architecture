using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(1, "Initial migration")]
public class Initial : BaseMigration
{
    protected override string UpSql =>
        """
           create table users (
            id text,
            first_name text,
            second_name text,
            birthdate date,
            biography text,
            city text,
            password_hash text
           );
        """;

    protected override string DownSql =>
        "drop table users;";
}