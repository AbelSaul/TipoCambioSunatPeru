using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;

namespace Peru.Sunat.Library
{
    public class TipoCambio
    {
        private const string URL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes={0}&anho={1}";

        private string dia = "01";
        private string mes = "01";
        private string anho = "1995";
        private string html = string.Empty;

        private double compra = 0.0;
        private double venta = 0.0;

        #region "Propiedades"

        public string Dia
        {
            get { return dia; }
            set { dia = value; }
        }

        public string Mes
        {
            get { return mes; }
            set { mes = value; }
        }

        public string Anho
        {
            get { return anho; }
            set { anho = value; }
        }

        public double Compra
        {
            get { return compra; }
            set { compra = value; }
        }

        public double Venta
        {
            get { return venta; }
            set { venta = value; }
        }

        #endregion

        #region "Metodos Privados"

        private DataTable obtenerDatos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dia", typeof(String));
            dt.Columns.Add("Compra", typeof(String));
            dt.Columns.Add("Venta", typeof(String));

            string urlcomplete = string.Format(URL, this.mes, this.anho);
            this.html = new WebClient().DownloadString(urlcomplete);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(this.html);

            HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
            if (NodesTr != null)
            {

                int iNumFila = 0;
                foreach (HtmlNode Node in NodesTr)
                {
                    if (iNumFila > 0)
                    {
                        int iNumColumna = 0;
                        DataRow dr = dt.NewRow();
                        foreach (HtmlNode subNode in Node.Elements("td"))
                        {

                            if (iNumColumna == 0) dr = dt.NewRow();

                            string sValue = subNode.InnerHtml.ToString().Trim();
                            sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
                            dr[iNumColumna] = sValue.Trim();

                            iNumColumna++;

                            if (iNumColumna == 3)
                            {
                                dt.Rows.Add(dr);
                                iNumColumna = 0;
                            }
                        }
                    }
                    iNumFila++;
                }

                dt.AcceptChanges();
            }

            return dt;
        }

        #endregion

        #region "Metodos Publicos"

        public bool ObtenerPorFecha(int day, int month, int year)
        {
            this.dia = day.ToString("00");
            this.mes = month.ToString("00");
            this.anho = year.ToString("0000");

            return this.ObtenerPorFecha();
        }

        public bool ObtenerPorFecha()
        {
            try
            {
                bool respuesta = false;

                string diaNumero = int.Parse(this.Dia).ToString();
                DataTable dt = obtenerDatos();

                string sCompra = (from DataRow dr in dt.AsEnumerable()
                                  where Convert.ToString(dr["Dia"]) == diaNumero
                                  select Convert.ToString(dr["Compra"])).FirstOrDefault();
                string sVenta = (from DataRow dr in dt.AsEnumerable()
                                 where Convert.ToString(dr["Dia"]) == diaNumero
                                 select Convert.ToString(dr["Venta"])).FirstOrDefault();

                if (sCompra.Trim().Length > 0)
                {
                    this.compra = double.Parse(sCompra);
                    respuesta = true;
                }


                if (sVenta.Trim().Length > 0)
                {
                    this.venta = double.Parse(sVenta);
                    respuesta = true;
                }

                return respuesta;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<TipoCambio> ObtenerPorMes(int month, int year)
        {
            this.mes = month.ToString("00");
            this.anho = year.ToString("0000");

            return this.ObtenerPorMes();
        }

        public List<TipoCambio> ObtenerPorMes()
        {
            try
            {

                List<TipoCambio> lstTc = new List<TipoCambio>();
                
                DataTable dt = obtenerDatos();
                foreach (DataRow dr in dt.Rows)
                {
                    string diaNumero = int.Parse(dr["Dia"].ToString()).ToString("00");
                    TipoCambio objTc = new TipoCambio()
                    {
                        Dia = diaNumero,
                        Mes = this.Mes,
                        Anho = this.Anho,
                        Compra = double.Parse(dr["Compra"].ToString()),
                        Venta = double.Parse(dr["Venta"].ToString())
                    };
                    lstTc.Add(objTc);
                }

                return lstTc;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}
