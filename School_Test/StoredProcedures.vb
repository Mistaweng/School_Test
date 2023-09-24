Public Class StoredProcedures
	'    USE [PTML_Test]
	'GO
	'/****** Object:  StoredProcedure [dbo].[spCRUDPro]    Script Date: 24/09/2023 23:59:02 ******/
	'Set ANSI_NULLS On
	'GO
	'Set QUOTED_IDENTIFIER On
	'GO
	'--=============================================
	'-- Author:		<Author,, Name>
	'-- Create Date <Create Date,,>
	'-- Description:	<Description,,>
	'--=============================================
	'ALTER PROCEDURE [dbo].[spCRUDPro]
	'	-- Add the parameters for the stored procedure here
	'	@BL_ID INT = 0,
	'	@BL_NUMBER NVARCHAR(50),
	'	@CONSIGNEE NVARCHAR(50),
	'	@BL_TYPE NVARCHAR(50),
	'	@CHOICE VARCHAR(50)
	'AS

	'BEGIN
	'	BEGIN TRY

	'	-- SET NOCOUNT ON added to prevent extra result sets from
	'	-- interfering with SELECT statements.
	'	Set NOCOUNT On;

	'    -- Insert statements for procedure here

	'	If (@CHOICE = 'Select')

	'		BEGIN
	'			Select Case@BL_ID, @BL_NUMBER, @CONSIGNEE, @BL_TYPE
	'			FROM TestTables
	'	End

	'	ElseIf (@CHOICE = 'Insert')

	'		BEGIN
	'			INSERT INTO TestTables (BL_NUMBER, CONSIGNEE, BL_TYPE)
	'			VALUES (@BL_NUMBER, @CONSIGNEE, @BL_TYPE)
	'		End

	'	ElseIf (@CHOICE = 'Update')

	'		BEGIN 
	'			UPDATE TestTables
	'	Set BL_NUMBER = @BL_NUMBER,
	'				CONSIGNEE = @CONSIGNEE,
	'				BL_TYPE = @BL_TYPE
	'			WHERE BL_ID = @BL_ID
	'		End

	'	ElseIf (@CHOICE = 'DELETE')

	'		BEGIN
	'			DELETE FROM TestTables
	'			WHERE BL_ID = @BL_ID
	'		End

	'	End Try
	'	BEGIN CATCH
	'		Declare @ErrorMessage NVARCHAR(4000);
	'		Set @ErrorMessage = ERROR_MESSAGE();
	'	End Catch
	'End

End Class
