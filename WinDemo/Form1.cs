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

namespace WinDemo
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
               .WithUrl("http://localhost:53353/ChatHub")
               .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection.On<Coord>("GetCoord", (coord) =>
            {
                textBox1.Text = coord.X.ToString();
                textBox2.Text = coord.Y.ToString();
            });

            connection.StartAsync();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //textBox1.Text = e.X.ToString();
            //textBox2.Text = e.Y.ToString();
            
            //textBox3.Text += $" {e.X},{e.Y} \n";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();

            textBox3.Text += $" {e.X},{e.Y} \n";
        }
    }
}
