using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BRMS
{
    public partial class CategoryTreeView : Form
    {
        cDatabaseConnect dbconn = new cDatabaseConnect();
        DataTable resultData = new DataTable();
        public event Action<string, string, string> CategorySelected;
        string catTop = "0";
        string catMid = "0";
        string catBot = "0";
        public CategoryTreeView()
        {
            InitializeComponent();
            AddCategoriesToTreeView();
        }

        private void AddCategoriesToTreeView()
        {
            string query = "SELECT cat_code,cat_top,cat_mid,cat_bot,cat_name_kr,cat_name_en FROM category ORDER BY cat_top,cat_mid,cat_bot";
            //DataTable resultData = new DataTable();
            dbconn.SqlDataAdapterQuery(query, resultData);
            DataView dataView = new DataView(resultData);

            // 최상위 노드 추가
            TreeNode rootNode = new TreeNode("전체");
            treeViewCategory.Nodes.Add(rootNode);
            rootNode.Expand();
            // 대분류만 가져와서 추가
            DataView topLevelDataView = new DataView(resultData);
            topLevelDataView.RowFilter = "cat_mid = 0 AND cat_bot = 0";

            foreach (DataRowView topLevelRow in topLevelDataView)
            {
                int catTop = Convert.ToInt32(topLevelRow["cat_top"]);
                int catMid = Convert.ToInt32(topLevelRow["cat_mid"]);
                int catBot = Convert.ToInt32(topLevelRow["cat_bot"]);
                int catCode = Convert.ToInt32(topLevelRow["cat_code"]);

                TreeNode topLevelNode = new TreeNode(string.Format("{0}({1})", topLevelRow["cat_name_kr"].ToString(), topLevelRow["cat_name_en"].ToString()));
                topLevelNode.Tag = new CategoryInfo(catCode, catTop, catMid, catBot);
                rootNode.Nodes.Add(topLevelNode);

                // 중분류 가져와서 추가
                DataView midLevelDataView = new DataView(resultData);
                midLevelDataView.RowFilter = $"cat_top = {catTop} AND cat_mid > 0 AND cat_bot = 0";

                foreach (DataRowView midLevelRow in midLevelDataView)
                {
                    int midCatCode = Convert.ToInt32(midLevelRow["cat_code"]);
                    int midCatMid = Convert.ToInt32(midLevelRow["cat_mid"]);
                    int midCatBot = Convert.ToInt32(midLevelRow["cat_bot"]);

                    TreeNode midLevelNode = new TreeNode(string.Format("{0}({1})", midLevelRow["cat_name_kr"].ToString(), midLevelRow["cat_name_en"].ToString()));
                    midLevelNode.Tag = new CategoryInfo(midCatCode, catTop, midCatMid, midCatBot);
                    topLevelNode.Nodes.Add(midLevelNode);

                    // 소분류 가져와서 추가
                    DataView botLevelDataView = new DataView(resultData);
                    botLevelDataView.RowFilter = $"cat_top = {catTop} AND cat_mid = {midCatMid} AND cat_bot > 0";

                    foreach (DataRowView botLevelRow in botLevelDataView)
                    {
                        int botCatCode = Convert.ToInt32(botLevelRow["cat_code"]);
                        int botCatBot = Convert.ToInt32(botLevelRow["cat_bot"]);

                        TreeNode botLevelNode = new TreeNode(string.Format("{0}({1})", botLevelRow["cat_name_kr"].ToString(), botLevelRow["cat_name_en"].ToString()));
                        botLevelNode.Tag = new CategoryInfo(botCatCode, catTop, midCatMid, botCatBot);
                        midLevelNode.Nodes.Add(botLevelNode);
                    }
                }
            }
        }

        private void TreeViewCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 선택한 노드의 데이터를 가져옴
            if (e.Node != null)
            {
                CategoryInfo selectedCategory = e.Node.Tag as CategoryInfo;

                if (selectedCategory != null)
                {
                    catTop = selectedCategory.CatTop.ToString();
                    catMid = selectedCategory.CatMid.ToString();
                    catBot = selectedCategory.CatBot.ToString();

                    // 이제 catTop, catMid, catBot 변수에 선택한 카테고리의 정보가 들어 있습니다.
                    // 여기서 원하는 동작을 수행하면 됩니다.
                    // 예시로 출력
                    //MessageBox.Show($"Selected Category Info:\nCat Top: {catTop}\nCat Mid: {catMid}\nCat Bot: {catBot}");
                }
            }
        }

        private class CategoryInfo
        {
            public int CatCode { get; }
            public int CatTop { get; }
            public int CatMid { get; }
            public int CatBot { get; }

            public CategoryInfo(int catCode, int catTop, int catMid, int catBot)
            {
                CatCode = catCode;
                CatTop = catTop;
                CatMid = catMid;
                CatBot = catBot;
            }
        }

        private void bntOk_Click(object sender, EventArgs e)
        {
            if (treeViewCategory.SelectedNode != null)
            {
                TreeViewCategory_AfterSelect(treeViewCategory, new TreeViewEventArgs(treeViewCategory.SelectedNode));
                //productInfo.GetCategory(catTop, catMid, catBot);
                CategorySelected?.Invoke(catTop, catMid, catBot);
                Close();
            }
        }

        private void bntClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
