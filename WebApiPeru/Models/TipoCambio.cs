using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Peru.Sunat.WebApi.Models
{
    public class TipoCambio
    {
        public string Fecha { get; set; }
        public double Compra { get; set; }
        public double Venta { get; set; }
    }
}