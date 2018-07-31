using System;

namespace GraduationTracker
{
    public partial class GraduationTracker
    {
        public Tuple<bool, int, Standing> HasGraduated(Diploma diploma, Student student)
        {
            var credits = 0;
            var average = 0;

            for (int i = 0; i < diploma.Requirements.Length; i++)
            {
                var requirement = Repository.GetRequirement(diploma.Requirements[i]);

                for (int j = 0; j < student.Courses.Length; j++)
                {
                    var check = Array.IndexOf(requirement.Courses, student.Courses[j].Id);

                    if (check > -1)
                    {
                        average += student.Courses[j].Mark;

                        if (student.Courses[j].Mark > requirement.MinimumMark)
                        {
                            credits += requirement.Credits;
                        }
                    }
                }
            }

            average = average / student.Courses.Length;

            if (average < 50)
            {
                return new Tuple<bool, int, Standing>(false, credits, Standing.Remedial);
            }
            else if (average < 80)
            {
                return new Tuple<bool, int, Standing>(true, credits, Standing.Average);
            }
            else if (average < 95)
            {
                return new Tuple<bool, int, Standing>(true, credits, Standing.SumaCumLaude);
            }
            else
            {
                return new Tuple<bool, int, Standing>(true, credits, Standing.SumaCumLaude);
            }
        }
    }
}