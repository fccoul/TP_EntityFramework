
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/26/2017 09:15:18
-- Generated from EDMX file: C:\Users\fhcoulibaly\Documents\Visual Studio 2013\Projects\EntityFrameWork_Approche\OthersFeatures\InHeritanceModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [InHeriTanceDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Teacher_inherits_Person]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Persons_Teacher] DROP CONSTRAINT [FK_Teacher_inherits_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Persons_Teacher]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons_Teacher];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FisrtName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Persons_Teacher'
CREATE TABLE [dbo].[Persons_Teacher] (
    [HireDate] datetime  NOT NULL,
    [ID] int  NOT NULL
);
GO

-- Creating table 'Persons_Eleve'
CREATE TABLE [dbo].[Persons_Eleve] (
    [EnrollmentDate] datetime  NOT NULL,
    [ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Persons_Teacher'
ALTER TABLE [dbo].[Persons_Teacher]
ADD CONSTRAINT [PK_Persons_Teacher]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Persons_Eleve'
ALTER TABLE [dbo].[Persons_Eleve]
ADD CONSTRAINT [PK_Persons_Eleve]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID] in table 'Persons_Teacher'
ALTER TABLE [dbo].[Persons_Teacher]
ADD CONSTRAINT [FK_Teacher_inherits_Person]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[Persons]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID] in table 'Persons_Eleve'
ALTER TABLE [dbo].[Persons_Eleve]
ADD CONSTRAINT [FK_Eleve_inherits_Person]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[Persons]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------