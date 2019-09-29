using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MailSender2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = Classes.VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
            cbSmtpSelect.ItemsSource = Classes.VariablesSmtp.Smtpserv;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValue = "Value";
            Classes.DBClass db = new Classes.DBClass();
            dgEmails.ItemsSource = db.Emails;
        }

        private void ExitMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnClock_Click(object sender, RoutedEventArgs e)
        {
            tbConrol.SelectedItem = tbPlanner;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strBody = BodyPost.Text;
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if(string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if(string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strBody))
            {
                MessageBox.Show("Письмо не заполнено");
                return;
            }
            Classes.EmailSendServiceClass emailSender = new Classes.EmailSendServiceClass(strLogin, strPassword);
             emailSender.SendMails((IQueryable<Classes.Email>)dgEmails.ItemsSource);
        }

        private void TabSwitcher_Back(object sender, RoutedEventArgs e)
        {
            if (tbConrol.SelectedIndex == 0) return;
            tbConrol.SelectedIndex--;
        }

        private void TabSwitcher_Forward(object sender, RoutedEventArgs e)
        {
            if (tbConrol.SelectedIndex == tbConrol.Items.Count - 1) return;
            tbConrol.SelectedIndex++;
        }
    }
}
