using Domain.Models.User;

namespace Application.Repositories;

public interface IUserRepository
{
    public Task<User?> FindById(string id, CancellationToken cancellationToken);
    public Task<string> Create(User user, CancellationToken cancellationToken);
}