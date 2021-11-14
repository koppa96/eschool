using ESchool.Libs.Interface.Query;

namespace ESchool.Messaging.Interface.Messages
{
    public class MessageRecipientListQuery : PagedListQuery<RecipientDto>
    {
        public string SearchText { get; set; }
    }
}