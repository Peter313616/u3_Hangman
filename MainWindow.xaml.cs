/*
 * Peter McEwen
 * April 18, 2018
 * Hangman summative
 * allow the user to play hangman
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace u3_Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string word = "";
        int randNumber = 0;
        string letterGuess = "";
        bool GameOver = false;
        
        public MainWindow()
        {
            InitializeComponent();

            randNumber = random.Next(10, 11);
            //MessageBox.Show(randNumber.ToString());

            StreamReader streamreader = new StreamReader("WordList.txt");

            int counter = 0;
            while (counter != randNumber)
            {
                string temp = streamreader.ReadLine();
                //MessageBox.Show(temp);
                counter++;
                word = temp;
            }
            //MessageBox.Show(word);

            string hiddenWord = "";
            //MessageBox.Show(word.Length.ToString());
            for (int i = 0; i < word.Length; i++)
            {
                hiddenWord += "-";
                Console.WriteLine(i.ToString());
            }
            lblHiddenWord.Content = hiddenWord;
        }

        private void btnCheckGuess_Click(object sender, RoutedEventArgs e)
        {
            letterGuess = txtUserGuess.Text;

            string hiddenWord = lblHiddenWord.Content.ToString();
            //MessageBox.Show(hiddenWord);

            string NewOutput = "";

            bool incorrectGuess = true;

            for (int i = 0; i < word.Length; i++)
            {
                string oldOutput = lblHiddenWord.Content.ToString();
                
                string currentLetter = word.Substring(i, 1);
                if (currentLetter == letterGuess)
                {
                    NewOutput += letterGuess;
                    incorrectGuess = false;
                }
                else
                {
                    NewOutput += oldOutput.Substring(i, 1);
                }                
            }
            
            lblHiddenWord.Content = NewOutput;
            lblUsedLetters.Content += letterGuess;

            if (incorrectGuess == true)
            {
                string temp = lblLivesRemianing.Content.ToString();
                string lives = temp.Substring(12, 1);
                //MessageBox.Show(lives);
                int GuessesLeft = 0;
                int.TryParse(lives, out GuessesLeft);
                int i = GuessesLeft - 1;
                lblLivesRemianing.Content = "Lives Left: " + i;

                if (i == 0)
                {
                    MessageBox.Show("You Lose" + "\n" + "Play Again");
                    GameOver = true;
                    startGame(GameOver);
                }
            }
            bool test = false;
            CheckForWin(test);
            
            //MessageBox.Show(test.ToString());
            startGame(test);      
        }

        private bool CheckForWin (bool Win)
        {
            if (lblHiddenWord.Content.ToString() == word)
            {
                MessageBox.Show("congrats, You Win");
                return Win = true;                                
            }
            else
            {
                return Win = false;
            }
            
        }

        private bool startGame (bool test)
        {
            //MessageBox.Show(test.ToString());
            if (lblHiddenWord.Content.ToString() == word || GameOver == true)
            {
                randNumber = random.Next(1, 11);
                //MessageBox.Show(randNumber.ToString());

                StreamReader streamreader = new StreamReader("WordList.txt");

                int counter = 0;
                while (counter != randNumber)
                {
                    string temp = streamreader.ReadLine();
                    //MessageBox.Show(temp);
                    counter++;
                    word = temp;
                }
                //MessageBox.Show(word);

                string hiddenWord = "";
                //MessageBox.Show(word.Length.ToString());
                for (int i = 0; i < word.Length; i++)
                {
                    hiddenWord += "-";
                    Console.WriteLine(i.ToString());
                }

                lblHiddenWord.Content = hiddenWord;
                lblLivesRemianing.Content = "Lives Left: 4";
                lblUsedLetters.Content = "";
                GameOver = false;
                return test = true;
            }
            else 
            {
                return test = false;
            }
        }
    }
}
