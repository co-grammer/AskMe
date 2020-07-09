using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.ViewModels;
using StackOverflowProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{
    public interface IAnswersService
    {
        void InsertAnswer(NewAnswerViewModel navm);

        void UpdateAnswer(EditAnswerViewModel navm);

        void UpdateAnswerVotesCount(int aid, int uid, int value);
        void DeleteAnswer(int aid);
        List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID);
        AnswerViewModel GetAnswersByAnswerID(int AnswerID);
    }

    public class AnswerService : IAnswersService
    {
        IAnswersRepository ar;

        public AnswerService()
        {
            ar = new AnswersRepository();
        }

        public void InsertAnswer(NewAnswerViewModel navm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<NewAnswerViewModel, Answer>(navm);
            ar.InsertAnswer(a);
        }

        public void UpdateAnswer(EditAnswerViewModel eavm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditAnswerViewModel, Answer>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Answer a = mapper.Map<EditAnswerViewModel, Answer>(eavm);
            ar.UpdateAnswer(a);
        }

        public void UpdateAnswerVotesCount(int aid, int uid, int value)
        {            
            ar.UpdateAnswerVotesCount(aid, uid, value);
        }

        public void DeleteAnswer(int aid)
        {
            ar.DeleteAnswer(aid);
        }

        public List<AnswerViewModel> GetAnswersByQuestionID(int QuestionID)
        {
            List<Answer> a = ar.GetAnswersByQuestionID(QuestionID);
            List<AnswerViewModel> avm = null;
            if(a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<List<Answer>, List<AnswerViewModel>>(a);
            }

            return avm;
        }

        public AnswerViewModel GetAnswersByAnswerID(int AnswerID)
        {
            Answer a = ar.GetAnswersByAnswerID(AnswerID).FirstOrDefault();
            AnswerViewModel avm = null;
            if (a != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Answer, AnswerViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                avm = mapper.Map<Answer, AnswerViewModel>(a);
            }

            return avm;
        }
    }
}
