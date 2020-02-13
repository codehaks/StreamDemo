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

namespace MyApp
{
    public partial class Main : Form
    {
        private HubConnection connection;

        public class Coord
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public Main()
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

        private async void button1_Click(object sender, EventArgs e)
        {
            await connection.SendAsync("SendCoord",Readcoords());
        }

        private void Main_Load(object sender, EventArgs e)
        {
            connection.On<Coord>("GetCoord", (Coord coord) =>
            {
                //textBox1.Text = coord.X.ToString();
                //textBox2.Text = coord.Y.ToString();
                textBox1.Text += $" {coord.X},{coord.Y} \n";
            });

            connection.StartAsync();
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text += $" {e.X},{e.Y} \n";
        }


        async IAsyncEnumerable<string> Readcoords()
        {
            for (int i = 0; i < textBox1.Lines.Count(); i++)
            {
                yield return textBox1.Lines[i];
                await Task.Delay(100);
            }


        }
    }
}
