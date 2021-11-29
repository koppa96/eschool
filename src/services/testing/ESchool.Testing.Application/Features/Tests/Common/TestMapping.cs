using AutoMapper;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Interface.Features.Tests;

namespace ESchool.Testing.Application.Features.Tests.Common
{
    public class TestMapping : Profile
    {
        public TestMapping()
        {
            CreateMap<Test, TestListResponse>()
                .ForMember(x => x.State,
                    o => o.MapFrom(x =>
                        x.StartedAt == null
                            ? TestState.HasNotStarted
                            : (x.ClosedAt == null
                                ? TestState.InProgress
                                : TestState.Finished)));
        }
    }
}