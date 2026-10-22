using ChatStudents_Alikin.Classes;
using ChatStudents_Alikin.Models;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ChatStudents_Alikin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public string srcUserImage = "";
        UsersContext usersContext = new UsersContext();
        public Login()
        {
            InitializeComponent();
        }
        public bool CheckEmpty(string Pattern, string Input)
        {
            Match m = Regex.Match(Input, Pattern);
            return m.Success;
        }
        private void Continue(object sender, RoutedEventArgs e)
        {
            if (!CheckEmpty("^[A-ЯЁ][а-яА-Яё]*$", Lastname.Text))
            {
                MessageBox.Show("Укажите фамилию.");
                return;
            }

            if (!CheckEmpty("^[A-ЯЁ][а-яА-Яё]*$", Firstname.Text))
            {
                MessageBox.Show("Укажите имя.");
                return;
            }

            if (!CheckEmpty("^[A-ЯЁ][а-яА-Яё]*$", Surname.Text))
            {
                MessageBox.Show("Укажите отчество.");
                return;
            }

            if (String.IsNullOrEmpty(srcUserImage))
            {
                MessageBox.Show("Выберите изображение.");
                return;
            }

            if (usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                              x.Lastname == Lastname.Text &&
                                              x.Surname == Surname.Text).Count() > 0)
            {
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                                              x.Lastname == Lastname.Text &&
                                                                              x.Surname == Surname.Text).First();
                MainWindow.Instance.LoginUser.Photo = File.ReadAllBytes(srcUserImage);
                usersContext.SaveChanges();
            }
            else
            {
                usersContext.Users.Add(new Users(Lastname.Text, Firstname.Text, Surname.Text, File.ReadAllBytes(srcUserImage)));
                usersContext.SaveChanges();
                MainWindow.Instance.LoginUser = usersContext.Users.Where(x => x.Firstname == Firstname.Text &&
                                                                              x.Lastname == Lastname.Text &&
                                                                              x.Surname == Surname.Text).First();
            }

            MainWindow.Instance.OpenPages(new Pages.Main());
        }
        private void SelectPhoto(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите фотографию:";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                srcUserImage = openFileDialog.FileName;
            }
        }
    }
}
