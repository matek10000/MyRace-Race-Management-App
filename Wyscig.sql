/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [IDwydarzenie]
      ,[nazwa]
      ,[data]
      ,[IDtrasa]
  FROM [Wyscig].[dbo].[Wydarzenia]