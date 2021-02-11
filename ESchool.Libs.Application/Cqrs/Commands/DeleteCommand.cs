using System;
using MediatR;

namespace ESchool.Libs.Application.Cqrs.Commands
{
    public class DeleteCommand<TKey> : IRequest
    {
        public TKey Id { get; set; }
    }

    public class DeleteCommand : DeleteCommand<Guid>
    {
    }
}