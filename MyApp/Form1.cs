using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    public partial class Form1 : Form
    {
        private HubConnection connection;

        public class Coord
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

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
            connection.On<Coord>("GetCoord", (Coord coord) =>
            {
                textBox1.Text = coord.X.ToString();
                textBox2.Text = coord.Y.ToString();
                textBox3.Text += $" {coord.X},{coord.Y} \n";
            });

            connection.StartAsync();
        }

        private async void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //textBox1.Text = e.X.ToString();
            //textBox2.Text = e.Y.ToString();

            //textBox3.Text += $" {e.X},{e.Y} \n";

            //var coord = new Coord();
            //coord.X = e.X;
            //coord.Y = e.Y;

            //await connection.SendAsync("SendCoord", coord);
        }

        async IAsyncEnumerable<string> readcoords()
        {
            for (var i = 0; i < 5; i++)
            {
                //var data = await fetchsomedata();
                yield return "";
            }
            //after the for loop has completed and the local function exits the stream completion will be sent.
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
