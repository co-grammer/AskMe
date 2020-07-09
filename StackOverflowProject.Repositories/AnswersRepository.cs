using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IAnswersRepository
    {
        void InsertAnswer(Answer a);
        void UpdateAnswer(Answer a);
        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<Answer> GetAnswersByQuestionID(int QuestionID);
        List<Answer> GetAnswersByAnswerID(int AnswerID);
    }

    public class AnswersRepository : IAnswersRepository
    {
        StackOverflowDatabaseDbContext db;
        IQuestionsRepository qr;
        IVotesRepository vr;

        public AnswersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
            qr = new QuestionsRepository();
            vr = new VotesRepository();
        }

        public void InsertAnswer(Answer a)
        {
            db.Answers.Add(a);
            db.SaveChanges();
            qr.UpdateQuestionAnswersCount(a.QuestionID, 1);
        }

        public void UpdateAnswer(Answer a)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == a.AnswerID).FirstOrDefault();
            if(ans != null)
            {
                ans.AnswerText = a.AnswerText;
                db.SaveChanges();
            }            
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (ans != null)
            {
                ans.VotesCount = value;
                db.SaveChanges();
                qr.UpdateQuestionVotesCount(ans.QuestionID, value);
                vr.UpdateVote(aid, uid, value);
            }
        }

        public void DeleteAnswer(int aid)
        {
            Answer ans = db.Answers.Where(temp => temp.AnswerID == aid).FirstOrDefault();
            if (ans != null)
            {
                db.Answers.Remove(ans);
                db.SaveChanges();
                qr.UpdateQuestionAnswersCount(ans.QuestionID, -1);
            }
        }

        public List<Answer> GetAnswers()
        {
            List<Answer> answers = db.Answers.ToList();
            return answers;
        }

        public List<Answer> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> answers = db.Answers.Where(temp => temp.QuestionID == QuestionID).OrderByDescending(temp => temp.AnswerDateAndTime).ToList();
            return answers;
        }

        public List<Answer> GetAnswersByAnswerID(int AnswerID)
        {
            List<Answer> answer = db.Answers.Where(temp => temp.QuestionID == AnswerID).ToList();
            return answer;
        }
    }
}
