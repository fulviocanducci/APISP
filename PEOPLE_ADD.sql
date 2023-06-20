USE [Sp]
GO

/****** Object:  StoredProcedure [dbo].[PEOPLE_ADD]    Script Date: 20/06/2023 18:18:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PEOPLE_ADD] 
	@Name nvarchar(50),
	@Active bit
AS
BEGIN	
	SET NOCOUNT ON;
    INSERT INTO Peoples([Name], [Active]) VALUES(@Name, @Active);
	SELECT * FROM Peoples WHERE Id=SCOPE_IDENTITY();
END
GO