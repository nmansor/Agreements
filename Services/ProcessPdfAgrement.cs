using DAL.Entities;



namespace Services
{
  
    public static class ProcessPdfAgrement
    {

        //public static void ProcessPdfDoc(string pdfPath)
        //{
        //    // var pdfPath = Path.Combine(Server.MapPath("~/PDFTemplates/fw9.pdf"));
        //    pdfPath = $"{pdfPath}/Templates/توكيل خاص.pdf";
        //    // Get the form fields for this PDF and fill them in!
        //    var formFieldMap = GetFormFieldNames(pdfPath);
        //    var pdfContents = GeneratePDF(pdfPath, formFieldMap);
        //}

        //public static Dictionary<string, string> GetFormFieldNames(string pdfPath)
        //{
        //    var fields = new Dictionary<string, string>();

        //    var reader = new PdfReader(pdfPath);
        //    foreach (DictionaryEntry entry in reader.AcroFields.Fields)
        //        fields.Add(entry.Key.ToString(), string.Empty);
        //    reader.Close();

        //    return fields;
        //}

        //public static byte[] GeneratePDF(string pdfPath, Dictionary<string, string> formFieldMap)
        //{
        //    var output = new MemoryStream();
        //    var reader = new PdfReader(pdfPath);
        //    var stamper = new PdfStamper(reader, output);
        //    var formFields = stamper.AcroFields;

        //    foreach (var fieldName in formFieldMap.Keys)
        //        formFields.SetField(fieldName, formFieldMap[fieldName]);

        //    stamper.FormFlattening = true;
        //    stamper.Close();
        //    reader.Close();

        //    return output.ToArray();
        //}

        //public static byte[] ReadPdfFile(string src)
        //{
        //    Stream pdfStream = null;
        //    PdfStamper stamper = null;

        //    try
        //    {
        //        List<int> pages = new List<int>();
        //        string outputFilename = $"{src}/Documents/filled.pdf";
        //        src = $"{src}/Templates/توكيل خاص.pdf";



        //        if (File.Exists(src))
        //        {
        //            FileInfo file = new FileInfo(src);
        //            string pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + "-";

        //            //using (var existingFileStream = new FileStream(templateFilename, FileMode.Open))
        //            //{
        //            //    using (var newFileStream = new FileStream(outputFilename, FileMode.Create))
        //            //    {

        //            //        using (var newFileStream = new MemoryStream())
        //            //{
        //            using (var existingFileStream = new FileStream(src, FileMode.Open))
        //            {
        //                // using (var newFileStream = new FileStream(outputFilename, FileMode.Create))
        //                using (var newFileStream = new MemoryStream())
        //                {
        //                    var pdfReader = new PdfReader(existingFileStream);
        //                    stamper = new PdfStamper(pdfReader, newFileStream);


        //                    var formFields = stamper.AcroFields;
        //                    var fieldKeys = formFields.Fields.Keys;
        //                    foreach (string fieldKey in fieldKeys)
        //                    {
        //                        //Replace Address Form field with my custom data
        //                        if (fieldKey.Equals("fill_1"))
        //                        {
        //                            formFields.SetField(fieldKey, "12 Pm");
        //                        }
        //                        if (fieldKey.Equals("fill_2"))
        //                        {

        //                            formFields.SetField(fieldKey, "Tuesday");
        //                        }
        //                        if (fieldKey.Equals("fill_3"))
        //                        {

        //                            formFields.SetField(fieldKey, "17/03/2019");
        //                        }
        //                        if (fieldKey.Equals("fill_4"))
        //                        {

        //                            formFields.SetField(fieldKey, "Nasser O Mansor");
        //                        }
        //                        if (fieldKey.Equals("fill_5"))
        //                        {

        //                            formFields.SetField(fieldKey, "3456");
        //                        }
        //                        if (fieldKey.Equals("fill_6"))
        //                        {

        //                            formFields.SetField(fieldKey, "8977");
        //                        }
        //                        if (fieldKey.Equals("fill_7"))
        //                        {

