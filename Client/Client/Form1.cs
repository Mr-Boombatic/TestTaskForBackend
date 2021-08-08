using Client.Models;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDeleteTicket_Click(object sender, EventArgs e)
        {

        }

        private void btnChangeTicket_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) => { return true; };


            // получение билетов
            List<Ticket> tickets;
            WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/GetTickets");
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    tickets = JsonSerializer.Deserialize<List<Ticket>>(reader.ReadToEnd());
                }
            }

            // получение тиражей
            List<Circulation> circulations;
            request = WebRequest.Create("https://localhost:44391/Lotto/GetCirculations");
            response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    circulations = JsonSerializer.Deserialize<List<Circulation>>(reader.ReadToEnd());
                }
            }

            foreach (var ticket in tickets)
            {
                var numbers = ticket.SelectedNum.Split(new char[] { ';' }).ToList();
                string orderedNumbering = "";
                foreach (var number in numbers)
                    orderedNumbering += number.ToString() + " ";

                dataGridView1.Rows.Add(new string[]
                { ticket.Variant.ToString(),
                  (circulations.Where(circulation => circulation.Circulation1 == ticket.СirculationNum).Select(circulation => circulation.Circulation1).First()).ToString(),
                  numbers.Count.ToString(),
                  orderedNumbering
                });
            }
        }

        private void btnAppendTicket_Click_1(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            newForm.Show();
            Dispose(false);
        }

        private void btnChangeTicket_Click_1(object sender, EventArgs e)
        {
            Form3 newForm = new Form3(dataGridView1.SelectedRows[0]);
            newForm.Show();
            Dispose(false);
        }

        private void btnDeleteTicket_Click_1(object sender, EventArgs e)
        {
            var jsonData = JsonSerializer.Serialize<string>(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/DeleteTicket");
            request.Method = "POST";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var numbersTempate = template.Text.Split(new char[] { ' ', ';', ',' })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x))
                .ToList();
            numbersTempate.Sort();

            var jsonData = JsonSerializer.Serialize<string>(string.Join(";", numbersTempate) + ";");
           WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/FilterTickets");
            request.Method = "POST";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
                dataStream.Write(byteArray, 0, byteArray.Length);

            WebResponse response = request.GetResponse();
            List<Ticket> filteredTickets;
            using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    filteredTickets = JsonSerializer.Deserialize<List<Ticket>>(reader.ReadToEnd());

            // получение тиражей
            List<Circulation> circulations;
            request = WebRequest.Create("https://localhost:44391/Lotto/GetCirculations");
            response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                    circulations = JsonSerializer.Deserialize<List<Circulation>>(reader.ReadToEnd());

            dataGridView1.Rows.Clear();
            foreach (var ticket in filteredTickets)
            {
                dataGridView1.Rows.Add(new string[]
                { ticket.Variant.ToString(),
                  (circulations.Where(circulation => circulation.Circulation1 == ticket.СirculationNum).Select(circulation => circulation.Circulation1).First()).ToString(),
                  ticket.SelectedNum.Split(new char[]{ ' ' }).Count().ToString(),
                  ticket.SelectedNum.Replace(';', ' ')
                });
            }

        }
    }
}
