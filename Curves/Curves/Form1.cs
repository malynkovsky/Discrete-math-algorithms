using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Curves
{
    public partial class Form1 : Form
    {
        private List<double> ptList = new List<double>();
        private BezierCurve bc = new BezierCurve();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ptList.Add(e.X);
            ptList.Add(e.Y);

            g.DrawRectangle(px, new Rectangle(e.X, e.Y, 1, 1));

        }

        Pen px = new Pen(Brushes.Red);
        Pen newpx = new Pen(Brushes.DodgerBlue);
        Graphics g;
        Graphics gp;
        Bitmap bmp;
        private void button1_Click(object sender, EventArgs e)
        {
            const int POINTS_ON_CURVE = 1000;
            List<double> gr = new List<double>();
            double[] ptind = new double[POINTS_ON_CURVE];
            double[] p = new double[POINTS_ON_CURVE];
            int k = 0;
            if (ptList.Count > 8)
            {

                for (int i = 0; i < ptList.Count; i += 2)
                {
                    k++;

                    if ((k == 4) && (i < ptList.Count - 1))
                    {
                        double xs = (ptList[i-2] + ptList[i]) / 2;
                        double ys = (ptList[i-1] + ptList[i + 1]) / 2;
                        gr.Add(xs);
                        gr.Add(ys);
                        gr.Add(xs);
                        gr.Add(ys);
                        k = 2;
                    }
                    gr.Add(ptList[i]);
                    gr.Add(ptList[i + 1]);
                }

                for (int i = 0; i < 8*(gr.Count / 8); i += 8)
                {
                    double[] send = new double[8];
                    send[0] = gr[i];
                    send[1] = gr[i + 1];
                    send[2] = gr[i + 2];
                    send[3] = gr[i + 3];
                    send[4] = gr[i + 4];
                    send[5] = gr[i + 5];
                    send[6] = gr[i + 6];
                    send[7] = gr[i + 7];
                    bc.Bezier2D(send, (POINTS_ON_CURVE) / 2, ptind);
                    for (int j = 1; j != POINTS_ON_CURVE - 1; j += 2)
                    {
                        g.DrawRectangle(newpx, new Rectangle((int)ptind[j + 1], (int)ptind[j], 1, 1));
                        g.Flush();
                        gp.DrawRectangle(newpx, new Rectangle((int)ptind[j + 1], (int)ptind[j], 1, 1));
                        gp.Flush();

                    }
                }
                for (int i = (8*(gr.Count / 8)); i < gr.Count; i += 100)
                {
                    double[] send = new double[gr.Count - (8 * (gr.Count / 8))];
                    for (int j = 0; j < send.Length; j+=2)
                    {
                        send[j] = gr[j + i];
                        send[j+1] = gr[j + i + 1];
                    }
                        
                    bc.Bezier2D(send, (POINTS_ON_CURVE) / 2, ptind);
                    for (int j = 1; j != POINTS_ON_CURVE - 1; j += 2)
                    {
                        g.DrawRectangle(newpx, new Rectangle((int)ptind[j + 1], (int)ptind[j], 1, 1));
                        g.Flush();
                        gp.DrawRectangle(newpx, new Rectangle((int)ptind[j + 1], (int)ptind[j], 1, 1));
                        gp.Flush();

                    }
                }
            }
            else
            {

                double[] send = new double[ptList.Count];
                ptList.CopyTo(send);
                bc.Bezier2D(send, (POINTS_ON_CURVE) / 2, p);
                for (int i = 1; i != POINTS_ON_CURVE - 1; i += 2)
                {
                    g.DrawRectangle(newpx, new Rectangle((int)p[i + 1], (int)p[i], 1, 1));
                    g.Flush();
                    gp.DrawRectangle(newpx, new Rectangle((int)p[i + 1], (int)p[i], 1, 1));
                    gp.Flush();
                    //Application.DoEvents();
                }

            }
            double pnext = ptList[ptList.Count - 2];
            double pnext2 = ptList[ptList.Count - 1];
            ptList.Clear();
            ptList.Add(pnext);
            ptList.Add(pnext2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = pictureBox1.CreateGraphics();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gp = Graphics.FromImage(bmp);
            gp.Clear(Color.Transparent);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            gp.Clear(Color.Transparent);
            ptList.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            DialogResult dr = open.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Refresh();
                gp.Clear(Color.Transparent);
                ptList.Clear();
                string file = open.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(file);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ptList.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorDialog col_d = new ColorDialog();
            if (col_d.ShowDialog() == DialogResult.OK)
            {
                newpx = new Pen(col_d.Color);
                panel1.BackColor = col_d.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bmpSave = new Bitmap( pictureBox1.Width, pictureBox1.Height);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "png";
            sfd.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void panel4_Paint(object sender, PaintEventArgs e) { }

        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog col_d = new ColorDialog();
            if (col_d.ShowDialog() == DialogResult.OK)
            {
                px = new Pen(col_d.Color);
                panel4.BackColor = col_d.Color;
            }
        }
    }
}