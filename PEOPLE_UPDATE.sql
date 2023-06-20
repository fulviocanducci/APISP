USE [Sp]
GO

/****** Object:  StoredProcedure [dbo].[PEOPLE_UPDATE]    Script Date: 20/06/2023 18:21:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PEOPLE_UPDATE] 
	@Name nvarchar(50),
	@Active bit, 
	@Id int
AS
BEGIN	
    UPDATE Peoples SET [Name]=@Name, [Active]=@Active WHERE Id=@Id;
END
GO