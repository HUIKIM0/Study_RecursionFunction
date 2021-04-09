using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study_RecursionFunction
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enControlType
        {
            UnKnown,
            Label,
            TextBox,
            Button,
        }


        // Level선택,Control선택,Text입력 -> 버튼 클릭 시 해당레벨의 해당컨트롤에 Text입력
        private void btnExe_Click(object sender, EventArgs e)
        {
            //레벨 선택
            int iLevel = (int)numLevel.Value;

            //컨트롤러 선택
            enControlType eControlType = enControlType.UnKnown;

            if (rdoLabel.Checked) eControlType = enControlType.Label;
            else if (rdoTextBox.Checked) eControlType = enControlType.TextBox;
            else if (rdoButton.Checked) eControlType = enControlType.Button;

            //텍스트박스 입력
            string strTextBox = tboxChangeText.Text;

            ControlSearch(gboxCheckList,iLevel, eControlType, strTextBox);

        }

        // gboxCheckList 찾기 -> Level에 해당하는 GroupBox찾기 -> 컨트롤 찾기 -> text작업 
        private void ControlSearch(GroupBox CheckList, int iLevel, enControlType eControlType, string strTextBox)
        {

            //  GroupBox 내에 있는 Control을 하나 씩 가져와서 체크 foreach. GroupBox 다 조사 후 Control작업
            foreach (var item in CheckList.Controls)
            {
                // Control 작업. 사용자 선택 Level과 일치하지 않으면 작업 X
                if (CheckList.Text.Equals("Level " + iLevel))
                {
                    switch (eControlType)
                    {
                        case enControlType.Label:
                            if (item is Label)
                            {
                                ((Label)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, Label Text : {1} 로 변경", CheckList.Text, strTextBox));
                            }
                            break;
                        case enControlType.TextBox:
                            if (item is TextBox)
                            {
                                ((TextBox)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, TextBox Text : {1} 로 변경", CheckList.Text, strTextBox));
                            }
                            break;
                        case enControlType.Button:
                            if (item is Button)
                            {
                                ((Button)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, Button Text : {1} 로 변경", CheckList.Text, strTextBox));
                            }
                            break;
                        default:
                            break;
                    }
                }

                // 처음 돌리면 여기먼저 온다(GroupBox가 젤 큰 박스인 Check List 이므로)
                // GroupBox안에 GroupBox가 또 있는 경우 또 다른 GroupBox를 기준으로 조사 ex)Check List였던 기준을 Level 1으로
                // 현재 돌고있는 함수 다시 호출.
                if (item is GroupBox)
                {
                    lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, 다음 조사 할 GroupBox : {1} 로 이동", gboxCheckList.Text, ((GroupBox)item).Text));
                    ControlSearch((GroupBox)item, iLevel, eControlType, strTextBox);
                }
            }

            if(gboxCheckList == CheckList)
            {
                lboxResult.Items.Add(string.Format("END"));
            }
        }
    }
}
