﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.Testing.Application.Features.Tests.Common;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Interface.Features.Tests;
using MediatR;

namespace ESchool.Testing.Application.Features.Tests
{
    public class TestCreateHandler : IRequestHandler<TestCreateCommand, TestDetailsResponse>
    {
        private readonly TestingContext context;
        private readonly IMapper mapper;

        public TestCreateHandler(TestingContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<TestDetailsResponse> Handle(TestCreateCommand request, CancellationToken cancellationToken)
        {
            var test = new Test
            {
                Name = request.Name,
                ScheduledStart = request.ScheduledStart,
                ScheduledLength = TimeSpan.FromMinutes(request.ScheduledLengthInMinutes),
                ClassSchoolYearSubjectId = request.ClassSchoolYearSubjectId,
                StudentTests = request.StudentIds.Select(x => new StudentTest
                {
                    StudentId = x
                }).ToList()
            };

            context.Tests.Add(test);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<TestDetailsResponse>(test);
        }
    }
}