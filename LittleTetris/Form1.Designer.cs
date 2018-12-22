﻿using System;
using System.Windows.Forms;

namespace LittleTetris
{
    public interface ITetrisTimer
    {
        void TickTimer_Tick(object sender, EventArgs e);
    }

    public interface ITetrisFill
    {
        void FillField();
    }

    public interface ITetrisKeyDown
    {
        void Form1_KeyDown(object sender, KeyEventArgs e);
    }

    public interface ITetrisSetShape
    {
        void SetShape();
    }

    public interface ITetrisFindMistake
    {
        bool FindMistake();
    }

    public interface ITetris : ITetrisTimer, ITetrisFill, ITetrisKeyDown, ITetrisSetShape, ITetrisFindMistake
    {
    }

    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private static System.Timers.Timer aTimer;
        private System.Windows.Forms.PictureBox FieldPictureBox;


        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                aTimer.Stop();
                aTimer.Dispose();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FieldPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FieldPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // FieldPictureBox
            // 
            this.FieldPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("FieldPictureBox.Image")));
            this.FieldPictureBox.Location = new System.Drawing.Point(111, 12);
            this.FieldPictureBox.Name = "FieldPictureBox";
            this.FieldPictureBox.Size = new System.Drawing.Size(301, 475);
            this.FieldPictureBox.TabIndex = 0;
            this.FieldPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 520);
            this.Controls.Add(this.FieldPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.FieldPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

