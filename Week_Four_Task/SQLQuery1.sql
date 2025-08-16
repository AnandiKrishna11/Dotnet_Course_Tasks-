USE StudentDataBase;
CREATE TABLE Student (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Age INT,
    Grade NVARCHAR(10)
);
-- Sample data inserted 
INSERT INTO Students (Name, Age, Grade) VALUES
('Anandi',20,'A');

SELECT * FROM Student;

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL
);
SELECT * FROM Users;
