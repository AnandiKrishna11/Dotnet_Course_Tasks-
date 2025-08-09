USE StudentDataBase;
CREATE TABLE Students (
    StudentId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Age INT,
    Grade NVARCHAR(10)
);
-- Sample data inserted 
INSERT INTO Students (Name, Age, Grade) VALUES
('Anandi',20,'A');

SELECT * FROM Students;

