IF EXISTS(select name from sysobjects where name = 'Questions') 
SELECT 'PRESENT'
ELSE
CREATE TABLE [dbo].[Questions] (
    [Id]              INT      IDENTITY(1,1)      NOT NULL,
    [ShortURL]        NVARCHAR (50)  NOT NULL,
    [QuestionText]    NVARCHAR (200) NOT NULL,
    [CreatedDateTime] SMALLDATETIME  DEFAULT (getdate()) NOT NULL,
    [Password]        NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);