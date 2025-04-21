using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using Pizzeria.Models.Boleta_cab;
using Pizzeria.Models.Boleta_det;
using Pizzeria.Models.Customer;
using Pizzeria.Models.Pizza;

namespace Pizzeria.Models.Boleta
{
    public class BoletaDAO
    {
        MySqlConnection con = new MySqlConnection();
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        public string server = "localhost";
        public string puerto = "3306";
        public string database = "bdpizzeria";
        public string uid = "root";
        public string password = "usbw";
        void ConnectionString()
        {
            string constring;
            constring = "SERVER=" + server + ";" + "PORT=" + puerto + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            con = new MySqlConnection(constring);
        }
        public List<Boleta> MostrarBoletas()
        {
            List<Boleta> lista = new List<Boleta>();
            ConnectionString();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText =
        "SELECT " +
        "bc.NumBol AS numero_boleta, " +
        "bc.Fecha AS fecha, " +
        "c.dni AS dni_cliente, " +
        "c.name AS nombre_cliente, " +
        "c.last_name AS apellido_cliente, " +
        "p.pizza_id AS cod_producto, " +
        "p.name AS nombre_producto, " +
        "bd.Cant AS cantidad_producto, " +
        "bd.Punt AS precio_producto, " +
        "bd.Importe AS sub_total, " +
        "bc.Igv, " +
        "bc.Total " +
        "FROM boleta_cab bc " +
        "INNER JOIN boleta_det bd ON bc.NumBol = bd.NumBol " +
        "INNER JOIN customer c ON bc.DNIcli = c.dni " +
        "INNER JOIN pizza p ON bd.CodProd = p.pizza_id " +
        "WHERE bc.Estado = 0";

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Boleta boleta = new Boleta();
                boleta.numero_boleta = dr["numero_boleta"].ToString();
                boleta.fecha = dr["fecha"].ToString();
                boleta.dni_cliente = dr["dni_cliente"].ToString();
                boleta.nombre_cliente = dr["nombre_cliente"].ToString();
                boleta.apellido_cliente = dr["apellido_cliente"].ToString();
                boleta.cod_producto = Convert.ToInt32(dr["cod_producto"]);
                boleta.nombre_producto = dr["nombre_producto"].ToString();
                boleta.cantidad_producto = Convert.ToInt32(dr["cantidad_producto"]);
                boleta.precio_producto = Convert.ToDouble(dr["precio_producto"]);
                boleta.sub_total = Convert.ToDouble(dr["sub_total"]);
                boleta.igv = Convert.ToDouble(dr["IGV"]);
                boleta.total = Convert.ToDouble(dr["Total"]);
                lista.Add(boleta);
            }
            con.Close();
            return lista;
        }
    

