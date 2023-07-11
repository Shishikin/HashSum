using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using System.Threading;
using System.Diagnostics.Metrics;


namespace CheshSumma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private const int BUFFER_SIZE = 16 * 1024 * 1024;
        /// функция для подсчёта хэшсуммы файла        
        async Task<byte[]> HashsumAsync(string file)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream fs = new FileStream
                    (
                    file, FileMode.Open, FileAccess.Read,
                    FileShare.Read, BUFFER_SIZE, FileOptions.Asynchronous
                    )
                    )
                {
                    byte[] buffer = new byte[BUFFER_SIZE];

                    int shift;
                    while ((shift = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        md5.TransformBlock(buffer, 0, shift, buffer, 0);
                    }
                    md5.TransformFinalBlock(buffer, 0, shift);

                    return md5.Hash;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                Task.Run(async () =>
            {
                byte[] hash;
                //                hash = await HashsumAsync(file);
                MessageBox.Show(BitConverter.ToString(await HashsumAsync(dialog.FileName)));
            }
            );
                /*
                int numberfiles = 4;
                string [] searchfiles = new string[numberfiles];
                int numberTask = 4;
                {
                    int counter = 0;
                    while (counter < numberfiles)
                    {
                        var dialog = new OpenFileDialog();
                        if (dialog.ShowDialog() == true)              
                        {
                            searchfiles[counter] = dialog.FileName;
                        }
                        counter++;
                    }
                }
                Task [] task = new Task[numberTask];
                MessageBox.Show(numberfiles.ToString());

                for (int i = 0; i < numberfiles; i++)
                {
     //               MessageBox.Show(i.ToString());
                    task[i] = new Task(() => Hashsumma(searchfiles[i]));
                    task[i].Start();
     //               MessageBox.Show(i.ToString());                
                }

                /*
                for (int i = 0; i < numberfiles; i++)
                {
                    task[i] = Task.Factory.StartNew(() => Hashsumma(searchfiles[i]));
                    MessageBox.Show(i.ToString());
                }
                */
                /*
                task[0] = new Task(() => Hashsumma(searchfiles[0]));
                task[0].Start();
                task[1] = new Task(() => Hashsumma(searchfiles[1]));
                task[1].Start();
                task[2] = new Task(() => Hashsumma(searchfiles[2]));
                task[2].Start();
                task[3] = new Task(() => Hashsumma(searchfiles[3]));
                task[3].Start();
                */
                MessageBox.Show("hello");
                //            Task.WaitAll(task);
                MessageBox.Show("good buy");
            }
        }
    }
}
