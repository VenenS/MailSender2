using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using MailSender2.Services;
using MailSender2.Classes;
using GalaSoft.MvvmLight.Command;

namespace MailSender2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            EmailInfo = new Email();
            ReadAllCommand = new RelayCommand(GetEmails);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
        }

        ObservableCollection<Email> _Emails;
        IDataAccessService _serviceProxy;
        public RelayCommand ReadAllCommand { get; set; }
        Email _EmailInfo;

        public ObservableCollection<Email> Emails
        {
            get { return _Emails; }
            set
            {
                _Emails = value;
                RaisePropertyChanged(nameof(Emails));
            }
        }
        void GetEmails()
        {
            Emails.Clear();
            foreach(var item in _serviceProxy.GetEmails())
            {
                Emails.Add(item);
            }
        }
        public Email EmailInfo
        {
            get { return _EmailInfo; }
            set
            {
                _EmailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }
        
        void SaveEmail(Email email)
        {
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if(EmailInfo.Id!=0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }
    }
}