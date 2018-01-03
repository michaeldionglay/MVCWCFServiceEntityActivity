
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/29/2017 16:13:01
-- Generated from EDMX file: C:\Users\michael.dionglay\Documents\Visual Studio 2015\Projects\PhysicianDirectoryCheckpoint\PhysicianDirectoryService\PhysicianEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PhysicianDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_PhysicianContactInformation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactInformations] DROP CONSTRAINT [FK_PhysicianContactInformation];
GO
IF OBJECT_ID(N'[dbo].[FK_PhysicianSpecialization]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Specializations] DROP CONSTRAINT [FK_PhysicianSpecialization];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ContactInformations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactInformations];
GO
IF OBJECT_ID(N'[dbo].[Physicians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Physicians];
GO
IF OBJECT_ID(N'[dbo].[Specializations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specializations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ContactInformations'
CREATE TABLE [dbo].[ContactInformations] (
    [Id] int  NOT NULL,
    [HomePhone] nvarchar(11)  NOT NULL,
    [HomeAddress] nvarchar(max)  NOT NULL,
    [OfficeAddress] nvarchar(max)  NOT NULL,
    [OfficePhone] nvarchar(11)  NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [CellphoneNumber] nvarchar(11)  NOT NULL
);
GO

-- Creating table 'Physicians'
CREATE TABLE [dbo].[Physicians] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL,
    [Height] float  NOT NULL,
    [Weight] float  NOT NULL
);
GO

-- Creating table 'Specializations'
CREATE TABLE [dbo].[Specializations] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ContactInformations'
ALTER TABLE [dbo].[ContactInformations]
ADD CONSTRAINT [PK_ContactInformations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Physicians'
ALTER TABLE [dbo].[Physicians]
ADD CONSTRAINT [PK_Physicians]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specializations'
ALTER TABLE [dbo].[Specializations]
ADD CONSTRAINT [PK_Specializations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'ContactInformations'
ALTER TABLE [dbo].[ContactInformations]
ADD CONSTRAINT [FK_PhysicianContactInformation]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Physicians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Specializations'
ALTER TABLE [dbo].[Specializations]
ADD CONSTRAINT [FK_PhysicianSpecialization]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Physicians]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------