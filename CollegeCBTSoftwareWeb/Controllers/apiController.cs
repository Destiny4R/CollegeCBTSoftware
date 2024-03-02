
using CollegeCBTSoftwareAccess.Data;
using CollegeCBTSoftwareModel;
using CollegeCBTSoftwareModel.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeCBTSoftwareWeb.Controllers
{
    public class apiController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public apiController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpDelete]
        public IActionResult DeleteSession(int id)
        {
            var getinfo2 = dbContext.StudentTables.Where(j => j.SessionYearId == id);
            if (getinfo2.Any())
            {
                return Json(new { success = false, message = "Admission is given under this session/year therefore it can't be deleted" });
            }
            var getinfo1 = dbContext.LevelRegistrations.Where(j => j.SessionYearId == id);
            if (getinfo1.Any())
            {
                return Json(new { success = false, message = "Students are registered in the term under this session/year" });
            }
            var getinfo3 = dbContext.ExamReadies.Where(j => j.SessionYearId == id);
            if (getinfo2.Any())
            {
                return Json(new { success = false, message = "Students Examination are set under this Session/Year, remove before proceeding." });
            }
            var session = dbContext.SessionTables.FirstOrDefault(k => k.Id == id);
            if (session == null)
            {
                return Json(new { success = false, message = "Error while deleting data" });
            }
            dbContext.SessionTables.Remove(session);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Session/Year Successfully Removed!" });
        }
        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            var getinfo = dbContext.QuestionTables.Where(j => j.CourseId == id);
            if (getinfo.Any())
            {
                return Json(new { success = false, message = "Questions are added under this course, remove all question before proceeding." });
            }

            var subject = dbContext.CourseTables.FirstOrDefault(k => k.Id == id);
            if (subject == null)
            {
                return Json(new { success = false, message = "Error while deleting data" });
            }
            dbContext.Remove(subject);
            dbContext.SaveChanges();
            TempData["success"] = "Course Successfully Removed!";
            return Json(new { success = true, message = "Course Successfully Removed!" });
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var getinfo = dbContext.LevelRegistrations.Where(j => j.StudentId == id);
            if (getinfo.Any())
            {
                return Json(new { success = false, message = "This student has data registered in exams registration in one or more level, remove all data before proceeding." });
            }
            
            var student = dbContext.StudentTables.FirstOrDefault(k => k.Id == id);
            if (student == null)
            {
                return Json(new { success = false, message = "Error while deleting data" });
            }
            dbContext.Remove(student);
            dbContext.SaveChanges();
            TempData["success"] = "Student Successfully Removed!";
            return Json(new { success = true, message = "Student Successfully Removed!" });
        }
        //StudentManager
        [HttpPost]
        public async Task<JsonResult> StudentManager(int id)
        {
            if (id < 1)
            {
                return Json(new { success = false, message = "Data not found" });
            }
            var getuser = dbContext.StudentTables.Include(k => k.ApplicationUser).FirstOrDefault(k => k.Id == id);
            if (getuser == null)
            {
                return Json(new { success = false, message = "Unknown Student Account!" });
            }

            if (getuser != null)
            {
                string msg = "";
                if (getuser.ApplicationUser.Status)
                {
                    getuser.ApplicationUser.Status = false;
                    msg = "Student Account Successfully Block!";
                }
                else
                {
                    getuser.ApplicationUser.Status = true;
                    msg = "Student Account Successfully Unblock!";
                }
                await userManager.UpdateAsync(getuser.ApplicationUser);

                return Json(new { success = true, message = msg });
            }
            return Json(new { success = false, message = "Data not found" });
        }
        [HttpDelete]
        public IActionResult DeleteStudentTermReg(int id)
        {
            if(id>0)
            {
                var getdata = dbContext.LevelRegistrations.Include(n => n.StudentCourseReg).FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    if(getdata.StudentCourseReg.Count()>0)
                    {
                        dbContext.RemoveRange(getdata.StudentCourseReg);
                    }
                    dbContext.Remove(getdata); 
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Student Registration Successfully Deleted!" });
                }
                return Json(new { success = false, message = "Unknown Student Account!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }
        [HttpDelete]
        public IActionResult SubectAbort(int id)
        {
            if (id > 0)
            {
                var getdata = dbContext.StudentCourseReg.FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    if (getdata.Taken)
                    {
                        return Json(new { success = false, message = "Examination has been taken for this course, therefore it cannot be removed!" });
                    }else
                    dbContext.Remove(getdata);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Course Successfully Removed For The Student!" });
                }
                return Json(new { success = false, message = "Unknown Course!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }

        //
        [HttpDelete]
        public IActionResult ResetExams(int id)
        {
            if (id > 0)
            {
                var getdata = dbContext.StudentCourseReg.FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    string msg = "";
                    if (getdata.Taken)
                    {
                        getdata.Taken = false;
                        msg = "Course Successfully Reset For The Student to Re-take!";
                    }
                    else
                    {
                        getdata.Taken = true;
                        msg = "Course Successfully Cancel For The Student!";
                    }
                    dbContext.Update(getdata);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = msg });
                }
                return Json(new { success = false, message = "Unknown Course!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }
        [HttpDelete]
        public IActionResult DeleteQuestion(int id)
        {
            if (id > 0)
            {
                var getdata = dbContext.QuestionTables.FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    dbContext.Remove(getdata);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Question Successfully Removed!" });
                }
                return Json(new { success = false, message = "Unknown Question!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }
        [HttpDelete]
        public IActionResult DeleteExamsReady(int id)
        {
            if (id > 0)
            {
                var getdata = dbContext.ExamReadies.FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    dbContext.Remove(getdata);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Examination Publication Successfully Removed!" });
                }
                return Json(new { success = false, message = "Unknown Publication!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }
        [HttpPost]
        public IActionResult PauseOrFinish(int id)
        {
            if (id > 0)
            {
                var getdata = dbContext.ExamReadies.FirstOrDefault(k => k.Id == id);
                if (getdata != null)
                {
                    string msg = "";
                    if (getdata.PublishExam)
                    {
                        getdata.PublishExam = false;
                        msg = "Examination Publication Successfully Pause/Finished!";
                    }
                    else
                    {
                        getdata.PublishExam = true;
                        msg = "Examination Publication Successfully!";
                    }
                    dbContext.Update(getdata);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = msg });
                }
                return Json(new { success = false, message = "Unknown Publication!" });
            }
            return Json(new { success = false, message = "Unknown data send to the server!" });
        }
        [HttpPost]
        public IActionResult ExaminationData(ObjectiveMarker collectData)
        {
            if (ModelState.IsValid)
            {
                if(!string.IsNullOrEmpty(collectData.RegNumber) && !string.IsNullOrEmpty(collectData.Level) && !string.IsNullOrEmpty(collectData.Semester) && collectData.CourseId>0 && collectData.SessionId > 0)
                {
                    var studentcourse = dbContext.StudentCourseReg
                        .Include(m => m.LevelExamReg)
                        .ThenInclude(l => l.StudentTable)
                        .ThenInclude(m => m.ApplicationUser)
                        .FirstOrDefault(k => k.LevelExamReg.StudentTable.ApplicationUser.UserName == collectData.RegNumber && k.LevelExamReg.Level == collectData.Level && k.LevelExamReg.Semester == collectData.Semester && k.CourseId == collectData.CourseId && k.LevelExamReg.SessionYearId == collectData.SessionId);
                    if(studentcourse != null )
                    {
                        if (studentcourse.Taken)
                        {
                            return Json(new { success = false, message = "Exams already submitted!" });
                        }
                        int correct = 0, wrong = 0;
                        if (collectData.GetQuestionz.Count() > 0)
                        {
                            foreach(var answer in collectData.GetQuestionz)
                            {
                                var quest = dbContext.QuestionTables.FirstOrDefault(k=>k.Id==answer.Id && k.Answer==answer.Answer);
                                if(quest != null)
                                {
                                    correct++;
                                }
                                else
                                {
                                    wrong++;
                                }
                            }
                        }
                        studentcourse.Attendance = DateTime.UtcNow;
                        //studentcourse.Taken = true;
                        studentcourse.Scores = correct;
                        dbContext.Update(studentcourse);
                        int result = dbContext.SaveChanges();
                        if(result>0 ) 
                        {
                            return Json(new { success = true, message = "Examination successfully marked and submtted", correct = correct, wrong = wrong, total = (correct+wrong) });
                        }
                    }
                    return Json(new { success = false, message = "Unidentify information, examination making failed!" });
                }
            }
            return Json(new { success = false, message = "Incompleted Information send for processing, Examination making failed!" });
        }
        //public IActionResult NodeQuestions(NodeQuestionVM nodequestionvm)
        //{
        //    if (nodequestionvm.Id > 0)
        //    {
        //        var nodetable = dbContext.NodeQuestionTables.FirstOrDefault(o => o.Id == nodequestionvm.Id);
        //        if (nodetable != null)
        //        {

        //            nodetable.PassageQuestion = nodequestionvm.PassageQuestion;
        //            nodetable.Title = nodequestionvm.Title;
        //            nodetable.Node_Subject = nodequestionvm.Node_Subject;
        //            nodetable.Compulsory = nodequestionvm.Compulsory;
        //            nodetable.ClassId = nodequestionvm.ClassId;
        //            nodetable.SubjectId = nodequestionvm.SubjectId;
        //            dbContext.Update(nodetable);

        //            if (nodequestionvm.Questions.Count() > 0)
        //            {
        //                var listQuestions = new List<QuestionTable>();
        //                foreach (var item in nodequestionvm.Questions)
        //                {
        //                    var quest = new QuestionTable()
        //                    {
        //                        Question = item.Question,
        //                        OptionOne = item.OptionOne,
        //                        OptionTwo = item.OptionTwo,
        //                        OptionThree = item.OptionThree,
        //                        OptionFour = item.OptionFour,
        //                        Answer = item.Answer,
        //                        ClassId = nodequestionvm.ClassId,
        //                        SubjectId = nodequestionvm.SubjectId,
        //                        NodeQId = nodetable.Id
        //                    };
        //                    listQuestions.Add(quest);
        //                }
        //                dbContext.AddRange(listQuestions);
        //            }
        //            var result = dbContext.SaveChanges();
        //            if (result > 0)
        //            {
        //                return Json(new { success = true, message = "Node Questions Successfully Updated!" });
        //            }
        //        }
        //        return Json(new { success = false, message = "An error occured while updating the node questions!" });
        //    }
        //    else
        //    {
        //        var nodetable = new NodeQuestionTable()
        //        {
        //            PassageQuestion = nodequestionvm.PassageQuestion,
        //            Title = nodequestionvm.Title,
        //            Node_Subject = nodequestionvm.Node_Subject,
        //            Compulsory = nodequestionvm.Compulsory,
        //            ClassId = nodequestionvm.ClassId,
        //            SubjectId = nodequestionvm.SubjectId
        //        };
        //        if (nodequestionvm.Questions.Count() > 0)
        //        {
        //            nodetable.QuestionTable = new List<QuestionTable>();
        //            foreach (var item in nodequestionvm.Questions)
        //            {
        //                var quest = new QuestionTable()
        //                {
        //                    Question = item.Question,
        //                    OptionOne = item.OptionOne,
        //                    OptionTwo = item.OptionTwo,
        //                    OptionThree = item.OptionThree,
        //                    OptionFour = item.OptionFour,
        //                    Answer = item.Answer,
        //                    ClassId = nodequestionvm.ClassId,
        //                    SubjectId = nodequestionvm.SubjectId,
        //                    NodeQId = nodetable.Id
        //                };
        //                nodetable.QuestionTable.Add(quest);
        //            }
        //        }
        //        dbContext.Add(nodetable);
        //        var result = dbContext.SaveChanges();
        //        if (result > 0)
        //        {
        //            return Json(new { success = true, message = "Node Questions Successfully Submitted!" });
        //        }
        //        return Json(new { success = false, message = "An error occured while processing the node questions!" });
        //    }
        //}
        //[HttpDelete]
        //public IActionResult DeleteQuestion(int id)
        //{
        //    if (id > 0)
        //    {
        //        var getdata = dbContext.QuestionTables.FirstOrDefault(k => k.Id == id);
        //        if (getdata != null)
        //        {
        //            dbContext.Remove(getdata);
        //            dbContext.SaveChanges();
        //            return Json(new { success = true, message = "Question Successfully Deleted!" });
        //        }
        //        return Json(new { success = false, message = "Unknown Question!" });
        //    }
        //    return Json(new { success = false, message = "Unknown data send to the server!" });
        //}
        //[HttpDelete]
        //public IActionResult ManageNode(int id)
        //{
        //    if (id > 0)
        //    {
        //        var getdata = dbContext.NodeQuestionTables.Include(j=>j.QuestionTable).FirstOrDefault(k => k.Id == id);
        //        if (getdata != null)
        //        {
        //            if (getdata.QuestionTable.Count()>0)
        //            {
        //                dbContext.RemoveRange(getdata.QuestionTable);
        //            } 
        //            dbContext.Remove(getdata);
        //            dbContext.SaveChanges();
        //            return Json(new { success = true, message = "Node question successfully deleted!" });
        //        }
        //        return Json(new { success = false, message = "Unknown Node question!" });
        //    }
        //    return Json(new { success = false, message = "Unknown data send to the server!" });
        //}
    }
}
