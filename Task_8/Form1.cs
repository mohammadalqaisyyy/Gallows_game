using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Mohammad Al-Qaisy

namespace Task_8
{
    public partial class Form1 : Form
    {
        struct line
        {
            public int x1, x2, y1, y2;
            public line(int x1, int x2, int y1, int y2) {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }
        }
        line[] lines = { new line(50,100,300,300), new line(75, 75, 0, 300),new line(75, 175, 0, 0),
            new line(175,175,0,75), new line(175,175,125,175), new line(175,150,175,200), 
            new line(175,150,140,125),new line(175,200,140,125)};
        string[] listString = {"Seven", "World", "About", "Again", "Happy", "Party", "Piano", "Apple"
            , "Tiger", "Woman", "China", "After", "Lemon", "Forty", "Money", "Earth", "Pizza", "Heart",
            "Amber", "Forty"};
        Point start = new Point(300, 50);
        int limit = -1;
        bool head_drawn = false;
        string picked = string.Empty;
        Random random = new Random();
        int score = 0;
        Rectangle O = new Rectangle();

        public Form1()
        {
            InitializeComponent();
            //label1.Text = picked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            picked = (listString[random.Next(0, 20)]);
            int x = 50, y = 75;
            for(int i = 1; i <= 26; i++) {
                if (i == 25)
                    x += 50;
                Button b = new Button();
                b.Text = ((char)(64+i)).ToString();
                b.Size = new Size(40, 30);
                b.Location = new Point(x, y);
                this.Controls.Add(b);
                b.Click += new System.EventHandler(Clicked);
                x += 50;
                if (i % 4 == 0) {
                    y += 40;
                    x = 50;
                }
            }
        }

        public void Clicked(object sender, EventArgs e)
        {
            List<int> inIndex = new List<int>();
            List<char> inChar = new List<char>();
            string x= (sender as Button).Text.ToString();
            char c1 = x[0], c2 = (char)((int)(c1 + 32));
            bool check = false;
            for (int i = 0; i < 5; i++) {
                if (c1 == picked[i] || c2 == picked[i]) {
                    (sender as Button).Enabled = false;
                    check = true;
                    inIndex.Add(i);
                    inChar.Add(c1);
                }
            }
            if (check)
                toTextBox(inIndex, inChar);
            else {
                limit++;
                draw(limit);
            }
        }

        void toTextBox(List<int> listI, List<char> listC)
        {
            int i = 0;
            foreach(int t in listI) {
                switch (t) {
                    case 0:
                        textBox1.Text = listC[i].ToString();
                        i++; break;
                    case 1:
                        textBox2.Text = listC[i].ToString();
                        i++; break;
                    case 2:
                        textBox3.Text = listC[i].ToString();
                        i++; break;
                    case 3:
                        textBox4.Text = listC[i].ToString();
                        i++; break;
                    case 4:
                        textBox5.Text = listC[i].ToString();
                        i++; break;
                }
            }
            score += i;
            if (score == 5)
                replay();
        }

        void replay()
        {
            if (score == 5) {
                MessageBox.Show("You win");
            }
            else if (limit == 8 && head_drawn) {
                head_drawn = false;
                limit = -1;
                MessageBox.Show("You lose");
            }
            this.Invalidate();
            picked = (listString[random.Next(0, 20)]);
            //label1.Text = picked;
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
            textBox4.Text = ""; textBox5.Text = "";
            score = 0; head_drawn = false; limit = -1;
            foreach (Button b in this.Controls.OfType<Button>())
                b.Enabled = true;
        }

        void draw(int x)
        {
            Graphics b = this.CreateGraphics();
            Pen p = new Pen(Color.Black,3);
            if (limit == 8) {
                O = new Rectangle(start.X + 150, start.Y + 75, 50, 50);
                b.DrawEllipse(p, O);
                head_drawn = true;
                replay();
            }
            else {
                line l = lines[x];
                b.DrawLine(p, new Point(l.x1 + start.X, l.y1 + start.Y), new Point(l.x2 + start.X, l.y2 + start.Y));
            }
            b.Dispose(); p.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i <= limit; i++) {
                draw(i);
            }
        }
    }
}
