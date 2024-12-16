using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace BRMS
{
    class cExport
    {
        public void ExportDataToExcelNoneColumn(DataTable dataTable, string filePath)
        {
            try
            {
                var execlApp = new Excel.Application();
                var Book = execlApp.Workbooks.Add();
                var Sheet = Book.Sheets[1] as Excel.Worksheet;


                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        Sheet.Cells[i + 1, j + 1] = dataTable.Rows[i][j];
                    }
                }
                Sheet.Columns.AutoFit();
                // 확인을 받아서 저장할지 여부 결정
                bool shouldSave = fileOverWrite(filePath);
                if (shouldSave)
                {
                    // 파일 저장
                    Book.SaveAs(filePath);
                }
                Book.Close();
                execlApp.Quit();
                MessageBox.Show("파일 저장이 완료 되었습니다", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(Book);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(execlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일을 저장하는 동안 오류가 발생했습니다.\n오류 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool fileOverWrite(string filePath)
        {
            if (File.Exists(filePath))
            {
                DialogResult result = MessageBox.Show(
            "동일한 이름의 파일이 이미 존재합니다. 덮어쓰시겠습니까?",
            "파일 덮어쓰기 확인",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning);
                switch (result)
                {
                    case DialogResult.Yes:
                        return true; // 덮어쓰기
                    case DialogResult.No:
                        // 다른 이름으로 저장 로직 추가 가능
                        // 이 예제에서는 사용자에게 다른 이름으로 저장하도록 요청
                        using (var saveFileDialog = new SaveFileDialog())
                        {
                            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                            saveFileDialog.DefaultExt = "xlsx";
                            saveFileDialog.AddExtension = true;
                            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(filePath) + "_New"; // 기본 파일 이름

                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                return true; // 다른 이름으로 저장
                            }
                            else
                            {
                                return false; // 저장 취소
                            }
                        }
                    case DialogResult.Cancel:
                        return false; // 저장 취소
                }
            }
            return true; // 파일이 존재하지 않으면 덮어쓰기 없이 저장
        }
    }
}
