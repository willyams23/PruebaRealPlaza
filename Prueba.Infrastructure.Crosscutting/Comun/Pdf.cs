using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting.Comun
{
    public class Pdf
    {
        //public static readonly PdfNumber PORTRAIT = new PdfNumber(0);
        //public static readonly PdfNumber LANDSCAPE = new PdfNumber(90);
        //public static readonly PdfNumber INVERTEDPORTRAIT = new PdfNumber(180);
        //public static readonly PdfNumber SEASCAPE = new PdfNumber(270);

        public Pdf()
        {
        }

        public Table GenerarLineaDoble(string valor1, string valor2, int anchoValor1, TextAlignment alineacionValor1)
        {
            Table table = new Table(2).UseAllAvailableWidth();
            Cell cellX = new Cell().Add(new Paragraph(valor1)).SetWidth(anchoValor1).SetTextAlignment(alineacionValor1).SetBorder(Border.NO_BORDER);
            Cell cellY = new Cell().Add(new Paragraph(valor2)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER);
            table.AddCell(cellX);
            table.AddCell(cellY);
            return table;
        }

        public Table GenerarDetalleReporte(List<PdfDetalle> detalle, float anchoTitulos, bool valoresSubrrayados = false, PdfFont fuente = null, float tamanoFuente = 0)
        {
            Table table = new Table(2).UseAllAvailableWidth();
            Border borde = new DottedBorder(1);
            if (fuente != null)
                table.SetFont(fuente);

            if (tamanoFuente > 0)
                table.SetFontSize(tamanoFuente);

            foreach (PdfDetalle item in detalle)
            {
                Cell cell1 = new Cell().Add(new Paragraph(item.Titulo)).SetWidth(anchoTitulos).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                Cell cell2 = null;

                if (valoresSubrrayados)
                    cell2 = new Cell().Add(new Paragraph(item.Valor).SetBorderBottom(borde)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                else
                    cell2 = new Cell().Add(new Paragraph(item.Valor)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);

                table.AddCell(cell1);
                table.AddCell(cell2);
            }

            return table;
        }

        public Table GenerarDetalleDobleReporte(List<PdfDetalle> detalle, bool valoresSubrrayados1 = false, bool valoresSubrrayados2 = false, int anchoTitulo1=0,
                                                int anchoValor1 = 0, int anchoTitulo2 =0, int anchoValor2 = 0, PdfFont fuente=null, float tamanoFuente=0)
        {
            Table table = new Table(4).UseAllAvailableWidth().SetBorder(Border.NO_BORDER);
            bool par = false;
            Border borde = new DottedBorder(1);

            if (fuente != null)
                table.SetFont(fuente);

            if (tamanoFuente > 0)
                table.SetFontSize(tamanoFuente);

            foreach (PdfDetalle item in detalle)
            {
                Cell cell1 = null;
                Cell cell2 = null;
                if (!par)
                {
                    cell1 = new Cell().Add(new Paragraph(item.Titulo)).SetBorder(Border.NO_BORDER);
                    if(valoresSubrrayados1)
                        cell2 = new Cell().Add(new Paragraph(item.Valor).SetBorderBottom(borde)).SetBorder(Border.NO_BORDER);
                    else
                        cell2 = new Cell().Add(new Paragraph(item.Valor)).SetBorder(Border.NO_BORDER);

                    if (anchoTitulo1 > 0)
                        cell1.SetWidth(anchoTitulo1);
                    if (anchoValor1 > 0)
                        cell2.SetWidth(anchoValor1);
                }
                else
                {
                    cell1 = new Cell().Add(new Paragraph(item.Titulo)).SetBorder(Border.NO_BORDER);
                    if (valoresSubrrayados2)
                        cell2 = new Cell().Add(new Paragraph(item.Valor).SetBorderBottom(borde)).SetBorder(Border.NO_BORDER);
                    else
                        cell2 = new Cell().Add(new Paragraph(item.Valor)).SetBorder(Border.NO_BORDER);

                    if (anchoTitulo2 > 0)
                        cell1.SetWidth(anchoTitulo2);
                    if (anchoValor2 > 0)
                        cell2.SetWidth(anchoValor2);
                }

                table.AddCell(cell1);
                table.AddCell(cell2);
                par = true;
            }

            return table;
        }

        public Paragraph GenerarLinea(string valor, float margenIzquierdo = 0, float margenSuperior = 0, PdfFont fuente = null, 
                                      float tamanoFuente = 0, bool negrita=false, bool conBorde = false)
        {
            Paragraph pg = new Paragraph(valor);
            Border borde = new SolidBorder(1);

            if (margenIzquierdo > 0)
                pg.SetMarginLeft(margenIzquierdo);

            if (margenSuperior > 0)
                pg.SetMarginTop(margenSuperior);

            if (fuente != null)
                pg.SetFont(fuente);

            if (tamanoFuente > 0)
                pg.SetFontSize(tamanoFuente);

            if (negrita)
                pg.SetBold();

            if (conBorde)
                pg.SetBorder(borde);

            return pg;
        }

        public Table GenerarFirmas(List<string> listaFirmas, float margen)
        {
            Table table = new Table(2).UseAllAvailableWidth();

            for (int i = 0; i < listaFirmas.Count; i++)
            {
                Cell cell = new Cell().Add(new Paragraph("____________________________")).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER);
                table.AddCell(cell);
            }

            foreach (string item in listaFirmas)
            {
                Cell cell = new Cell().Add(new Paragraph(item)).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER);
                table.AddCell(cell);
            }

            table.SetMarginTop(margen);

            return table;
        }

        public Table GenerarChecks(List<PdfCheck> checks, PdfFont fuente = null, int tamanoFuente = 0)
        {
            Table table = new Table(6).UseAllAvailableWidth();

            if (fuente != null)
                table.SetFont(fuente);

            if (tamanoFuente > 0)
                table.SetFontSize(tamanoFuente);

            foreach (PdfCheck item in checks)
            {
                Table tableCheck = new Table(1).SetWidth(11).SetHeight(11).SetMargin(0).SetPadding(0);
                Cell cell1 = null;
                if (item.Seleccionado)
                {
                    tableCheck.AddCell(new Cell().Add(new Paragraph("X").SetFontSize(7).SetMargin(0).SetPadding(0)).SetTextAlignment(TextAlignment.CENTER).SetMargin(0).SetPadding(0));
                }
                else
                {
                    tableCheck.AddCell(new Cell().Add(new Paragraph(" ")).SetTextAlignment(TextAlignment.CENTER));
                }
                cell1 = new Cell().Add(tableCheck.SetMarginLeft(18)).SetBorder(Border.NO_BORDER).SetWidth(15).SetTextAlignment(TextAlignment.CENTER);

                Cell cell2 = new Cell().Add(new Paragraph(item.Texto).SetMarginLeft(18)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                table.AddCell(cell1);
                table.AddCell(cell2);
            }

            return table;
        }

        public Table GenerarDetalleReporteDobleValor(List<PdfDetalleDobleValor> datos, int ancho1=0, int ancho2=0, bool valoresSubrrayados=false, PdfFont fuente = null, int tamanoFuente = 0)
        {
            Table table = new Table(3).UseAllAvailableWidth();
            Border borde = new DottedBorder(1);
            if (fuente != null)
                table.SetFont(fuente);

            if (tamanoFuente > 0)
                table.SetFontSize(tamanoFuente);

            foreach (PdfDetalleDobleValor item in datos)
            {
                Cell cell1 = new Cell().Add(new Paragraph(item.Titulo)).SetWidth(ancho1).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                Cell cell2 = null;
                Cell cell3 = null;

                if (valoresSubrrayados)
                    cell2 = new Cell().Add(new Paragraph(item.Valor1).SetBorderBottom(borde)).SetWidth(ancho2).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                else
                    cell2 = new Cell().Add(new Paragraph(item.Valor1)).SetWidth(ancho2).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);

                if (valoresSubrrayados)
                    cell3 = new Cell().Add(new Paragraph(item.Valor2).SetBorderBottom(borde)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);
                else
                    cell3 = new Cell().Add(new Paragraph(item.Valor2)).SetTextAlignment(TextAlignment.LEFT).SetBorder(Border.NO_BORDER);

                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);
            }

            return table;
        }
        
        /*public class PageOrientationsEventHandler : IEventHandler
        {
            private PdfNumber orientation = PORTRAIT;

            public void SetOrientation(PdfNumber orientation)
            {
                this.orientation = orientation;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                docEvent.GetPage().Put(PdfName.Rotate, orientation);
            }
        }

        private class PageRotationEventHandler : IEventHandler
        {
            private PdfNumber rotation = PORTRAIT;

            public void SetRotation(PdfNumber orientation)
            {
                this.rotation = orientation;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
                docEvent.GetPage().Put(PdfName.Rotate, rotation);
            }
        }*/
    }
}
