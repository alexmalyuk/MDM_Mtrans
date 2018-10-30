﻿using Data.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
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
        private LinkRepository linkRepository;
        private ContractorInfoRepository contractorInfoRepository;

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

        public LinkRepository Links
        {
            get
            {
                if (linkRepository == null)
                    linkRepository = new LinkRepository(db);
                return linkRepository;
            }
        }
        public ContractorInfoRepository ContractorInfos
        {
            get
            {
                if (contractorInfoRepository == null)
                    contractorInfoRepository = new ContractorInfoRepository(db);
                return contractorInfoRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
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
