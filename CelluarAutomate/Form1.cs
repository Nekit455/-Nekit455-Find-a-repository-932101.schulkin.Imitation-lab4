using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CelluarAutomate
{
    public partial class Form1 : Form
    {
        private const int CellSize = 20;
        private const int NumCells = 30;

        private bool[] cells;
        private int rule;

        public Form1()
        {
            InitializeComponent();
            cells = new bool[NumCells];
            InitializeCells();
        }

        private void InitializeCells()
        {
            Random random = new Random();
            for (int i = 0; i < NumCells; i++) // Заполнение массива клеток (1 или 0)
            {
                cells[i] = random.Next(2) == 0;
            }
        }

        private void DrawCells()
        {
            Graphics g = CreateGraphics();
            g.Clear(Color.White);

            for (int i =0; i < NumCells; i++)
            {
                Brush brush = cells[i] ? Brushes.Black : Brushes.White;
                g.FillRectangle(brush, i * CellSize, 50, CellSize, CellSize);
            }
        }

        private void EvolveCells()
        {
            bool[] newCells = new bool[NumCells];

            for (int i = 0; i < NumCells; i++)
            {
                int left = (i - 1 + NumCells) % NumCells;
                int right = (i + 1) % NumCells;

                bool leftCell = cells[left];
                bool centerCell = cells[i];
                bool rightCell = cells[right];

                newCells[i] = ApplyRule(leftCell, centerCell, rightCell);
            }

            cells = newCells;
        }

        private bool ApplyRule(bool leftC, bool centerC, bool rightC)
        {
            
            int ruleValue = (leftC ? 4 : 0) + (centerC ? 2 : 0) + (rightC ? 1 : 0);
            return (rule & (1 << ruleValue)) != 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            EvolveCells();
            DrawCells();
        }

        private void BtnStart_Click_1(object sender, EventArgs e)
        {
            rule = (int)numRule.Value;
            timer2.Start();
        }

        private void BtnStop_Click_1(object sender, EventArgs e)
        {
            timer2.Stop();
        }
    }
}
