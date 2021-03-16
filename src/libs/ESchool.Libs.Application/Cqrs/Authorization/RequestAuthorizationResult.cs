namespace ESchool.Libs.Application.Cqrs.Authorization
{
    public class RequestAuthorizationResult
    {
        public bool AuthorizationSuccessful { get; private init; }
        public string ErrorMessage { get; private init; }
        
        private RequestAuthorizationResult()
        {
        }

        public static RequestAuthorizationResult Success { get; } = new RequestAuthorizationResult
        {
            AuthorizationSuccessful = true
        };

        public static RequestAuthorizationResult Failure(string errorMessage)
        {
            return new RequestAuthorizationResult
            {
                AuthorizationSuccessful = false,
                ErrorMessage = errorMessage
            };
        }
    }
}