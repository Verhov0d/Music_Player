using System.IO;
using System.Linq;
using System.Windows;

namespace Player
{
    public partial class reg : Window
    {
        string imgpath = "";
        public reg()
        {
            InitializeComponent();
        }

        private void b_reg_Click(object sender, RoutedEventArgs e)
        {
            var cyrillic = Enumerable.Range(1024, 256).Select(ch => (char)ch);

            if (tb_log.Text.Length > 0)
            {
                if(pb_pass.Password.Length > 0)
                {

                    if(!tb_log.Text.Any(cyrillic.Contains))
                    {

                        if (!pb_pass.Password.Any(cyrillic.Contains))
                        {
                            if (!Directory.Exists(Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}"))
                            {
                                Directory.CreateDirectory(Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}");

                                var path = Directory.GetCurrentDirectory() + $"\\accs\\{tb_log.Text}";

                                using (StreamWriter sw = File.CreateText(path + "\u005Cpassword.txt"))
                                {
                                    sw.WriteLine(pb_pass.Password);
                                }

                                File.Create(path + "\u005CHistory.txt");

                                MessageBox.Show("Регистрация прошла успешна");

                                this.Hide();
                                log_reg lg = new log_reg();

                                lg.tb_log.Text = this.tb_log.Text;

                                lg.Show();
                            }
                            else
                            {
                                MessageBox.Show("Пользователь уже существует");
                            }
                        }
                        else
                            MessageBox.Show("Пароль не должен содержать кириллицу");
                    }
                    else
                        MessageBox.Show("Логин не должен содержать кириллицу");
                }
                else
                    MessageBox.Show("Введите пароль");
            }
            else
                MessageBox.Show("Введите логин");
        }
    }
}