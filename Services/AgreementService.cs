using Common;
using DAL.Entities;
using iText.Forms;
using iText.Forms.Fields;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.License;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace Services
{
    public interface IAgreementService
    {
        Task<IList<DocumentStatusCnt>> GetAgreementsCounts(int id);
        Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId);

        Task<int> GetAgrrementsCnt(int ownerId, string year, string month);
        Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId, string year, string month, string status);
        Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId, string year, string month, string sortOrder, string pageNumber, string pageSize);
       

        Task<Agreement> GetAgrrement(long id);

      //  byte[] AddPdfAgrrement(byte[] pdfDoc, Dictionary<string, string> paramList);
        byte[] GeneratePDF(string pdfPath, Dictionary<string, string> formFieldMap);
        void WritePDF(string pdfPath, Dictionary<string, string> formFieldMap, string outPutPath);

        Task<int> DeleteAgreement(Agreement agreement);

       // void MapPDFDocumentParams(string src, string dest, CompanyInitiationVM formFieldMap);
    }

        public class AgreementService: IAgreementService
    {
        private readonly AgreementsContext _repo;
        public AgreementService(AgreementsContext AgreementsContext)
        {
            _repo = AgreementsContext;
        }

        public async Task<int> GetAgrrementsCnt(int ownerId, string year, string month)
        {


            return await _repo.Agreements.AsNoTracking().Include(a => a.Document)
                                   .Where(a => a.Notarizer.NotarizerId == ownerId)
                                   .Select(x => new AgreementListItem
                                   {
                                       Id = x.AgreementId,
                                       AgreementDate = x.AgreementDate,
                                       Title = x.Title,
                                       AgreementBody = x.Document.DocumentBody,
                                       DateCompleted = x.DateCompleted,
                                       Status = EnumFriendlyNames.GetDesplayName(x.Status) // x.Status.GetAttribute<DisplayAttribute>()

                                   }).CountAsync();
        }
        
        public async Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId, string year, string month, string status)
        {
            IQueryable<Agreement> result;

            if ( status == "All")
            {
                 result = _repo.Agreements.AsNoTracking().Include(a => a.Document)
                                   .Where(a => a.Notarizer.NotarizerId == ownerId);
            }
            else
            {
                var v = Enum.Parse(typeof(DocumentStatusEnum), status);
                 result = _repo.Agreements.AsNoTracking().Include(a => a.Document)
                                   .Where(a => a.Notarizer.NotarizerId == ownerId && a.Status == (DocumentStatusEnum)v);
            }

            return await result.Select(x => new AgreementListItem
                                   {
                                       Id = x.AgreementId,
                                       AgreementDate = x.AgreementDate,
                                       Title = x.Title,
                                       AgreementBody = x.Document.DocumentBody,
                                       DateCompleted = x.DateCompleted,
                                       Status = EnumFriendlyNames.GetDesplayName(x.Status) // x.Status.GetAttribute<DisplayAttribute>()

                                   }).ToListAsync<AgreementListItem>();
        }
        public async Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId, string year, string month, string sortOrder, string pageNumber, string pageSize)
        {
            short pgeSize = 10;
            short pgeNumber = 0;
            short.TryParse(pageSize, out pgeSize);
            short.TryParse(pageNumber, out pgeNumber);

            return await _repo.Agreements.AsNoTracking().Include(a => a.Document)
                                   .Where(a => a.Notarizer.NotarizerId == ownerId)
                                   .Select(x => new AgreementListItem
                                   {
                                       Id = x.AgreementId,
                                       AgreementDate = x.AgreementDate,
                                       Title = x.Title,
                                       AgreementBody = x.Document.DocumentBody,
                                       DateCompleted = x.DateCompleted,
                                       Status = EnumFriendlyNames.GetDesplayName(x.Status) // x.Status.GetAttribute<DisplayAttribute>()
           
                                   }).Skip(pgeSize * pgeNumber).Take(pgeSize).ToListAsync<AgreementListItem>();
        }

        public async Task<IList<DocumentStatusCnt>> GetAgreementsCounts(int id = 16 )
        {
            var grouped = from b in _repo.Agreements.Where(n => n.NotarizerId == 16)
                          group b.Status by b.Status into g
                          select new
                          {
                              StatusId = g.Key,
                              Count = g.Count()

                          };

            var glist = await grouped.ToListAsync();

            var result = new List<DocumentStatusCnt>();
            foreach (var g in glist)
            {
                result.Add(new DocumentStatusCnt { StatusId = g.StatusId, Count = g.Count, Status = EnumFriendlyNames.GetDesplayName(g.StatusId) });
            }

            return result;
        }
        public async Task<IEnumerable<AgreementListItem>> GetAgrrements(int ownerId)
        {

            return await _repo.Agreements.AsNoTracking().Include(a => a.Document)
                                   .Where(a => a.Notarizer.NotarizerId == ownerId)
                                   .Select(x => new AgreementListItem
                                   {
                                       Id = x.AgreementId,
                                       AgreementDate = x.AgreementDate,
                                       Title = x.Title,
                                       AgreementBody = x.Document.DocumentBody
                                   }).ToListAsync<AgreementListItem>();
        }

        public async Task<Agreement> GetAgrrement(long id)
        {
            return await _repo.Agreements.Include(a => a.Document)
                                   .Where(a => a.AgreementId == id)
                                  .FirstOrDefaultAsync();
        }

        //public byte[] AddPdfAgrrement(byte[] pdfDoc, Dictionary<string, string> paramList)
        //{

        //    try
        //    {
                

        //        byte[] updatedDoc = UpdatePdfDocument(pdfDoc, paramList);
               


        //        Agreement ag = new Agreement();
        //        short userId = 1;
        //        ag.WakeelName = "Agent Name";
        //        ag.AgreementDate = DateTime.Now;
        //        ag.MuaKalName = "Client Name";
        //        ag.DateCreated = DateTime.Now;
        //        ag.Day = DateTime.Now.DayOfWeek.ToString();
        //        ag.NotarizerId = 2;
        //        Document d = new Document();
        //        d.DocumentBody = updatedDoc;
        //        d.DateCreated = DateTime.Now;
        //        ag.Document = d;
        //        _repo.Documents.Add(d);
        //        Notarizer o = new Notarizer();
        //        o.Address = "address 1";
        //        o.Name = "Notoraizer Name";
        //        o.Email = "nm.yh.com";
        //        ag.Notarizer = o;
        //        _repo.Notarizers.Add(o);
        //        _repo.Agreements.Add(ag);
        //        _repo.SaveChanges();
        //        return updatedDoc;
        //    }
        //    catch(Exception ex)
        //    {
        //        var msg = ex.Message;
        //        return null;
        //    }
        //}
        //public  byte[] UpdatePdfDocument(byte[] pdfDoc, Dictionary<string, string> paramList)
        //{
        //    Stream pdfStream = null;
        //    PdfStamper stamper = null;

        //    try
        //    {

        //       //  PdfDocument pdf = new PdfDocument();

        //      var r =   new PdfReader(pdfDoc, )

        //        PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(output));
        //        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);

        //        IDictionary<String, PdfFormField> fields = form.GetFormFields();

        //        PdfFormField toSet;
        //        foreach (var fieldName in formFieldMap.Keys)
        //        {


        //            fields.TryGetValue(fieldName, out toSet);
        //            toSet.SetValue(formFieldMap[fieldName]);
        //        }

        //        form.FlattenFields();
        //        pdf.Close();


        //        return output.ToArray();

        //        using (var existingFileStream = new MemoryStream(pdfDoc))
        //            {
        //                // using (var newFileStream = new FileStream(outputFilename, FileMode.Create))
        //                using (var newFileStream = new MemoryStream())
        //                {
        //                    var pdfReader = new PdfReader(existingFileStream);
                       
        //                    stamper = new PdfStamper(pdfReader, newFileStream);
        //                    var form = stamper.AcroFields;
        //                   stamper.Writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
        //                form.GenerateAppearances = false;
        //                var fieldKeys = form.Fields.Keys;

                            

        //                foreach (string fieldKey in fieldKeys)
        //                    {
        //                       form.SetField(fieldKey, paramList[fieldKey]);
                               
        //                    //formFields.SetListOption(f)
        //                    }
                       
                       
        //                stamper.FormFlattening = true;

        //                    pdfReader.Close();
        //                    stamper.Close();
        //                    newFileStream.Close();
        //                    return newFileStream.ToArray();
                          
        //                }

        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (pdfStream != null)
        //        {
        //            pdfStream.Close();
        //        }
               
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        // stamper.Close();
        //    }
        //}

        public byte[] GeneratePDF(string src, Dictionary<string, string> formFieldMap)
        {


            ///
            var output = new MemoryStream();
            //var reader = new PdfReader(pdfPath);
            //var stamper = new PdfStamper(reader, output);
            //var form = stamper.AcroFields;
            // form.GenerateAppearances = false;
            //  stamper.Writer.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            // commented out until license issue is cleared
          //  LicenseKey.LoadLicenseFile("./License/Eula/itextkey1564766929114_0.xml");

            PdfFont font = PdfFontFactory.CreateFont(fontProgram: "c:/windows/fonts/arialuni.ttf", PdfEncodings.IDENTITY_H);

            PdfDocument pdf = new PdfDocument(new PdfReader(src), new PdfWriter(output));
            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
           
            IDictionary<String, PdfFormField> fields = form.GetFormFields();

            PdfFormField toSet;
            foreach (var fieldName in formFieldMap.Keys)
            {

                if (fieldName != "agreementFormName")
                {   // agreementFormName is not a field pn document form

                   
                    fields.TryGetValue(fieldName, out toSet);
                    if (toSet != null)
                    {
                        toSet.SetFont(font).SetFontSize(11);
                        // commented out until license issue is cleared
                        // toSet.SetValue(formFieldMap[fieldName]);
                    }
                }
               
            }

            var img = _repo.Notarizers.Find(1).Stamp;

            PdfFormField field = form.GetField("notarization_stamp");
            // PdfWidgetAnnotation widgetAnnotation = field.GetWidgets().First().GetRectangle();
            // PdfArray annotationRect = widgetAnnotation.GetRectangle();

       //     var f2 = form.GetField("notarization_stamp");

            Image stamp = new Image(ImageDataFactory.Create(img));





            PdfArray position = field.GetWidgets().First().GetRectangle();
            float width = (float)(position.GetAsNumber(2).GetValue() - position.GetAsNumber(0).GetValue());
            float height = (float)(position.GetAsNumber(3).GetValue() - position.GetAsNumber(1).GetValue());

            stamp.SetFixedPosition((float)position.GetAsNumber(0).GetValue(), (float)position.GetAsNumber(1).GetValue());
            stamp.SetHeight(height);
            stamp.SetWidth(width);
            PdfPage page = pdf.GetLastPage();

           var  canvas = new PdfCanvas(page, true);

            new Canvas(canvas, pdf, page.GetPageSize()).Add(stamp);
            canvas.Release();

            form.FlattenFields();
            pdf.Close();
            var r = SaveDocument(output, formFieldMap["agreementFormName"] );
            return output.ToArray();
        }

        public void WritePDF(string src, Dictionary<string, string> formFieldMap, string dest)
        {
            //  var  f = new FileStream(path: "./Documents/طلب _انسحاب _من _ شركة.pdf", mode: FileMode.Open);
            //  var w = new PdfWriter(dest);

            PdfCanvas canvas;
            PdfDocument pdf = null;
            PdfReader pdfReader = null;
            try
            {
                LicenseKey.LoadLicenseFile("./License/Eula/itextkey1564766929114_0.xml");

                //  PdfDocument pdf = new PdfDocument(new PdfReader(src));
                pdfReader = new PdfReader(src);
                pdf = new PdfDocument(pdfReader, new PdfWriter(dest));
                PdfFont font = PdfFontFactory.CreateFont(fontProgram: "c:/windows/fonts/arialuni.ttf", PdfEncodings.IDENTITY_H);
                //   font = PdfFontFactory.CreateFont("c:\\windows\\fonts\\times.ttf", PdfEncodings.IDENTITY_H);
                //  PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                //  paragraph.Alignment = Element.ALIGN_LEFT;
                //  font = PdfFontFactory.CreateFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);

                //  pdf.AddFont(font);

                //  new PdfDocument(new PdfReader(src), new PdfWriter(f));
                //  pdf.SetTextAlignment(TextAlignment.RIGHT));
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
                IDictionary<String, PdfFormField> fields = form.GetFormFields();

                PdfFormField toSet;
                foreach (var fieldName in formFieldMap.Keys)
                {

                    fields.TryGetValue(fieldName, out toSet);
                    if (toSet != null)
                    {
                        toSet.SetFont(font).SetFontSize(11);
                        //  form.GetField(fieldName);
                        toSet.SetValue(formFieldMap[fieldName]);
                    }
                }

                var img = _repo.Notarizers.Find(1).Stamp;

                PdfFormField field = form.GetField("notarization_stamp");
                // PdfWidgetAnnotation widgetAnnotation = field.GetWidgets().First().GetRectangle();
                // PdfArray annotationRect = widgetAnnotation.GetRectangle();

                var f2 = form.GetField("notarization_stamp");

                Image stamp = new Image(ImageDataFactory.Create(img));





                PdfArray position = field.GetWidgets().First().GetRectangle();
                float width = (float)(position.GetAsNumber(2).GetValue() - position.GetAsNumber(0).GetValue());
                float height = (float)(position.GetAsNumber(3).GetValue() - position.GetAsNumber(1).GetValue());

                stamp.SetFixedPosition((float)position.GetAsNumber(0).GetValue(), (float)position.GetAsNumber(1).GetValue());
                stamp.SetHeight(height);
                stamp.SetWidth(width);
                PdfPage page = pdf.GetLastPage();

                canvas = new PdfCanvas(page, true);

                new Canvas(canvas, pdf, page.GetPageSize()).Add(stamp);
                canvas.Release();
                // imge.SetFixedPosition();
                form.FlattenFields();

                pdf.Close();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                if (!pdf.IsClosed())
                {
                    pdf.Close();
                }
            }
            finally
            {
                canvas = null;
                if (!pdf.IsClosed())
                {
                    pdf.Close();
                    pdfReader.Close();
                }

            }
        }


            private int SaveDocument(MemoryStream doc, string agreementName)
            {
                try
                {
                var an = agreementName.Split('.')[0];
                   short agTypeId = _repo.AgreementTypes.Where(a => a.TemplateName.Contains(an)).FirstOrDefault().Id;


                    Agreement ag = new Agreement();
                    short userId = 1;
                    ag.WakeelName = "Agent Name";
                    ag.Notarizer.NotarizerId = userId;
                    ag.AgreementTypeId = _repo.AgreementTypes.Where(a => a.TemplateName == an).FirstOrDefault().Id;
                    ag.AgreementDate = DateTime.Now;
                    ag.MuaKalName = "Client Name";
                    ag.DateCreated = DateTime.Now;
                    ag.Day = DateTime.Now.DayOfWeek.ToString();
                    ag.Notarizer.NotarizerId = 2;
                    ag.Status = DocumentStatusEnum.Open;
                    ag.Title = _repo.AgreementTypes.Where(a => a.Id == agTypeId).FirstOrDefault().Name;
                //  ag.DateCompleted = null;
                    DAL.Entities.Document d = new DAL.Entities.Document();
                    d.DocumentBody = doc.ToArray();
                    d.DateCreated = DateTime.Now;
                    ag.Document = d;
                   
                    _repo.Documents.Add(d);
                   
                    //Notarizer o = new Notarizer();
                    //o.Address = "address 1";
                    //o.Name = "Notoraizer Name";
                    //o.Email = "nm.yh.com";
                    //ag.Notarizer = o;
                    //_repo.Notarizers.Add(o);
                    _repo.Agreements.Add(ag);
                   return _repo.SaveChanges();
                }
                catch (Exception ex)
                {
                    string m = ex.Message;
                return 0;
                   
                }
                finally
                {
                    

                }
            }


        public async Task<int> DeleteAgreement(Agreement agreement)
        {
            try
            {
                _repo.Agreements.Remove(agreement);

                return await _repo.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

                //Initialize PDF document
                //var existingFileStream = new FileStream(src, FileMode.Open));

                // var w = PdfWriter(new PdfDocument(), existingFileStream);
                // PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
                // //
                // // Summary:
                // //     Constructs a PdfWriter .
                //  PdfWriter();
                //  PdfWriter(PdfDocument document, Stream os);

                // using (var fileStream = new FileStream(dest, FileMode.Create,
                // FileAccess.Write))
                // {
                //     iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 30, 30);
                //     PdfWriter writer = PdfWriter.GetInstance(document, fileStream);
                //     document.Open();
                //     document.Open();
                //     document.Add(new Paragraph(editedText));
                //     document.Close();
                //     writer.Close();
                //     fileStream.Close();
                // }
            }

        //public void MapPDFDocumentParams(string src, string dest,  CompanyInitiationVM formFieldMap)
        //{
        //    //  var  f = new FileStream(path: "./Documents/طلب _انسحاب _من _ شركة.pdf", mode: FileMode.Open);
        //    //  var w = new PdfWriter(dest);

        //    PdfCanvas canvas;
        //    PdfDocument pdf = null ;
        //    PdfReader pdfReader = null;
        //    try
        //    {
        //       // LicenseKey.LoadLicenseFile("./License/Eula/itextkey1556982124976_0.xml");
            
        //      //  PdfDocument pdf = new PdfDocument(new PdfReader(src));
        //         pdfReader = new PdfReader(src);
        //         pdf = new PdfDocument(pdfReader, new PdfWriter(dest));
        //        PdfFont font = PdfFontFactory.CreateFont(fontProgram: "c:/windows/fonts/arialuni.ttf", PdfEncodings.IDENTITY_H);
        //        //  font = PdfFontFactory.CreateFont("c:\\windows\\fonts\\times.ttf", PdfEncodings.IDENTITY_H);
        //      //  PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
        //        //  paragraph.Alignment = Element.ALIGN_LEFT;
        //        //    font = PdfFontFactory.CreateFont(FontFactory.HELVETICA, 12f, BaseColor.GREEN);

        //          pdf.AddFont(font);

        //        //  new PdfDocument(new PdfReader(src), new PdfWriter(f));
        //       //  pdf.SetTextAlignment(TextAlignment.RIGHT));
        //        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdf, true);
        //        IDictionary<String, PdfFormField> fields = form.GetFormFields();




                

                

        //        PdfFormField toSet;
        //        //foreach (var fieldName in formFieldMap.Keys)
        //        //{


        //        //    fields.TryGetValue(fieldName, out toSet);
        //        //    toSet.SetValue(formFieldMap[fieldName]);
        //       // }

        //        form.FlattenFields();
        //        pdf.Close();

        //        // PdfFormField toSet;
        //        //foreach (var fieldName in formFieldMap.Keys)
        //        //{

        //        //    fields.TryGetValue(fieldName, out toSet);
        //        //    if (toSet != null)
        //        //    {
        //        //        toSet.SetFont(font);
        //        //      //  form.GetField(fieldName);
        //        //        toSet.SetValue(formFieldMap[fieldName]);
        //        //    }
        //        //}

        //        var img = _repo.Users.Find(1).Stamp;

        //        PdfFormField field = form.GetField("notarization_stamp");
        //       // PdfWidgetAnnotation widgetAnnotation = field.GetWidgets().First().GetRectangle();
        //       // PdfArray annotationRect = widgetAnnotation.GetRectangle();

        //        var f2 = form.GetField("signature_1");
               
        //        Image stamp = new Image(ImageDataFactory.Create(img));
               

              


        //        PdfArray position = field.GetWidgets().First().GetRectangle();
        //        float width = (float)(position.GetAsNumber(2).GetValue() - position.GetAsNumber(0).GetValue());
        //        float height = (float)(position.GetAsNumber(3).GetValue() - position.GetAsNumber(1).GetValue());

        //        stamp.SetFixedPosition((float)position.GetAsNumber(0).GetValue(), (float)position.GetAsNumber(1).GetValue());
        //        stamp.SetHeight(height);
        //        stamp.SetWidth(width);
        //        PdfPage page = pdf.GetLastPage();

        //         canvas = new PdfCanvas(page, true);
               
        //        new Canvas(canvas, pdf, page.GetPageSize()).Add(stamp);
        //         canvas.Release();
        //        // imge.SetFixedPosition();
        //        form.FlattenFields();

        //        pdf.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        string m = ex.Message;
        //        if (!pdf.IsClosed())
        //        {
        //            pdf.Close();
        //        }
        //    }
        //    finally {
        //        canvas = null;
        //        if (!pdf.IsClosed())
        //        {
        //            pdf.Close();
        //            pdfReader.Close();
        //        }
               
        //    }





       
       
    }



