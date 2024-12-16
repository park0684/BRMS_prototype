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
    public partial class EmployeePermission : Form
    {
        Dictionary<int, int> updatedPermissions = new Dictionary<int, int>();
        public event Action<Dictionary<int, int>> PermissionsUpdated;
        //public Dictionary<int, int> UpdatedPermissions => updatedPermissions;
        public EmployeePermission()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = false;
            MaximizeBox = false;
            SetPermissionCheckBox();
            
        }
        public void GetPermission(Dictionary<int,int> empPermission)
        {
            foreach(CheckBox checkBox in pnlPermission.Controls)
            {
                int permissionKey = (int)checkBox.Tag;

                if (empPermission.TryGetValue(permissionKey, out int status))
                {
                    checkBox.Checked = status == 1; // 상태가 1이면 체크
                }
                else
                {
                    checkBox.Checked = false; // 딕셔너리에 없으면 기본값으로 체크 해제
                }
            }
        }
        private void SetPermissionCheckBox()
        {
            int yPosition = 10;
            pnlPermission.BorderStyle = BorderStyle.Fixed3D;
            // cStatus 클래스에서 EmployeePermission을 사용하여 체크박스 생성
            foreach (var permission in cStatusCode.EmployeePermission)
            {
                CheckBox checkBox = new CheckBox
                {
                    Text = permission.Value,
                    Tag = permission.Key, // 권한 키값을 Tag에 저장
                    Location = new Point(10, yPosition),
                    AutoSize = true
                };
                pnlPermission.Controls.Add(checkBox);
                yPosition += 30; // 다음 항목 위치
            }
            pnlPermission.AutoScroll = true;
        }
        private Dictionary<int, int> GetUpdatedPermissions()
        {
            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                int permissionKey = (int)checkBox.Tag;
                int status = checkBox.Checked ? 1 : 0; // 체크되었으면 1, 아니면 0

                if (updatedPermissions.ContainsKey(permissionKey))
                {
                    updatedPermissions[permissionKey] = status; // 이미 있으면 업데이트
                }
                else
                {
                    updatedPermissions.Add(permissionKey, status); // 새 항목 추가
                }
            }

            return updatedPermissions;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            GetUpdatedPermissions();
            PermissionsUpdated?.Invoke(updatedPermissions);
            Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToggleCheckBox(int code)
        {
            bool allChecked = true;

            
            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                if (checkBox.Tag.ToString().Substring(2) == code.ToString())
                {
                    if (!checkBox.Checked)
                    {
                        allChecked = false;
                        break; // 하나라도 체크가 안 되어 있으면 중단
                    }
                }
            }

            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                if (checkBox.Tag.ToString().Substring(2) == code.ToString())
                {
                    checkBox.Checked = !allChecked;
                }
            }
        }
        private void btnCheckViewOnly_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                checkBox.Checked = false;
            }

            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                
                if (checkBox.Tag.ToString().Substring(2) == "1") // "제품 조회"의 Key 값이 101
                {
                    checkBox.Checked = true;
                }
                else
                {
                    checkBox.Checked = false;
                }
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                checkBox.Checked = true;
            }
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (CheckBox checkBox in pnlPermission.Controls)
            {
                checkBox.Checked = false;
            }
        }

        private void btnTogglePrint_Click(object sender, EventArgs e)
        {
            ToggleCheckBox(3);
        }

        private void btnToggleExcel_Click(object sender, EventArgs e)
        {
            ToggleCheckBox(4);
        }
    }
}
