namespace EGS.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string CreateJwtSecurityToken(string id, List<string> roles);
    }
}
