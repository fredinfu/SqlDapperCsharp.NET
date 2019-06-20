USE [Sample]
GO
/****** Object:  StoredProcedure [dbo].[People_Insert]    Script Date: 6/20/2019 10:26:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[People_Update] 
	@id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@EmailAddress nvarchar(100),
	@PhoneNumber varchar(20)
AS
BEGIN
	SET NOCOUNT ON;

    UPDATE dbo.People 
	SET 
		FirstName = @FirstName, 
		LastName = @LastName, 
		EmailAddress = @EmailAddress, 
		PhoneNumber = @PhoneNumber
	WHERE id = @id;


END
