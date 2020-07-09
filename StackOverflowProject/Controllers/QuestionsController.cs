using StackOverflowProject.CustomFilters;
using StackOverflowProject.ServiceLayer;
using StackOverflowProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StackOverflowProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        ICategoriesService cs;
        IAnswersService asr;
        
        public QuestionsController(IQuestionsService qs, ICategoriesService cs, IAnswersService asr)
        {
            this.qs = qs;
            this.cs = cs;
            this.asr = asr;
        }

        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(id, Convert.ToInt32(Session["CurrentUserID"]));
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            navm.AnswerDateAndTime = DateTime.Now;
            navm.VotesCount = 0;
            if (ModelState.IsValid)
            {
                this.asr.InsertAnswer(navm);
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                QuestionViewModel qvm = new QuestionViewModel();
                return View("View", qvm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult EditAnswer(EditAnswerViewModel eavm)
        {
            if (ModelState.IsValid)
            {
                eavm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.asr.UpdateAnswer(eavm);

                return RedirectToAction("View", "Questions", new { id = eavm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");

                return RedirectToAction("View", "Questions", new { id = eavm.QuestionID });
            }
        }
        
        public ActionResult Create()
        {
            List<CategoryViewModel> categories = cs.GetCategories();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.VotesCount = 0;
                qvm.ViewsCount = 0;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                qvm.QuestionDateAndTime = DateTime.Now;
                this.qs.InsertQuestion(qvm);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");

                return View();
            }
        }
    }
}