using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormUI
{
    public class DataAccess
    {
        public List<Person> GetPeople()
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConnectionValue("SampleDbStandard")))
            {
                var res = connection.Query<Person>("SELECT * FROM People ORDER BY id DESC").ToList();
                return res;
            }
        }

        public List<Person> GetPeopleByLastName(string lastName)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConnectionValue("SampleDbStandard")))
            {
                var res = connection.Query<Person>("dbo.People_GetByLastName @LastName", new Parameters { LastName = lastName }).ToList();
                return res;
            }
        }

        public List<Person> AddPeople(Person person)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConnectionValue("SampleDbStandard")))
            {
                var res = connection.Query<Person>("dbo.People_Insert @FirstName, @LastName, @EmailAddress, @PhoneNumber", person).ToList();
                return res;
            }
        }

        public List<Person> UpdatePeople(Person person)
        {
            using (IDbConnection connection = new SqlConnection(Helper.ConnectionValue("SampleDbStandard")))
            {
                var res = connection.Query<Person>("dbo.People_Update @id, @FirstName, @LastName, @EmailAddress, @PhoneNumber", person).ToList();
                return res;
            }
        }

    }

    public class Parameters
    {
        public string LastName { get; set; }
    }
}
