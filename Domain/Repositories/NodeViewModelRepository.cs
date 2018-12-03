using Data.Models;
using Data.Models.Core;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public class NodeViewModelRepository : IRepository<NodeViewModel>
    {
        private DataContext db;

        public NodeViewModelRepository(DataContext db)
        {
            this.db = db;
        }

        public void AddOrUpdate(NodeViewModel model)
        {
            Node node = db.Nodes.Where(a => a.Id == model.Id).FirstOrDefault();

            if (node == null)
            {
                node = new Node();
                db.Entry(node).State = EntityState.Added;
            }
            else
            {
                db.Entry(node).State = EntityState.Unchanged;
            }

            if (node.Name != model.Name)
                node.Name = model.Name;
            if (node.Alias != model.Alias)
                node.Alias = model.Alias;

        }

        public void Delete(Guid id)
        {
            Node node = db.Nodes.Find(id);
            if (node != null)
                db.Entry(node).State = EntityState.Deleted;

        }

        public NodeViewModel Get(Guid id)
        {
            var q = db.Nodes.Where(c => c.Id == id)
                .Select(c => new NodeViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Alias = c.Alias
                });

            return q.FirstOrDefault();
        }

        public IQueryable<NodeViewModel> GetAll()
        {
            var q = db.Nodes
                .Select(c => new NodeViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Alias = c.Alias
                });

            return q;
        }

    }
}
