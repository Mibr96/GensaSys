using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginForm.CustomControls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {

        public static readonly DependencyProperty passwordProperty =
            DependencyProperty.Register("Password", typeof(SecureString),typeof(BindablePasswordBox));
        public SecureString Password
        {
            get{ return (SecureString)GetValue(passwordProperty); }
            set { SetValue(passwordProperty, value); }
             }
        public BindablePasswordBox()
        {
            InitializeComponent();
            userPassword.PasswordChanged += OnPasswordChanged;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = userPassword.SecurePassword;
        }
    }
}
