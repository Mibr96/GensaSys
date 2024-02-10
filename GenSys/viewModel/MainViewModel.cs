using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FontAwesome.Sharp;
using LoginForm.Model;
using LoginForm.Repositories;


namespace LoginForm.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;

        //Properties
        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ViewModelBase CurrentChildView 
        { 

            get {
                return _currentChildView;
            }
            set { 
                    _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
                }
            }

        public string Caption
        {
            get
            {
               return _caption;
            }
            set {
                    _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        { 
            get
            {
               return _icon;
            }
            set {
                    _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
            }

        //Commands

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowPage2ViewCommand { get; }
        public ICommand ShowUserSettingsCommand {  get; }



        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();

            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowPage2ViewCommand = new ViewModelCommand(ExecuteShowPage2ViewCommand);
            ShowUserSettingsCommand = new ViewModelCommand(ExecuteUserSettingsCommand);

            //Default view
            ExecuteShowHomeViewCommand(null);

            LoadCurrentUserData();
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Home";
            Icon = IconChar.Home;
        }

        private void ExecuteShowPage2ViewCommand(object obj)
        {
            CurrentChildView = new Page2Model();
            Caption = "Enrolled Devices";
            Icon = IconChar.Laptop ;
        }

        private void ExecuteUserSettingsCommand(object obj)
        {
            CurrentChildView = new UserSettingsModel();
            Caption = "Settings";
            Icon = IconChar.Gear;
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = user.Name;
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }
    }
}
