using Azure.Core;
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Security.Claims;

namespace CollegeCBTSoftwareWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult StudentTermReg()
        {

            try
            {
                IEnumerable<LevelRegistration> Tables = dbContext.LevelRegistrations.Include(d => d.SessionTable).Include(h => h.StudentCourseReg).Include(h => h.StudentTable).ThenInclude(h => h.ApplicationUser).OrderByDescending(k => k.CreatedDate).ToList();

                Tables = Tables.OrderByDescending(k => k.Id).ToList();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var Table = Tables.Where(m => m.StudentTable.FullName.Contains(searchValue)
                                                    || m.SessionTable.Name.Contains(searchValue)
                                                    || m.StudentTable.ApplicationUser.UserName.Contains(searchValue)
                                                    || m.Level.Contains(searchValue)
                                                    || m.Semester.Contains(searchValue));
                    Tables = Table;
                }
                recordsTotal = Tables.Count();
                var data = Tables.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult StudentTableLoader()
        {
            try
            {
                IEnumerable<StudentTable> Tables = dbContext.StudentTables.Include(d => d.ApplicationUser).Include(h => h.SessionTable).ToList();

                Tables = Tables.OrderByDescending(k => k.Id).ToList();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var Table = Tables.Where(m => m.FullName.Contains(searchValue)
                                                    || m.Gender.Contains(searchValue)
                                                    || m.ApplicationUser.UserName.Contains(searchValue)
                                                    || m.SessionTable.Name.Contains(searchValue));
                    Tables = Table;
                }
                recordsTotal = Tables.Count();
                var data = Tables.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult QuestionTableLoader()
        {
            try 
            { 
                IEnumerable<QuestionTable> Tables = dbContext.QuestionTables.Include(h => h.CourseTable).ToList();
                Tables = Tables.OrderByDescending(k => k.Id).ToList();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    var Table = Tables.Where(m => m.Question.Contains(searchValue)
                                                        || m.CourseTable.Name.Contains(searchValue)
                                                        || m.Level.Contains(searchValue)
                                                        || m.Semester.Contains(searchValue)
                                                        || m.Answer.Contains(searchValue));
                    Tables = Table;
                }
                    recordsTotal = Tables.Count();
                    var data = Tables.Skip(skip).Take(pageSize).ToList();
                    var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                    return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult ExampublishLoader()
        {
            try
            {
                IEnumerable<ExamReady> Tables = dbContext.ExamReadies.Include(h => h.SessionTable).Include(k=>k.CourseTable).ToList();

                Tables = Tables.OrderByDescending(k => k.Id).ToList();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var Table = Tables.Where(m => m.Level.Contains(searchValue)
                                                    || m.Semester.Contains(searchValue)
                                                    || m.SessionTable.Name.Contains(searchValue));
                    Tables = Table;
                }
                recordsTotal = Tables.Count();
                var data = Tables.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        [HttpPost]
        public JsonResult StudentTable()
        {
            try
            {
                IEnumerable<StudentTable> Tables = dbContext.StudentTables.Include(d => d.ApplicationUser).Include(h => h.SessionTable).ToList();

                Tables = Tables.OrderByDescending(k => k.Id).ToList();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var Table = Tables.Where(m => m.FullName.Contains(searchValue)
                                                        || m.Gender.Contains(searchValue)
                                                        || m.ApplicationUser.UserName.Contains(searchValue)
                                                        || m.SessionTable.Name.Contains(searchValue));
                    Tables = Table;
                }
                recordsTotal = Tables.Count();
                var data = Tables.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //LoadNodeQuestion
        //[HttpPost]
        //public JsonResult LoadNodeQuestion()
        //{
        //    try
        //    {
        //        IEnumerable<NodeQuestionTable> Tables = dbContext.NodeQuestionTables.Include(d => d.ClassTable).Include(h => h.SubjectTable).Include(k=>k.QuestionTable).ToList();

        //        Tables = Tables.OrderByDescending(k => k.Id).ToList();
        //        var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
        //        var draw = Request.Form["draw"].FirstOrDefault();
        //        var start = Request.Form["start"].FirstOrDefault();
        //        var length = Request.Form["length"].FirstOrDefault();
        //        var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
        //        var searchValue = Request.Form["search[value]"].FirstOrDefault();
        //        int pageSize = length != null ? Convert.ToInt32(length) : 0;
        //        int skip = start != null ? Convert.ToInt32(start) : 0;
        //        int recordsTotal = 0;
        //        if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
        //        {
        //            Tables = Tables.OrderBy(s => sortColumn + " " + sortColumnDirection).ToList();
        //        }
        //        if (!string.IsNullOrEmpty(searchValue))
        //        {
        //            var Table = Tables.Where(m => m.ClassTable.Name.Contains(searchValue)
        //                                                || m.SubjectTable.Name.Contains(searchValue)
        //                                                || m.Node_Subject.Contains(searchValue)
        //                                                || m.Title.Contains(searchValue));
        //            Tables = Table;
        //        }
        //        recordsTotal = Tables.Count();
        //        var data = Tables.Skip(skip).Take(pageSize).ToList();
        //        var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
        //        return Json(jsonData);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
