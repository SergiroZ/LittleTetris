using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Drawing.Graphics;
using Timer = System.Timers.Timer;

namespace LittleTetris
{
    public partial class Form1 : Form, ITetris
    {
        public const int width = 15;
        public const int height = 25;
        public const int k = 15;

        public Bitmap Bitfield = new Bitmap(k * width + 1, k * height + 1);
        public int[,] Field = new int[width, height];
        public Graphics Gr;
        public int[,] Shape = new int[2, 4];


        public Form1()
        {
            aTimer = new Timer(200);
            aTimer.Elapsed += TickTimer_Tick;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            InitializeComponent();
            Gr = FromImage(Bitfield);
            for (var i = 0; i < width; i++)
                Field[i, height - 1] = 1;
            for (var i = 0; i < height; i++)
            {
                Field[0, i] = 1;
                Field[width - 1, i] = 1;
            }

            SetShape();
        }

        public void FillField()
        {
            Gr.Clear(Color.Black);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (Field[i, j] != 1) continue;
                    Gr.FillRectangle(Brushes.Green, i * k,
                                     j * k, k, k);
                    Gr.DrawRectangle(Pens.Black, i * k,
                                     j * k, k, k);
                }
            }

            for (var i = 0; i < 4; i++)
            {
                Gr.FillRectangle(Brushes.Red, Shape[1, i] * k, Shape[0, i] * k,
                                 k, k);
                Gr.DrawRectangle(Pens.Black, Shape[1, i] * k, Shape[0, i] * k,
                                 k, k);
            }

            FieldPictureBox.Image = Bitfield;
        }

        public void TickTimer_Tick(object sender, EventArgs e)
        {
            if (Field[8, 3] == 1)
                Environment.Exit(0);
            for (var i = 0; i < 4; i++)
                Shape[0, i]++;
            for (var i = height - 2; i > 2; i--)
            {
                var cross = (from t in Enumerable.Range(0, Field.GetLength(0))
                                                 .Select(j => Field[j, i])
                                                 .ToArray()
                             where t == 1
                             select t).Count();
                if (cross != width) continue;
                for (var kIndex = i; kIndex > 1; kIndex--)
                    for (var l = 1; l < width - 1; l++)
                        Field[l, kIndex] = Field[l, kIndex - 1];
            }

            if (FindMistake())
            {
                for (var i = 0; i < 4; i++)
                    Field[Shape[1, i], --Shape[0, i]]++;
                SetShape();
            }

            FillField();
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.NumPad4:
                    for (var i = 0; i < 4; i++)
                        Shape[1, i]--;
                    if (FindMistake())
                        for (var i = 0; i < 4; i++)
                            Shape[1, i]++;
                    break;
                case Keys.NumPad6:
                    for (var i = 0; i < 4; i++)
                        Shape[1, i]++;
                    if (FindMistake())
                        for (var i = 0; i < 4; i++)
                            Shape[1, i]--;
                    break;
                case Keys.NumPad5:
                    var shapeT = new int[2, 4];
                    Array.Copy(Shape, shapeT, Shape.Length);
                    int max_x = 0, max_y = 0;
                    for (var i = 0; i < 4; i++)
                    {
                        if (Shape[0, i] > max_y)
                            max_y = Shape[0, i];
                        if (Shape[1, i] > max_x)
                            max_x = Shape[1, i];
                    }

                    for (var i = 0; i < 4; i++)
                    {
                        var temp = Shape[0, i];
                        Shape[0, i] = max_y - (max_x - Shape[1, i]) - 1;
                        Shape[1, i] = max_x - (3 - (max_y - temp)) + 1;
                    }

                    if (FindMistake())
                        Array.Copy(shapeT, Shape, Shape.Length);
                    break;
            }
        }

        public void SetShape()
        {
            var x = new Random(DateTime.Now.Millisecond);
            var val = x.Next(7);
            switch (val)
            {
                case 0:
                    Shape = new[,] {{2, 3, 4, 5}, {8, 8, 8, 8}};
                    break;
                case 1:
                    Shape = new[,] {{2, 3, 2, 3}, {8, 8, 9, 9}};
                    break;
                case 2:
                    Shape = new[,] {{2, 3, 4, 4}, {8, 8, 8, 9}};
                    break;
                case 3:
                    Shape = new[,] {{2, 3, 4, 4}, {8, 8, 8, 7}};
                    break;
                case 4:
                    Shape = new[,] {{3, 3, 4, 4}, {7, 8, 8, 9}};
                    break;
                case 5:
                    Shape = new[,] {{3, 3, 4, 4}, {9, 8, 8, 7}};
                    break;
                case 6:
                    Shape = new[,] {{3, 4, 4, 4}, {8, 7, 8, 9}};
                    break;
            }
        }

        public bool FindMistake()
        {
            for (var i = 0; i < 4; i++)
                if (Shape[1, i] >= width || Shape[0, i] >= height ||
                    Shape[1, i] <= 0 || Shape[0, i] <= 0 ||
                    Field[Shape[1, i], Shape[0, i]] == 1)
                    return true;
            return false;
        }
    }
}