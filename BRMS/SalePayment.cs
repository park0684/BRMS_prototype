using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BRMS
{
    public partial class SalePayment : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        double exChange = 0;
        public event Action<int, decimal, bool> ForwardAmount;
        int empCode = 0;

        public SalePayment()
        {
            InitializeComponent();
            GetExchange();
            tBoxPaymnetKrw.KeyUp += KrwToUsdExchange;
            tBoxpaymentUsd.KeyUp += UsdToKrwExchange;
        }

        public double GetExchange()
        {
            string query = "SELECT cf_value FROM config WHERE cf_code = 1";
            object resultObj = new object();
            dbconn.sqlScalaQuery(query, out resultObj);
            if (!string.IsNullOrEmpty(resultObj.ToString()))
            {
                exChange = Convert.ToDouble(resultObj);
            }
            return exChange;
        }
        public void GetPaymntInfo(string name, int paymentKrw, double paymentUsd, double exchange)
        {
            exChange = exchange;
            lblName.Text = name;
            tBoxPaymnetKrw.Text = paymentKrw.ToString("#,##0");
            tBoxpaymentUsd.Text = paymentUsd.ToString("#,##0.00");
        }

        private void UsdToKrwExchange(object sener, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tBoxpaymentUsd.Text))
            {
                tBoxPaymnetKrw.Text = (double.Parse(tBoxpaymentUsd.Text) * exChange).ToString("#,##0");
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tBoxpaymentUsd.Text))
                {
                    tBoxPaymnetKrw.Text = (double.Parse(tBoxpaymentUsd.Text) * exChange).ToString("#,##0");
                }
            }
        }

        /// <summary>
        /// 결제 TextBox KRW -> USD로 전환
        /// </summary>
        /// <param name="sener"></param>
        /// <param name="e"></param>
        private void KrwToUsdExchange(object sener, KeyEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tBoxPaymnetKrw.Text))
            {
                tBoxpaymentUsd.Text = (double.Parse(tBoxPaymnetKrw.Text) / exChange).ToString("#,##0.00");
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(tBoxPaymnetKrw.Text))
                {
                    tBoxpaymentUsd.Text = (double.Parse(tBoxPaymnetKrw.Text) / exChange).ToString("#,##0.00");
                }
            }
        }
        private void ConfirmedAmount()
        {
            int resultKrw = Convert.ToInt32(tBoxPaymnetKrw.Text.Replace(",",""));
            decimal resultUsd = Convert.ToDecimal(tBoxpaymentUsd.Text.Replace(",",""));
            ForwardAmount?.Invoke(resultKrw, resultUsd, true);

        }
        private void bntSave_Click(object sender, EventArgs e)
        {
            ConfirmedAmount();
            Close();
        }

        private void bntCancle_Click(object sender, EventArgs e)
        {
            ForwardAmount?.Invoke(0, 0, false);
            Close();
        }
    }
}
