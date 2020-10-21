
using Models.Interface.Repository;
using Models.Ofx;
using Models.Result;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;




namespace DataAcessMock
{
    public class DataAcessOFX : BaseDataAcess, IRepositoryOFX
    {


        #region CRUD
        public ValidResult<List<OfxModel>> Get(string file, string origem)
        {
            var content = "";
            string sample = "";            
            var ofxs = new List<OfxModel>();
            string formatString = "yyyyMMddHHmmss";

            var valid = new ValidResult<List<OfxModel>>
            {
                Status = true,
                Value = new List<OfxModel>(),
                Message = ""
            };
            try
            {
                var fullFile = $"{new resources().GetFilePath(origem)}{file}";

                // Create a reader to read the broken file
                using (var reader = new StreamReader(fullFile))
                {
                    // Read the whole file into a string
                    content = reader.ReadToEnd();
                    //
                    var position = content.IndexOf("<STMTTRN>");
                    //
                    var xmlTemp = content.Substring((position - 1)).Trim();
                    // 
                    var end = xmlTemp.IndexOf("</BANKTRANLIST>");
                    //
                    var opt = xmlTemp.Substring(0, end);
                    //
                    var ops = RemoveTag(opt);

                    foreach (var item in ops)
                    {
                        if (!string.IsNullOrEmpty(item.Trim()))
                        {
                            var el = item.Split(',');

                            sample = el[1].Split('[')[0];

                            ofxs.Add(new OfxModel
                            {
                                DTPOSTED = DateTime.ParseExact(sample, formatString, null),
                                //FITID = elemlist[i].SelectSingleNode("FITID").InnerText,
                                MEMO = el[3].Trim(),
                                TRNAMT = Convert.ToDecimal(el[2].Trim()),
                                TRNTYPE = el[0].Trim()
                            });

                        }
                    }
                }

                valid.Value = ofxs;

            }
            catch (Exception ex)
            {
                valid.Status = false;
                valid.Message = MSGErro(ex.Message, file);                
            }
            return valid;
        }
        #endregion

        private void ReadXML() {

            //xmlDoc.LoadXml(xmlTemp);

            //XmlElement root = xmlDoc.DocumentElement;

            //XmlNodeList elemlist = root.GetElementsByTagName("STMTTRN");

            //for (int i = 0; i < elemlist.Count; i++)
            //{
            //    sample = elemlist[i].SelectSingleNode("DTPOSTED").InnerText.Split('[')[0];

            //    ofxs.Add(new OfxModel
            //    {
            //        DTPOSTED = DateTime.ParseExact(sample, formatString, null),
            //        FITID = elemlist[i].SelectSingleNode("FITID").InnerText,
            //        MEMO = elemlist[i].SelectSingleNode("MEMO").InnerText,
            //        TRNAMT = Convert.ToDecimal(elemlist[i].SelectSingleNode("TRNAMT").InnerText),
            //        TRNTYPE = elemlist[i].SelectSingleNode("TRNTYPE").InnerText
            //    });
            //}
        }
        private List<string> RemoveTag(string elment) {

            elment = elment.Replace("<STMTTRN>", "");
            elment = elment.Replace("</STMTTRN>", ";");

            elment = elment.Replace("<TRNTYPE>", "");
            elment = elment.Replace("</TRNTYPE>", "");

            elment = elment.Replace("<DTPOSTED>", ",");
            elment = elment.Replace("</DTPOSTED>", "");

            elment = elment.Replace("<TRNAMT>", ",");
            elment = elment.Replace("</TRNAMT>", "");

            elment = elment.Replace("<MEMO>", ",");
            elment = elment.Replace("</MEMO>", "");

            
            return elment.Split(';').ToList();

        }
        public string ReadMessage(string response)
        {
            /*
             * https://dev.gerencianet.com.br/docs/relatorio-ofx
             * https://csharp.hotexamples.com/pt/site/file?hash=0xea2822ed605a66e714fd3b8325979d3cd52c577b8a82fd9123f0c41e4515aeb2&fullName=Program.cs&project=hymerman/ofxtools
             * regex = '|<STMTTRN><TRNTYPE>(.+?)<DTPOSTED>(.+?)<TRNAMT>(.+?)<FITID>(.+?)<NAME>(.+?)<MEMO>(.+?)</STMTTRN>|';

              */
            return null;

        }


    }




}
