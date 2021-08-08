
using Client.Models;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAppendTicket = new System.Windows.Forms.Button();
            this.btnChangeTicket = new System.Windows.Forms.Button();
            this.btnDeleteTicket = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumberTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CirculationNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountNumTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.template = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAppendTicket
            // 
            this.btnAppendTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAppendTicket.Location = new System.Drawing.Point(174, 415);
            this.btnAppendTicket.Name = "btnAppendTicket";
            this.btnAppendTicket.Size = new System.Drawing.Size(75, 23);
            this.btnAppendTicket.TabIndex = 0;
            this.btnAppendTicket.Text = "Добавить";
            this.btnAppendTicket.UseVisualStyleBackColor = true;
            this.btnAppendTicket.Click += new System.EventHandler(this.btnAppendTicket_Click_1);
            // 
            // btnChangeTicket
            // 
            this.btnChangeTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeTicket.Location = new System.Drawing.Point(93, 415);
            this.btnChangeTicket.Name = "btnChangeTicket";
            this.btnChangeTicket.Size = new System.Drawing.Size(75, 23);
            this.btnChangeTicket.TabIndex = 1;
            this.btnChangeTicket.Text = "Изменить";
            this.btnChangeTicket.UseVisualStyleBackColor = true;
            this.btnChangeTicket.Click += new System.EventHandler(this.btnChangeTicket_Click_1);
            // 
            // btnDeleteTicket
            // 
            this.btnDeleteTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteTicket.Location = new System.Drawing.Point(12, 415);
            this.btnDeleteTicket.Name = "btnDeleteTicket";
            this.btnDeleteTicket.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteTicket.TabIndex = 2;
            this.btnDeleteTicket.Text = "Удалить";
            this.btnDeleteTicket.UseVisualStyleBackColor = true;
            this.btnDeleteTicket.Click += new System.EventHandler(this.btnDeleteTicket_Click_1);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumberTicket,
            this.CirculationNumber,
            this.CountNumTicket,
            this.SelectedNum});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 409);
            this.dataGridView1.TabIndex = 3;
            // 
            // NumberTicket
            // 
            this.NumberTicket.HeaderText = "Номер билета";
            this.NumberTicket.Name = "NumberTicket";
            // 
            // CirculationNumber
            // 
            this.CirculationNumber.HeaderText = "Номер тиража";
            this.CirculationNumber.Name = "CirculationNumber";
            // 
            // CountNumTicket
            // 
            this.CountNumTicket.HeaderText = "Кол-во выбранных чисел в билете ";
            this.CountNumTicket.Name = "CountNumTicket";
            this.CountNumTicket.Width = 200;
            // 
            // SelectedNum
            // 
            this.SelectedNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SelectedNum.HeaderText = "Выбранные числа";
            this.SelectedNum.Name = "SelectedNum";
            // 
            // template
            // 
            this.template.Location = new System.Drawing.Point(630, 418);
            this.template.Name = "template";
            this.template.Size = new System.Drawing.Size(158, 20);
            this.template.TabIndex = 4;
            this.template.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(575, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Шаблон:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.template);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnDeleteTicket);
            this.Controls.Add(this.btnChangeTicket);
            this.Controls.Add(this.btnAppendTicket);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Button btnAppendTicket;
        private System.Windows.Forms.Button btnChangeTicket;
        private System.Windows.Forms.Button btnDeleteTicket;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NumberTicket;
        private DataGridViewTextBoxColumn CirculationNumber;
        private DataGridViewTextBoxColumn CountNumTicket;
        private DataGridViewTextBoxColumn SelectedNum;
        private TextBox template;
        private Label label1;
    }
}

