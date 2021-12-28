﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace CM.WeeklyTeamReport.Domain
{
    public interface IReportsFromTo<TEntity> : IRepository<ReportsFromTo>
    {
        public List<string[]> ReadReportTo(int idMemberTo);
        public List<string[]> ReadReportFrom(int idMemberFrom);
        public void DeleteFromTo(int reportTo, int reportFrom);
    }
    [ExcludeFromCodeCoverage]
    public class ReportsFromToRepository : IReportsFromTo<ReportsFromTo>
    {
        private readonly IConfiguration _configuration;
        public ReportsFromToRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        SqlConnection GetSqlConnection()
        {
            var connectionString = _configuration.GetConnectionString("Sql");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public ReportsFromTo Create(ReportsFromTo reportFromTo)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO ReportFromTo (TeamMemberFrom,  TeamMemberTo) " +
                                                           "VALUES (@TeamMemberFrom, @TeamMemberTo);" +
                                                           "SELECT * FROM ReportFromTo WHERE TeamMemberFrom = @TeamMemberFrom " +
                                                           "AND TeamMemberTo = @TeamMemberTo", connection);
                SqlParameter TeamMemberFrom = new("@TeamMemberFrom", SqlDbType.Int)
                {
                    Value = reportFromTo.TeamMemberFrom
                };
                SqlParameter TeamMemberTo = new("@TeamMemberTo", SqlDbType.Int)
                {
                    Value = reportFromTo.TeamMemberTo
                };

                command.Parameters.Add(TeamMemberFrom);
                command.Parameters.Add(TeamMemberTo);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapReportFromTo(reader);
                }
            }
            return null;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public List<string[]> ReadReportTo(int idMemberTo)
        {
            using (var connection = GetSqlConnection())
            {
                List<string[]> membersWhichReportTo = new();
                var command = new SqlCommand("SELECT Rep.TeamMemberFrom, TM.FirstName, TM.LastName " +
                    "FROM ReportFromTo Rep JOIN TeamMembers TM " +
                    "on Rep.TeamMemberFrom = TM.TeamMemberId " +
                    "WHERE TeamMemberTo = @TeamMemberTo", connection);
                SqlParameter TeamMemberTo = new("@TeamMemberTo", SqlDbType.Int)
                {
                    Value = idMemberTo
                };

                command.Parameters.Add(TeamMemberTo);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var memberId = reader["TeamMemberFrom"].ToString();
                    var firstName = reader["FirstName"].ToString();
                    var lastName = reader["LastName"].ToString();
                    membersWhichReportTo.Add(new string[] { memberId, firstName, lastName });
                }
                return membersWhichReportTo;
            }
        }
        public List<string[]> ReadReportFrom(int idMemberFrom)
        {
            using (var connection = GetSqlConnection())
            {
                List<string[]> membersWhichReportTo = new();
                var command = new SqlCommand("SELECT Rep.TeamMemberTo, TM.FirstName, TM.LastName " +
                    "FROM ReportFromTo Rep JOIN TeamMembers TM " +
                    "on Rep.TeamMemberTo = TM.TeamMemberId " +
                    "WHERE TeamMemberFrom = @TeamMemberFrom", connection);
                SqlParameter TeamMemberFrom = new("@TeamMemberFrom", SqlDbType.Int)
                {
                    Value = idMemberFrom
                };

                command.Parameters.Add(TeamMemberFrom);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var memberId = reader["TeamMemberTo"].ToString();
                    var firstName = reader["FirstName"].ToString();
                    var lastName = reader["LastName"].ToString();
                    membersWhichReportTo.Add(new string[] { memberId,firstName,lastName});
                }
                return membersWhichReportTo;
            }
        }

        public void DeleteFromTo(int reportTo, int reportFrom)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("DELETE FROM ReportFromTo WHERE TeamMemberFrom = @TeamMemberFrom " +
                                             "AND TeamMemberTo = @TeamMemberTo", connection);
                SqlParameter TeamMemberFrom = new("@TeamMemberFrom", SqlDbType.Int)
                {
                    Value = reportFrom
                };
                SqlParameter TeamMemberTo = new("@TeamMemberTo", SqlDbType.Int)
                {
                    Value = reportTo
                };

                command.Parameters.Add(TeamMemberFrom);
                command.Parameters.Add(TeamMemberTo);
                command.ExecuteNonQuery();
            }
        }

        public List<ReportsFromTo> ReadAll()
        {
            throw new NotImplementedException();
        }

        public ReportsFromTo Update(ReportsFromTo entity)
        {
            throw new NotImplementedException();
        }
        private static ReportsFromTo MapReportFromTo(SqlDataReader reader)
        {
            return new ReportsFromTo()
            {
                TeamMemberFrom = (int)reader["TeamMemberFrom"],
                TeamMemberTo = (int)reader["TeamMemberTo"]
            };
        }

        public ReportsFromTo Read(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
