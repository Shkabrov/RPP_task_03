using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using AxWMPLib;

namespace RPP_task_03
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Интерфейс для реализации проигрывателя по шаблону "Адаптер"
        /// </summary>
        public interface IPlayer
        {
            /// <summary>
            /// Метод реализующий загрузку файла
            /// </summary>
            void LoadFile();

            /// <summary>
            /// Метод реализующий выбор файла.
            /// </summary>
            void SelectFile();
        }

        /// <summary>
        /// Класс реализующий аудио-проигрыватель.
        /// </summary>
        public class AudioPlayer : IPlayer
        {
            List<String> files, Paths;
            ListBox listBox1;
            AxWindowsMediaPlayer axWindowsMediaPlayer1;

            /// <summary>
            /// Конструктор класса AudioPlayer, инициализирующий его объекты.
            /// </summary>
            /// <param name="listBox">Список для отображения файлов</param>
            /// <param name="axWindowsMediaPlayer">Объект отображающий проигрыватель</param>
            public AudioPlayer(ListBox listBox, AxWindowsMediaPlayer axWindowsMediaPlayer)
            {
                listBox1 = listBox;
                axWindowsMediaPlayer1 = axWindowsMediaPlayer;
                files = new List<string>();
                Paths = new List<string>();
            }

            /// <summary>
            /// Метод реализующий загрузку аудио-файла.
            /// </summary>
            public void LoadFile()
            {
                listBox1.Items.Clear();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                ofd.Filter = "MP3 Files|*.mp3";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(ofd.SafeFileNames);
                    Paths.AddRange(ofd.FileNames);

                    for (int i = 0; i < files.Count; i++)
                    {
                        listBox1.Items.Add(files[i]);
                    }
                }

            }
            /// <summary>
            /// Метод реализующий выбор аудио-файла из списка.
            /// </summary>
            public void SelectFile()
            {
                if (listBox1.SelectedIndex != -1)
                    axWindowsMediaPlayer1.URL = Paths[listBox1.SelectedIndex];
            }
        }
        
        /// <summary>
        /// Класс реализующий видео-проигрыватель.
        /// </summary>
        public class VideoPlayer : IPlayer
        {
            List<String> files, Paths;
            ListBox listBox1;
            AxWindowsMediaPlayer axWindowsMediaPlayer1;
            /// <summary>
            /// Конструктор класса VideoPlayer, инициализирующий его объекты.
            /// </summary>
            /// <param name="listBox"></param>
            /// <param name="axWindowsMediaPlayer"></param>
            public VideoPlayer(ListBox listBox, AxWindowsMediaPlayer axWindowsMediaPlayer)
            {
                listBox1 = listBox;
                axWindowsMediaPlayer1 = axWindowsMediaPlayer;
                files = new List<string>();
                Paths = new List<string>();
            }
            /// <summary>
            /// Метод реализующий загрузку видео-файла.
            /// </summary>
            public void LoadFile()
            {
                listBox1.Items.Clear();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                ofd.Filter = "MKV Files|*.mkv";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    files.AddRange(ofd.SafeFileNames);
                    Paths.AddRange(ofd.FileNames);

                    for (int i = 0; i < files.Count; i++)
                    {
                        listBox1.Items.Add(files[i]);
                    }
                }

            }
            /// <summary>
            /// Метод реализующий выбор видео-файла.
            /// </summary>
            public void SelectFile()
            {
                if (listBox1.SelectedIndex != -1)
                    axWindowsMediaPlayer1.URL = Paths[listBox1.SelectedIndex];
            }
        }

        IPlayer AP;
        IPlayer VP;

        /// <summary>
        /// Класс реализующий работу с Windows Forms
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            AP = new AudioPlayer(listBox1, axWindowsMediaPlayer1);
            VP = new VideoPlayer(listBox2, axWindowsMediaPlayer1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AP.SelectFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AP.LoadFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VP.LoadFile();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            VP.SelectFile();
        }
    }
}
