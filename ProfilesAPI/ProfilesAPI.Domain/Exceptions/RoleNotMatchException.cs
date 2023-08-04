namespace ProfilesAPI.Domain.Exceptions
{
    public class RoleNotMatchException : BadRequestException
    {
        public RoleNotMatchException(Role userRole, Role needRole)
            : base($"Role {userRole} are not match with {needRole}.")
        {
        }
    }
}
