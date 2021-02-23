using System;
using MediatR;

namespace ESchool.Libs.Application.Cqrs.Commands
{
    public class DeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}