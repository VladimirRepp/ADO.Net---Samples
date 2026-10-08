Go 
Create Database ItTop

Go
USe ItTop

Go 
Create Table StudentAssessment
(
    Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
    Fullname NVARCHAR(100),
    GroupName NVARCHAR(50),
    AverageRating FLOAT,
	NameObjectWithMinAverageRating NVARCHAR(50),
	NameObjectWithMaxAverageRating NVARCHAR(50)
)

Go
Create Table Books(
	Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Title NVARCHAR(100),
)

Go
Create Table Authors(
	Id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
	Fullname NVARCHAR(100),
)

Go
Insert Into Authors	(Fullname) Values ('‘»Œ 1')
Insert Into Authors	(Fullname) Values ('‘»Œ 2')
Insert Into Authors	(Fullname) Values ('‘»Œ 3')
Insert Into Authors	(Fullname) Values ('‘»Œ 4')
Insert Into Authors	(Fullname) Values ('‘»Œ 5')

Go 
Select * from Books;

Go 
Select * from Authors;