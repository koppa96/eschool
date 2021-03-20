using System;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class TeacherCreatedOrUpdatedConsumer : IConsumer<TeacherCreatedOrUpdatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;
        private readonly UserService.UserServiceClient client;

        public TeacherCreatedOrUpdatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext, UserService.UserServiceClient client)
        {
            this.lazyDbContext = lazyDbContext;
            this.client = client;
        }

        public async Task Consume(ConsumeContext<TeacherCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var teacher = await client.GetTeacherAsync(new TeacherGetRequest
            {
                Id = context.Message.Id.ToString()
            });

            var existingTeacher = await dbContext.Teachers.FindAsync(context.Message.Id);
            if (existingTeacher == null)
            {
                existingTeacher = new Teacher
                {
                    Id = context.Message.Id,
                    Name = teacher.Name,
                    UserId = Guid.Parse(teacher.UserId)
                };
                dbContext.Teachers.Add(existingTeacher);
            }
            else
            {
                existingTeacher.Name = teacher.Name;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}