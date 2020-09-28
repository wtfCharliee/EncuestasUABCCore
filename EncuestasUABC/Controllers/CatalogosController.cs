﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EncuestasUABC.Controllers
{
    public class CatalogosController : Controller
    {
        private readonly ILogger<CatalogosController> _logger;
        private readonly IRepository _repository;

        public CatalogosController(ILogger<CatalogosController> logger,
            IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CarrerasSelect()
        {
            #region CarrerasSelect
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return View();


            }
            #endregion
        }
    }
}