using Libs.Domain.Exceptions;

namespace Libs.Domain.Model;

public abstract class BaseUserEntity : BaseEntity
{
    protected BaseUserEntity(Guid userId)
    {
        BindUser(userId);
        Validate();  
    }
    
    public Guid UserId { get; private set; }

    private void BindUser(Guid id)
    {
        UserId = id;
    }

    private void Validate()
    {
        if (UserId.Equals(Guid.Empty))
            throw new UserNotBoundException();
    }
}