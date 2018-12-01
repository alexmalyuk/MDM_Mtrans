using Data.Models;
using Data.Models.Core;
using System;
using System.Data.Entity;
using System.Linq;

namespace Domain.Repositories
{
    public class NodeRepository : IRepository<Node>
    {
        private DataContext db;

        public NodeRepository(DataContext db)
        {
            this.db = db;
        }

        public void AddOrUpdate(Node item)
        {
            Node node = db.Nodes.Find(item.Id);
            if (node == null)
            {
                db.Entry(item).State = EntityState.Added;
            }
            else
            {
                db.Entry(item).State = EntityState.Modified;
            }
        }

        public void Delete(Guid id)
        {
            Node item = db.Nodes.Find(id);
            if (item != null)
                db.Nodes.Remove(item);
        }

        public Node Get(Guid id)
        {
            return db.Nodes.Find(id);
        }

        public IQueryable<Node> GetAll()
        {
            return db.Nodes;
        }

        public Node GetByAlias(string alias)
        {
            return db.Nodes.Include(n => n.Links).Include("Links.Subject").Where(a => a.Alias == alias).FirstOrDefault();
        }
    }
}
