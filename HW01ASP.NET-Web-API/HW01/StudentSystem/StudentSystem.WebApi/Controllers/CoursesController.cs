namespace StudentSystem.WebApi.Controllers
{
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.WebApi.Models;
    using System.Linq;
    using System.Web.Http;

    public class CoursesController : ApiController
    {
        private StudentsSystemData data;

        public CoursesController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var result = this.data.Courses.All()
                .OrderByDescending(c => c.Name)
                .Take(10)
                .Select(c => new CourseRequestModel
                {
                    Name = c.Name,
                    Description = c.Description,
                    Students = c.Students,
                    Homeworks = c.Homeworks
                })
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest("Course name is null or empty");
            }
            var result = this.data.Courses
                .All()
                .FirstOrDefault(cr => cr.Name == id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public IHttpActionResult Post(CourseRequestModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Course is not set to un instance of object");
            }

            var newCourse = new Course()
            {
                Name = model.Name,
                Description = model.Description,
            };

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            return this.Ok(newCourse.Id);
        }

        public IHttpActionResult Put(CourseRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var courseForUpdate = this.data.Courses
                .SearchFor(c => c.Name == model.Name)
                .FirstOrDefault();

            if (courseForUpdate == null)
            {
                return this.NotFound();
            }

            courseForUpdate.Description = model.Description;
            courseForUpdate.Name = model.Name;

            this.data.Courses.Update(courseForUpdate);
            this.data.SaveChanges();

            return this.Ok(courseForUpdate);
        }

        public IHttpActionResult Delete(string id) // id is the GUID taken from the query
        {
            var course = this.data.Courses.SearchFor(c => c.Id.ToString() == id).FirstOrDefault();

            if (course == null)
            {
                return this.NotFound();
            }

            this.data.Courses.Delete(course);
            this.data.SaveChanges();

            return this.Ok(string.Format("Course: {0} has been deleted from the database", course.Name));
        }
    }
}