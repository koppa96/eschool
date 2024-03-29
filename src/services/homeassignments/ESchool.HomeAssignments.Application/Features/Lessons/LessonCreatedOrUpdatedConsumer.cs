﻿using System;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.IntegrationEvents.Lessons;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Lessons
{
    public class LessonCreatedOrUpdatedConsumer : IConsumer<LessonCreatedOrUpdatedEvent>
    {
        private readonly Lazy<HomeAssignmentsContext> lazyDbContext;

        public LessonCreatedOrUpdatedConsumer(Lazy<HomeAssignmentsContext> lazyDbContext)
        {
            this.lazyDbContext = lazyDbContext;
        }
        
        public async Task Consume(ConsumeContext<LessonCreatedOrUpdatedEvent> context)
        {
            var dbContext = lazyDbContext.Value;

            var lesson = await dbContext.Lessons.FindAsync(context.Message.LessonId);
            if (lesson == null)
            {
                var classSchoolYearSubject = await dbContext.ClassSchoolYearSubjects.SingleAsync(x =>
                    x.Class.Id == context.Message.ClassId &&
                    x.SchoolYear.Id == context.Message.SchoolYearId &&
                    x.Subject.Id == context.Message.SubjectId);
                
                lesson = new Lesson
                {
                    Id = context.Message.LessonId,
                    ClassSchoolYearSubject = classSchoolYearSubject
                };
                dbContext.Lessons.Add(lesson);
            }

            lesson.Title = context.Message.Title;
            await dbContext.SaveChangesAsync();
        }
    }
}