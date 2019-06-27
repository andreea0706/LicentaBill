using Invoice.Core.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace PharmaApp.Web.Reports
{
    public class SalesReportSmall
    {
        #region Declaration

        private int _totalColumn = 5;
        private Document _document;
        private iTextSharp.text.Font _fontStyle;
        private iTextSharp.text.Font _fontStyleNormal;
        private PdfPTable _pdfPTable;
        private PdfPCell _pdfPCell;
        private MemoryStream _memoryStream;
        private PdfWriter writer;

        private PdfPTable paymentTable;

       private SaleReportViewModel _model;

        public SalesReportSmall()
        {

            paymentTable = new PdfPTable(2);
            _pdfPTable = new PdfPTable(_totalColumn);
            _memoryStream = new MemoryStream();

        }
        #endregion
        public byte[] CreateReport(SaleReportViewModel model)
        {
           _model = model;

            #region page
            

            _document = new Document(new iTextSharp.text.Rectangle(200f, 440f));
            _document.SetPageSize(new iTextSharp.text.Rectangle(200f, 440f));
            _document.SetMargins(10f, 10f, 10f, 10f);
            _pdfPTable.WidthPercentage = 100;
            _pdfPTable.HorizontalAlignment = Element.ALIGN_LEFT;
            _fontStyle = FontFactory.GetFont("Tahoma", 7f, 1);
            writer = PdfWriter.GetInstance(_document, _memoryStream);
            _document.Open();
            _pdfPTable.SetWidths(new float[] { 40f, 180f, 60f, 40f, 70f });

            paymentTable.WidthPercentage = 100;
            paymentTable.HorizontalAlignment = Element.ALIGN_LEFT;
            paymentTable.SetWidths(new float[] { 50f, 50f });

            this.reportHeader();
            this.reportBody();

            _pdfPTable.HeaderRows = 2;
            _document.Add(paymentTable);
            _document.Add(_pdfPTable);
            _document.Close();
            return _memoryStream.ToArray();

            #endregion

        }
        private void reportHeader()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 11f, 1);
            _pdfPCell = new PdfPCell(new Phrase(_model.company.StoreName, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.company.Email, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.company.Web, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.company.Phone, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

          

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.company.Address, _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 7f, 1);
            _pdfPCell = new PdfPCell(new Phrase("CATRE : ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Nume Client : ", _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.CustomerModel.Name, _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Numar Telefon: ", _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.CustomerModel.Phone, _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();


            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 7f, 1);
            _pdfPCell = new PdfPCell(new Phrase("FACTURA :  ", _fontStyle));
            _pdfPCell.Colspan = 2;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Serie Document: ", _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);


            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.SaleCode, _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Data Vanzare: ", _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);


            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.SalesDate.ToShortDateString(), _fontStyle));
            _pdfPCell.Colspan = 1;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();



        }
        private void reportBody()
        {
            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 1);

            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();

            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_LEFT;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            paymentTable.AddCell(_pdfPCell);
            paymentTable.CompleteRow();


            #region TableHeader



            _fontStyle = FontFactory.GetFont("Tahoma", 5f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Nr. Crt.", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Nume Produs", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Pret", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Cantitate", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase("Valoare", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            _pdfPTable.AddCell(_pdfPCell);


            _pdfPTable.CompleteRow();
            #endregion

            #region Table body
            _fontStyle = FontFactory.GetFont("Tahoma", 5f, 0);
            int sl = 0;
            foreach (var item in _model.Sales.Items)
            {
                ++sl;
                _pdfPCell = new PdfPCell(new Phrase(sl.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.WHITE;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(item.Name, _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.WHITE;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(item.Price.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.WHITE;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(item.Quantity.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.WHITE;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPCell = new PdfPCell(new Phrase(item.Amount.ToString(), _fontStyle));
                _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                _pdfPCell.BackgroundColor = BaseColor.WHITE;
                _pdfPTable.AddCell(_pdfPCell);

                _pdfPTable.CompleteRow();
            }


            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = 5;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Total :", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Colspan = 4;
            _pdfPCell.Border = 0;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase( _model.Sales.Total.ToString() + " " + _model.company.Currency , _fontStyle));
            _pdfPCell.Border = 0;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();


            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Discount :", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Colspan = 4;
            _pdfPCell.Border = 0;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.Discount.ToString() + " " + _model.company.Currency, _fontStyle));
            _pdfPCell.Border = 0;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();



            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Grand Total :", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Colspan = 4;
            _pdfPCell.Border = 0;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_model.company.Currency + _model.Sales.GrandTotal.ToString(), _fontStyle));
            _pdfPCell.Border = 0;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();



            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 1);
            _pdfPCell = new PdfPCell(new Phrase("Status  :", _fontStyle));
            _pdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _pdfPCell.Colspan = 4;
            _pdfPCell.Border = 0;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPCell = new PdfPCell(new Phrase(_model.Sales.Status, _fontStyle));
            _pdfPCell.Border = 0;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPTable.AddCell(_pdfPCell);

            _pdfPTable.CompleteRow();

           
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = 5;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = 5;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = 5;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
            _pdfPCell = new PdfPCell(new Phrase(" ", _fontStyle));
            _pdfPCell.Colspan = 5;
            _pdfPCell.Border = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();





            #endregion
            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase(" Multumim pentru cumparaturi!", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();

            _fontStyle = FontFactory.GetFont("Tahoma", 6f, 0);
            _pdfPCell = new PdfPCell(new Phrase("Factura generata de : Andreea Ionescu, contact : andreea.ionescu@gmail.com ", _fontStyle));
            _pdfPCell.Colspan = _totalColumn;
            _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            _pdfPCell.Border = 0;
            _pdfPCell.BackgroundColor = BaseColor.WHITE;
            _pdfPCell.ExtraParagraphSpace = 0;
            _pdfPTable.AddCell(_pdfPCell);
            _pdfPTable.CompleteRow();
        }

        
    
    }
}
