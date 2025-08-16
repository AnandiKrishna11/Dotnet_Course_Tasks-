
CREATE PROCEDURE Student_Create
    @Name VARCHAR(100),
    @Age INT,
    @Grade VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[Student] ([Name], [Age], [Grade])
    VALUES (@Name, @Age, @Grade);
    SELECT SCOPE_IDENTITY() AS StudentId;
END
GO


CREATE PROCEDURE Student_Read
    @StudentId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [StudentId], [Name], [Age], [Grade]
    FROM [dbo].[Student]
    WHERE [StudentId] = @StudentId;
END
GO


CREATE PROCEDURE Student_ReadAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [StudentId], [Name], [Age], [Grade]
    FROM [dbo].[Student];
END
GO


CREATE PROCEDURE Student_Update
    @StudentId INT,
    @Name VARCHAR(100),
    @Age INT,
    @Grade VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[Student]
    SET
        [Name] = @Name,
        [Age] = @Age,
        [Grade] = @Grade
    WHERE [StudentId] = @StudentId;
END
GO

CREATE PROCEDURE Student_Delete
    @StudentId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM [dbo].[Student]
    WHERE [StudentId] = @StudentId;
END
GO


SELECT * FROM STUDENT;