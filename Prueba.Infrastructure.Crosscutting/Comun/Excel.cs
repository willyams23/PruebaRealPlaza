using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Crosscutting.Comun
{
    public class Excel
    {
        public Excel()
        {

        }

        public byte[] DataTableToExcelArrayByte(DataTable data, string nombreHoja, string headers, string titulo="", string asunto = "", string detalle = "")
        {
            byte[] workbookBytes = new byte[0];

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Properties.Title = titulo;
                wb.Properties.Author = Constants.autorExcel;
                wb.Properties.Subject = asunto;
                wb.Properties.Keywords = detalle;

                int idx = -1;
                foreach (DataColumn col in data.Columns)
                {
                    idx++;
                    col.Caption = headers.Split('|')[idx].ToString();
                }
                IXLWorksheet ws = wb.Worksheets.Add(data, nombreHoja);

                ws.Columns().AdjustToContents();
                string cad = Convert.ToChar(data.Columns.Count + 64).ToString();
                ws.Range("A1", cad + "1").Style.Fill.BackgroundColor = XLColor.FromArgb(255, 106, 5);
                ws.Range("A1", cad + "1").Style.Font.FontColor = XLColor.White;
                ws.Range("A1", cad + "1").SetAutoFilter(false);
                using (var ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    workbookBytes = ms.ToArray();
                }
            }
            return workbookBytes;
        }

        public DataTable StreamToDataTable(MemoryStream stream, string NombreHoja)
        {
            DataTable dt = new DataTable();
            using (XLWorkbook wb = new XLWorkbook(stream))
            {
                IXLWorksheet workSheet = wb.Worksheet(1);
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }
            }
            return dt;
        }
    }
}
