using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace ALFREDPOS
{
    public class PaymentResult
    {
        public string TextResponse { get; set; }
        public string CmdStatus { get; set; }
        public string ApprovalCode { get; set; }
        public bool IsSuccess { get; set; }
        public string AuthCode { get; set; }
        public string AcctNo { get; set; }
        public string ExpDate { get; set; }
        public string RecordNo { get; set; }
        public string RefNo { get; set; }
        public string xmlRequest { get; set; }
    }

    public class PayPalPayment
    {
        public PaymentResult DoPayment(IDictionary<string,string> dict, string merchantId, string password)
        {
            Double amount = 0;
            string encBlk = "";
            string walletType = string.Empty;
            string walletInputType = string.Empty;

            if (dict.ContainsKey("AMOUNT"))
                amount = Convert.ToDouble(dict["AMOUNT"]);

            if (dict.ContainsKey("ENCRYPTEDBLOCK"))
                encBlk = dict["ENCRYPTEDBLOCK"];

            if (dict.ContainsKey("WALLETTYPE"))
                walletType = dict["WALLETTYPE"];

            if (dict.ContainsKey("WALLETINPUTTYPE"))
                walletInputType = dict["WALLETINPUTTYPE"];            

            return ProcessCardData(amount, merchantId, password, encBlk, walletType);
        }

        public PaymentResult ProcessCardData(double total, string merchantId, string password, string encryptedBlock, string walletType)
        {
            var client = new MPSPaymentWS.wsSoapClient();
            string xmlRequest = BuildRequestXml(total, merchantId, encryptedBlock, walletType);
            var response = new PaymentResult();
            response.xmlRequest = xmlRequest;
            return response;
        }

        private string BuildRequestXml(double total, string merchantId, string encryptedBlock, string walletType)
        {
            string xmlstring = string.Empty;
            var rnd = new Random(System.DateTime.Now.Millisecond);
            string invoice = rnd.Next(0, 100).ToString();

            using (var sw = new System.IO.StringWriter())
            {
                using (var w = new System.Xml.XmlTextWriter(sw))
                {

                    w.Formatting = System.Xml.Formatting.Indented;
                    w.Indentation = 10;

                    w.WriteStartDocument();
                    w.WriteStartElement("TStream");
                    w.WriteStartElement("Transaction");
                    w.WriteElementString("MerchantID", merchantId);
                    w.WriteElementString("OperatorID", "test");
                    w.WriteElementString("TranType", "Credit");
                    w.WriteElementString("TranCode", "Sale");
                    w.WriteElementString("InvoiceNo", invoice);
                    w.WriteElementString("RefNo", invoice);
                    w.WriteElementString("Memo", "AlfredPOS " + Assembly.GetExecutingAssembly().GetName().Version);
                    w.WriteElementString("AdditionalData", "PGl0ZW1zPg0KICAgPGl0ZW0+DQogICAgICA8bmFtZT5vcmFuZ2U8L25hbWU+DQogICAgICA8cHJpY2U+MS4yMjwvcHJpY2U+DQogICAgICA8cXVhbnRpdHk+MjwvcXVhbnRpdHk+DQogIDwvaXRlbT4NCiAgIDxpdGVtPg0KICAgICAgPG5hbWU+YXBwbGU8L25hbWU+DQogICAgICA8cHJpY2U+Mi4yMjwvcHJpY2U+DQogICAgICA8cXVhbnRpdHk+NTwvcXVhbnRpdHk+DQogIDwvaXRlbT4NCjwvaXRlbXM+");

                    w.WriteStartElement("Account");
                    w.WriteElementString("EncryptedFormat", walletType);
                    w.WriteElementString("EncryptedBlock", encryptedBlock);
                    w.WriteElementString("AccountSource", "2dBarCode");
                    w.WriteEndElement();

                    w.WriteStartElement("Amount");
                    w.WriteElementString("Purchase", string.Format("{0:0.00}", total));
                    w.WriteEndElement();

                    w.WriteEndElement();
                    w.WriteEndElement();
                    w.WriteEndDocument();
                    w.Close();

                }
                xmlstring = sw.ToString();
            }

            return xmlstring;
        }

        public PaymentResult ParsePaymentResponse(string response)
        {
            var result = new PaymentResult();

            try
            {
                string ret = string.Empty;
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(response);
                var root = xmlDoc.DocumentElement;
                string tempString = string.Empty;

                XmlNode node = root.SelectSingleNode("//RStream/CmdResponse/CmdStatus");

                if (node != null)
                {
                    result.CmdStatus = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/CmdResponse/TextResponse");

                if (node != null)
                {
                    result.TextResponse = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/TranResponse/AuthCode");
                if (node != null)
                {
                    result.AuthCode = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/TranResponse/AcctNo");
                if (node != null)
                {
                    result.AcctNo = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/TranResponse/ExpDate");
                if (node != null)
                {
                    result.ExpDate = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/TranResponse/RecordNo");
                if (node != null)
                {
                    result.RecordNo = node.InnerText;
                }

                node = root.SelectSingleNode("//RStream/TranResponse/RefNo");
                if (node != null)
                {
                    result.RefNo = node.InnerText;
                }

                if (result.CmdStatus.ToLower() == "success")
                {
                    result.IsSuccess = true;
                }
            }
            catch
            {
                result.IsSuccess = false;
            }

            return result;
        }
    }
}
