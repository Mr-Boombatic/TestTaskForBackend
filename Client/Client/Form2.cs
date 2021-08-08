using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        void CreateMatrixTicket()
        {
            int currentVal = 1;
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Rows.Add(new string[] { });
                for (int j = 0; j < 6; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = (currentVal).ToString();
                    currentVal++;
                }
            }
        }
        private void Form2_Load_1(object sender, EventArgs e)
        {
            CreateMatrixTicket();
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        int CountSelectedCell;
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                if (dataGridView1.SelectedCells[i].Style.BackColor == Color.Blue)
                {
                    CountSelectedCell--;
                    dataGridView1.SelectedCells[i].Style.BackColor = Color.White;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.White;
                }
                else
                {
                    if (CountSelectedCell > 17)
                        return;
                    CountSelectedCell++;
                    dataGridView1.SelectedCells[i].Style.BackColor = Color.Blue;
                    dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Blue;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedNumbers = new List<int>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Blue)
                        selectedNumbers.Add(int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()));

            var ticket = new Ticket() { SelectedNum = string.Join(";", selectedNumbers) + ";", СirculationNum = int.Parse(circulation.Text) };

            var jsonData = JsonSerializer.Serialize<Ticket>(ticket);
            WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/RegistrTicket");
            request.Method = "POST";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream()) { }


            Form1 newForm = new Form1();
            newForm.Show();
            Dispose(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            Dispose(false);
        }
    }
}
