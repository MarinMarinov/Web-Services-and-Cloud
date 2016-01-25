namespace StudentSystem.WebApi.Controllers
{
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.WebApi.Models;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class HomeworksController : ApiController
    {
        private StudentsSystemData data;

        public HomeworksController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var result = this.data.Homeworks.All().ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            if (id < 1)
            {
                return this.BadRequest("Homework id cannot be 0 or negative");
            }

            var result = this.data.Homeworks
                .All()
                .FirstOrDefault(h => h.Id == id);

            if (result == null)
            {
                return this.NotFound();
            }

            var homeworkModel = new HomeworkRequestModel
            {
                StudentIdentification = result.StudentIdentification,
                CourseId = result.CourseId,
                FileUrl = result.FileUrl,
                TimeSent = result.TimeSent
            };

            return this.Ok(homeworkModel);
        }

        public IHttpActionResult Post(HomeworkRequestModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Homework is not set to un instance of object");
            }

            var newHomework = new Homework()
            {
                FileUrl = model.FileUrl,
                TimeSent = DateTime.Now,
                StudentIdentification = model.StudentIdentification,
                CourseId = model.CourseId
            };
            
            this.data.Homeworks.Add(newHomework);
            this.data.SaveChanges();

            return this.Ok(newHomework.Id);
        }

        public IHttpActionResult Put(HomeworkRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var homeworkForUpdate = this.data.Homeworks
                .SearchFor(h => h.StudentIdentification == model.StudentIdentification && h.CourseId == model.CourseId)
                .FirstOrDefault();

            if (homeworkForUpdate == null)
            {
                return this.NotFound();
            }

            homeworkForUpdate.FileUrl = model.FileUrl;
            homeworkForUpdate.TimeSent = DateTime.Now;

            this.data.Homeworks.Update(homeworkForUpdate);
            this.data.SaveChanges();

            return this.Ok(homeworkForUpdate);
        }

        public IHttpActionResult Delete(int id) // id is the number taken from the query
        {
            var homeworkToDelete = this.data.Homeworks.SearchFor(h => h.Id == id).FirstOrDefault();

            if (homeworkToDelete == null)
            {
                return this.NotFound();
            }

            this.data.Homeworks.Delete(homeworkToDelete);
            this.data.SaveChanges();

            return this.Ok(string.Format("Homework with id {0} has been deleted from the database", homeworkToDelete.Id));
        }
    }
}
