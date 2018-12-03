using Data.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UnitOfWork : IDisposable
    {

        private DataContext db = new DataContext();
        private NodeRepository nodeRepository;
        private ContractorRepository contractorRepository;
        //private LinkRepository linkRepository;
        private ContractorApiModelRepository contractorInfoRepository;
        private ContractorViewModelRepository contractorViewModelRepository;
        private NodeViewModelRepository nodeViewModelRepository;

        public NodeRepository Nodes
        {
            get
            {
                if (nodeRepository == null)
                    nodeRepository = new NodeRepository(db);
                return nodeRepository;
            }
        }

        public ContractorRepository Contractors
        {
            get
            {
                if (contractorRepository == null)
                    contractorRepository = new ContractorRepository(db);
                return contractorRepository;
            }
        }

        public ContractorViewModelRepository ContractorViewModel
        {
            get
            {
                if (contractorViewModelRepository == null)
                    contractorViewModelRepository = new ContractorViewModelRepository(db);
                return contractorViewModelRepository;
            }
        }

        public NodeViewModelRepository NodeViewModel
        {
            get
            {
                if (nodeViewModelRepository == null)
                    nodeViewModelRepository = new NodeViewModelRepository(db);
                return nodeViewModelRepository;
            }
        }

        //public LinkRepository Links
        //{
        //    get
        //    {
        //        if (linkRepository == null)
        //            linkRepository = new LinkRepository(db);
        //        return linkRepository;
        //    }
        //}
        public ContractorApiModelRepository ContractorInfos
        {
            get
            {
                if (contractorInfoRepository == null)
                    contractorInfoRepository = new ContractorApiModelRepository(db);
                return contractorInfoRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void SetEntityStateAsModified(object item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void SetEntityStateAsUnchanged(object item)
        {
            db.Entry(item).State = EntityState.Unchanged;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        // переопределить метод завершения, только если Dispose(bool disposing) выше включает код для освобождения неуправляемых ресурсов.
        // ~UnitOfWork() {
        //   // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
        //   Dispose(false);
        // }

        // Этот код добавлен для правильной реализации шаблона высвобождаемого класса.
        public void Dispose()
        {
            // Не изменяйте этот код. Разместите код очистки выше, в методе Dispose(bool disposing).
            Dispose(true);
            // раскомментировать следующую строку, если метод завершения переопределен выше.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
