namespace ESchool.Testing.Interface
{
    public static class TestingConstants
    {
        public const string DiscriminatorName = "taskType";
        
        public static class Discriminators
        {
            public const string FreeText = nameof(FreeText);
            public const string MultipleChoice = nameof(MultipleChoice);
            public const string TrueOrFalse = nameof(TrueOrFalse);
        }
    }
}