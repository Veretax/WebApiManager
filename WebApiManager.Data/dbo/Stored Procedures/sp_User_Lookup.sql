CREATE PROCEDURE [dbo].[sp_User_Lookup]
	@Id nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [FirstName], [LastName], [EmailAddress], [CreatedDate] 
	FROM [dbo].[User]
	WHERE [dbo].[User].Id = @Id
END