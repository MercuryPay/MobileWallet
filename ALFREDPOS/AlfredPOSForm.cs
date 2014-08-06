using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace ALFREDPOS
{
    public partial class AlfredPOSForm : Form
    {
        delegate void SetTextCallback(string text);

        public AlfredPOSForm()
        {
            InitializeComponent();
            mobileWalletWidget.DataReady += mobileWalletWidget_DataReady;
            lblProgress.Text = string.Empty;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double amount = 0;
            lblProgress.Text = "CALL WALLET HOST";
            txtResponse.Text = string.Empty;

            if (double.TryParse(txtAmount.Text, out amount))
            {
                mobileWalletWidget.AddAttribute("MERCHANTID", ConfigurationManager.AppSettings["MERCHANTID"]);
                mobileWalletWidget.AddAttribute("PASSWORD", ConfigurationManager.AppSettings["PASSWORD"]);
                mobileWalletWidget.AddAttribute("WHURL", ConfigurationManager.AppSettings["WHURL"]);
                mobileWalletWidget.AddAttribute("COMPLETEURL", ConfigurationManager.AppSettings["COMPLETEURL"]);
                mobileWalletWidget.AddAttribute("AMOUNT", txtAmount.Text);
                var resp = mobileWalletWidget.Navigate();
                if (!String.IsNullOrEmpty(resp))
                {
                    //there was an error
                    MessageBox.Show(resp);
                }
            }
            else
            {
                MessageBox.Show("Invalid Amount");
            }
        }

        private void SetText(string text)
        {
            if (this.txtResponse.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtResponse.Text = text;
            }
        }

        void mobileWalletWidget_DataReady(string data)
        {
            lblProgress.Text = "DATA RECIEVED";
            SetText(data);

            if (String.IsNullOrEmpty(data))
            {
                MessageBox.Show("No data returned.");
                return;
            }

            var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

            if (dict == null || dict.Count == 0)
            {
                MessageBox.Show("No JSON data parsed.");
                return;    
            }

            if (dict["STATUS"] == "ERROR")
            {
                MessageBox.Show(dict["MESSAGE"]);
                return;
            }

            if (dict.ContainsKey("WALLETINPUTTYPE") && dict.ContainsKey("WALLETTYPE"))
            {
                if ((dict["WALLETINPUTTYPE"] == "QR") && dict["WALLETTYPE"] == "WellsFargo")
                {
                    //simulate qr code read by stuffing a value in encrypted Block
                    if (dict.ContainsKey("ENCRYPTEDBLOCK"))
                    {
                        dict["ENCRYPTEDBLOCK"] = ";5004273327621157534=92051717061615938";
                    }
                    else
                    {
                        dict.Add("ENCRYPTEDBLOCK", ";5004273327621157534=92051717061615938");
                    }
                }
            }
            
            var payment = new PayPalPayment();
            lblProgress.Text = "PROCESSING...";
            var result = payment.DoPayment(dict, ConfigurationManager.AppSettings["MERCHANTID"],
                ConfigurationManager.AppSettings["PASSWORD"]);
            lblProgress.Text = "PAYMENT COMPLETE";
            txtTranResults.Text = result.xmlRequest;           
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "9";
        }

        private void btnbs_Click(object sender, EventArgs e)
        {
            txtAmount.Text = txtAmount.Text.Substring(0, txtAmount.Text.Length - 1);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtAmount.Text += "0";
        }

        private void btnperiod_Click(object sender, EventArgs e)
        {
            txtAmount.Text += ".";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAmount.Text = string.Empty;
        }
    }
}
