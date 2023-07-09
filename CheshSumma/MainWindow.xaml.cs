﻿using Microsoft.Win32;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            void Hashsumma()
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                {
                    var md5 = MD5.Create().ComputeHash(File.ReadAllBytes(dialog.FileName));
                    MessageBox.Show(BitConverter.ToString(md5));
                }
            }
            ThreadStart threadStart = new ThreadStart(Hashsumma);
            Thread thread = new Thread(threadStart);
            thread.Start();
            Hashsumma();

 /*           
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                var md5 = MD5.Create().ComputeHash(File.ReadAllBytes(dialog.FileName));
                MessageBox.Show(BitConverter.ToString(md5));
            }
 */
        }
    }
}
