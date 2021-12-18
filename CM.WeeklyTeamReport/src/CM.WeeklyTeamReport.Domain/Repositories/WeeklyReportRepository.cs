﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace CM.WeeklyTeamReport.Domain
{
    [ExcludeFromCodeCoverage]
    public class WeeklyReportRepository : IRepository<WeeklyReport>
    {
        private readonly IConfiguration _configuration;
        public WeeklyReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WeeklyReportRepository()
        {

        }

        SqlConnection GetSqlConnection()
        {
            var connectionString = _configuration.GetConnectionString("AntonM");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public WeeklyReport Create(WeeklyReport weeklyReport)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("INSERT INTO WeeklyReports (StartDate,  EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, " +
                                             "WeekHighComment, WeekLowComment, AnythingElseComment, TeamMemberId)" +
                                             "VALUES (@StartDate,  @EndDate, @MoraleValueId, @StressValueId, @WorkloadValueId, @MoraleComment, @StressComment, @WorkloadComment, @WeekHighComment," +
                                             "@WeekLowComment, @AnythingElseComment, @TeamMemberId);" +
                                             "SELECT * FROM WeeklyReports WHERE WeeklyReportId = SCOPE_IDENTITY()", connection);
                SqlParameter StartDate = new("@StartDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateFrom
                };
                SqlParameter EndDate = new("@EndDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateTo
                };
                SqlParameter MoraleValueId = new("@MoraleValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValueId
                };
                SqlParameter StressValueId = new("@StressValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValueId
                };
                SqlParameter WorkloadValueId = new("@WorkloadValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValueId
                };
                SqlParameter MoraleComment = new("@MoraleComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new("@StressComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new("@WorkloadComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new("@WeekHighComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new("@WeekLowComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new("@AnythingElseComment", SqlDbType.NVarChar, 400)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = weeklyReport.TeamMemberId
                };

                command.Parameters.AddRange(new object[] { StartDate, EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, WeekHighComment,
                                                           WeekLowComment, AnythingElseComment, TeamMemberId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }

        public void Delete(int weeklyReportId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("DELETE FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
                {
                    Value = weeklyReportId
                };

                command.Parameters.Add(WeeklyReportId);
                command.ExecuteNonQuery();
            }
        }

        public WeeklyReport Read(int weeklyReportId)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
                {
                    Value = weeklyReportId
                };

                command.Parameters.Add(WeeklyReportId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }

        public WeeklyReport Update(WeeklyReport weeklyReport)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("UPDATE WeeklyReports " +
                                             "SET StartDate = @StartDate, EndDate = @EndDate, MoraleValueId = @MoraleValueId, StressValueId = @StressValueId, WorkloadValueId = @WorkloadValueId," +
                                             "MoraleComment = @MoraleComment, StressComment = @StressComment, WorkloadComment = @WorkloadComment, WeekHighComment = @WeekHighComment, WeekLowComment = @WeekLowComment, AnythingElseComment = @AnythingElseComment " +
                                             "WHERE WeeklyReportId = @WeeklyReportId;" +
                                             "SELECT * FROM WeeklyReports WHERE WeeklyReportId = @WeeklyReportId", connection);
                SqlParameter StartDate = new("@StartDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateFrom
                };
                SqlParameter EndDate = new("@EndDate", SqlDbType.Date)
                {
                    Value = weeklyReport.DateTo
                };
                SqlParameter MoraleValueId = new("@MoraleValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.MoraleValueId
                };
                SqlParameter StressValueId = new("@StressValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.StressValueId
                };
                SqlParameter WorkloadValueId = new("@WorkloadValueId", SqlDbType.Int)
                {
                    Value = (int)weeklyReport.WorkloadValueId
                };
                SqlParameter MoraleComment = new("@MoraleComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.MoraleComment
                };
                SqlParameter StressComment = new("@StressComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.StressComment
                };
                SqlParameter WorkloadComment = new("@WorkloadComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WorkloadComment
                };
                SqlParameter WeekHighComment = new("@WeekHighComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekHighComment
                };
                SqlParameter WeekLowComment = new("@WeekLowComment", SqlDbType.NVarChar, 600)
                {
                    Value = weeklyReport.WeekLowComment
                };
                SqlParameter AnythingElseComment = new("@AnythingElseComment", SqlDbType.NVarChar, 400)
                {
                    Value = weeklyReport.AnythingElseComment
                };
                SqlParameter WeeklyReportId = new("@WeeklyReportId", SqlDbType.Int)
                {
                    Value = weeklyReport.WeeklyReportId
                };

                command.Parameters.AddRange(new object[] { StartDate, EndDate, MoraleValueId, StressValueId, WorkloadValueId, MoraleComment, StressComment, WorkloadComment, WeekHighComment,
                                                           WeekLowComment, AnythingElseComment, WeeklyReportId });
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return MapWeeklyReport(reader);
                }
            }
            return null;
        }


        private static WeeklyReport MapWeeklyReport(SqlDataReader reader)
        {
            return new WeeklyReport()
            {
                DateFrom = DateTime.Parse(reader["DateFrom"].ToString()).ToString("yyyy-MM-dd"),
                DateTo = DateTime.Parse(reader["DateTo"].ToString()).ToString("yyyy-MM-dd"),
                MoraleValueId = (Morales)(int)reader["MoraleValueId"],
                StressValueId = (Morales)(int)reader["StressValueId"],
                WorkloadValueId = (Morales)(int)reader["WorkloadValueId"],
                MoraleComment = reader["MoraleComment"].ToString(),
                StressComment = reader["StressComment"].ToString(),
                WorkloadComment = reader["WorkloadComment"].ToString(),
                WeekHighComment = reader["WeekHighComment"].ToString(),
                WeekLowComment = reader["WeekLowComment"].ToString(),
                AnythingElseComment = reader["AnythingElseComment"].ToString(),
                TeamMemberId = (int)reader["TeamMemberId"],
                WeeklyReportId = (int)reader["WeeklyReportId"]
            };
        }

        public List<WeeklyReport> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<WeeklyReport> ReadAllByIdAndReportTo(int teamMemberId)
        {
            List<WeeklyReport> weeklyReports = new();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE TeamMemberId=@TeamMemberId", connection);

                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMemberId
                };

                command.Parameters.Add(TeamMemberId);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weeklyReport = MapWeeklyReport(reader);
                    weeklyReports.Add(weeklyReport);
                }
                return weeklyReports;
            }
        }

        public List<WeeklyReport> ReadAllById(int teamMemberId)
        {
            List<WeeklyReport> weeklyReports = new();
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM WeeklyReports WHERE TeamMemberId=@TeamMemberId", connection);

                SqlParameter TeamMemberId = new("@TeamMemberId", SqlDbType.Int)
                {
                    Value = teamMemberId
                };

                command.Parameters.Add(TeamMemberId);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var weeklyReport = MapWeeklyReport(reader);
                    weeklyReports.Add(weeklyReport);
                }
                return weeklyReports;
            }
        }
    }
}