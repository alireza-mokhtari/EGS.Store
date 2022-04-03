namespace EGS.Domain.Common
{
    public interface IEntity<TId> where TId : class
    {
        TId Id { get; set; }
    }
}
