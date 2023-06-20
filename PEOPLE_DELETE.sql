USE [Sp]
GO

/****** Object:  StoredProcedure [dbo].[PEOPLE_DELETE]    Script Date: 20/06/2023 18:19:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PEOPLE_DELETE]	
	@Id int
AS
BEGIN	
    DELETE FROM Peoples WHERE [Id]=@Id;
END
GO