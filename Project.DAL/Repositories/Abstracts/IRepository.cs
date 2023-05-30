using Project.ENTITIES.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Repositories.Abstarcts
{
    public interface IRepository<T> where T: IEntity
    {
        //List Commands
        //
        //database de var onlan bilgileri listelemek için kullandığımız metodlar var bu list commandslerde yani neye göre listelemek istiyorsak onunla ilgili....

        List<T> GetAll();        // tüm verileri getirmek için kullanıyoruz.
        List<T> GetActives();    // Aktif olan verileri getirmek için kullanıyoruz.
        List<T> GetPassives();   // Pasife çekilmiş verileri getirmek için kullanıyoruz.
        List<T> GetUpdates();     // Güncellenen verileri getirmek için kullanıyoruz.

        //Modify Commands
        //
        //CRUD işlemlerini yaparken kullandığımız metodlardır.

        void Add(T item);       // veri eklemek için kullanırız.
        void AddRange(List<T> list);   // birden fazla veriyi eklemek için kullanırız.
        void Update(T item);    //veri güncelleme işlemi için kullanırız.
        void UpdateRange(List<T> list);   // birden fazla verinin güncellenmesinde kullanılır.
        void Delete(T item);    // veriyi pasife çekmek için kullanılır.
        void DeleteRange(List<T> list);   // birden fazla verinin pasife çekilmesi için kullanılır.
        void Destroy(T item);    // verinin komple veri tabanında yok olmasını sağlar.
        void DestroyRange(List<T> list);    //birden fazla verinin veri tabanından yok olmasını sağlar.

        //Linq Commands
        //
        //linquery işlemlerinin  yapıldı yerdir.
        List<T> Where(Expression<Func<T, bool>>exp);    //Where metodu, belirli bir koşulu sağlayan nesneleri filtrelemek için kullanılır ve sonuç olarak uygun koşulu sağlayan nesnelerin bir listesini döndürür.

        bool Any(Expression<Func<T, bool>>exp);     //belirli bir koşulu sağlayan en az bir nesne var mı diye kontrol etmek için kullanılır. Bunu database de eşleşme var mı diye kontrol etmek için kullanırsın onu düşün

        T FirstOrDefault(Expression<Func<T, bool>>exp);   //belirli bir koşulu sağlayan ilk nesneyi döndürmek için kullanılır ve sonuç olarak nesneyi veya varsayılan değeri döndürür.

        object Select(Expression<Func<T, object>>exp); //object>> exp olmalı ki istediğimiz hale çevirebilelim T nesnesini object her türü içinde barındırır unutma. bool>>exp olsaydı filtreleme yaparken kullanabilirdik.

        IQueryable<X> Select<X>(Expression<Func<T, X>> exp);  //veri kaynağı üzerinde belirli bir dönüşüm veya özellik seçimi yapmak için kullanılır 

        //Find Command
        T Find(int id);   //nesneyi id üzerinden bulmamızı sağlar.

        //Last Datas
        List<T> GetLastDatas(int number);    // son veriyi getirir orber by descending ile kullanırız.

        //First Datas
        List<T> GetFirstDatas(int number);  // ilk veriyi getirmek için order by ile kullanılır

    }
}
