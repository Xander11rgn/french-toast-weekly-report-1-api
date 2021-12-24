﻿using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [ApiController]
    [Route("api/report-from-to")]
    public class ReportsFromToController : ControllerBase
    {
        private readonly IRepository<ReportsFromTo> _repository;
        private readonly IConfiguration _configuration;
        [ActivatorUtilitiesConstructor]
        public ReportsFromToController(IRepository<ReportsFromTo> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        public ReportsFromToController(IRepository<ReportsFromTo> repository)
        {
            _repository = repository;
        }
        public ReportsFromToController()
        {

        }

        [HttpGet]
        [Route("/api/report-to/{idMemberReportTo}")]
        public ActionResult<List<string[]>> ReadReportTo([FromRoute] string idMemberReportTo)
        {
            if (!Regex.IsMatch(idMemberReportTo, @"^\d+$"))
            {
                return new BadRequestObjectResult("idMemberReportTo should be positive integer.");
            }
            ReportsFromToRepository reportsFromToRepository = new(_configuration);
            var result = reportsFromToRepository.ReadReportTo(Convert.ToInt32(idMemberReportTo));
            if (result == null)
            {
                return new NotFoundObjectResult($"No one sends a report to Member {idMemberReportTo}");
            }
            return new OkObjectResult(result);
        }
        [HttpGet]
        [Route("/api/report-from/{idMemberReportFrom}")]
        public ActionResult<List<string[]>> ReadReportFrom([FromRoute] string idMemberReportFrom)
        {
            if (!Regex.IsMatch(idMemberReportFrom, @"^\d+$"))
            {
                return new BadRequestObjectResult("idMemberReportTo should be positive integer.");
            }
            ReportsFromToRepository reportsFromToRepository = new(_configuration);
            var result = reportsFromToRepository.ReadReportFrom(Convert.ToInt32(idMemberReportFrom));
            if (result == null)
            {
                return new NotFoundObjectResult($"No one sends a report to Member {idMemberReportFrom}");
            }
            return new OkObjectResult(result);
        }

        [HttpDelete]
        [Route("{idMemberReportTo}/{idMemberReportFrom}")]
        public ActionResult Delete([FromRoute] string idMemberReportTo, [FromRoute] string idMemberReportFrom)
        {
            if (!Regex.IsMatch(idMemberReportTo, @"^\d+$"))
            {
                return new BadRequestObjectResult("idMemberTo should be positive integer.");
            }
            if (!Regex.IsMatch(idMemberReportFrom, @"^\d+$"))
            {
                return new BadRequestObjectResult("idMemberFrom should be positive integer.");
            }
            ReportsFromToRepository reportsFromToRepository = new(_configuration);
            reportsFromToRepository.DeleteFromTo(Convert.ToInt32(idMemberReportTo), Convert.ToInt32(idMemberReportFrom));
            return new OkObjectResult($"Member {idMemberReportFrom} don't send reports to member {idMemberReportTo} anymore.");
        }

        [HttpPost]
        public ActionResult<ReportsFromTo> Create([FromBody] ReportsFromTo reportFromTo)
        {
            if (reportFromTo == null)
            {
                return new BadRequestObjectResult("Company should not be null.");
            }
            var result = _repository.Create(reportFromTo);
            return new OkObjectResult("got it");
        }
    }
}
