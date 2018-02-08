CREATE PROC GetAllWithPermission
	@userId VARCHAR(256)
AS
BEGIN
		  select f.*
			from Functions f
			join Permissions p on f.Id = p.FunctionId
			join AppRoles r on p.RoleId = r.Id
			join AppUserRoles ur on r.id = ur.RoleId
			join AppUsers u on ur.UserId = u.Id
			where u.Id = @userId and (p.CanRead = 1)
END