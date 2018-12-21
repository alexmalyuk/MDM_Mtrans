using Data.Models;
using Data.Models.Core;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class HistoryViewModelRepository : IRepository<HistoryViewModel>
    {
        private DataContext db;

        public HistoryViewModelRepository(DataContext db)
        {
            this.db = db;
        }

        public HistoryViewModel Get(Guid id)
        {
            var q = db.HistoryEntries
                .Where(h => h.Id == id)
                .Select(h => new HistoryViewModel
                {
                    Id = h.Id,
                    DateUTC = h.DateUTC,
                    Node = h.Node.Name,
                    User = h.User,
                    SubjectId = h.Subject.Id
                });

            return q.FirstOrDefault();
        }
        public Subject GetSubjectSnapshot(Guid id)
        {
            HistoryEntry historyEntry = db.HistoryEntries.Where(h => h.Id == id).Include("Subject").FirstOrDefault();
            return historyEntry?.SubjectSnapshot;
        }

        public IQueryable<HistoryViewModel> GetAllBySubject(Guid subjectId)
        {
            var q = db.HistoryEntries
                .Where(h => h.Subject.Id == subjectId)
                .OrderBy(h => h.DateUTC)
                .Select(h => new HistoryViewModel
                 {
                     Id = h.Id,
                     DateUTC = h.DateUTC,
                     Node = h.Node.Name,
                     User = h.User,
                     SubjectId = h.Subject.Id
                 });

            return q;
        }

        public void AddOrUpdate(HistoryViewModel item, string currentUserName = "")
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<HistoryViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
