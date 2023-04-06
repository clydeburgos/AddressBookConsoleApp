using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookConsoleApp
{
    public class PersonRepository
    {
        private readonly string connectionString;
        //DRY
        //SOLID
        public PersonRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Person>("SELECT * FROM Person").ToList();
            }
        }

        public Person GetById(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Person>("SELECT * FROM Person WHERE Id = @Id", new { Id = id }).SingleOrDefault();
            }
        }

        public void Add(Person person)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "INSERT INTO Person (FirstName, LastName, DateOfBirth, Address, City, Country) " +
                                  "VALUES(@FirstName, @LastName, @DateOfBirth, @Address, @City, @Country)";
                db.Execute(sqlQuery, person);
            }
        }

        public void Update(Person person)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "UPDATE Person SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, " +
                                  "Address = @Address, City = @City, Country = @Country WHERE Id = @Id";
                db.Execute(sqlQuery, person);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM Person WHERE Id = @Id";
                db.Execute(sqlQuery, new { Id = id });
            }
        }
    }
}
