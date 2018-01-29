using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DiakontTestProject.Model;

namespace DiakontTestProject.Data
{
    public class DefaultRepository:IRepository
    {
        private readonly IReferenceRepository _referenceRepository;
        private readonly string _connectionString;

        public DefaultRepository(IReferenceRepository referenceRepository)
        {
            _referenceRepository = referenceRepository;
            _connectionString = ConfigurationManager.ConnectionStrings["Diakont"].ConnectionString;
        }

        public IEnumerable<Rate> GetRates()
        {
            var positions = _referenceRepository.GetPositions();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("select Id, PositionId, ApplyFromDate, Amount from dbo.Rate", connection);
                connection.Open();
                IDataReader reader = cmd.ExecuteReader();
                List<Rate> items = new List<Rate>();

                while (reader.Read())
                {
                    int positionId = (int)reader["PositionId"];
                    items.Add(new Rate(positions.FirstOrDefault(x => x.Id == positionId), Convert.ToInt32(reader["Amount"]), (DateTime) reader["ApplyFromDate"]));
                }

                reader.Close();
                return items;
            }
        }

        public IEnumerable<SchedulePosition> GetSchedule()
        {
            var positions = _referenceRepository.GetPositions();
            var departments = _referenceRepository.GetDepartments();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("select Id, PositionId, DepartmentId, ApplyFromDate, EmployerCount from dbo.Schedule", connection);
                connection.Open();
                IDataReader reader = cmd.ExecuteReader();
                List<SchedulePosition> items = new List<SchedulePosition>();

                while (reader.Read())
                {
                    int positionId = (int)reader["PositionId"];
                    int departmentId = (int) reader["DepartmentId"];

                    items.Add(new SchedulePosition(departments.FirstOrDefault(x => x.Id == departmentId), positions.FirstOrDefault(x => x.Id == positionId), (DateTime) reader["ApplyFromDate"], (int) reader["EmployerCount"]));
                }

                reader.Close();
                return items;
            }
        }

        public IEnumerable<SearchResult> FindFundOfSalary(DateTime from, DateTime to)
        {
            string sql = @"select d.Name, r.ApplyFromDate, s.ApplyFromDate, r.Amount*s.EmployerCount from dbo.Rate r
                           join dbo.Schedule s on r.PositionId = s.PositionId 
                           join dbo.Departments d on d.Id = s.DepartmentId
                           where r.ApplyFromDate >= @from and r.ApplyFromDate <= @to";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.Add("@from", SqlDbType.Date).Value =from;
                cmd.Parameters.Add("@to", SqlDbType.Date).Value = to;
                connection.Open();
                IDataReader reader = cmd.ExecuteReader();
                List<SearchResult> items = new List<SearchResult>();

                while (reader.Read())
                {
                    items.Add(new SearchResult{Department = (string)reader["Name"], From = (DateTime)reader[1], To = (DateTime)reader[2], TotalFundOfSalary = Convert.ToInt32(reader[3])});
                }

                reader.Close();
                return items;
            }
        }

        public void AddRate(Rate rate)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Rate (PositionId, ApplyFromDate, Amount) values (@PositionId, @ApplyFromDate, @Amount)", connection);
                cmd.Parameters.Add("@PositionId", SqlDbType.Int).Value = rate.Position.Id;
                cmd.Parameters.Add("@ApplyFromDate", SqlDbType.Date).Value = rate.ApplyFromDate;
                cmd.Parameters.Add("@Amount", SqlDbType.Money).Value = rate.Amount;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSchedulePosition(SchedulePosition schedulePosition)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("insert into Schedule (PositionId, DepartmentId, ApplyFromDate, EmployerCount) values (@PositionId, @DepartmentId, @ApplyFromDate, @EmployerCount)", connection);
                cmd.Parameters.Add("@PositionId", SqlDbType.Int).Value = schedulePosition.Position.Id;
                cmd.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = schedulePosition.Department.Id;
                cmd.Parameters.Add("@ApplyFromDate", SqlDbType.Date).Value = schedulePosition.ApplyFromDate;
                cmd.Parameters.Add("@EmployerCount", SqlDbType.Int).Value = schedulePosition.EmployersCount;

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
