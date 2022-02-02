using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace kakurosTest
{
    public partial class Form1 : Form
    {
        private GameBoard gb = new GameBoard();

        //Music
        private bool musicOn = false;

        private SoundPlayer music = new SoundPlayer("Song01.wav");
        private int songNumber = 1;

        public Form1()
        {
            InitializeComponent();
            radioButtonEasyComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            radioButtonHardComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            radioButtonNmComp.CheckedChanged += new EventHandler(radioButtonComplex_CheckedChanged);
            //okok
        }

        private void GenerateBoard(int difficulty, int complexity)
        {
            Random rnd = new Random();
            int numberRange = 1;

            if (complexity == 1) { numberRange = 25; }
            if (complexity == 2) { numberRange = 50; }
            if (complexity == 3) { numberRange = 100; }

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

            textBoxNum1xy.Text = gb.n1.ToString();
            textBoxNum2x.Text = gb.n2.ToString();
            textBoxNum3x.Text = gb.n3.ToString();
            textBoxNum2y.Text = gb.n4.ToString();
            textBoxNum2x2y.Text = gb.n5.ToString();
            textBoxNum3x2y.Text = gb.n6.ToString();
            textBoxNum3y.Text = gb.n7.ToString();
            textBoxNum2x3y.Text = gb.n8.ToString();
            textBoxNum3x3y.Text = gb.n9.ToString();
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
            comboBoxBackColor.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Setting time limit based on grid size and time selected
            if (radioButton3x3.Checked && radioButtonSlow.Checked) { gb.SetTime(7, 0); }
            if (radioButton3x3.Checked && radioButtonNormal.Checked) { gb.SetTime(5, 0); }
            if (radioButton3x3.Checked && radioButtonFast.Checked) { gb.SetTime(2, 25); }

            if (radioButton6x6.Checked && radioButtonSlow.Checked) { gb.SetTime(10, 0); }
            if (radioButton6x6.Checked && radioButtonNormal.Checked) { gb.SetTime(8, 25); }
            if (radioButton6x6.Checked && radioButtonFast.Checked) { gb.SetTime(6, 0); }

            if (radioButton9x9.Checked && radioButtonSlow.Checked) { gb.SetTime(20, 0); }
            if (radioButton9x9.Checked && radioButtonNormal.Checked) { gb.SetTime(15, 0); }
            if (radioButton9x9.Checked && radioButtonFast.Checked) { gb.SetTime(12, 0); }

            //Starting timer if user chose and option other than none
            if (!radioButtonNone.Checked) { timerGame.Start(); } else { labelSeconds.Text = "∞"; labelMinutes.Text = "∞"; }


            //Generating game board
            GenerateBoard(gb.gridSize, gb.complexity);
        }

        private void radioButtonComplex_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            int buttonId = Convert.ToInt32(rb.Tag);
            gb.complexity = buttonId;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int mistakes = 0;

            if (textBoxNum1xy.Text != gb.n1.ToString()) { textBoxNum1xy.BackColor = Color.Red; mistakes++; } else { textBoxNum1xy.BackColor = Color.Green; }
            if (textBoxNum2x.Text != gb.n2.ToString()) { textBoxNum2x.BackColor = Color.Red; mistakes++; } else { textBoxNum2x.BackColor = Color.Green; }
            if (textBoxNum3x.Text != gb.n3.ToString()) { textBoxNum3x.BackColor = Color.Red; mistakes++; } else { textBoxNum3x.BackColor = Color.Green; }
            if (textBoxNum2y.Text != gb.n4.ToString()) { textBoxNum2y.BackColor = Color.Red; mistakes++; } else { textBoxNum2y.BackColor = Color.Green; }
            if (textBoxNum2x2y.Text != gb.n5.ToString()) { textBoxNum2x2y.BackColor = Color.Red; mistakes++; } else { textBoxNum2x2y.BackColor = Color.Green; }
            if (textBoxNum3x2y.Text != gb.n6.ToString()) { textBoxNum3x2y.BackColor = Color.Red; mistakes++; } else { textBoxNum3x2y.BackColor = Color.Green; }
            if (textBoxNum3y.Text != gb.n7.ToString()) { textBoxNum3y.BackColor = Color.Red; mistakes++; } else { textBoxNum3y.BackColor = Color.Green; }
            if (textBoxNum2x3y.Text != gb.n8.ToString()) { textBoxNum2x3y.BackColor = Color.Red; mistakes++; } else { textBoxNum2x3y.BackColor = Color.Green; }
            if (textBoxNum3x3y.Text != gb.n9.ToString()) { textBoxNum3x3y.BackColor = Color.Red; mistakes++; } else { textBoxNum3x3y.BackColor = Color.Green; }

            if (mistakes >= 1) { MessageBox.Show("You made " + mistakes.ToString() + " mistakes."); } else { MessageBox.Show("Congratulations you got it right!"); StopTimer(); }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            labelMinutes.Text = gb.minutes.ToString();
            labelSeconds.Text = gb.seconds.ToString();

            if (gb.minutes == 0 && gb.seconds <= 59) { labelSeconds.ForeColor = Color.Orange; }
            if (gb.minutes == 0 && gb.seconds < 30) { labelSeconds.ForeColor = Color.Red; }

            if (labelMinutes.Text == "0" && labelSeconds.Text == "0")
            {
                timerGame.Stop();
                MessageBox.Show("Fail");
                ResetBoard();
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

        private void buttonNextSong_Click(object sender, EventArgs e)
        {
            if (songNumber == 1) { music = new SoundPlayer("Song02.wav"); }
            if (songNumber == 2) { music = new SoundPlayer("Song03.wav"); }
            if (songNumber == 3) { music = new SoundPlayer("Song04.wav"); }
            if (songNumber == 4) { music = new SoundPlayer("Song05.wav"); }
            if (songNumber == 5) { music = new SoundPlayer("Song06.wav"); }
            if (songNumber == 6) { music = new SoundPlayer("Song07.wav"); }
            if (songNumber == 7) { music = new SoundPlayer("Song08.wav"); }
            if (songNumber == 8) { music = new SoundPlayer("Song09.wav"); }
            if (songNumber == 9) { music = new SoundPlayer("Song01.wav"); songNumber = 0; }

            buttonMusic.Image = kakurosTest.Properties.Resources.volume__1_;
            musicOn = true;
            music.PlayLooping();
            songNumber++;
        }

        private void comboBoxBackColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BackgroundImage = null;
            if (comboBoxBackColor.SelectedIndex == 0)
            {
                this.BackColor = Color.White;
                buttonNextSong.BackColor = Color.Transparent;
                buttonMusic.BackColor = Color.Transparent;
                radioButtonNone.ForeColor = Color.Black;
                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        continue;
                    }
                    if (control is Button)
                    {
                        control.BackColor = Color.LightGray;
                        continue;
                    }
                    control.ForeColor = Color.Black;
                }
            }

            if (comboBoxBackColor.SelectedIndex == 1)
            {
                this.BackColor = Color.Black;
                buttonNextSong.BackColor = Color.White;
                buttonMusic.BackColor = Color.White;
                radioButtonNone.ForeColor = Color.White;
                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        continue;
                    }
                    if (control is Button)
                    {
                        control.BackColor = Color.Green;
                        continue;
                    }
                    if (control is ComboBox)
                    {
                        continue;
                    }
                    control.ForeColor = Color.White;
                }
            }

            if (comboBoxBackColor.SelectedIndex == 2) { this.BackgroundImage = kakurosTest.Properties.Resources.Background01; }
            if (comboBoxBackColor.SelectedIndex == 3) { this.BackgroundImage = kakurosTest.Properties.Resources.Background02; }
            if (comboBoxBackColor.SelectedIndex == 4) { this.BackgroundImage = kakurosTest.Properties.Resources.Background03; }
            if (comboBoxBackColor.SelectedIndex == 5) { this.BackgroundImage = kakurosTest.Properties.Resources.BackGround04; }
            if (comboBoxBackColor.SelectedIndex == 6) { this.BackgroundImage = kakurosTest.Properties.Resources.BackGround05; }
            if (comboBoxBackColor.SelectedIndex == 7) { this.BackgroundImage = kakurosTest.Properties.Resources.BackGround06; }
            if (comboBoxBackColor.SelectedIndex == 8) { this.BackgroundImage = kakurosTest.Properties.Resources.BackGround07; }
            ResetGridBoxes();


        }

        private void radioButton6x6_CheckedChanged(object sender, EventArgs e)
        {

            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    if (control.Tag == "6" || control.Tag == "3")
                    {
                        control.Enabled = true;
                    }
                    else 
                    {
                        control.Enabled = false;
                    }
                }


            }
           

            gb.gridSize = 6;
            ResetGridBoxes();
        }

        private void radioButton3x3_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control control in Controls)
            {
                if (control is TextBox)
                {
                    if (control.Tag == "3")
                    {
                        control.Enabled = true;
                    }
                    else
                    {
                        control.Enabled = false;
                    }
                }


            }
            gb.gridSize = 3;
            ResetGridBoxes();
        }

        public void ResetGridBoxes() {

                foreach (Control control in Controls)
                {
                    if (control is TextBox)
                    {
                        if (control.Enabled == false && comboBoxBackColor.SelectedIndex != 0)
                        {
                            control.BackColor = Color.Black;
                        }
                        else {
                            control.BackColor = Color.White;
                        }
                    }
                }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetBoard();
           
        }

        private void ResetBoard() 
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
                if (control is Label)
                {
                    control.Text = "0";
                }

                if (control is TextBox && comboBoxBackColor.SelectedIndex == 0 && control.Enabled == false)
                {
                    control.BackColor = Color.White;
                }
                if (control is TextBox && comboBoxBackColor.SelectedIndex < 0 && control.Enabled == false)
                {
                    control.BackColor = Color.Black;
                }
                if (control is TextBox && control.Enabled == true)
                {
                    control.BackColor = Color.White;
                }


            }
            StopTimer();
        }
        public void StopTimer() 
        {
            labelSeconds.Text = "0";
            labelMinutes.Text = "0";
            timerGame.Stop();
        }

        private void radioButton9x9_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                {
                    if (control.Tag == "3" || control.Tag == "6" || control.Tag == "9")
                    {
                        control.Enabled = true;
                    }
                }

            }
            gb.gridSize = 9;
            ResetGridBoxes();

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

    Song03:
    Lucid Dreaming by | e s c p | https://escp-music.bandcamp.com
    Music promoted by https://www.free-stock-music.com
    Attribution 4.0 International (CC BY 4.0)
    https://creativecommons.org/licenses/by/4.0/

    Song04:
    Back Home by | e s c p | https://escp-music.bandcamp.com
    Music promoted by https://www.free-stock-music.com
    Attribution 4.0 International (CC BY 4.0)
    https://creativecommons.org/licenses/by/4.0/

    Song05:
    Over The Ocean by | e s c p | https://escp-music.bandcamp.com
    Music promoted by https://www.free-stock-music.com
    Attribution 4.0 International (CC BY 4.0)
    https://creativecommons.org/licenses/by/4.0/

    Song06:
    Dreaming Of Island by EuGenius Music | https://soundcloud.com/eu-genius
    Music promoted by https://www.free-stock-music.com
    Creative Commons Attribution-ShareAlike 3.0 Unported
    https://creativecommons.org/licenses/by-sa/3.0/deed.en_US

    Song07:
    A Dispute by Niya | https://soundcloud.com/niya90s
    Music promoted by https://www.free-stock-music.com
    Creative Commons Attribution-ShareAlike 3.0 Unported
    https://creativecommons.org/licenses/by-sa/3.0/deed.en_US

    Song08:
    Eternal Springtime by | e s c p | https://escp-music.bandcamp.com
    Music promoted by https://www.free-stock-music.com
    Attribution 4.0 International (CC BY 4.0)
    https://creativecommons.org/licenses/by/4.0/

    SONG

 */