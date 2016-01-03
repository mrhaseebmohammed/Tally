using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tally.Models;
using Tally.Entities;

namespace Tally.Controllers
{
    public class HomeController : Controller
    {

        private TallyEntities db = new TallyEntities();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult PollResults(string shortURL)
        {
            PollViewModel pvm = new PollViewModel();
            Question q = new Question();
            q = db.Questions.First(x => x.ShortURL == shortURL);

            pvm.Question = q;

            pvm.Answers = db.Answers.Where(x => x.QuestionID == q.Id).ToArray();


            return View(pvm);
        }

        public ActionResult AdminPoll(string password)
        {
            PollViewModel pvm = new PollViewModel();
            Question q = new Question();
            q = db.Questions.First(x => x.Password == password);

            pvm.Question = q;

            pvm.Answers = db.Answers.Where(x => x.QuestionID == q.Id).ToArray();


            return View(pvm);
        }
        
        [HttpPost]
        public ActionResult Poll(string shortURL, int Answer)
        {
            Question q = new Question();
            q = db.Questions.First(x => x.ShortURL == shortURL);
            Answer a = new Answer();
            a = db.Answers.First(x => x.Id == Answer);
            a.Count = a.Count + 1;

            db.SaveChanges();

            return RedirectToAction("PollResults", new { shortUrl = q.ShortURL });
        }


        public ActionResult Poll(string shortURL)
        {
            PollViewModel pvm = new PollViewModel();
            Question q = new Question();
            q = db.Questions.First(x => x.ShortURL == shortURL);

            pvm.Question = q;

            pvm.Answers = db.Answers.Where(x => x.QuestionID == q.Id).ToArray();


            return View(pvm);
        }

        public string generateRandom(int maxLength, Random rnd)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[maxLength];
            for (int i = 0; i < maxLength; i++)
            {
                chars[i] = allowedChars[rnd.Next(0, allowedChars.Length)];
            }

            return new string(chars);

        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "Question, Answers")] CreatePollViewModel cpvm)
        {
            if(ModelState.IsValid)
            {
                Question newQuestion = new Question();

                newQuestion.QuestionText = cpvm.Question;

                Random rnd = new Random();

                newQuestion.ShortURL = generateRandom(6,rnd);
                newQuestion.Password = generateRandom(6,rnd);

                newQuestion.CreatedDateTime = DateTime.Now;

                db.Questions.Add(newQuestion);

                db.SaveChanges();

                //int id = newQuestion.Id;

                Answer[] newAnswer = new Answer[cpvm.Answers.Length];

                for(int ndx = 0; ndx<cpvm.Answers.Length; ndx++)
                {
                    newAnswer[ndx] = new Answer();

                    newAnswer[ndx].QuestionID = newQuestion.Id;
                    newAnswer[ndx].AnswerText = cpvm.Answers[ndx].Value;

                    db.Answers.Add(newAnswer[ndx]);
                }                

                db.SaveChanges();

                //PollViewModel pvm = new PollViewModel();
                //pvm.Question = cpvm.Question;
                //pvm.Answers = cpvm.Answers.Select(x => x.Value).ToArray();
                return RedirectToAction("AdminPoll", new { password = newQuestion.Password });
            }

            return View();
        }

    }
}