using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentApiSection
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PlutoContext();

            // Two different ways of querying a database
            // The extension methods are more powerful, the LINQ syntax is a subset of what you can accomplish using the extension methods

            // LINQ syntax
            //var query =
            //    from c in context.Courses
            //    where c.Name.Contains("c#")
            //    orderby c.Name
            //    select c;

            //foreach (var course in query)
            //{
            //    Console.WriteLine(course.Name);
            //}

            // Extension methods
            //var courses = context.Courses
            //    .Where(c => c.Name.Contains("c#"))
            //    .OrderBy(c => c.Name);

            //foreach (var course in courses)
            //{
            //    Console.WriteLine(course.Name);
            //}



            // LINQ syntax continued

            // restricting
            //var query =
            //    from c in context.Courses
            //    where c.Level == 1 && c.Author.Id == 1
            //    select c;

            // orderby by level first, then inside each level, order by name
            //var query =
            //    from c in context.Courses
            //    where c.Level == 1
            //    orderby c.Level descending, c.Name
            //    select c;

            // optimize what gets returned from the query
            //var query =
            //    from c in context.Courses
            //    where c.Level == 1
            //    orderby c.Level descending, c.Name
            //    select new { Name = c.Name, Author = c.Author.Name };

            // groupby - unlike sql groupby does not require the use of an aggragate function like count
            //var query =
            //    from c in context.Courses
            //    group c by c.Level
            //    into g
            //    select g;
            //foreach (var group in query)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (var course in group)
            //    {
            //        Console.WriteLine("\t {0}", course.Name);
            //    }
            //}

            // Joining - inner and cross join are similar to SQL, no such thing as group join in sql
            // inner join isnt necessary unless there isn't a navigation property... if there is one, just use it like this:
            // select new { Name = c.Name, Author = c.Author.Name }
            //var query =
            //    from c in context.Courses
            //    join a in context.Authors on c.AuthorId equals a.Id
            //    select new { CourseName = c.Name, AuthorName = a.Name };

            // groupjoins do not have a sql equivilant
            //var query =
            //    from a in context.Authors
            //    join c in context.Courses on a.Id equals c.AuthorId
            //    into g
            //    select new { AuthorName = a.Name, Courses = g.Count() };

            // crossjoin
            //var query =
            //    from a in context.Authors
            //    from c in context.Courses
            //    select new { AuthorName = a.Name, CourseName = c.Name };





            // Syntax methods continued...
            //var courses = context.Courses
            //    .Where(c => c.Level == 1)
            //    .OrderBy(c => c.Name)
            //    .ThenByDescending(c => c.Level)
            //    //.Select(c => new { CourseName = c.Name, AuthorName = c.Author.Name }); // because there is an Author navigation property, we can access the author name
            //    .Select(c => c.Tags);

            //foreach (var c in courses)
            //{
            //    foreach (var tag in c)
            //    {
            //        Console.WriteLine(tag.Name);
            //    }
            //}



            //var tags = context.Courses
            //    .Where(c => c.Level == 1)
            //    .OrderBy(c => c.Name)
            //    .ThenByDescending(c => c.Level)
            //    .SelectMany(c => c.Tags)
            //    .Distinct();

            //foreach (var t in tags)
            //{
            //    Console.WriteLine(t.Name);
            //}



            //var groups = context.Courses
            //    .GroupBy(c => c.Level);
            //foreach (var group in groups)
            //{
            //    Console.WriteLine("Key: " + group.Key);
            //    foreach (var course in group)
            //    {
            //        Console.WriteLine("\t" + course.Name);
            //    }
            //}



            // joining
            //context.Courses.Join(
            //    context.Authors,
            //    c => c.AuthorId,
            //    a => a.Id,
            //    (course, author) => new
            //    {
            //        CourseName = course.Name,
            //        AuthorName = author.Name
            //    }
            //);


            //context.Authors.GroupJoin(
            //    context.Courses,
            //    a => a.Id,
            //    c => c.AuthorId,
            //    (author, courses) => new
            //    {
            //        AuthorName = author,
            //        Courses = courses.Count()
            //    }
            //);


            //context.Authors.SelectMany(a => context.Courses, (author, course) => new
            //{
            //    AuthorName = author.Name,
            //    CourseName = course.Name
            //});




            // Extension methods - partitioning
            //context.Courses.Skip(10).Take(10);

            // Element operators
            //context.Courses.OrderBy(c => c.Level).First();
            //context.Courses.OrderBy(c => c.Level).FirstOrDefault();  // will return null instead of error
            //context.Courses.OrderBy(c => c.Level).First(c => c.FullPrice > 100);  // first course that has a price of greater than 100


            // quantifying
            //context.Courses.All(c => c.FullPrice > 10); // are all courses about $10?
            //context.Courses.Any(c => c.FullPrice > 200); // any courses over $200?
            //context.Courses.Count();
            //context.Courses.Max(c => c.FullPrice);
            //context.Courses.Min(c => c.FullPrice);
            //context.Courses.Average(c => c.FullPrice);












            // deferred execution

            // SQL query isn't fired immediately...
            //var courses = context.Courses;
            //var filteredCourses = courses.Where(c => c.Level == 1);
            //var sorted = filteredCourses.OrderBy(c => c.Name);

            // SQL query is fired when it's time to use the results
            //foreach (var c in courses)
            //    Console.WriteLine(c.Name);

            // This kind of thing, a calculated (not stored) property... LINQ doesn't understand
            //var courses = context.Courses.Where(c => c.IsBeginnerCourse == true);
            // To fix it, use immediate execution... won't be optimized because loading all courses from the DB
            //var courses = context.Courses.ToList().Where(c => c.IsBeginnerCourse == true);





            // IQueryable - allows modification of query, since it isn't immediately executed
            //IQueryable<Course> courses = context.Courses;
            //var filtered = courses.Where(c => c.Level == 1);
            // Because it is iqueryable, the query to filtered will have the necessary where clause... not all courses were necessary
            //foreach (var course in filtered)
            //    Console.WriteLine(course.Name);







        }
    }
}
