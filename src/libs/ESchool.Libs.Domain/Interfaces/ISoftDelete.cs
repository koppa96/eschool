namespace ESchool.Libs.Domain.Interfaces
{
    public interface ISoftDelete : IEntity
    {
        public bool IsDeleted { get; set; }
    }
}