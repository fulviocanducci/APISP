﻿USE [Sp]
GO

/****** Object:  StoredProcedure [dbo].[PEOPLE_FIND]    Script Date: 20/06/2023 18:20:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PEOPLE_FIND] 	
	@Id int
AS
BEGIN	
	SET NOCOUNT ON;
    SELECT TOP(1) * FROM Peoples WHERE [Id]=@Id;
END
GO