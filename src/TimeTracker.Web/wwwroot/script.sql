IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    IF SCHEMA_ID(N'time-tracker') IS NULL EXEC(N'CREATE SCHEMA [time-tracker];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    CREATE TABLE [time-tracker].[ActivityLabels] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(450) NOT NULL,
        [ColorCode] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_ActivityLabels] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    CREATE TABLE [time-tracker].[Categories] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(450) NOT NULL,
        [Priority] nvarchar(max) NOT NULL,
        [ColorCode] nvarchar(max) NOT NULL,
        [IconUrl] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    CREATE UNIQUE INDEX [IX_ActivityLabels_Name] ON [time-tracker].[ActivityLabels] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    CREATE UNIQUE INDEX [IX_Categories_Name] ON [time-tracker].[Categories] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220425104224_InitialTimeTrackerModelsCreated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220425104224_InitialTimeTrackerModelsCreated', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426171611_AddedMandates')
BEGIN
    CREATE TABLE [time-tracker].[Mandates] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(450) NOT NULL,
        [ColorCode] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Mandates] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426171611_AddedMandates')
BEGIN
    CREATE UNIQUE INDEX [IX_Mandates_Name] ON [time-tracker].[Mandates] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426171611_AddedMandates')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220426171611_AddedMandates', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE TABLE [time-tracker].[Activities] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [DurationInSeconds] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [MandateId] int NOT NULL,
        [CategoryId] int NOT NULL,
        [CreatedTime] datetime2 NOT NULL,
        [CreatedBy] nvarchar(max) NOT NULL,
        [LastModifiedTime] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Activities_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [time-tracker].[Categories] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Activities_Mandates_MandateId] FOREIGN KEY ([MandateId]) REFERENCES [time-tracker].[Mandates] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE TABLE [time-tracker].[Tag] (
        [Id] int NOT NULL IDENTITY,
        [ActivityLabelId] int NOT NULL,
        [ActivityId] int NOT NULL,
        CONSTRAINT [PK_Tag] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Tag_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [time-tracker].[Activities] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tag_ActivityLabels_ActivityLabelId] FOREIGN KEY ([ActivityLabelId]) REFERENCES [time-tracker].[ActivityLabels] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE INDEX [IX_Activities_CategoryId] ON [time-tracker].[Activities] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE INDEX [IX_Activities_MandateId] ON [time-tracker].[Activities] ([MandateId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE INDEX [IX_Tag_ActivityId] ON [time-tracker].[Tag] ([ActivityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    CREATE INDEX [IX_Tag_ActivityLabelId] ON [time-tracker].[Tag] ([ActivityLabelId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220430040417_AddedActivityModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220430040417_AddedActivityModel', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    ALTER TABLE [time-tracker].[Tag] ADD [IndividualId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    ALTER TABLE [time-tracker].[Mandates] ADD [IndividualId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    ALTER TABLE [time-tracker].[Categories] ADD [IndividualId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    ALTER TABLE [time-tracker].[ActivityLabels] ADD [IndividualId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    ALTER TABLE [time-tracker].[Activities] ADD [IndividualId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    CREATE INDEX [IX_Tag_IndividualId] ON [time-tracker].[Tag] ([IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    CREATE INDEX [IX_Mandates_IndividualId] ON [time-tracker].[Mandates] ([IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    CREATE INDEX [IX_Categories_IndividualId] ON [time-tracker].[Categories] ([IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    CREATE INDEX [IX_ActivityLabels_IndividualId] ON [time-tracker].[ActivityLabels] ([IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    CREATE INDEX [IX_Activities_IndividualId] ON [time-tracker].[Activities] ([IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220505020812_AddedIndividualSpecifierToEntity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220505020812_AddedIndividualSpecifierToEntity', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    DROP INDEX [IX_Mandates_Name] ON [time-tracker].[Mandates];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    DROP INDEX [IX_Categories_Name] ON [time-tracker].[Categories];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    DROP INDEX [IX_ActivityLabels_Name] ON [time-tracker].[ActivityLabels];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    CREATE UNIQUE INDEX [IX_Mandates_Name_IndividualId] ON [time-tracker].[Mandates] ([Name], [IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    CREATE UNIQUE INDEX [IX_Categories_Name_IndividualId] ON [time-tracker].[Categories] ([Name], [IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    CREATE UNIQUE INDEX [IX_ActivityLabels_Name_IndividualId] ON [time-tracker].[ActivityLabels] ([Name], [IndividualId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220506142702_UpdatedUniqueConstraintsToIndividualSpecific', N'6.0.2');
END;
GO

COMMIT;
GO

