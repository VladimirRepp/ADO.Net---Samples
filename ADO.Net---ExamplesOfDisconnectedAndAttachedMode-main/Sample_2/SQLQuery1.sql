Go
USe ItTop

Go
Create Table Pictures(
	Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	BookId INT NOT NULL,
	FOREIGN KEY (BookId) REFERENCES Books (Id),
	Name VARCHAR(100) NOT NULL,
	Picture VARBINARY (MAX)
)