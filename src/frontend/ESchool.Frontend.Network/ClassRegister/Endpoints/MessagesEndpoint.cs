using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Messaging;
using ESchool.Libs.Interface.Response;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class MessagesEndpoint
    {
        private readonly IFlurlClient flurlClient;
        public const string BasePath = ClassRegisterApi.BasePath + "/messages";
        
        public MessagesEndpoint(IFlurlClient flurlClient)
        {
            this.flurlClient = flurlClient;
        }

        public Task<PagedListResponse<MessageListResponse>> ListIncomingMessagesAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, "incoming")
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<MessageListResponse>>(cancellationToken);
        }

        public Task<PagedListResponse<MessageListResponse>> ListSentMessagesAsync(
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, "sent")
                .SetQueryParams(new { pageIndex, pageSize })
                .GetJsonAsync<PagedListResponse<MessageListResponse>>(cancellationToken);
        }

        public Task<MessageDetailsResponse> SendMessageAsync(
            MessageSendCommand command,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath)
                .PostJsonAsync(command, cancellationToken)
                .ReceiveJson<MessageDetailsResponse>();
        }

        public Task<MessageDetailsResponse> GetMessageDetailsAsync(
            Guid messageId,
            CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, messageId)
                .GetJsonAsync<MessageDetailsResponse>(cancellationToken);
        }
    }
}