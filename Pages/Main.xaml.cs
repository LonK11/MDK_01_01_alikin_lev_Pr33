using ChatStudents_Alikin.Classes;
using ChatStudents_Alikin.Classes.Common;
using ChatStudents_Alikin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace ChatStudents_Alikin.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Users SelectedUser = null;
        public UsersContext usersContext = new UsersContext();
        public MessagesContext messagesContext = new MessagesContext();
        public DispatcherTimer Timer = new DispatcherTimer() { Interval = new System.TimeSpan(0, 0, 3) };
        public Main()
        {
            InitializeComponent();
            Timer.Tick += Timer_Tick;
            Timer.Start();
            UpdateLastAction();
        }
        public void LoadUsers()
        {
            ParentUsers.Children.Clear();
            foreach (Users user in usersContext.Users)
            {
                if (user.Id != MainWindow.Instance.LoginUser.Id)
                    ParentUsers.Children.Add(new Pages.Items.User(user, this));
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadUsers();

            if (SelectedUser != null)
            {
                var selected = usersContext.Users.Where(x => x.Id == SelectedUser.Id).First();
                imgOnline.Visibility = selected.IsOnline ? Visibility.Visible : Visibility.Hidden;
                Status.Content = selected.IsOnline ? "онлайн" : "был(а) недавно";
            }

            UpdateLastAction();
        }
        public void UpdateLastAction()
        {
            try
            {
                var loginUser = usersContext.Users.Where(x => x.Id == MainWindow.Instance.LoginUser.Id).First();
                loginUser.LastAction = DateTime.Now;
                usersContext.SaveChanges();
            }
            catch { }
        }
        public void SelectUser(Users User)
        {
            SelectedUser = User;
            Chat.Visibility = Visibility.Visible;
            imgUser.Source = BitmapFromArrayByte.LoadImage(User.Photo);
            FIO.Content = User.ToFIO();
            imgOnline.Visibility = User.IsOnline ? Visibility.Visible : Visibility.Hidden;
            Status.Content = User.IsOnline ? "онлайн" : "был(а) недавно";
            ParentMessages.Children.Clear();
            foreach (Messages Message in messagesContext.Messages.Where(x =>
                (x.UserFrom == User.Id && x.UserTo == MainWindow.Instance.LoginUser.Id) ||
                (x.UserFrom == MainWindow.Instance.LoginUser.Id && x.UserTo == User.Id)).OrderBy(x => x.Id))
            {
                ParentMessages.Children.Add(new Pages.Items.Message(Message, usersContext.Users.Where(x => x.Id == Message.UserFrom).First()));
            }
        }
        public void Send(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Messages message = new Messages(
                    MainWindow.Instance.LoginUser.Id,
                    SelectedUser.Id,
                    Message.Text
                    );
                messagesContext.Messages.Add(message);
                messagesContext.SaveChanges();
                ParentMessages.Children.Add(new Pages.Items.Message(message, MainWindow.Instance.LoginUser));
                Message.Text = "";
            }
        }
    }
}
