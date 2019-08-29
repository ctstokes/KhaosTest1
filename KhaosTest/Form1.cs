using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KhaosTest
{
    public partial class Form1 : Form
    {

        List<string> listItems;
        int listQuantity = 100;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            listView1.Columns.Add("");
        }

        private void PopulateButton_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                listView1.Items.Clear();
            }

            listItems = new List<string>();

            for (int i = 1; i <= listQuantity; i++)
            {
                string itemText = "This is item number " + i;
                listItems.Add(itemText);

                ListViewItem listItem = new ListViewItem(itemText);
                listView1.Items.Add(listItem);
            }

            listView1.Columns[0].Width = listView1.Width - 25;
        }

        private void ColorBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pb = sender as PictureBox;
                if (pb == pictureBox7)
                {
                    pb.DoDragDrop(RandomBGColor(), DragDropEffects.Move);
                }
                else
                {
                    pb.DoDragDrop(ColorTranslator.ToHtml(pb.BackColor), DragDropEffects.Move);
                }
            }
            else if (sender is Label)
            {
                Label lbl = sender as Label;
                lbl.DoDragDrop(RandomBGColor(), DragDropEffects.Move);
            }
        }

        private void ListView1_DragDrop(object sender, DragEventArgs e)
        {
            //consoleOutput.Add("Drag Drop");
            Point dropPoint = listView1.PointToClient(new Point(e.X, e.Y));
            ListViewItem dropItem = listView1.GetItemAt(dropPoint.X, dropPoint.Y);
            
            if (dropItem != null) {
                int dropIndex = dropItem.Index;
                Color nbgColor = ColorTranslator.FromHtml(e.Data.GetData(DataFormats.Text) as string);
                dropItem.BackColor = nbgColor;
            }
        }

        private void ListView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                Color bgColor = Color.FromName(e.Data.GetData(DataFormats.Text) as string);
                if (bgColor != null)
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private string RandomBGColor ()
        {
            Color randColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            return ColorTranslator.ToHtml(randColor);
        }

    }
}
