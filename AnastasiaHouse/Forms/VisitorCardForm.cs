﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnastasiaHouse.Forms
{
    public partial class VisitorCardForm : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public VisitorCardForm(Visitor visitor)
        {
            InitializeComponent();
            FullNameLabel.Text = $"{visitor.SecondName} {visitor.FirstName} {visitor.Patronymic}";
            BirthdateLabel.Text = visitor.Birthdate.ToString("dd.MM.yyyy");
            PayComboBox.SelectedItem = visitor.Pay;
            AmountDaysNumeric.Value = visitor.DaysAmount;
            TakeAnimalsCheckBox.Checked = visitor.TakeAmimals;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VisitorCardForm_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
