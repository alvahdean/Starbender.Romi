﻿namespace Starbender.Romi.Web.Service.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.IO;
    using System.Threading;

    using AutoMapper;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Microsoft.Extensions.Logging;

    using Starbender.Romi.Contracts;
    using Starbender.Romi.Data;
    using Starbender.Romi.Data.Models;
    using Starbender.Romi.Services.Device;
    using Starbender.Romi.Services.Configuration;
    using Starbender.Romi.Web.Service.Models;

    using RomiSettings = Starbender.Romi.Data.Models.RomiSettings;


    [ApiController]
    [Produces("application/json")]
    [Route("api/gpio")]
    public class GpioController : ControllerBase
    {
        private readonly IConfigurationService _config;

        private readonly IDeviceService _service;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public GpioController(IConfigurationService config, IDeviceService service, ILogger<SensorController> logger, IMapper mapper)
        {
            this._config = config;
            this._service = service;
            this._logger = logger;
            this._mapper = mapper;
        }
        [Route("register/{typeName}")]
        [HttpPost,HttpGet]
        public async Task<SensorInfo> RegisterSensor(string typeName)
        {
            var sensor = await this._service.Register(typeName);

            return sensor;
        }

        //[Route("register/{typeName}")]
        //[HttpPost]
        //public async Task<SensorInfo> RegisterSensorDriver(string typeName, [FromBody] IDeviceDriverFile driver)
        //{
        //    var sensor = await this._service.Register(typeName);

        //    // full path to file in temp location
        //    var filePath = Path.GetTempFileName();
        //    if (driver!=null && driver.Length > 0)
        //    {
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await driver.CopyToAsync(stream,CancellationToken.None);
        //        }
        //    }

        //    return sensor;
        //}

        [HttpGet]
        public async Task<IEnumerable<SensorInfo>> GetSensors(CancellationToken cancelToken)
        {
            return await this._service.GetSensors();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteSensor(int id)
        {
            var sensor= await this._service.GetSensor(id);
            if (sensor != null)
            {
                this._service.DeleteSensor(sensor);
            }
        }

        [HttpPut]
        public void UpdateSensor([FromBody] SensorInfo sensor)
        {
            this._service.UpdateSensor(sensor);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetSupportedTypes(int id, CancellationToken cancelToken)
        {
            var sensor = await this._service.GetSensor(id);
            var result = sensor.SupportedTypes;
            return result;
        }

        [Route("{id}/{sensorType}")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetSupportedNames(int id, string sensorType, CancellationToken cancelToken)
        {
            var sensor = await _service.GetSensor(id);
            var result = sensor.SupportedNames(sensorType);
            return result;
        }

        [Route("{id}/{sensorType}/{sensorName}")]
        [HttpGet]
        public async Task<SensorResult> Read(int id, string sensorType, string sensorName, CancellationToken cancelToken)
        {
            var sensor = await this._service.GetSensor(id);
            var result = await sensor.ReadAsync(sensorType, sensorName);
            return result;
        }
    }
}