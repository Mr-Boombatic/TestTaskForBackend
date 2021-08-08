USE Lotto
CREATE PROCEDURE FilterTickets
	@template NVARCHAR(20)
AS
BEGIN
	select * from Tickets where SelectedNum LIKE @template +'%';
END
GO
