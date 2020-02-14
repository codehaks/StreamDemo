using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UploadStreamDemo
{
    public partial class Form1 : Form
    {

        private HubConnection connection;

        public Form1()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
      .WithUrl("http://localhost:5000/streamHub")
      .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.On<string>("GetLine", (string line) =>
            {
                listBox1.Items.Add(line);
            });

            connection.StartAsync();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var counter = 1;

            await connection.SendAsync("SendLine", ReadLine(textBox1.Text));

            //await foreach (var line in ReadLine(@"E:\Projects\Data\Firstnames.txt"))
            //{
            //    if (line.Trim() != "hakim")
            //    {
            //        counter++;
            //        label1.Text = counter.ToString();
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
        }


        private async IAsyncEnumerable<string> ReadLine(string path)
        {
            using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fs, Encoding.UTF8);
            var counter = 1;
            while (reader.Peek() >= 0)
            {
                label1.Text = counter.ToString();
                counter++;

                await Task.Delay(1000);
                yield return await reader.ReadLineAsync();
            }

        }
    }
}
