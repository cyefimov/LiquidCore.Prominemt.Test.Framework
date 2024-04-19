using LiquidCore.Prominemt.Test.Framework.Builders.Entities.User;

namespace LiquidCore.Prominemt.Test.Framework.Extensions;

/// <summary>
/// DB context extensions
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Database initialization for testing
    /// </summary>
    /// <param name="dbContext">DB context. Instance of <see cref="DbContext"/></param>
    public static void InitializeTestDatabase(this DbContext dbContext)
    {
        const int minUsers = 1;
        const int maxUsers = 1000;

        const string rootAdminDisplayName = "Root Admin";
        const string rootAdminEmail = "root.admin@prominemt.com";
        var rootAdminExternalId = Guid.Parse("B047A58C-3019-488E-AA9B-EE70B8257612");
        var rootAdminUserId = new Guid();

        const string demoUserDisplayName = "Demo User";
        const string demoUserEmail = "demo.user@prominemt.com";
        var demoUserExternalId = Guid.Parse("4365DAC3-3D21-474B-9F99-3D8A9B703140");

        var paramItems = new object[]
        {
            new SqlParameter("@RootAdminUserId", rootAdminUserId),
            new SqlParameter("@RootAdminExternalId", rootAdminExternalId),
            new SqlParameter("@RootAdminDisplayName", rootAdminDisplayName),
            new SqlParameter("@RootAdminEmail", rootAdminEmail),
            new SqlParameter("@DemoUserExternalId", demoUserExternalId),
            new SqlParameter("@DemoUserDisplayName", demoUserDisplayName),
            new SqlParameter("@DemoUserEmail", demoUserEmail)
        };

        var initSql = new StringBuilder();
        initSql.AppendLine(" ALTER TABLE [Security].[User] DROP CONSTRAINT [FK_Security_User_CreatedBy_Security_User]");
        initSql.AppendLine(" ALTER TABLE [Security].[User] DROP CONSTRAINT [FK_Security_User_UpdatedBy_Security_User]");
        
        initSql.AppendLine(" INSERT INTO [Security].[User] ([CreatedBy],[UpdatedBy],[ExternalId],[DisplayName],[Email])");
        initSql.AppendLine(" VALUES (@RootAdminUserId, @RootAdminUserId, @RootAdminExternalId, @RootAdminDisplayName, @RootAdminEmail)");
        
        initSql.AppendLine(" SELECT @RootAdminUserId = UserId FROM [Security].[User] WHERE [DisplayName] = @RootAdminDisplayName");
        initSql.AppendLine(" UPDATE [Security].[User] SET [CreatedBy] = @RootAdminUserId, [UpdatedBy] = @RootAdminUserId");
        
        initSql.AppendLine(" INSERT INTO [Security].[User] ([CreatedBy],[UpdatedBy],[ExternalId],[DisplayName],[Email])");
        initSql.AppendLine(" VALUES (@RootAdminUserId, @RootAdminUserId, @DemoUserExternalId, @DemoUserDisplayName, @DemoUserEmail)");
        
        initSql.AppendLine(" ALTER TABLE [Security].[User] WITH CHECK ADD CONSTRAINT [FK_Security_User_CreatedBy_Security_User] FOREIGN KEY([CreatedBy]) REFERENCES [Security].[User] ([UserId])");
        initSql.AppendLine(" ALTER TABLE [Security].[User] CHECK CONSTRAINT [FK_Security_User_CreatedBy_Security_User]");
        
        initSql.AppendLine(" ALTER TABLE [Security].[User] WITH CHECK ADD CONSTRAINT [FK_Security_User_UpdatedBy_Security_User] FOREIGN KEY([UpdatedBy]) REFERENCES [Security].[User] ([UserId])");
        initSql.AppendLine(" ALTER TABLE [Security].[User] CHECK CONSTRAINT [FK_Security_User_UpdatedBy_Security_User]");
                       
        var rand = new Random();
        var qty = rand.Next(minUsers, maxUsers);

        for (var i = 0; i < qty; i++)
        {
            var user = UserBuilder.Typical().Build();

            initSql.AppendLine(" INSERT INTO [Security].[User] ([CreatedBy],[UpdatedBy],[ExternalId],[DisplayName],[Email])");
            initSql.AppendLine($" VALUES (@RootAdminUserId, @RootAdminUserId,'{user.ExternalId}','{user.DisplayName}','{user.Email}')");
        }

        dbContext.Database.ExecuteSqlRaw(initSql.ToString(), paramItems);
    }
}