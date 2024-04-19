namespace LiquidCore.Prominemt.Test.Framework.Builders.Entities.User;

public sealed class UserBuilder
{
    private Lazy<Guid> _id = new(default(Guid));
    private Lazy<bool> _isActive = new(default(bool));
    private Lazy<DateTime> _createdOn = new(default(DateTime));
    private Lazy<Guid> _createdBy = new(default(Guid));
    private Lazy<UserEntity> _createdByUser = new(default(UserEntity)!);
    private Lazy<DateTime> _updatedOn = new(default(DateTime));
    private Lazy<Guid> _updatedBy = new(default(Guid));
    private Lazy<UserEntity> _updatedByUser = new(default(UserEntity)!);
    private Lazy<Guid> _externalId = new(default(Guid));
    private Lazy<string> _displayName = new(default(string)!);
    private Lazy<string> _email = new(default(string)!);

    public UserEntity Build() =>
        new Lazy<UserEntity>(
            new UserEntity
            {
                Id = _id.Value,
                IsActive = _isActive.Value,
                CreatedOn = _createdOn.Value,
                CreatedBy = _createdBy.Value,
                UpdatedOn = _updatedOn.Value,
                UpdatedBy = _updatedBy.Value,
                CreatedByUser = _createdByUser.Value,
                UpdatedByUser = _updatedByUser.Value,
                ExternalId = _externalId.Value,
                DisplayName = _displayName.Value,
                Email = _email.Value
            }).Value;

    public static UserBuilder Default() => new();

    public static UserBuilder Simple() =>
        Default()
            .WithExternalId(GetRandom.ExternalId())
            .WithDisplayName(GetRandom.Name())
            .WithEmail(GetRandom.Email(5, 20).ToLower());

    public static UserBuilder Typical() =>
        Simple()
            .WithId(GetRandom.Id())
            .WithIsActive(GetRandom.Bool())
            .WithCreatedOn(GetRandom.DateTime())
            .WithCreatedBy(GetRandom.Id())
            .WithUpdatedOn(GetRandom.DateTime())
            .WithUpdatedBy(GetRandom.Id());

    public UserBuilder Relational() =>
        Typical()
            .WithCreatedByUser(Simple().WithId(_createdBy.Value).Build())
            .WithUpdatedByUser(Simple().WithId(_updatedBy.Value).Build());

    public UserBuilder WithId(Func<Guid> func)
    {
        _id = new Lazy<Guid>(func);
        return this;
    }

    public UserBuilder WithId(Guid value) => WithId(() => value);

    public UserBuilder WithExternalId(Func<Guid> func)
    {
        _externalId = new Lazy<Guid>(func);
        return this;
    }

    public UserBuilder WithExternalId(Guid value) => WithExternalId(() => value);

    public UserBuilder WithDisplayName(Func<string> func)
    {
        _displayName = new Lazy<string>(func);
        return this;
    }

    public UserBuilder WithDisplayName(string value) => WithDisplayName(() => value);

    public UserBuilder WithEmail(Func<string> func)
    {
        _email = new Lazy<string>(func);
        return this;
    }

    public UserBuilder WithEmail(string value) => WithEmail(() => value);

    public UserBuilder WithIsActive(Func<bool> func)
    {
        _isActive = new Lazy<bool>(func);
        return this;
    }

    public UserBuilder WithIsActive(bool value) => WithIsActive(() => value);

    public UserBuilder WithCreatedOn(Func<DateTime> func)
    {
        _createdOn = new Lazy<DateTime>(func);
        return this;
    }

    public UserBuilder WithCreatedOn(DateTime value) => WithCreatedOn(() => value);

    public UserBuilder WithCreatedBy(Func<Guid> func)
    {
        _createdBy = new Lazy<Guid>(func);
        return this;
    }

    public UserBuilder WithCreatedBy(Guid value) => WithCreatedBy(() => value);

    public UserBuilder WithUpdatedOn(Func<DateTime> func)
    {
        _updatedOn = new Lazy<DateTime>(func);
        return this;
    }

    public UserBuilder WithUpdatedOn(DateTime value) => WithUpdatedOn(() => value);

    public UserBuilder WithUpdatedBy(Func<Guid> func)
    {
        _updatedBy = new Lazy<Guid>(func);
        return this;
    }

    public UserBuilder WithUpdatedBy(Guid value) => WithUpdatedBy(() => value);

    public UserBuilder WithCreatedByUser(Func<UserEntity> func)
    {
        _createdByUser = new Lazy<UserEntity>(func);
        return this;
    }

    public UserBuilder WithCreatedByUser(UserEntity value) => WithCreatedByUser(() => value);

    public UserBuilder WithUpdatedByUser(Func<UserEntity> func)
    {
        _updatedByUser = new Lazy<UserEntity>(func);
        return this;
    }

    public UserBuilder WithUpdatedByUser(UserEntity value) => WithUpdatedByUser(() => value);
}