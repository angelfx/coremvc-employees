IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Units] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Login] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [ShortTitle] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    [UnitId] int NULL,
    [Count] int NOT NULL,
    [Image] varbinary(max) NULL,
    [ImageExtension] nvarchar(max) NULL,
    [ImageName] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Products_UnitId] ON [Products] ([UnitId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190705002704_IntialCreate', N'2.2.4-servicing-10062');

GO

