namespace ESchool.Libs.Outbox.EntityFrameworkCore.Entities
{
    public enum OutboxEntryState
    {
        Pending,
        Sent,
        Failed
    }
}