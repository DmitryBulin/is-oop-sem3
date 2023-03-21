using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule : ISchedule
{
    private Schedule(List<Lesson> lessons)
    {
        Lessons = lessons;
    }

    public static ScheduleBuilder Builder => new ScheduleBuilder();
    public IReadOnlyList<Lesson> Lessons { get; }

    public bool GroupsLessonsIntersect(Group firstGroup, Group secondGroup)
    {
        IEnumerable<Lesson> firstGroupLessons = Lessons.Where(lesson => lesson.Group == firstGroup);
        IEnumerable<Lesson> secondGroupLessons = Lessons.Where(lesson => lesson.Group == secondGroup);
        return firstGroupLessons.Any(lhs => secondGroupLessons.Any(rhs => lhs.Time == rhs.Time));
    }

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons = new List<Lesson>();

        public ScheduleBuilder WithLesson(Lesson lessonToAdd)
        {
            if (_lessons.Any(lesson => lesson == lessonToAdd))
            {
                throw ScheduleException.LessonDuplicationException(lessonToAdd);
            }

            _lessons.Add(lessonToAdd);
            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }
    }
}
