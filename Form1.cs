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

        // ★ GroupBox찾기 -> Control찾기 -> 해당 Level의 Control 싹 다 text작업
        private void ControlSearch(GroupBox gboxCheck, int iLevel, enControlType eControlType, string strTextBox)
        {

            // 현재 GroupBox 안에 있는 Control을 하나 씩 가져오는 foreach. 
            foreach (var item in gboxCheck.Controls)
            {
                // Control이 GroupBox일때 GroupBox.Text가 사용자가 선택한 Level 과 일치하지 않으면 작업 X
                // ex) Level 1 선택 => gboxCheck.Text 가 Level 1 이여야 Level 1 Text 입력 작업. Level 2~3인 gboxCheck는 if문 패스

                // ★처음 실행해준 경우 gboxCheck.Text가 Check List 이기 때문에 이 if 문 패스한다★
                if (gboxCheck.Text.Equals("Level " + iLevel))
                {
                    switch (eControlType)  //Label,TextBox, Button
                    {
                        case enControlType.Label:
                            if (item is Label)
                            {
                                ((Label)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, Label Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                            }
                            break;
                        case enControlType.TextBox:
                            if (item is TextBox)
                            {
                                ((TextBox)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, TextBox Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                            }
                            break;
                        case enControlType.Button:
                            if (item is Button)
                            {
                                ((Button)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, Button Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                            }
                            break;
                        default:
                            break;
                    }
                }

                // 사용자가 선택한 Level과는 안 맞지만  foreach로 꺼내온 컨트롤이 GroupBox라면
                // 현재 돌고있는 함수 다시 호출. 재귀함수. ControlSearch함수 호출 시 다음 GroupBox를 새로운 기준으로 준다
                if (item is GroupBox)
                {
                    lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, 다음 GroupBox : {1} 로 이동", gboxCheck.Text, ((GroupBox)item).Text));
                    ControlSearch((GroupBox)item, iLevel, eControlType, strTextBox);
                }

            }

            if(gboxCheckList == gboxCheck)
            {
                lboxResult.Items.Add(string.Format("END"));
                lboxResult.Items.Add(string.Format("\n"));
            }
        }
    }
}
