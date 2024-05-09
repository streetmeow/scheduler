﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp
{
    public partial class Schedule : Form
    {
        private StartUI startUI;
        private bool IsMake;

        public string detail;
        public string file;
        public string fileFullPath;
        public string time;
        public string alarmTime;
        
        private List<string> LinkList;
        private List<string> ProgramList;
        private List<Image> ScreenShotList;

        private DateTime ScheduleTime;
        public Schedule(StartUI startUI)
        {
            InitializeComponent();
            this.startUI = startUI;
            InitSchedule();
            InitComboBox();
        }

        private void InitSchedule()
        {
            ScreenShotList = new List<Image>();
            LinkList = new List<string>();
            ProgramList = new List<string>();
            detail = "no detail";
            file = "no file";
            fileFullPath = "no file";
            ScheduleTime = startUI.GetDate();
            MakeCheckBox();
            IsMake = true;
        }
        #region public method
        public void InitSchedule(Schedule saveSchedule)
        {
            detail = saveSchedule.detail;
            file = saveSchedule.file;
            fileFullPath = saveSchedule.fileFullPath;
            LinkList = saveSchedule.LinkList;
            ScreenShotList = saveSchedule.ScreenShotList;
            alarmTime = saveSchedule.alarmTime;
            time = saveSchedule.time;
            IsMake = false;
            /////////////////////////////
            ShowLink();
            ContentText.Text = detail;
            comboBox.Text = alarmTime;
            textBox1.Text = time;
            fileName.Text = file;
            ScheduleTime = saveSchedule.ScheduleTime;
        }
        public void AddLink(string link) => LinkList.Add(link);
        public void AddProgram(string program) => ProgramList.Add(program);

        public void ShowLink()
        {
            for (int i = 0; i < LinkList.Count; i++)
            {
                LinkPopUpButton.Text += LinkList[i] + "  ";
            }
        }

        public DateTime GetScheduleTime() => ScheduleTime;

        #endregion
        private void InitComboBox()
        {
            comboBox.Items.Add("5분 전");
            comboBox.Items.Add("10분 전");
            comboBox.Items.Add("30분 전");
        }
        private void MakeCheckBox()
        {
            Screen[] screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                CheckBox screenCheckBox = new CheckBox();
                screenCheckBox.Text = "Screen " + (i + 1);
                screenCheckBox.Location = new Point(140 + i * 150, 120);
                screenCheckBox.Size = new Size(100, 30);
                this.Controls.Add(screenCheckBox);
            }
        }

        private void TakeScreenShot()
        {
            /*
            Screen[] screens = Screen.AllScreens;
            for (int i = 0; i < screens.Length; i++)
            {
                Bitmap bitmap = new Bitmap(screens[i].Bounds.Width, screens[i].Bounds.Height);
                Graphics graphics = Graphics.FromImage(bitmap);
                Image img = Clipboard.GetImage();

                graphics.CopyFromScreen(screens[i].Bounds.X, screens[i].Bounds.Y, 0, 0, screens[i].Bounds.Size);
                Clipboard.SetImage(bitmap);
                graphics.Dispose();
                ScreenShotList.Add(img);
            }
            */
        }

        private void CompletedButton_Click(object sender, EventArgs e)
        {
            //작성 완료 버튼 - test ver
            Debug.WriteLine(IsMake);
            if (IsMake)
            {
                detail = ContentText.Text;
                startUI.AddSchedule(this);
                startUI.LoadMemoAndSchedule(startUI.GetDate());
            }         
            Close();
        }

        private void LinkPopUpButton_Click(object sender, EventArgs e)
        {
            //링크 버튼 클릭 시 링크 선택 창 띄우기
            PopUp popUp = new PopUp(this);
            popUp.Show();

        }

        private void FilePopUPButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "모든 파일(*.*)|*.*";
            openFileDialog.Title = "파일 선택";

            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog.SafeFileName;
                fileFullPath = openFileDialog.FileName;
                fileName.Text = file;
            }
        }

        private void ApplicationButton_Click(object sender, EventArgs e)
        {
            ProgramPopUp programPopUp = new ProgramPopUp(this);
            programPopUp.Show();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            alarmTime = comboBox.SelectedItem.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            time = textBox1.Text;
        }
    }
}