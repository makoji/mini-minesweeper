using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace msweep
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // difficulty choice buttons
        // pressing any button starts a game of the corresponding difficulty (opens in a new window) and hides this window

        private void bttnDiffEasy_Click(object sender, EventArgs e)
        {
            StartGame("Easy");
        }

        private void bttnDiffMedium_Click(object sender, EventArgs e)
        {
            StartGame("Medium");
        }

        private void bttnDiffHard_Click(object sender, EventArgs e)
        {
            StartGame("Hard");
        }

        private void StartGame(string difficulty)
        {
            Form2 f2 = new Form2();
            f2.SetDifficulty(difficulty);
            f2.Show();
            this.Hide();
        }
    }
}
