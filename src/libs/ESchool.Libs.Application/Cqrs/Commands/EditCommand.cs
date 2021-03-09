using System;
using MediatR;

namespace ESchool.Libs.Application.Cqrs.Commands
{
    public class EditCommand<TInnerCommand, TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; set; }
        public TInnerCommand InnerCommand { get; set; }
    }

    public class EditCommand<TInnerCommand> : EditCommand<TInnerCommand, Unit>
    {
    }
}