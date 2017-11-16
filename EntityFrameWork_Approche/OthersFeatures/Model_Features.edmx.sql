
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/20/2017 13:30:19
-- Generated from EDMX file: C:\Users\FHCOULIBALY.UNIVERS\documents\visual studio 2012\Projects\EntityFrameWork_Approche\OthersFeatures\Model_Features.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Features_EF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Departements'
CREATE TABLE [dbo].[Departements] (
    [DeptID] int IDENTITY(1,1) NOT NULL,
    [DeptName] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DeptID] in table 'Departements'
ALTER TABLE [dbo].[Departements]
ADD CONSTRAINT [PK_Departements]
    PRIMARY KEY CLUSTERED ([DeptID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------