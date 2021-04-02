namespace ESchool.Libs.Outbox
{
    public class OutboxConfiguration
    {
        public int DispatchDelay { get; set; }
        public int DispatchInterval { get; set; }
        public int RetryCount { get; set; }
    }
}