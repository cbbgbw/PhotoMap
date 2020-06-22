SELECT  *
  FROM [dbo].[User] U
    LEFT JOIN Photo P ON P.UserROWGUID = U.UserROWGUID