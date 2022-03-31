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

        // 사용자가 선택한 Level(GroupBox) 컨트롤(Label/TextBox/Button) 을 찾아서 선택한 GroupBox,Control은 싹 다 작업
        private void ControlSearch(GroupBox gboxCheck, int iLevel, enControlType eControlType, string strTextBox)
        {

            // 현재 GroupBox 안에 있는 Control을 하나 씩 가져오는 foreach.
            foreach (var item in gboxCheck.Controls)
            {

                // ★처음 실행해준 경우 gboxCheck.Text가 Check List 이기 때문에 이 if 문 패스한다★
                // Level 그룹 찾기
                if (gboxCheck.Text.Equals("Level " + iLevel))
                {
                    switch (eControlType)  //그룹 안의 Label,TextBox, Button 찾기
                    {
                        case enControlType.Label:
                            if (item is Label)
                            {
                                ((Label)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("작업 GroupBox : {0}, Label Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                                lboxResult.Items.Add("\n");
                            }
                            break;
                        case enControlType.TextBox:
                            if (item is TextBox)
                            {
                                ((TextBox)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("작업 GroupBox : {0}, TextBox Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                                lboxResult.Items.Add("\n");
                            }
                            break;
                        case enControlType.Button:
                            if (item is Button)
                            {
                                ((Button)item).Text = strTextBox;
                                lboxResult.Items.Add(string.Format("작업 GroupBox : {0}, Button Text : {1} 로 변경", gboxCheck.Text, strTextBox));
                                lboxResult.Items.Add("\n");
                            }
                            break;
                        default:
                            break;
                    }
                }

                // foreach로 꺼내온 컨트롤이 Label,TextBox,Button이 아닌 ★GroupBox라면
                // ex) GroupBox Level1의  Label 작업 완료 -> Level2로 가야함(새로운 GroupBox)
                // !재귀함수! ControlSearch함수 호출 시 다음 GroupBox를 새로운 기준으로 준다
                if (item is GroupBox)
                {

                    lboxResult.Items.Add(string.Format("현재 GroupBox : {0}, 다음 GroupBox : {1} 로 이동", gboxCheck.Text, ((GroupBox)item).Text));
                    ControlSearch((GroupBox)item, iLevel, eControlType, strTextBox);
                }

            }


            //함수에 처음 들고 들어온 값 gboxCheckList와 검색을 다 돌고 나온 gboxCheck가 같다면 끝이라 판단
            if(gboxCheckList == gboxCheck)
            {
                lboxResult.Items.Add(string.Format("END"));
                lboxResult.Items.Add(string.Format("\n"));
            }
        }


    }
}
