using ESchool.Libs.Interface.Query;

namespace ESchool.Messaging.Interface.RecipientGroups
{
    public class RecipientGroupListQuery : PagedListQuery<RecipientGroupListResponse>
    {
        public string SearchText { get; set; }
    }
}