using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Peru.Sunat.Library;

namespace Peru.Sunat.Library.Test
{
    [TestClass]
    public class TipoCambio_Test01
    {
        [TestMethod]
        public void ObtenerPorFecha_Test01()
        {
            double expectedResultCompra = 2.160;

            double actualResultCompra = 0.0;

            TipoCambio objTc = new TipoCambio();
            if (objTc.ObtenerPorFecha(2, 1, 1995) == true)
            {
                actualResultCompra = objTc.Compra;
            }

            Assert.AreEqual<double>(expectedResultCompra, actualResultCompra);
        }

        [TestMethod]
        public void ObtenerPorFecha_Test02()
        {
            double expectedResultVenta = 2.180;

            double actualResultVenta = 0.0;

            TipoCambio objTc = new TipoCambio();
            if (objTc.ObtenerPorFecha(2, 1, 1995) == true)
            {
                actualResultVenta = objTc.Venta;
            }

            Assert.AreEqual<double>(expectedResultVenta, actualResultVenta);
        }

        [TestMethod]
        public void ObtenerPorMes_Test01()
        {
            TipoCambio expectedResultVenta = new TipoCambio()
            {
                Dia = "02",
                Mes = "01",
                Anho = "1995",
                Compra = 2.160,
                Venta = 2.180,
            };

            List<TipoCambio> lstTc = new TipoCambio().ObtenerPorMes(1, 1995);

            TipoCambio actualResultVenta = (from tc in lstTc
                                            where tc.Dia == "02" && tc.Mes == "01" && tc.Anho == "1995" 
                                            select tc).First<TipoCambio>();

            Assert.AreEqual<double>(expectedResultVenta.Compra, actualResultVenta.Compra);
        }

        [TestMethod]
        public void ObtenerPorMes_Test02()
        {
            TipoCambio expectedResultVenta = new TipoCambio()
            {
                Dia = "02",
                Mes = "01",
                Anho = "1995",
                Compra = 2.160,
                Venta = 2.180,
            };

            List<TipoCambio> lstTc = new TipoCambio().ObtenerPorMes(1, 1995);

            TipoCambio actualResultVenta = (from tc in lstTc
                                            where tc.Dia == "02" && tc.Mes == "01" && tc.Anho == "1995"
                                            select tc).First<TipoCambio>();

            Assert.AreEqual<double>(expectedResultVenta.Venta, actualResultVenta.Venta);
        }

    }
}
