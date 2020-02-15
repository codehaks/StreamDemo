using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
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
                .ConfigureLogging(logging =>
                {
                    logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
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
            await connection.SendAsync("SendLine", ReadLine(textBox1.Text));
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
