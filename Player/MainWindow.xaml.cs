using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace Player
{
    public partial class MainWindow : Window
    {
        public ListView curr_lv = new ListView();
        public TabItem curr_ti = new TabItem();
        ListView nemix = new ListView();
        Random rnd = new Random();
        int tCount = 0;
        string tempti;
        int ka = 0;
        public bool vip = true;
        public bool ziti = false;
        bool p = false;
        ListViewItem lv_copy = new ListViewItem();
        public string Hpath;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            App.Current.StartupUri = new Uri("/Player;component/MainWindow.xaml", UriKind.Relative);
            App.Current.MainWindow = this;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_Tick);
            curr_ti = tc_main.Items[0] as TabItem;
            curr_lv = curr_ti.Content as ListView;
            curr_lv.SelectionChanged += Mpl_SelectionChanged;
            lv_copy.Content = null;
            lv_copy.Tag = null;
            me_main.Volume = 1;
            string[] MPLfiles = Directory.GetFiles(Directory.GetCurrentDirectory() + $"\\mp3\\");
            foreach (string f in MPLfiles)
            {
                string[] ff = f.Split('\u005C');
                curr_lv.Items.Add(new ListViewItem { Content = ff[ff.Length - 1].Replace('_', ' '), Tag = f.ToString() });
            }
            curr_lv = (tc_main.Items[0] as TabItem).Content as ListView;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            s_duration.Value = me_main.Position.TotalSeconds;
        }

        private void Mpl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (curr_lv.SelectedItem != null)
            {
                try
                {
                    tBox_author.Content = "";
                    tBox_song.Content = "";
                    me_main.Source = new Uri(((curr_lv).SelectedItem as ListViewItem).Tag.ToString());
                    var taglib = TagLib.File.Create((curr_lv.SelectedItem as ListViewItem).Tag.ToString());
                    TagLib.File FD = TagLib.File.Create((curr_lv.SelectedItem as ListViewItem).Tag.ToString());
                    TimeSpan Duration = taglib.Properties.Duration;
                    me_videos.Position = TimeSpan.FromSeconds(rnd.Next(0, 3000));
                    me_main.Pause();

                    try
                    {
                        tBox_song.Content = taglib.Tag.Title;
                        tBox_author.Content = taglib.Tag.FirstPerformer;
                    }
                    catch { }
                }
                catch { }
            }
        }

        private void tc_main_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (tc_main.SelectedItem != null)
                {
                    tempti = curr_ti.Header.ToString();
                    curr_ti = tc_main.SelectedItem as TabItem;
                }

            }
            catch { }

            if (curr_ti.Header.ToString() != tempti)
            {
                if (curr_ti.Content != null)
                {
                    curr_lv = curr_ti.Content as ListView;
                    curr_lv.SelectionChanged += Mpl_SelectionChanged;
                }
            }

            me_main.Play();
            me_videos.Play();
        }

        private void b_back_Click(object sender, RoutedEventArgs e)
        {
            me_videos.Close();
            int temp = curr_lv.Items.Count;
            temp--;
            int temp1 = curr_lv.SelectedIndex;
            temp1--;
            tb_repeat.IsChecked = false;

            if (temp1 < 0)
            {
                curr_lv.SelectedIndex = temp;
            }
            else if (temp1 >= 0)
            {
                curr_lv.SelectedIndex--;
            }

            me_main.Play();
        }

        private void b_pause_Click(object sender, RoutedEventArgs e)
        {
            me_main.Pause();
            me_videos.Pause();
            p = true;
        }

        private void b_play_Click(object sender, RoutedEventArgs e)
        {
            me_main.Play();
            me_videos.Play();
            p = false;
        }

        private void b_forward_Click(object sender, RoutedEventArgs e)
        {
            me_videos.Close();

            int temp = curr_lv.Items.Count;
            temp--;
            tb_repeat.IsChecked = false;

            if (curr_lv.SelectedIndex++ < temp)
            {
                curr_lv.SelectedIndex = curr_lv.SelectedIndex;
            }
            else
            {
                curr_lv.SelectedIndex = 0;
            }

            me_main.Play();
        }

        private void tb_mix_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tempti == nemix.Name.ToString())
                {
                    string[] a = new string[nemix.Items.Count];
                    string[] atag = new string[nemix.Items.Count];

                    for (int i = 0; i < nemix.Items.Count; i++)
                    {
                        nemix.SelectedIndex = i;
                        a[i] = (nemix.SelectedItem as ListViewItem).Content.ToString();
                        atag[i] = (nemix.SelectedItem as ListViewItem).Tag.ToString();
                    }

                    while (curr_lv.Items.Count != 0)
                        curr_lv.Items.Clear();

                    for (int i = 0; i < nemix.Items.Count; i++)
                    {
                        ListViewItem lv = new ListViewItem();
                        lv.Content = a[i];
                        lv.Tag = atag[i];
                        curr_lv.Items.Add(lv);
                    }
                    me_main.Pause();
                    tempti = null;
                }
                else
                {

                }
            }
            catch { }
        }

        private void me_main_MediaEnded(object sender, RoutedEventArgs e)
        {
            int temp = curr_lv.Items.Count;
            temp--;
            int temp1 = curr_lv.SelectedIndex;
            temp1++;
            me_videos.Close();
            

            if (tb_repeat.IsChecked == false && (me_main.Position > TimeSpan.FromSeconds(60) || (curr_lv.SelectedItem as ListViewItem).Content.ToString().Contains(".mp4")))
            {

                if (temp1 <= temp)
                {
                    curr_lv.SelectedIndex++;
                }
                else
                {
                    curr_lv.SelectedIndex = 0;
                }

                me_main.Source = new Uri(((curr_lv.SelectedItem as ListViewItem).Tag.ToString()));
                var taglib = TagLib.File.Create(((curr_lv).SelectedItem as ListViewItem).Tag.ToString());
                TimeSpan Duration = taglib.Properties.Duration;
                me_main.Position = TimeSpan.FromSeconds(0);
                me_videos.Position = TimeSpan.FromSeconds(rnd.Next(0, 3000));
                me_main.Play();

                try
                {
                    tBox_song.Content = taglib.Tag.Title;
                    tBox_author.Content = taglib.Tag.FirstPerformer;
                }
                catch { }

               

                me_main.Position = TimeSpan.FromSeconds(0);
                me_videos.Position = TimeSpan.FromSeconds(0);
                s_duration.Value = 0;
            }
            else if(tb_repeat.IsChecked == true)
            {
                me_main.Position = TimeSpan.FromSeconds(0);
            }
        }

        private void s_duration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(!p)
                me_videos.Play();
            me_main.Position = TimeSpan.FromSeconds(s_duration.Value);
        }

        private void me_main_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                history((curr_lv.SelectedItem as ListViewItem).Content.ToString());

                TimeSpan ts = me_main.NaturalDuration.TimeSpan;
                s_duration.Maximum = ts.TotalSeconds;
                timer.Start();

            }
            catch(Exception ex) 
            {

            }
            me_videos.Source = new Uri(Directory.GetCurrentDirectory() + "\\vid\\def.mp4");
            me_videos.Position = TimeSpan.FromSeconds(rnd.Next(0, 3000));
            me_videos.Play();
            me_main.Play();

        }

        private void s_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                me_main.Volume = s_volume.Value / 100;
            }
            catch { }
        }

        private void b_Madd_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Медиа файлы (*.aif, *.m3u, *.m4a, *.mid, *.mp3, *.mpa, *.wav, *.wma, *.mp4)|*.aif; *.m3u; *.m4a; *.mid; *.mp3; *.mpa; *.wav; *.wma; *.mp4"; ;

            if(ofd.ShowDialog() == true)
            {
                string[] ff = ofd.FileName.Split('\u005C');
                if(!error)
                curr_lv.Items.Add((new ListViewItem { Content = ff[ff.Length - 1], Tag = ofd.FileName.ToString() }));
                
            }
                nemix.Items.Clear();

                string[] a = new string[curr_lv.Items.Count];
                string[] atag = new string[curr_lv.Items.Count];

            if (curr_lv.SelectedValue != null)
            {
                for (int i = 0; i < curr_lv.Items.Count; i++)
                {
                    curr_lv.SelectedIndex = i;
                    a[i] = (curr_lv.SelectedItem as ListViewItem).Content.ToString().Replace('_', ' ');
                    atag[i] = (curr_lv.SelectedItem as ListViewItem).Tag.ToString();
                }

                for (int i = 0; i < curr_lv.Items.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    lv.Content = a[i];
                    lv.Tag = atag[i];
                    nemix.Items.Add(lv);
                }
            }
                int temp = curr_lv.Items.Count;
        }

        private void b_Mdelete_Click(object sender, RoutedEventArgs e)
        {
            curr_lv.Items.RemoveAt(curr_lv.SelectedIndex);
            curr_lv.SelectedIndex = 0;
        }

        private void b_Pladd_Click(object sender, RoutedEventArgs e)
        {
            
            me_videos.Source = null;
            me_main.Close();
            tBox_author.Content = "";
            tBox_song.Content = "";

            try
            {
                TabItem ti = new TabItem();
                ListView lv = new ListView();

                ti.Content = lv;

                try
                {

                    string res = Interaction.InputBox("Введите название альбома: ");

                    if (res != null)
                    {
                        ti.Header = res;
                        ti.Name = res + tCount.ToString();
                        tc_main.Items.Add(ti);

                        tc_main.SelectedItem = ti;
                        tCount++;
                    }
                }
                catch
                {
                    MessageBox.Show("Недопустимое название альбома!");
                }

                nemix.Items.Clear();

                string[] a = new string[curr_lv.Items.Count];
                string[] atag = new string[curr_lv.Items.Count];

                for (int i = 0; i < curr_lv.Items.Count; i++)
                {
                    curr_lv.SelectedIndex = i;
                    a[i] = (curr_lv.SelectedItem as ListViewItem).Content.ToString().Replace('_', ' ');
                    atag[i] = (curr_lv.SelectedItem as ListViewItem).Tag.ToString();
                }

                for (int i = 0; i < curr_lv.Items.Count; i++)
                {
                    ListViewItem lv1 = new ListViewItem();
                    lv1.Content = a[i];
                    lv1.Tag = atag[i];
                    nemix.Items.Add(lv1);
                }
            }
            catch 
            {
                
            }
        }

        private void b_PLdelete_Click(object sender, RoutedEventArgs e)
        {
            if ((tc_main.SelectedItem as TabItem).Name.ToString() != "ti_main")
                tc_main.Items.Remove(tc_main.SelectedItem);
            else
                MessageBox.Show("Закрыть основной плейлист нельзя!");
        }

        private void b_PLrename_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tc_main.SelectedItem as TabItem).Header.ToString() != "mainPL")
                {
                    string res = Interaction.InputBox("Введите название альбома: ");

                    if (res != null)
                    {
                        curr_ti.Header = res;
                        curr_ti.Name = res;
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя переименовать основной альбом!");
                }
            }
            catch { }
        }

        private void s_duration_Drop(object sender, DragEventArgs e)
        {
            me_videos.Position = TimeSpan.FromSeconds(me_videos.Position.TotalSeconds) + TimeSpan.FromSeconds(s_duration.Value);
        }

        async private void B_find_Click(object sender, RoutedEventArgs e)
        {
            lv_find.Items.Clear();
            ListView templv = new ListView();
            for (int i = 0; i < curr_lv.Items.Count; i++)
            {
                ListViewItem tempti = new ListViewItem();

                tempti.Content = (curr_lv.Items[i] as ListViewItem).Content;
                tempti.Tag = (curr_lv.Items[i] as ListViewItem).Tag;

                templv.Items.Add(tempti);
            }

            string temp = tb_find.Text;

            string[] a = new string[templv.Items.Count];
            string[] atag = new string[templv.Items.Count];

            for (int i = 0; i < templv.Items.Count; i++)
            {
                templv.SelectedIndex = i;
                a[i] = (templv.SelectedItem as ListViewItem).Content.ToString().Replace('_', ' ');
                atag[i] = (templv.SelectedItem as ListViewItem).Tag.ToString();
            }

            for (int i = 0; i < templv.Items.Count; i++)
            {
                string[] tempai = tb_find.Text.Split(' ');
                int k = 0;

                for (int j = 0; j < tempai.Length; j++)
                {
                    if (a[i].ToLower().Contains(tempai[j].ToLower()))
                    {
                        k++;
                    }
                    if (k == tempai.Length)
                    {
                        lv_find.Items.Add(new ListViewItem { Content = a[i], Tag = atag[i] });
                    }
                }
            }
        }
        private void lv_find_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tBox_author.Content = "";
                tBox_song.Content = "";


                me_main.Source = new Uri(((lv_find).SelectedItem as ListViewItem).Tag.ToString());
                var taglib = TagLib.File.Create((lv_find.SelectedItem as ListViewItem).Tag.ToString());
                TagLib.File FD = TagLib.File.Create((lv_find.SelectedItem as ListViewItem).Tag.ToString());
                TimeSpan Duration = taglib.Properties.Duration;
                me_videos.Position = TimeSpan.FromSeconds(rnd.Next(0, 3000));
                me_main.Pause();

                try
                {
                    tBox_song.Content = taglib.Tag.Title;
                    tBox_author.Content = taglib.Tag.FirstPerformer;
                }
                catch { }

                
            }
            catch { }
        }
        private void MW_Activated(object sender, EventArgs e)
        {
            if (tc_main.Items.Count == 1)
            {
                try
                {
                    string[] MPLfiles = Directory.GetFiles($@"{Hpath}\PaL\");

                    if (MPLfiles.Length == 1)
                    {
                        TabItem ti = new TabItem();
                        ListView lv = new ListView();

                        string[] Tplname = MPLfiles[0].Split('\u005C');
                        string plname = Tplname[Tplname.Length - 1];

                        ti.Name = plname.Split('.')[0];
                        ti.Header = plname.Split('.')[0];

                        string[] temp = File.ReadAllLines(MPLfiles[0]);

                        foreach (string f in temp)
                        {
                            string[] ff = f.Split('\u005C');
                            lv.Items.Add(new ListViewItem { Content = ff[ff.Length - 1].Replace('_', ' '), Tag = f.ToString() });
                        }
                        ti.Content = lv;

                        tc_main.Items.Add(ti);
                    }
                    else
                    {
                        for (int k = 0; k < MPLfiles.Length; k++)
                        {

                            string[] Tplname = MPLfiles[k].Split('\u005C');
                            string plname = Tplname[Tplname.Length - 1];
                                TabItem ti = new TabItem();
                                ListView lv = new ListView();

                                string[] tf = File.ReadAllLines($@"{Hpath}\PaL\{plname}");


                            if (tf.Length == 0)
                            {
                                ti.Name = plname.Split('.')[0];
                                ti.Header = plname.Split('.')[0];

                                ti.Content = lv;
                            }
                            else
                            {
                                for (int j = 0; j < tf.Length; j++)
                                {
                                    ti.Name = plname.Split('.')[0];
                                    ti.Header = plname.Split('.')[0];

                                    string[] ff = tf[j].Split('\u005C');
                                    lv.Items.Add(new ListViewItem { Content = ff[ff.Length - 1].Replace('_', ' '), Tag = tf[j].ToString() });
                                }

                                ti.Content = lv;
                            }
                            tc_main.Items.Add(ti);
                            curr_ti = tc_main.SelectedItem as TabItem;
                            curr_lv = curr_ti.Content as ListView;
                            curr_lv.SelectionChanged += Mpl_SelectionChanged;

                            curr_lv = curr_ti.Content as ListView;
                        }
                        string[] tpath = File.ReadAllLines($@"{Hpath}History.txt");

                        foreach (string tempH in tpath)
                        {
                            cb_his.Items.Add(tempH);
                        }
                    }
                }
                catch { }
            }
        }

        private void history(string address)
        {
            StreamWriter sw = File.AppendText($@"{Hpath}History.txt");

            sw.WriteLine(address.ToString() + "\t\t\t" + DateTime.Now);

            sw.Close();

            cb_his.Items.Clear();

            string[] tpath = File.ReadAllLines($@"{Hpath}History.txt");

            foreach (string tempH in tpath)
            {
                cb_his.Items.Add(tempH);
            }
        }
        private void MW_Activated_1(object sender, EventArgs e)
        {
            if (ka == 0)
            ka++;
        }

        private void MW_Deactivated(object sender, EventArgs e)
        {
            ka = 0;
        }

        

        private void cb_his_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_his.SelectedItem = null;
        }
    }
}