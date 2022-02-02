using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakurosTest
{
    public partial class Form1 : Form
    {
        GameBoard gb = new GameBoard();

        //Music
        bool musicOn = false;
        SoundPlayer music = new SoundPlayer("Song01.wav");

        public Form1()
        {
            InitializeComponent();
            radioButtonEasyComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            radioButtonHardComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            radioButtonNmComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            //okok
        }
        public void PlayMusic() 
        {
            if (musicOn == false)
            {
                music.PlayLooping();
                buttonMusic.Image = kakurosTest.Properties.Resources.volume__1_;
                musicOn = true;
            }
            else
            {
                buttonMusic.Image = kakurosTest.Properties.Resources.mute;
                music.Stop();
                musicOn = false;
            }

        }

        private void GenerateBoard(int difficulty, int complexity)
        {
            Random rnd = new Random();
            int numberRange = 1;

            if (complexity == 1) {  numberRange = 25;  }
            if (complexity == 2) {  numberRange = 50;  }
            if (complexity == 3) {  numberRange = 100; }


            gb.n1 = rnd.Next(2, numberRange);
            gb.n4 = rnd.Next(2, numberRange);
            gb.n7 = rnd.Next(2, numberRange);

            gb.a1 = gb.n1 + gb.n4 + gb.n7;

            gb.n2 = rnd.Next(2, numberRange);
            gb.n3 = rnd.Next(2, numberRange);

            gb.a4 = gb.n1 + gb.n2 + gb.n3;

            gb.n5 = rnd.Next(2, numberRange);
            gb.n6 = rnd.Next(2, numberRange);

            gb.a5 = gb.n4 + gb.n5 + gb.n6;


            gb.n8 = rnd.Next(2, numberRange);
            gb.n9 = rnd.Next(2, numberRange);

            gb.a6 = gb.n7 + gb.n8 + gb.n9;

            gb.a2 = gb.n2 + gb.n5 + gb.n8;
            gb.a3 = gb.n3 + gb.n6 + gb.n9;

            

            labelNum1x.Text = gb.a1.ToString();
            labelNum2x.Text = gb.a2.ToString();
            labelNum3x.Text = gb.a3.ToString();

            labelNum1y.Text = gb.a4.ToString();
            labelNum2y.Text = gb.a5.ToString();
            labelNum3y.Text = gb.a6.ToString();

            textBoxNum1.Text = gb.n1.ToString();
            textBoxNum2.Text = gb.n2.ToString();
            textBoxNum3.Text = gb.n3.ToString();
            textBoxNum4.Text = gb.n4.ToString();
            textBoxNum5.Text = gb.n5.ToString();
            textBoxNum6.Text = gb.n6.ToString();
            textBoxNum7.Text = gb.n7.ToString();
            textBoxNum8.Text = gb.n8.ToString();
            textBoxNum9.Text = gb.n9.ToString();

        }

        public bool verifyBoard(GameBoard gb)
        {
            return true;
        }
        public void verifyAnswer() 
        { 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButtonNmComp.Checked = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gb.minutes = 1;
            gb.seconds = 21;
            timerGame.Start();
            GenerateBoard(1, gb.complexity);
        }



        private void radioButtonComplex_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int buttonId =  Convert.ToInt32(rb.Tag);
            gb.complexity = buttonId;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
                int mistakes = 0;

                if (textBoxNum1.Text != gb.n1.ToString()) { textBoxNum1.BackColor = Color.Red; mistakes++; } else { textBoxNum1.BackColor = Color.Green; }
                if (textBoxNum2.Text != gb.n2.ToString()) { textBoxNum2.BackColor = Color.Red; mistakes++; } else { textBoxNum2.BackColor = Color.Green; }
                if (textBoxNum3.Text != gb.n3.ToString()) { textBoxNum3.BackColor = Color.Red; mistakes++; } else { textBoxNum3.BackColor = Color.Green; }
                if (textBoxNum4.Text != gb.n4.ToString()) { textBoxNum4.BackColor = Color.Red; mistakes++; } else { textBoxNum4.BackColor = Color.Green; }
                if (textBoxNum5.Text != gb.n5.ToString()) { textBoxNum5.BackColor = Color.Red; mistakes++; } else { textBoxNum5.BackColor = Color.Green; }
                if (textBoxNum6.Text != gb.n6.ToString()) { textBoxNum6.BackColor = Color.Red; mistakes++; } else { textBoxNum6.BackColor = Color.Green; }
                if (textBoxNum7.Text != gb.n7.ToString()) { textBoxNum7.BackColor = Color.Red; mistakes++; } else { textBoxNum7.BackColor = Color.Green; }
                if (textBoxNum8.Text != gb.n8.ToString()) { textBoxNum8.BackColor = Color.Red; mistakes++; } else { textBoxNum8.BackColor = Color.Green; }
                if (textBoxNum9.Text != gb.n9.ToString()) { textBoxNum9.BackColor = Color.Red; mistakes++; } else { textBoxNum9.BackColor = Color.Green; }

                if (mistakes >= 1) { MessageBox.Show("You made " + mistakes.ToString() + " mistakes."); } else { MessageBox.Show("Congratulations you got it right!"); }


        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            labelMinutes.Text = gb.minutes.ToString();
            labelSeconds.Text = gb.seconds.ToString();

            if (gb.minutes == 0 && gb.seconds <= 59) { labelSeconds.ForeColor = Color.Orange; } 
            if (gb.minutes == 0 && gb.seconds < 30){ labelSeconds.ForeColor = Color.Red; }


            if (labelMinutes.Text == "0" && labelSeconds.Text == "0") 
            {
                timerGame.Stop();
                MessageBox.Show("Fail");
            }

            if (labelSeconds.Text == "0")
            {
                gb.minutes--;
                gb.seconds = 59;
            }
            else 
            {
                gb.seconds--;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayMusic();
        }

        private void buttonNextSong_Click(object sender, EventArgs e)
        {
             music = new SoundPlayer("Song02.wav");
            music.PlayLooping();
        }
    }
    }

/*  SOURCES OF MEDIA:
 
    Icon mute:
    https://www.flaticon.com/free-icons/mute 
    Mute icons created by Pixel perfect - Flaticon

    Icon play:
    https://www.flaticon.com/free-icons/speaker
    Speaker icons created by Pixel perfect - Flaticon

    Icon next song:
    https://www.flaticon.com/free-icons/next
    Next icons created by Freepik - Flaticon
 
    Song01:
    Summer by Ron Gelinas Chillout Lounge | https://open.spotify.com/artist/03JYfsI9Ke7JFuxHD239m2
    Music promoted by https://www.free-stock-music.com

    Song02:
    Madrugada by Popoi | https://soundcloud.com/popoimusic
    Music promoted by https://www.free-stock-music.com
    Creative Commons Attribution 3.0 Unported License
    https://creativecommons.org/licenses/by/3.0/deed.en_US
 

 
 
 
 
 
 
 
 */


