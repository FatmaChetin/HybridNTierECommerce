using Project.BLL.ManagerServices.Abstracts;
using Project.DAL.Repositories.Abstarcts;
using Project.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.ManagerServices.Concretes
{
    public class BaseManager<T>:IManager<T> where T : class,IEntity
    {
        protected IRepository<T> _iRep;
        public BaseManager(IRepository<T> iRep)
        {
            _iRep = iRep;
        }
        public string Add(T item)
        {
            if (item.CreatedDate!=null)
            {
                _iRep.Add(item);
                return "Ekleme Başarılıdır.";
            }
            return "Ekleme tarihi kısmında bir sorunla karşılaşıldı";

        }

        public string AddRange(List<T> list)
        {
            if (list.Count>5)
            {
                return "Maksimum 5 veri ekleyebileceğiniz için işlem gerçekleştirilemedi";
            }
            return "Ekleme durumu başarılı bir şekilde gerçekleştirilmiştir";
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            //exp kontrol işlemleri
            return _iRep.Any(exp);
        }

        public void Delete(T item)
        {
          _iRep.Delete(item);
        }

        public void DeleteRange(List<T> list)
        {
           _iRep.DeleteRange(list);
        }

        public string Destroy(T item)
        {
            if (item.DataStatus!=ENTITIES.Enums.DataStatus.Deleted)
            {
                return "bir veriyi yok etmek için öncelikle onun pasif hale getirildiğinden emin olmanız gerekir";
            }
            _iRep.Destroy(item);
            return "veri yok edildi";
        }

        public void DestroyRange(List<T> list)
        {
           _iRep.DestroyRange(list);
        }

        public T Find(int id)
        {
            return _iRep.Find(id);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> exp)
        {
           return _iRep.FirstOrDefault(exp);
        }

        public List<T> GetActives()
        {
            return _iRep.GetActives();
        }

        public List<T> GetAll()
        {
            return _iRep.GetAll();
        }

        public List<T> GetFirstDatas(int number)
        {
            return _iRep.GetFirstDatas(number);
        }

        public List<T> GetLastDatas(int number)
        {
           return _iRep.GetLastDatas(number);
        }

        public List<T> GetPassives()
        {
           return _iRep.GetPassives();
        }

        public List<T> GetUpdates()
        {
            return _iRep.GetUpdates();
        }

        public object Select(Expression<Func<T, object>> exp)
        {
            return _iRep.Select(exp);
        }

        public IQueryable<X> Select<X>(Expression<Func<T, X>> exp)
        {
            return _iRep.Select(exp);
        }

        public void Update(T item)
        {
            _iRep.Update(item);
        }

        public void UpdateRange(List<T> list)
        {
            _iRep.UpdateRange(list);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _iRep.Where(exp);
        }
    }
}
