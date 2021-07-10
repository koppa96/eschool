using System;
using MediatR;

namespace ESchool.Libs.Interface.Commands
{
    public class DeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}