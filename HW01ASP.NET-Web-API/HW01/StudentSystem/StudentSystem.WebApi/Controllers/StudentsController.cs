namespace StudentSystem.WebApi.Controllers
{
    using StudentSystem.Data;
    using StudentSystem.Models;
    using StudentSystem.WebApi.Models;
    using System.Linq;
    using System.Web.Http;

    public class StudentsController : ApiController
    {
        private StudentsSystemData data;

        public StudentsController()
        {
            this.data = new StudentsSystemData();
        }

        public IHttpActionResult Get()
        {
            var result = this.data.Students.All()
                .OrderByDescending(c => c.StudentIdentification)
                .Take(10)
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(int id)
        {
            if (id < 1)
            {
                return this.BadRequest("Student id cannot be 0 or negative");
            }

            var result = this.data.Students
                .All()
                .FirstOrDefault(s => s.StudentIdentification == id);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public IHttpActionResult Post(StudentRequestModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Student is not set to un instance of object");
            }

            var newStudent = new Student()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Level = model.Level,
                //AdditionalInformation = new StudentInfo(){Email = model.Email, Address = model.Address},
            };
            newStudent.AdditionalInformation.Email = model.Email;
            newStudent.AdditionalInformation.Address = model.Address;

            this.data.Students.Add(newStudent);
            this.data.SaveChanges();

            return this.Ok(newStudent.StudentIdentification);
        }

        public IHttpActionResult Put(StudentRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var studentForUpdate = this.data.Students
                .SearchFor(c => c.FirstName == model.FirstName && c.LastName == model.LastName)
                .FirstOrDefault();

            if (studentForUpdate == null)
            {
                return this.NotFound();
            }

            studentForUpdate.Level = model.Level;
            studentForUpdate.AdditionalInformation.Address = model.Address;
            studentForUpdate.AdditionalInformation.Email = model.Email;

            this.data.Students.Update(studentForUpdate);
            this.data.SaveChanges();

            return this.Ok(studentForUpdate);
        }

        public IHttpActionResult Delete(int id) // id is the number taken from the query
        {
            var studentToDelete = this.data.Students.SearchFor(s => s.StudentIdentification == id).FirstOrDefault();

            if (studentToDelete == null)
            {
                return this.NotFound();
            }

            this.data.Students.Delete(studentToDelete);
            this.data.SaveChanges();

            return this.Ok(string.Format("Student: {0} {1} has been deleted from the database", studentToDelete.FirstName, studentToDelete.LastName));
        }
    }
}