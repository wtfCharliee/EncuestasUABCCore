﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestasUABC.AccesoDatos.Repository.Interfaces;
using EncuestasUABC.Models.Catalogos;
using EncuestasUABC.Models;
using EncuestasUABC.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EncuestasUABC.Models.Paginacion;

namespace EncuestasUABC.Controllers
{
    public class EncuestasController : BaseController
    {
        private readonly ILogger<EncuestasController> _logger;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository _repository;

        public EncuestasController(ILogger<EncuestasController> logger,
            IEncuestasRepository encuestasRepository,
            IUsuarioRepository usuarioRepository,
            IRepository repository)
        {
            _encuestasRepository = encuestasRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region CREADAS

        #region INDEX
        public async Task<IActionResult> Creadas()
        {
            #region Creadas

            return View();

            #endregion
        }

        #endregion

        #region EDITAR
        public async Task<IActionResult> Editar(int id)
        {
            #region Edit
            try
            {
                var encuesta = _repository.GetById<Encuesta>(id);
                return View(encuesta);
            }
            catch (MessageAlertException ex)
            {
                _logger.LogInformation(ex.Message);
                GenerarAlerta(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            return RedirectToAction(nameof(Index));

            #endregion
        }
        #endregion

        #region CREAR
        [HttpPost]
        public async Task<IActionResult> Crear(Encuesta model)
        {
            #region Crear
            try
            {
                var user = await _usuarioRepository.Get(User.Identity.Name);
                model.UsuarioId = user.Id;
                model.Fecha = DateTime.Now;
                model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.INACTIVA;
                var result = await _repository.Add<Encuesta>(model);
                ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ01);
                return RedirectToAction(nameof(Editar), new { id = result.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ShowMessageException(ex.Message);
            }
            return View(nameof(Creadas));

            #endregion
        }
        #endregion

        #region AJAX
        /// <summary>
        /// Se obtienen los registro de las Encuestas Creadas de la base de datos
        /// </summary>
        /// <param name="paginacion">Contiene los valores para la paginación</param>
        /// <returns>Retorna una estructura Json que permite a la herramienta Datatables.js crear la paginación en el HTML.</returns>
        [HttpPost]
        public async Task<IActionResult> CreadasPaginado([FromBody] Paginacion paginacion)
        {
            #region CreadasPaginado
            try
            {
                var user = await _usuarioRepository.Get(User.Identity.Name);

                //Aqui se traen los datos de tipo PaginacionResult
                var listaEncuestas = await _repository.FindBy<Encuesta>(x => x.UsuarioId == user.Id, x => x.OrderByDescending(x => x.Fecha));
                
                int totalRegistros = listaEncuestas.Count();
                int column = paginacion.Order[0].Column;
                var order = paginacion.Order[0].Dir;


                switch (column)
                {
                    case 1:
                        if (order.Equals("asc"))
                            listaEncuestas = listaEncuestas.OrderBy(x => x.Fecha);
                        else
                            listaEncuestas = listaEncuestas.OrderBy(x => x.Fecha);
                        break;
                    default:
                        break;
                }
                listaEncuestas = listaEncuestas
                                       .Skip(paginacion.Start)
                                       .Take(paginacion.Length)
                                       .ToList();


                var registrosFiltrados = listaEncuestas.Count();
                var totalRegistrosFiltrados = totalRegistros;
                var datosPaginados = listaEncuestas;
                return Json(new { draw = paginacion.Draw, recordsFiltered = registrosFiltrados, recordsTotal = totalRegistrosFiltrados, data = datosPaginados });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            #endregion
        }
        //[HttpPost]
        //public async Task<IActionResult> EditarNombreDescripcion(Encuesta model, bool encuestaEstatus = false)
        //{
        //    #region EditarNombreDescripcion
        //    try
        //    {
        //        if (encuestaEstatus)
        //            model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.ACTIVA;
        //        else
        //            model.EstatusEncuestaId = (int)Enumerador.EstatusEncuesta.INACTIVA;

        //        var encuesta = await _encuestasRepository.Get(model.Id);
        //        encuesta.Nombre = model.Nombre;
        //        encuesta.Descripcion = model.Descripcion;
        //        encuesta.EstatusEncuestaId = model.EstatusEncuestaId;
        //        await _encuestasRepository.Update(encuesta);
        //        ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ03);
        //    }
        //    catch (MessageAlertException ex)
        //    {
        //        _logger.LogInformation(ex.Message);
        //        GenerarAlerta(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        ShowMessageException(ex.Message);
        //    }
        //    return RedirectToAction(nameof(Editar), new { id = model.Id });
        //    #endregion
        //}

        //[HttpPost]
        //public async Task<IActionResult> CrearSeccion(EncuestaSeccion model)
        //{
        //    #region Crear
        //    try
        //    {
        //        var user = await _usuarioRepository.Get(User.Identity.Name);
        //        var encuesta = await _encuestasRepository.Get(model.EncuestaId);
        //        int orden = 1;
        //        if (encuesta.EncuestaSecciones.Any())
        //            orden = encuesta.EncuestaSecciones.OrderBy(x => x.Orden).Last().Orden + 1;
        //        model.Orden = orden;
        //        encuesta.EncuestaSecciones.Add(model);
        //        await _encuestasRepository.Update(encuesta);
        //        ShowMessageSuccess(Constantes.Mensajes.ENCUESTAS_MSJ05);
        //        return RedirectToAction(nameof(Editar), new { id = model.EncuestaId });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        ShowMessageException(ex.Message);
        //    }
        //    return RedirectToAction(nameof(Creadas));

        //    #endregion
        //}

        #endregion

        #endregion


        #region VIEWBAGS
        public async Task Campus()
        {
            #region Campus

            ViewBag.Campus = new SelectList(await _repository.GetAll<Campus>(), "Id", "Nombre");

            #endregion
        }

        #endregion

     
    }
}