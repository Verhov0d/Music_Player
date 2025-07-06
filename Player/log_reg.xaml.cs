using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Player
{
    public partial class log_reg : Window
    {
        Image imag = new Image();
        public string Hpath;
        public bool log = false;
        public log_reg()
        {
            InitializeComponent();
            tb_log.Text = "";
            pb_pass.Password = "";
        }

        private void b_reg_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            reg r = new reg();
            r.Show();
        }

        private void b_log_Click(object sender, RoutedEventArgs e)
        {
            if (tb_log.Text.Length > 0)
            {
                if (pb_pass.Password.Length > 0)
                {
                    if (Directory.Exists(Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}"))
                    {
                        Hpath = Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}" + "\u005C";
                        var path = Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}";
                        string pass = "";

                        using(StreamReader sr = new StreamReader(path + "\u005Cpassword.txt"))
                        {
                            pass += sr.ReadLine();
                        }

                        if (pb_pass.Password == pass)
                        {
                            var imgpath = Directory.GetFiles(path);
                            string Pimg = imgpath[imgpath.Length - 1];
                            log = true;
                            MainWindow me = new MainWindow();
                            this.Hide();
                            me.Hpath = Hpath;
                            me.vip = true;
                            me.Show();
                        }
                        else
                            MessageBox.Show("Неверный пароль");
                    }
                    else
                        MessageBox.Show("Пользователя не найден");
                }
                else
                    MessageBox.Show("Поле пароль не может быть пустым");
            }
            else
                MessageBox.Show("Поле логин не может быть пустым");
        }
    }
}