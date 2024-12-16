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
    public partial class SaleOrderList : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        cDataGridDefaultSet OrderList = new cDataGridDefaultSet();
        int empCode = 0;
        public SaleOrderList()
        {
            InitializeComponent();
            panelDatagrid.Controls.Add(OrderList.Dgr);
            OrderList.Dgr.Dock = DockStyle.Fill;
            ComboBoxSetting();
            GridForm();
        }

        private void ComboBoxSetting()
        {
            cmBoxDateType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmBoxDateType.Items.AddRange(new string[] { "등록일", "수정일", "판매일" });
            cmBoxStatus.Items.Add(new KeyValuePair<int, string>(3, "전체"));
            cmBoxStatus.Items.Add(new KeyValuePair<int, string>(1, "주문"));
            cmBoxStatus.Items.Add(new KeyValuePair<int, string>(2, "판매"));
            cmBoxStatus.Items.Add(new KeyValuePair<int, string>(0, "취소"));
            cmBoxDateType.SelectedIndex = 0;
            cmBoxStatus.SelectedValue = 1; 
        }

        private void GridForm()
        {
            OrderList.Dgr.Columns.Add("orderCode", "주문서코드");
            OrderList.Dgr.Columns.Add("orderStatus", "상태");
            OrderList.Dgr.Columns.Add("orderDate", "주문일");
            OrderList.Dgr.Columns.Add("orderSaleDate", "판매일");
            OrderList.Dgr.Columns.Add("orderCustomer", "회원");
            OrderList.Dgr.Columns.Add("orderAmount", "주문액");
            OrderList.Dgr.Columns.Add("orderStaff", "담당자");
            OrderList.Dgr.Columns.Add("orderNote", "메모");
            

            OrderList.Dgr.ReadOnly = true;
            OrderList.Dgr.Columns["orderCode"].Visible = false;
            //포멧 설정
            OrderList.FormatAsStringLeft("회원");
            OrderList.FormatAsStringCenter("상태", "담당자", "메모");
            OrderList.FormatAsInteger("주문액");
            OrderList.FormatAsDateTime("주문일");
            OrderList.FormatAsDate("판매일");
        }

        private void bntCustomer_Click(object sender, EventArgs e)
        {

        }

        private void bntOrderReg_Click(object sender, EventArgs e)
        {

        }
    }
}
