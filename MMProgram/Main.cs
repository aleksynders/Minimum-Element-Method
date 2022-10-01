using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMProgram
{
    public partial class Main : Form
    {
        public int rows;
        public int columns;
        public Main(int poi, int poj)
        {
            InitializeComponent();
            rows = poi;
            columns = poj;
            int x = 100;
            int y = 40;
            TextBox tb;
            for (int i = 0; i < rows; i++)  //СОЗДАНИЕ ОСНОВНЫХ ПОЛЕЙ
            {
                for (int j = 0; j < columns; j++)
                {
                    this.Controls.Add(new TextBox
                    {
                        Name = "tb_" + i + j,
                        Left = x,
                        Top = y,
                        Height = 20,
                        Width = 30,
                        MaxLength = 2,
                        ShortcutsEnabled = false
                    });
                    x += 50;
                }
                x = 100;
                y += 35;
            }
            y = 40;
            x = 650;
            for (int i = 0; i < rows; i++)  //СОЗДАНИЕ ВЕКТОРА M
            {
                if(i == 0)
                {
                    this.Controls.Add(new Label
                    {
                        Name = "vektorMnaz",
                        Text = "M",
                        Font = new Font("Microsoft Sans Serif", 12),
                        Height = 20,
                        Width = 30,
                        Left = x + 24,
                        Top = y - 27
                    });
                }
                    this.Controls.Add(new TextBox
                    {
                        Name = "vektorM" + i,
                        Left = x + 20,
                        Top = y,
                        Height = 20,
                        Width = 30,
                        MaxLength = 3,
                        ShortcutsEnabled = false
                    });
                y += 35;
            }
            y = 40;
            for (int i = 0; i < columns; i++)  //СОЗДАНИЕ ВЕКТОРА N
            {
                if (i == 0)
                {
                    this.Controls.Add(new Label
                    {
                        Name = "vektorNnaz",
                        Text = "N",
                        Height = 20,
                        Width = 30,
                        Font = new Font("Microsoft Sans Serif", 12),
                        Left = x + 64,
                        Top = y - 27
                    });
                }
                    this.Controls.Add(new TextBox
                    {
                        Name = "vektorN" + i,
                        Left = x + 60,
                        Top = y,
                        Height = 20,
                        Width = 30,
                        MaxLength = 3,
                        ShortcutsEnabled = false
                    });
                    y += 35;
            }
            foreach (Control control in Controls) //СОБЫТИЕ ДЛЯ ВСЕХ ТЕКСТБОКСОВ (ЗАПРЕТ НА ВВОД СИМВОЛОВ)
            {
                tb = control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += textBox_KeyPress;
                }
            }
        }


        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] massivM = new int[rows];
            int[] massivN = new int[columns];
            int znachN = 0;
            int znachM = 0;
            int ProverkaNaPystoty = 0;
            for (int i = 0; i < rows; i++) //ПРОВЕРКА НА ЗАПОЛНЕНИЕ ПОЛЕЙ ВЕКТОРОВ
            {
                if (Controls["vektorM" + i].Text.Length == 0|| Controls["vektorN" + i].Text.Length == 0) ProverkaNaPystoty = ProverkaNaPystoty + 1;
            }
            if (ProverkaNaPystoty == 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    if (Convert.ToInt32(Controls["vektorM" + i].Text) == 0) ProverkaNaPystoty = ProverkaNaPystoty + 1;
                }
                for (int i = 0; i < columns; i++)
                {
                    if ((Convert.ToInt32(Controls["vektorN" + i].Text) == 0)) ProverkaNaPystoty = ProverkaNaPystoty + 1;
                }
                if(ProverkaNaPystoty == 0)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        massivM[i] = Convert.ToInt32(Controls["vektorM" + i].Text);
                        znachM = znachM + massivM[i];
                    }
                    for (int i = 0; i < columns; i++)
                    {
                        massivN[i] = Convert.ToInt32(Controls["vektorN" + i].Text);
                        znachN = znachN + massivN[i];
                    }
                    if (znachN != znachM) //ПРОВЕРКА НА РАВНОСТЬ ВЕКТОРОВ
                        MessageBox.Show("Вектор M не равен вектору N", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        int temp = 0;
                        for (int i = 0; i < rows; i++)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                if (Controls["tb_" + i + j].Text.Length == 0)
                                {
                                    temp = temp + 1;
                                }
                            }
                        }
                        if (temp >= 1) MessageBox.Show("Заполните все поля массива!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            temp = 0;
                            for (int i = 0; i < rows; i++)
                            {
                                for (int j = 0; j < columns; j++)
                                {
                                    if (Convert.ToInt32(Controls["tb_" + i + j].Text) == 0)
                                    {
                                        temp = temp + 1;
                                    }
                                }
                            }
                            if (temp >= 1) MessageBox.Show("Значения в массиве не могут быть равны 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                int[,] massivChisel = new int[rows, columns];
                                int YDE = 0;
                                for (int i = 0; i < rows; i++)
                                {
                                    for (int j = 0; j < columns; j++)
                                    {
                                        massivChisel[i, j] = Convert.ToInt32(Controls["tb_" + i + j].Text);
                                    }
                                }
                                int indexI = 0;
                                int indexJ = 0;
                                while (znachM != 0 && znachN != 0)
                                {
                                    znachN = znachM = 0;
                                    int minElement = 100;
                                    for (int i = 0; i < rows; i++)
                                    {
                                        for(int j = 0; j < columns; j++)
                                        {
                                            if((minElement > massivChisel[i, j]) && (massivChisel[i,j] != 0) && (massivM[i] != 0) && (massivN[j] !=0))
                                            {
                                                minElement = massivChisel[i,j];
                                                indexI = i;
                                                indexJ = j;
                                            }

                                        }
                                    }
                                    massivChisel[indexI, indexJ] = 0;
                                    if (massivM[indexI] < massivN[indexJ])
                                    {
                                        massivN[indexJ] = massivN[indexJ] - massivM[indexI];
                                        YDE = YDE + (minElement * massivM[indexI]);
                                        massivM[indexI] = 0;
                                    }
                                    else if (massivM[indexI] > massivN[indexJ])
                                    {
                                        massivM[indexI] = massivM[indexI] - massivN[indexJ];
                                        YDE = YDE + (minElement * massivN[indexJ]);
                                        massivN[indexJ] = 0;
                                    }
                                    else if (massivM[indexI] == massivN[indexJ])
                                    {
                                        YDE = YDE + (minElement * massivM[indexI]);
                                        massivM[indexI] = 0;
                                        massivN[indexJ] = 0;
                                    }
                                    for (int i = 0; i < rows; i++)
                                    {
                                        znachM = znachM + massivM[i];
                                    }
                                    for(int i=0; i < columns; i++)
                                    {
                                        znachN = znachN + massivN[i];
                                    }
                                }
                                MessageBox.Show(Convert.ToString("F = " + YDE + " у.д.е."));
                            }
                        }
                    }
                }
                else MessageBox.Show("Значения в векторах M и N не могут быть равны 0!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Заполните все поля векторов M и N!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}