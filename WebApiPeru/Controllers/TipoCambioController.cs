using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Peru.Sunat.WebApi.Models;
using Peru.Sunat;

namespace Peru.Sunat.WebApi.Controllers
{

    public class TipoCambioController : ApiController
    {

        [HttpGet]
        public IEnumerable<TipoCambio> ObtenerPorMes(int anho, int mes)
        {
            List<Library.TipoCambio> lstTC =  new Library.TipoCambio().ObtenerPorMes(mes, anho);

            List<Models.TipoCambio> lstTcModel = new List<Models.TipoCambio>();
            foreach(Library.TipoCambio objTC in lstTC)
            {
                lstTcModel.Add(new Models.TipoCambio()
                {
                    Fecha = objTC.Dia + "/" + objTC.Mes + "/" + objTC.Anho,
                    Compra = objTC.Compra,
                    Venta = objTC.Venta
                });
            }

            return lstTcModel;
        }

        [HttpGet]
        public IHttpActionResult ObtenerPorFecha(int anho, int mes, int dia)
        {

            Models.TipoCambio objTC = null;

            Library.TipoCambio objLibTC = new Library.TipoCambio();
            if (objLibTC.ObtenerPorFecha(dia, mes, anho) == true)
            {
                objTC = new Models.TipoCambio()
                {
                    Fecha = objLibTC.Dia + "/" + objLibTC.Mes + "/" + objLibTC.Anho,
                    Compra = objLibTC.Compra,
                    Venta = objLibTC.Venta
                };
            }
            else
            {
                return NotFound();
            }

            return Ok(objTC);
        }

    }
}
