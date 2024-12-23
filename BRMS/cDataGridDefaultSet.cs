using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.IO;

namespace BRMS
{
    class cDataGridDefaultSet
    {
        public DataGridView Dgr { get; }
        public event EventHandler<DataGridViewCellEventArgs> CellDoubleClick;

        private ContextMenuStrip contextMenu;
        public cDataGridDefaultSet()
        {
            Dgr = new DataGridView();
            SetDefaultSetting();
            Dgr.CellDoubleClick += DataGridView_CellDoubleClick;
            contextMenu = new ContextMenuStrip();

        }

        private void SetDefaultSetting()
        {
            Dgr.BackgroundColor = Color.White;
            Dgr.EnableHeadersVisualStyles = false;
            Dgr.AllowUserToAddRows = false;
            Dgr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgr.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            Dgr.BackgroundColor = Color.White;
            Dgr.AllowUserToResizeRows = false;
            Dgr.RowHeadersVisible = false;
            Dgr.ColumnHeadersHeight = 35;
            Dgr.RowTemplate.Height = 25;
            Dgr.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(234, 238, 244);
            Dgr.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(234, 238, 244);
            Dgr.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, 212, 255);
            Dgr.DefaultCellStyle.SelectionForeColor = Color.Black;
            Dgr.SelectionMode = DataGridViewSelectionMode.CellSelect;
            Dgr.AllowUserToResizeRows = false;
            Dgr.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Dgr.DefaultCellStyle.Font = new Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            Dgr.Columns.Add("No", "No");
            Dgr.Columns["No"].Width = 50;
            Dgr.Columns["No"].ReadOnly = true;
            Dgr.Columns["No"].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
            Dgr.Columns["No"].DefaultCellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
            FormatAsStringCenter("No");
        }
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 이벤트를 불러온 클래스에서 이벤트 핸들러를 구현할 수 있도록 호출
            CellDoubleClick?.Invoke(sender, e);
        }
        

    
        public void ExcludeLastRowSort(DataGridViewCellMouseEventArgs e)
        {
            // 행이 없으면 바로 리턴
            if (Dgr.Rows.Count == 0)
            {
                return;
            }
            // sort 작동 불가 설정
            foreach (DataGridViewColumn column in Dgr.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }      

            // 마지막 행을 임시로 저장하고 삭제
            int lastRowIndex = Dgr.Rows.Count - 1;
            int columnIndex = e.ColumnIndex;
            DataGridViewRow lastRow = Dgr.Rows[lastRowIndex];
            Dgr.Rows.RemoveAt(lastRowIndex);

            var clickedColumn = Dgr.Columns[e.ColumnIndex];
            DataGridViewColumn previousSortedColumn = Dgr.SortedColumn;
            SortOrder previousSortOrder = Dgr.SortOrder;
            // DataGridView의 현재 정렬 상태를 확인
            if (previousSortedColumn == clickedColumn)
            {
                // 정렬된 열이 클릭한 열과 동일한 경우
                if (previousSortOrder == SortOrder.Ascending)
                {
                    Dgr.Sort(Dgr.Columns[e.ColumnIndex], ListSortDirection.Descending);
                }
                else if (previousSortOrder == SortOrder.Descending)
                {
                    Dgr.Sort(Dgr.Columns[e.ColumnIndex], ListSortDirection.Ascending);
                }
            }
            else
            {
                Dgr.Sort(Dgr.Columns[e.ColumnIndex], ListSortDirection.Descending);
            }           
            Dgr.Rows.Add(lastRow);
        }
        /// <summary>
        /// 컬럼 사이즈 조절 함수
        /// </summary>
        public void ApplyDefaultColumnSettings()
        {
            foreach (DataGridViewColumn column in Dgr.Columns)
            {
                column.HeaderCell.Style.WrapMode = DataGridViewTriState.False; // 줄바꿈 방지
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;  // 셀 크기 자동 조정
            }

            // 그리드 전체 크기 자동 조정 모드 설정
            Dgr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells; 
        }

        public void ExportToExcel()
        {
            DataTable exportData = new DataTable();
            cExport export = new cExport();
            // 1. DataGridView의 컬럼 헤더를 DataTable의 컬럼으로 추가
            foreach (DataGridViewColumn column in Dgr.Columns)
            {
                if (column.Visible)  // 숨겨진 컬럼은 제외
                {
                    exportData.Columns.Add(column.HeaderText);
                }
            }
            DataRow headerRow = exportData.NewRow();
            foreach (DataGridViewColumn column in Dgr.Columns)
            {
                if (column.Visible)  // 숨겨진 컬럼은 제외
                {
                    headerRow[column.HeaderText] = column.HeaderText; // 첫 번째 행에 컬럼명 추가
                }
            }
            exportData.Rows.Add(headerRow);
            // 2. DataGridView의 각 행 데이터를 DataTable에 추가
            foreach (DataGridViewRow row in Dgr.Rows)
            {
                DataRow dataRow = exportData.NewRow();

                foreach (DataGridViewColumn column in Dgr.Columns)
                {
                    if (column.Visible)  // 숨겨진 컬럼은 제외
                    {
                        dataRow[column.HeaderText] = row.Cells[column.Index].Value ?? DBNull.Value;
                    }
                }

                exportData.Rows.Add(dataRow);
            }

            // 3. DataTable을 Excel로 내보내기
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                Form parentForm = Dgr.FindForm();
                string formText = parentForm.Text;
                saveFileDialog.Filter = "Execl Files (*.xlsx)|*.xlsx|Allfils(*.*)|*.*";
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName =$"{DateTime.Now.ToString("yyyyMMddHHmm")}({formText})";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    export.ExportDataToExcelNoneColumn(exportData, filePath);

                }
            }

        }
        
        /// <summary>
        /// 데이터 그리드 구조 정수형
        /// </summary>
        /// <param name="columnNames"></param>
        public void FormatAsInteger(params  string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Format = "#,###;#,###;0";
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgr.Columns[columnName].MinimumWidth = 80;
                }
            }
            
        }
        public void FormatAsDecimal(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Format = "#,###.##;#,###.##;0";
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    Dgr.Columns[columnName].MinimumWidth = 80;
                }
            }
        }

        public void FormatAsStringLeft(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }
        public void FormatAsStringRight(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

        }
        public void FormatAsStringCenter(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            
        }
        public void FormatAsDate(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgr.Columns[columnName].MinimumWidth = 120;
                    Dgr.Columns[columnName].DefaultCellStyle.Format = "yyyy년MM월dd일";
                }
            }
        }
        public void FormatAsDateTime(params string[] columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (Dgr.Columns.Contains(columnName))
                {
                    Dgr.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgr.Columns[columnName].MinimumWidth = 120;
                    Dgr.Columns[columnName].DefaultCellStyle.Format = "yyyy년MM월dd일 hh시mm분";
                }
            }
        }
        public decimal ConvertToDecimal(object value)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Convert the value to a string, remove any commas, and then convert to int
                string valueAsString = value.ToString().Replace(",", ""); // 콤마 제거
                if (decimal.TryParse(valueAsString, out decimal number))
                {
                    return number;
                }
            }
            return 0;
        }

        public int ConvertToInt(object value)
        {
            if(value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                // Convert the value to a string, remove any commas, and then convert to int
                string valueAsString = value.ToString().Replace(",", ""); // 콤마 제거
                if (decimal.TryParse(valueAsString, out decimal number))
                {
                    return (int)number;
                }
            }
            return 0;
        }
    }
}
