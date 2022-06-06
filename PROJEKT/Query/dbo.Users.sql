CREATE TABLE [dbo].[Users] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [userName] NCHAR (64) NOT NULL,
    [password] NCHAR (64) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

