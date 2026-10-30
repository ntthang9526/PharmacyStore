using System.Data;
using ClosedXML.Excel;
namespace Store.POS.Common;
public static class ExcelTable
{
    public static void Luu(string path,string title,DataTable data,params (string Key,string Title)[] columns)
    {
        using var workbook=new XLWorkbook();var sheet=workbook.AddWorksheet("Dữ liệu");
        sheet.Cell(1,1).Value=title;sheet.Range(1,1,1,columns.Length).Merge().Style.Font.SetBold().Font.SetFontSize(16);
        for(int c=0;c<columns.Length;c++)
        {
            sheet.Cell(2,c+1).Value=columns[c].Title;
            sheet.Cell(2,c+1).Style.Fill.BackgroundColor=XLColor.FromHtml("#0F766E");
            sheet.Cell(2,c+1).Style.Font.FontColor=XLColor.White;sheet.Cell(2,c+1).Style.Font.Bold=true;
        }
        for(int row=0;row<data.Rows.Count;row++) for(int col=0;col<columns.Length;col++)
        {
            object value=data.Rows[row][columns[col].Key];var cell=sheet.Cell(row+3,col+1);
            if(value==DBNull.Value) continue;
            if(value is decimal money) {cell.Value=money;cell.Style.NumberFormat.Format="#,##0";}
            else if(value is int number) cell.Value=number;
            else if(value is DateTime date) {cell.Value=date;cell.Style.DateFormat.Format="dd/MM/yyyy HH:mm";}
            else cell.Value=value.ToString()??"";
        }
        sheet.SheetView.FreezeRows(2);sheet.Columns().AdjustToContents(8,60);workbook.SaveAs(path);
    }
}
