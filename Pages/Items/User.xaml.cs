using ChatStudents_Alikin.Classes.Common;
using ChatStudents_Alikin.Models;
using System.Printing;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace ChatStudents_Alikin.Pages.Items
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        Users user;
        Main main;
        public User(Users user, Main main)
        {
            InitializeComponent();
            this.main = main;
            this.user = user;
            imgUser.Source = BitmapFromArrayByte.LoadImage(user.Photo);
            FIO.Content = user.ToFIO();
            imgOnline.Visibility = user.IsOnline ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
        }
        private void SelectChat(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            main.SelectUser(user);
        }
    }
}
