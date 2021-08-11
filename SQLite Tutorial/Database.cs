using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Tutorial
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _db;

        public Database(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Person>().Wait();
        }

        public Task<List<Person>> GetPeopleAsync()
        {
            return _db.Table<Person>().ToListAsync();
        }

        public Task<int> SavePersonAsync(Person person)
        {
            return _db.InsertAsync(person);
        }

        public Task<int> UpdatePersonAsync(Person person)
        {
            return _db.UpdateAsync(person);
        }

        public Task<int> DeletePersonAsync(Person person)
        {
            return _db.DeleteAsync(person);
        }

        public Task<List<Person>> QuerySubscribedAsync()
        {
            return _db.QueryAsync<Person>("SELECT * FROM Person WHERE Subscribed = true");
        }

        public Task<List<Person>> LinqNotSubscribed()
        {
            return _db.Table<Person>().Where(p => p.Subscribed == false).ToListAsync();
        }
    }
}
