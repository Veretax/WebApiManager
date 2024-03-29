﻿CREATE PROCEDURE [dbo].[sp_Product_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [ProductName], [Description], [RetailPrice], [QuantityInStock], [IsTaxable]
		FROM dbo.Product
		ORDER by Product.ProductName ASC;
END