CREATE PROCEDURE [dbo].[sp_UserLookup]
	@Id nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [FirstName], [LastName], [Email], [CreatedDate] 
	FROM [dbo].[User]
	WHERE [dbo].[User].Id = @Id
END