namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            // Seed these two users based on the table data inserted previously.
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a5ad505b-16ac-4f7d-afd3-21b2cf3ac757', N'user@vidly.com', 0, N'AOnoc9y401DhOL2aTrAbbdEqg5qRLJiyDMRHA1aucxTC8igoCj/n0ZoBqdFQg2RY6Q==', N'c0409779-523a-4044-8409-3e22add34346', NULL, 0, 0, NULL, 1, 0, N'user@vidly.com')");
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'fa5af688-a113-44f3-b5e1-1dfddd1b1722', N'storemanager@vidly.com', 0, N'ADwUK8eWkR/mAOgq09C1fKsQRn4twP+c6W4vXdacZ8IV0iqMpmDfBrn590nWSgVTVg==', N'39cfd17c-0227-4728-961c-862bdebe3976', NULL, 0, 0, NULL, 1, 0, N'storemanager@vidly.com')");
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ff5ab8ee-5ab5-4c36-9f0b-c2a2154cd1ce', N'CanManageMovies')");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fa5af688-a113-44f3-b5e1-1dfddd1b1722', N'ff5ab8ee-5ab5-4c36-9f0b-c2a2154cd1ce')");
        }
        
        public override void Down()
        {
        }
    }
}
