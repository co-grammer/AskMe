using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IVotesRepository
    {
        void UpdateVote(int aid, int uid, int value);
    }

    public class VotesRepository : IVotesRepository
    {
        StackOverflowDatabaseDbContext db;

        public VotesRepository()
        {
            db = new StackOverflowDatabaseDbContext(); 
        }

        public void UpdateVote(int aid, int uid, int value)
        {
            int updatevote;
            if (value > 0) updatevote = 1;
            else if (value < 0) updatevote = -1;
            else updatevote = 0;
            Vote vote = db.Votes.Where(temp => temp.AnswerID == aid && temp.UserID == uid).FirstOrDefault();
            if(vote != null)
            {
                vote.VoteValue = updatevote;
            }
            else
            {
                Vote newvote = new Vote() { AnswerID = aid, UserID = uid, VoteValue = updatevote };
                db.Votes.Add(newvote);
            }
            db.SaveChanges();
        }
    }
}
