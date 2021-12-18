﻿using System;
using Xunit;

namespace CM.WeeklyTeamReport.Domain.Tests
{
    public class WeeklyReportTests
    {
        [Fact]
        public void ShouldBeAbleToCreateWeeklyReportObject()
        {
            WeeklyReport weeklyReport = new()
            {
                DateFrom = DateTime.Parse("2021-12-12"),
                DateTo = DateTime.Parse("2021-12-12"),
                MoraleValueId = Morales.Okay,
                StressValueId = Morales.Low,
                WorkloadValueId = Morales.Great,
                MoraleComment = "wadad",
                StressComment = "wadad",
                WorkloadComment = "wadad",
                WeekHighComment = "wadad",
                WeekLowComment = "wadad",
                AnythingElseComment = "wadad",
                WeeklyReportId = 1,
                TeamMemberId = 2
            };
            Assert.NotNull(weeklyReport);
            Assert.Equal(DateTime.Parse("2021-12-12"), weeklyReport.DateFrom);
            Assert.Equal(DateTime.Parse("2021-12-12"), weeklyReport.DateTo);
            Assert.Equal(Morales.Okay, weeklyReport.MoraleValueId);
            Assert.Equal(Morales.Low, weeklyReport.StressValueId);
            Assert.Equal(Morales.Great, weeklyReport.WorkloadValueId);
            Assert.Equal("wadad", weeklyReport.MoraleComment);
            Assert.Equal("wadad", weeklyReport.StressComment);
            Assert.Equal("wadad", weeklyReport.WorkloadComment);
            Assert.Equal("wadad", weeklyReport.WeekHighComment);
            Assert.Equal("wadad", weeklyReport.WeekLowComment);
            Assert.Equal("wadad", weeklyReport.AnythingElseComment);
            Assert.Equal(1, weeklyReport.WeeklyReportId);
            Assert.Equal(2, weeklyReport.TeamMemberId);
        }
    }
}
