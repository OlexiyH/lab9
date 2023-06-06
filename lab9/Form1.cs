using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB9
{
    public partial class Form1 : Form
    {
        private const int plotWidth = 800;
        private const int plotHeight = 600;
        private const int axisPadding = 30;

        private double a, b;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Запускаємо діалогове вікно для введення коефіцієнтів
            InputCoefficientsDialog dialog = new InputCoefficientsDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                a = dialog.A;
                b = dialog.B;
                PlotGraph();
            }
            else
            {
                Close();
            }
        }

        private void PlotGraph()
        {
            // Створюємо новий об'єкт Bitmap для графіки
            Bitmap bmp = new Bitmap(plotWidth, plotHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Встановлюємо чорний колір для ліній і тексту
                g.Clear(Color.White);
                Pen pen = new Pen(Color.Black);

                // Малюємо осі координат
                g.DrawLine(pen, axisPadding, plotHeight / 2, plotWidth - axisPadding, plotHeight / 2); // Ось X
                g.DrawLine(pen, axisPadding, 0, axisPadding, plotHeight); // Ось Y

                // Малюємо підписи осей
                Font font = new Font("Colibri", 14);
                g.DrawString("X", font, Brushes.Red, plotWidth - axisPadding, plotHeight / 2 + 5);
                g.DrawString("Y", font, Brushes.Red, axisPadding - 30, 0);

                // Обчислюємо та малюємо точки на графіку
                double t = -10;
                double dt = 0.1;

                while (t <= 10)
                {
                    double x = a * Math.Cosh(t);
                    double y = b * Math.Cosh(t);

                    int pixelX = (int)(plotWidth / 2 + x * 20); // Масштабуємо для зручного відображення
                    int pixelY = (int)(plotHeight / 2 - y * 20);

                    g.DrawRectangle(pen, pixelX, pixelY, 1, 1);

                    // Додаємо підписи значень на осі
                    if (t % 1 == 0)
                    {
                        g.DrawString(t.ToString(), font, Brushes.Red, pixelX, plotHeight / 2 + 5);
                        g.DrawString((-t).ToString(), font, Brushes.Red, pixelX, plotHeight / 2 - 15);
                    }

                    t += dt;
                }
            }
        // Відображаємо графіку на формі
        graphPictureBox.Image = bmp;
        }
    }

    public class InputCoefficientsDialog : Form
    {
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public InputCoefficientsDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Коеф. a:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Коеф. b:";
            // 
            // textBoxA
            // 
            this.textBox1.Location = new System.Drawing.Point(63, 12);
            this.textBox1.Name = "textBoxA";
            this.textBox1.Size = new System.Drawing.Size(50, 22);
            this.textBox1.Location = new System.Drawing.Point(100, 12);
            this.textBox1.TabIndex = 3;
            // 
            // textBoxB
            // 
            this.textBox2.Location = new System.Drawing.Point(63, 42);
            this.textBox2.Name = "textBoxB";
            this.textBox2.Size = new System.Drawing.Size(50, 22);
            this.textBox2.Location = new System.Drawing.Point(100, 42);
            this.textBox2.TabIndex = 4;
            // 
            // buttonOK
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 110);
            this.button1.Name = "buttonOK";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(93, 110);
            this.button2.Name = "buttonCancel";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // InputCoefficientsDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(184, 145);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputCoefficientsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Введіть коефіцієнти";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double a) &&
                double.TryParse(textBox2.Text, out double b))
            {
                A = a;
                B = b;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Неправильний формат введених даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}