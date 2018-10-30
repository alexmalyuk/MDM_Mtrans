using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class LinkRepository: IRepository<Link>
    {
        private DataContext db;

        public LinkRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(Link item)
        {
            db.Links.Add(item);
        }

        public void Delete(Guid id)
        {
            Link item = db.Links.Find(id);
            if (item != null)
                db.Links.Remove(item);
        }

        public Link Get(Guid id)
        {
            return db.Links.Find(id);
        }

        public IQueryable<Link> GetAll()
        {
            return db.Links;
        }

        public void Update(Link item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

    }
}
