namespace ESchool.Libs.Outbox
{
    public class OutboxConfiguration
    {
        public int DispatchInterval { get; set; }
        public int RetryCount { get; set; }
    }
}