    public string GenerarBoletaPDF(string orden, string nombre, string dni, string fecha, string codPro, string nombrePro, string cantidad, string precioUni, string imp, string igv, string total)
    {
        // Crear el documento PDF
        Document documento = new Document();
        string rutaPDF = HttpContext.Current.Server.MapPath("~/BoletasPizzeria.pdf");

        // Establecer el escritor que generará el archivo PDF
        PdfWriter.GetInstance(documento, new FileStream(rutaPDF, FileMode.Create));
        documento.Open();

        // Fuentes
        Font fontTable = new Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 12, Font.NORMAL, BaseColor.WHITE));
        Font fontTitle = new Font(FontFactory.GetFont(FontFactory.TIMES_BOLD, 18, Font.BOLDITALIC, BaseColor.BLUE));

        // Agregar logo (imagen)
        string imagePath = HttpContext.Current.Server.MapPath("~/img/image.png");
        Image logo = Image.GetInstance(imagePath);
        logo.Alignment = Element.ALIGN_LEFT;
        documento.Add(logo);

        // Título y dirección
        Paragraph titulo = new Paragraph("SEDE LIMA-2025", fontTitle);
        titulo.Alignment = Element.ALIGN_LEFT;
        documento.Add(titulo);

        Paragraph titulo2 = new Paragraph("Av. 28 de Julio 715, Lima 15046", fontTitle);
        titulo2.Alignment = Element.ALIGN_LEFT;
        documento.Add(titulo2);

        // RUC y tipo de boleta
        Paragraph ruc = new Paragraph("R.U.C: 20131376503");
        Paragraph title = new Paragraph("BOLETA DE VENTA");
        Paragraph serie = new Paragraph(orden);
        title.Alignment = Element.ALIGN_RIGHT;
        serie.Alignment = Element.ALIGN_RIGHT;
        ruc.Alignment = Element.ALIGN_RIGHT;

        documento.Add(ruc);
        documento.Add(title);
        documento.Add(serie);

        // Información del cliente
        documento.Add(new Paragraph("SEÑOR(ES): " + nombre));
        documento.Add(new Paragraph("DOCUMENTO DE IDENTIDAD: " + dni));
        documento.Add(new Paragraph("FECHA DE EMISION: " + fecha));
        documento.Add(new Paragraph("\n"));
        documento.Add(new Paragraph("\n"));
        documento.Add(new Paragraph("\n"));

        // Crear tabla para productos
        PdfPTable table = new PdfPTable(5);
        table.WidthPercentage = 100;

        // Definir encabezados de la tabla
        PdfPCell cel1 = new PdfPCell(new Paragraph("CODIGO DE PRODUCTO", fontTable));
        cel1.HorizontalAlignment = Element.ALIGN_CENTER;
        cel1.BackgroundColor = BaseColor.GRAY;
        PdfPCell cel2 = new PdfPCell(new Paragraph("PRODUCTO", fontTable));
        cel2.HorizontalAlignment = Element.ALIGN_CENTER;
        cel2.BackgroundColor = BaseColor.GRAY;
        PdfPCell cel3 = new PdfPCell(new Paragraph("CANTIDAD", fontTable));
        cel3.HorizontalAlignment = Element.ALIGN_CENTER;
        cel3.BackgroundColor = BaseColor.GRAY;
        PdfPCell cel4 = new PdfPCell(new Paragraph("PRECIO UNITARIO", fontTable));
        cel4.HorizontalAlignment = Element.ALIGN_CENTER;
        cel4.BackgroundColor = BaseColor.GRAY;
        PdfPCell cel5 = new PdfPCell(new Paragraph("IMPORTE", fontTable));
        cel5.HorizontalAlignment = Element.ALIGN_CENTER;
        cel5.BackgroundColor = BaseColor.GRAY;

        table.AddCell(cel1);
        table.AddCell(cel2);
        table.AddCell(cel3);
        table.AddCell(cel4);
        table.AddCell(cel5);

        // Añadir fila con los detalles del producto
        table.AddCell(codPro);
        table.AddCell(nombrePro);
        table.AddCell(cantidad);
        table.AddCell(precioUni);
        table.AddCell(imp);

        // Tabla con los totales
        PdfPTable tableTotales = new PdfPTable(2);
        tableTotales.WidthPercentage = 40;
        tableTotales.HorizontalAlignment = Element.ALIGN_RIGHT;

        tableTotales.AddCell(new PdfPCell(new Paragraph("Subtotal:")));
        tableTotales.AddCell(new PdfPCell(new Paragraph("S/ " + imp)));

        tableTotales.AddCell(new PdfPCell(new Paragraph("IGV (18%):")));
        tableTotales.AddCell(new PdfPCell(new Paragraph("S/ " + igv)));

        tableTotales.AddCell(new PdfPCell(new Paragraph("Total:")));
        tableTotales.AddCell(new PdfPCell(new Paragraph("S/ " + total)));

        documento.Add(table);
        documento.Add(tableTotales);
        documento.Add(new Paragraph("\n"));
        documento.Add(new Paragraph("\n"));

        // Agradecimiento
        Paragraph thanks = new Paragraph("GRACIAS POR SU PREFERENCIA " + nombre, fontTitle);
        thanks.Alignment = Element.ALIGN_CENTER;
        documento.Add(thanks);

        // Cerrar el documento
        documento.Close();

        return rutaPDF;
    }


}
}