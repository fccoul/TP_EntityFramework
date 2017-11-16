
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/16/2017 15:04:19
-- Generated from EDMX file: c:\users\fhcoulibaly.univers\documents\visual studio 2012\Projects\EntityFrameWork_Approche\ModelFirst\ModelFirt_DB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModelFirst_DB];
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

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [StudentID] int IDENTITY(1,1) NOT NULL,
    [FisrtName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [EnrollmentDate] datetime  NOT NULL
);
GO

-- Creating table 'Enrollements'
CREATE TABLE [dbo].[Enrollements] (
    [EnrollmentID] int IDENTITY(1,1) NOT NULL,
    [CourseID] int  NOT NULL,
    [StudentID] int  NOT NULL,
    [Grade] nvarchar(max)  NOT NULL,
    [StudentStudentID] int  NOT NULL,
    [CourseCourseID] int  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Credit] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [StudentID] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([StudentID] ASC);
GO

-- Creating primary key on [EnrollmentID] in table 'Enrollements'
ALTER TABLE [dbo].[Enrollements]
ADD CONSTRAINT [PK_Enrollements]
    PRIMARY KEY CLUSTERED ([EnrollmentID] ASC);
GO

-- Creating primary key on [CourseID] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [StudentStudentID] in table 'Enrollements'
ALTER TABLE [dbo].[Enrollements]
ADD CONSTRAINT [FK_StudentEnrollement]
    FOREIGN KEY ([StudentStudentID])
    REFERENCES [dbo].[Students]
        ([StudentID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentEnrollement'
CREATE INDEX [IX_FK_StudentEnrollement]
ON [dbo].[Enrollements]
    ([StudentStudentID]);
GO

-- Creating foreign key on [CourseCourseID] in table 'Enrollements'
ALTER TABLE [dbo].[Enrollements]
ADD CONSTRAINT [FK_CourseEnrollement]
    FOREIGN KEY ([CourseCourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseEnrollement'
CREATE INDEX [IX_FK_CourseEnrollement]
ON [dbo].[Enrollements]
    ([CourseCourseID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------