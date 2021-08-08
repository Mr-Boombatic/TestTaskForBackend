using Client.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form3 : Form
    {
        private DataGridViewRow SelectedRow;
        public Form3(DataGridViewRow selectedRow)
        {
            InitializeComponent();
            SelectedRow = selectedRow;
        }

        void CreateMatrixTicket()
        {
            int currentVal = 1;
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Rows.Add(new string[] { });
                for (int j = 0; j < 6; j++)
                {
                    if (SelectedRow.Cells[3].Value.ToString().Split(new char[] { ' ' }).Contains(currentVal.ToString()))
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;

                    dataGridView1.Rows[i].Cells[j].Value = currentVal.ToString();
                    currentVal++;
                }
            }
            dataGridView1.Rows[0].Cells[0].Selected = false;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            CreateMatrixTicket();
            circulation.Text = SelectedRow.Cells[1].Value.ToString();
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

        private void ChangeTicket(object sender, EventArgs e)
        {
            var selectedNumbers = new List<int>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Blue)
                        selectedNumbers.Add(int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()));

            var ticket = new Ticket() { SelectedNum = string.Join(";", selectedNumbers), Variant = int.Parse(SelectedRow.Cells[0].Value.ToString()) };

            var jsonData = JsonSerializer.Serialize<Ticket>(ticket);
            WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/ChangeTicket");
            request.Method = "POST";

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
        }

        private void undoAction_Click_1(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            Dispose(false);
        }

        private void apply_Click(object sender, EventArgs e)
        {
            var selectedNumbers = new List<int>();

            var countSelectedCell = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    if (dataGridView1.Rows[i].Cells[j].Style.BackColor == Color.Blue)
                    {
                        selectedNumbers.Add(int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()));
                        countSelectedCell++;
                    }

            if (countSelectedCell > 17 || countSelectedCell < 6)
            {
                MessageBox.Show("Количество выбранных позиций должно быть от 6 до 17!");
                return;
            }

            var ticket = new Ticket()
            {
                SelectedNum = string.Join(";", selectedNumbers) + ";",
                Variant = int.Parse(SelectedRow.Cells[0].Value.ToString()),
                СirculationNum = int.Parse(circulation.Text)
            };

            var jsonData = JsonSerializer.Serialize<Ticket>(ticket);
            WebRequest request = WebRequest.Create("https://localhost:44391/Lotto/ChangeTicket");
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
    }
}
