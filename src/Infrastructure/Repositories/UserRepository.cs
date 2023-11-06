using Application.Repositories;
using Dapper;
using Domain.Models.User;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository(INpgsqlConnectionFactory npgsqlConnectionFactory) : IUserRepository
{
    private const string TableName = "users";
    private const string FieldsForSelect = "id, first_name, second_name, birthdate, biography, city, password_hash";
    private const string FieldsForInsert = "id, first_name, second_name, birthdate, biography, city, password_hash";

    public async Task<User?> FindById(string id, CancellationToken cancellationToken)
    {
        const string sql =
            $"""
             select {FieldsForSelect} from {TableName}
             where id = @userId
             """;
        
        await using var connection = npgsqlConnectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        var args = new
        {
            userId = id
        };
        
        return await connection.QuerySingleOrDefaultAsync<User>(sql, args);
    }

    public async Task<string> Create(User user, CancellationToken cancellationToken)
    {
        const string sql =
            $"""
             insert into {TableName}({FieldsForInsert})
             values (@id, @first_name, @second_name, @birthdate, @biography, @city, @password_hash)
             """;
        
        await using var connection = npgsqlConnectionFactory.Create();
        await connection.OpenAsync(cancellationToken);

        var userId = UserId.New().Value;
        var args = new
        {
            id = userId,
            first_name = user.FirstName,
            second_name = user.SecondName,
            birthdate = user.Birthdate,
            biography = user.Biography,
            city = user.City,
            password_hash = user.PasswordHash
        };
        
        await connection.ExecuteAsync(sql, args);

        return userId;
    }
}