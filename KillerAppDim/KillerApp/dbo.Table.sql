CREATE TABLE [dbo].[Review]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Tip] NCHAR(100) NULL, 
    [Opmerking] NCHAR(100) NULL, 
    [Cijfer] VARCHAR(10) NOT NULL, 
    [Gebruiker_id] INT NOT NULL, 
    CONSTRAINT [FK_Review_Gebruiker] FOREIGN KEY ([Gebruiker_id]) REFERENCES [Gebruiker]([ID])
)
