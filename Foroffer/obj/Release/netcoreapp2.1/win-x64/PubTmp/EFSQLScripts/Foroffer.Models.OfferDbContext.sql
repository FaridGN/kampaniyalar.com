IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Action] nvarchar(max) NULL,
        [Controller] nvarchar(max) NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Companies] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [VOEN] int NULL,
        [LogoPath] nvarchar(max) NULL,
        [Address] nvarchar(max) NOT NULL,
        [MoreLink] nvarchar(max) NULL,
        [Membership] nvarchar(max) NULL,
        CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Images] (
        [Id] int NOT NULL IDENTITY,
        [Path] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Subcategories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [CategoryId] int NOT NULL,
        [Action] nvarchar(max) NULL,
        [Controller] nvarchar(max) NULL,
        CONSTRAINT [PK_Subcategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Subcategories_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [CompanySubcategories] (
        [CompanyId] int NOT NULL,
        [SubcategoryId] int NOT NULL,
        CONSTRAINT [PK_CompanySubcategories] PRIMARY KEY ([CompanyId], [SubcategoryId]),
        CONSTRAINT [FK_CompanySubcategories_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CompanySubcategories_Subcategories_SubcategoryId] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [NestedCategories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [SubcategoryId] int NOT NULL,
        CONSTRAINT [PK_NestedCategories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NestedCategories_Subcategories_SubcategoryId] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(55) NOT NULL,
        [Description] nvarchar(145) NOT NULL,
        [Image] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ExpirationDate] datetime2 NOT NULL,
        [URL] nvarchar(max) NULL,
        [CompanyId] int NOT NULL,
        [SubcategoryId] int NOT NULL,
        [SpecDiscount] bit NOT NULL,
        [Gifted] bit NOT NULL,
        [NestedCategoryId] int NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Posts_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Posts_NestedCategories_NestedCategoryId] FOREIGN KEY ([NestedCategoryId]) REFERENCES [NestedCategories] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Posts_Subcategories_SubcategoryId] FOREIGN KEY ([SubcategoryId]) REFERENCES [Subcategories] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Detaileds] (
        [Id] int NOT NULL IDENTITY,
        [Outline] nvarchar(max) NULL,
        [CompanyInfo] nvarchar(max) NULL,
        [PostId] int NOT NULL,
        CONSTRAINT [PK_Detaileds] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Detaileds_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Features] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Parameter] nvarchar(max) NULL,
        [DetailedId] int NULL,
        [DetaliedId] int NOT NULL,
        CONSTRAINT [PK_Features] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Features_Detaileds_DetailedId] FOREIGN KEY ([DetailedId]) REFERENCES [Detaileds] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE TABLE [Pictures] (
        [Id] int NOT NULL IDENTITY,
        [PicPath] nvarchar(max) NULL,
        [DetailedId] int NOT NULL,
        CONSTRAINT [PK_Pictures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Pictures_Detaileds_DetailedId] FOREIGN KEY ([DetailedId]) REFERENCES [Detaileds] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_CompanySubcategories_SubcategoryId] ON [CompanySubcategories] ([SubcategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Detaileds_PostId] ON [Detaileds] ([PostId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Features_DetailedId] ON [Features] ([DetailedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_NestedCategories_SubcategoryId] ON [NestedCategories] ([SubcategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Pictures_DetailedId] ON [Pictures] ([DetailedId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Posts_CompanyId] ON [Posts] ([CompanyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Posts_NestedCategoryId] ON [Posts] ([NestedCategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Posts_SubcategoryId] ON [Posts] ([SubcategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    CREATE INDEX [IX_Subcategories_CategoryId] ON [Subcategories] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302163705_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190302163705_Initial', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190302165416_hashed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190302165416_hashed', N'2.1.4-rtm-31024');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20190313200417_correctdet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20190313200417_correctdet', N'2.1.4-rtm-31024');
END;

GO