        //                            formFields.SetField(fieldKey, "ABC company");
        //                        }
        //                        if (fieldKey.Equals("fill_8"))
        //                        {

        //                            formFields.SetField(fieldKey, "Maha Fesatwi");
        //                        }
        //                        if (fieldKey.Equals("fill_9"))
        //                        {

        //                            formFields.SetField(fieldKey, "987655");
        //                        }
        //                        if (fieldKey.Equals("fill_10"))
        //                        {

        //                            formFields.SetField(fieldKey, "Owner");
        //                        }
        //                        if (fieldKey.Equals("fill_11"))
        //                        {

        //                            formFields.SetField(fieldKey, "Juman Nasser");
        //                        }
        //                    }

        //                    stamper.FormFlattening = true;


        //                    //setup PdfStamper
        //                    pdfReader.Close();

        //                    stamper.Close();
        //                    newFileStream.Close();
        //                    return newFileStream.ToArray();
        //                    // return newFileStream;
        //                }

        //            }

        //        }
        //        return null;
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

        //public static ICollection GetFormFields(Stream pdfStream)
        //{
        //    PdfReader reader = null;
        //    PdfReader pdfReader = null;
        //    try
        //    {
        //        pdfStream.Position = 0;
        //        pdfReader = new PdfReader(pdfStream);
        //        AcroFields acroFields = pdfReader.AcroFields;
        //        return acroFields.Fields.Keys;
        //    }
        //    finally
        //    {
        //        reader?.Close();
        //        pdfReader?.Close();
        //    }
        //}

        //public static byte[] UpdatePdfDoc(byte[] inputStream, Dictionary<string, Microsoft.Extensions.Primitives.StringValues> model)
        //{
        //    MemoryStream outStream = new MemoryStream();
        //    PdfReader pdfReader = null;
        //    PdfStamper pdfStamper = null;
        //    Stream inStream = null;
        //    try
        //    {

        //        var t1 = model.Values;

        //        // var feilds = GetFormFields(inputStream);
        //        pdfReader = new PdfReader(inputStream);
        //        pdfStamper = new PdfStamper(pdfReader, outStream);
        //        AcroFields form = pdfStamper.AcroFields;
        //        var fieldKeys = form.Fields.Keys;
        //        foreach (string fieldKey in fieldKeys)
        //        {
        //           var t =  model.Values;

        //            //Replace Address Form field with my custom data
        //            if (fieldKey.Equals("fill_1"))
        //            {
                      
        //               // form.SetField(fieldKey,);
        //            }
        //            if (fieldKey.Equals("fill_2"))
        //            {

        //                form.SetField(fieldKey, "17/03/2019");
        //            }
        //            if (fieldKey.Equals("fill_4"))
        //            {

        //                form.SetField(fieldKey, "Nasser O Mansor");
        //            }
        //            if (fieldKey.Equals("fill_5"))
        //            {

        //                form.SetField(fieldKey, "3456");
        //            }
        //            if (fieldKey.Equals("fill_6"))
        //            {

        //                form.SetField(fieldKey, "8977");
        //            }
        //            if (fieldKey.Equals("fill_7"))
        //            {

        //                form.SetField(fieldKey, "ABC company");
        //            }
        //            if (fieldKey.Equals("fill_8"))
        //            {

        //                form.SetField(fieldKey, "Maha Fesatwi");
        //            }
        //        }


        //        pdfStamper.FormFlattening = true;
        //        // pdfStamper.Close();
        //        return outStream.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        var msg = ex.Message;
        //        return null;
        //    }
        //    finally
        //    {
        //        pdfStamper?.Close();
        //        pdfReader?.Close();
        //        inStream?.Close();
        //    }

        //}


        //public static PdfCreator()
        //{
        //    public MemoryStream GetDocumentStream()
        //    {
        //        var ms = new MemoryStream();
        //        var doc = new Document();
        //        var writer = PdfWriter.GetInstance(doc, ms);
        //        writer.CloseStream = false;
        //        doc.Open();
        //        doc.Add(new Paragraph("Hello World"));
        //        doc.Close();
        //        writer.Close();
        //        return ms;
        //    }
        //}
    }
}
