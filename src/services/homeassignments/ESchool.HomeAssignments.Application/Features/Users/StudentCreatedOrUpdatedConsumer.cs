using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Grpc;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.Users;
using MassTransit;

namespace ESchool.HomeAssignments.Application.Features.Users
{
    public class StudentCreatedOrUpdatedConsumer : IConsumer<StudentCreatedOrUpdatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;
        private readonly UserService.UserServiceClient client;

        public StudentCreatedOrUpdatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext, UserService.UserServiceClient client)
        {
            this.lazyDbContext = lazyDbContext;
            this.client = client;
        }

        public async Task Consume(ConsumeContext<StudentCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;
            var student = await client.GetStudentAsync(new StudentGetRequest
            {
                Id = context.Message.Id.ToString()
            });
            
            var existingStudent = await dbContext.Students.FindAsync(context.Message.Id);
            if (existingStudent == null)
            {
                existingStudent = new Student
                {
                    Id = context.Message.Id,
                    Name = student.Name,
                    UserId = Guid.Parse(student.UserId)
                };
                dbContext.Students.Add(existingStudent);
            }
            else
            {
                existingStudent.Name = student.Name;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}