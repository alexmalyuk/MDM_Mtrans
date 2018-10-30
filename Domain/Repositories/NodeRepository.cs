using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class NodeRepository : IRepository<Node>
    {
        private DataContext db;

        public NodeRepository(DataContext db)
        {
            this.db = db;
        }

        public void Create(Node item)
        {
            db.Nodes.Add(item);
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

        public void Update(Node item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public Node GetByAlias(string alias)
        {
            return db.Nodes.Where(a => a.Alias == alias).FirstOrDefault();
        }
    }
}
