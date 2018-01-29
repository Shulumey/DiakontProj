using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DiakontTestProject.Model;

namespace DiakontTestProject.Data
{
    public class ReferenceRepository:IReferenceRepository
    {
        private readonly string _connectionString;

        public ReferenceRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Diakont"].ConnectionString;
        }

        public IEnumerable<ReferenceItem> GetPositions()
        {
            return GetData("select Id, Name from dbo.Positions");
        }

        public IEnumerable<ReferenceItem> GetDepartments()
        {
            return GetData("select Id, Name from dbo.Departments");         
        }

        private IEnumerable<ReferenceItem> GetData(string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                IDataReader reader = cmd.ExecuteReader();
                List<ReferenceItem> items = new List<ReferenceItem>();

                while (reader.Read())
                {
                    items.Add(new ReferenceItem { Id = (int)reader[0], Name = (string)reader[1] });
                }

                reader.Close();
                return items;
            }
        }
    }
}
