using System;
using MediatR;

namespace ESchool.Libs.Interface.Commands
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