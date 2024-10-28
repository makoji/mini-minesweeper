using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace msweep
{
    public partial class Form2 : Form
    {
        private int nRows, nColumns, nMines, flags, points, mineCounter, minesDiffused;
        private byte[,] Positions;
        private Button[,] ButtonList;
        private bool gameOver;
        private bool gameWon;

        public Form2()
        {
            InitializeComponent();
        }

        Random randomizer = new Random();

        // takes the player's choice from Form 1, and sets the size of the grid and number of mines accordingly
        public void SetDifficulty(string diffChoice)
        {
            switch (diffChoice)
            {
                case "Easy":
                    nRows = 9;
                    nColumns = 9;
                    nMines = 10;
                    Console.WriteLine("diff Easy");
                    break;
                case "Medium":
                    nRows = 16;
                    nColumns = 16;
                    nMines = 40;
                    Console.WriteLine("diff Medium");
                    break;
                case "Hard":
                    nRows = 16;
                    nColumns = 30;
                    nMines = 99;
                    Console.WriteLine("diff Hard");
                    break;
                default:
                    throw new ArgumentException("Invalid difficulty choice");
            }
            flags = nMines;
            minesDiffused = 0;
            gameOver = false;
            gameWon = false;

            // setting up the name and details of the game window/Form 2
            this.Text = $"minesweeper - {diffChoice.ToLower()}";
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            InitializeGame();  // Initialize the game after setting difficulty
        }

        private void InitializeGame()
        {
            Positions = new byte[nRows, nColumns];
            ButtonList = new Button[nRows, nColumns];
            CreateGameBoard();
            GenerateMines();
            ResizeFormToFitGameBoard();
        }

        private void CreateGameBoard()
        {
            // Clears the contents of the 'game board' (the panel in which the board is generated)
            pnlBody.Controls.Clear();

            int xLoc = 5;
            int yLoc = 5;

            ButtonList = new Button[nRows, nColumns];

            for (int y = 0; y < nRows; y++)
            {
                for (int x = 0; x < nColumns; x++)
                {
                    Button bttn = new Button
                    {
                        // sets up the look of the buttons/spaces on the board, and places them in pnlBody
                        Parent = pnlBody,
                        Location = new Point(xLoc, yLoc),
                        Size = new Size(25, 25),
                        Tag = $"{x},{y}",
                        BackgroundImage = null,
                        BackgroundImageLayout = ImageLayout.Center,
                        FlatStyle = FlatStyle.Standard,
                        FlatAppearance = { BorderSize = 0 }, // Remove border
                        BackColor = SystemColors.ControlLight
                    };

                    // makes the buttons clickable and right-clickable (can place a flag)
                    bttn.Click += BttnClick;
                    bttn.MouseUp += BttnMouseUp;

                    xLoc += 25;
                    ButtonList[y, x] = bttn;
                }

                yLoc += 25;
                xLoc = 5;
            }
        }

        private void GenerateMines()
        {
            // generates mines at random coordinates until the number of mines placed is equal to the number of mines set per the difficulty
            for (int mines = 0; mines < nMines; mines++)
            {
                int x, y;

                do
                {
                    x = randomizer.Next(0, nColumns);
                    y = randomizer.Next(0, nRows);
                } while (Positions[y, x] == 9);

                Positions[y, x] = 9;
            }

            // goes through every tile in the board
            // and every non-mine tile is assigned a value corresponding to the number of mines adjacent to it
            for (int x = 0; x < nColumns; x++)
            {
                for (int y = 0; y < nRows; y++)
                {
                    if (Positions[y, x] != 9)
                    {
                        mineCounter = CountAdjacentMines(x, y);
                        Positions[y, x] = (byte)(mineCounter == 0 ? 0 : mineCounter);
                    }
                }
            }
        }

        private int CountAdjacentMines(int x, int y)
        {
            int mineCount = 0;

            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    // looks at each of the tiles adjacent to the tile CountAdjacentMines is currently looking at
                    int newX = x + offsetX;
                    int newY = y + offsetY;

                    // if a position is valid and has a mine, mineCount is incremented
                    if (IsValidPosition(newX, newY) && Positions[newY, newX] == 9)
                    {
                        mineCount++;
                    }
                }
            }

            return mineCount;
        }

        // checks that the tile CountAdjacentMines is looking at is a valid spot on the board
        // (within the range of the board, ie not a negative number and not a number that has coordinates exceeding the grid size)
        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < nColumns && y < nRows;
        }

        // handles what happens when a tile is clicked
        private void BttnClick(object sender, EventArgs e)
        {
            // if the game has been won or lost, right-clicking doesn't do anything
            if (gameOver) return;
            if (gameWon) return;

            Button bttn = (Button)sender;

            int x = Convert.ToInt32(bttn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(bttn.Tag.ToString().Split(',')[1]);
            byte value = Positions[y, x];

            // regardless of what the value of the button is, the image is set to null if it's clicked - this gets rid of any flags.
            // if there *was* one, the player gets a flag back to use
            if (bttn.BackgroundImage == msweep.Properties.Resources.flagicon)
                flags++;
            bttn.BackgroundImage = null;

            // if the clicked tile has a mine -> game over
            if (value == 9)
            {
                bttn.FlatStyle = FlatStyle.Flat;
                bttn.BackgroundImage = msweep.Properties.Resources.mineicon;
                GameOver();
            }
            // if the clicked tile is not adjacent to any mines (empty spot)
            // -> all tiles it touches will also be opened
            // --> if it touches other empty spots, they will also have all their adjacent tiles opened, which is how large portions of the board can be cleared in one click
            else if (value == 0)
            {
                bttn.FlatStyle = FlatStyle.Flat;
                bttn.FlatAppearance.BorderSize = 0;
                bttn.Enabled = false;
                OpenAdjacentEmptyTiles(bttn);
                points++;
            }
            // if the tile is not a mine, but is adjacent to at least one mine
            else
            {
                bttn.FlatStyle = FlatStyle.Flat;
                bttn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                bttn.FlatAppearance.MouseDownBackColor = bttn.BackColor;
                bttn.FlatAppearance.MouseOverBackColor = bttn.BackColor;
                bttn.Text = value.ToString();
                points++;
            }

            // the button that was clicked can't be clicked again
            bttn.Click -= BttnClick;
        }
        
        // goes through all empty tiles adjacent to a an empty tile that was clicked, opening all of them 
        private void OpenAdjacentEmptyTiles(Button bttn)
        {
            int x = Convert.ToInt32(bttn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(bttn.Tag.ToString().Split(',')[1]);

            List<Button> emptyButtons = new List<Button>();
            for (int offsetX = -1; offsetX <= 1; offsetX++)
            {
                int checkerX = x + offsetX;

                for (int offsetY = -1; offsetY <= 1; offsetY++)
                {
                    int checkerY = y + offsetY;

                    if (checkerX == x && checkerY == y)
                        continue;

                    if (IsValidPosition(checkerX, checkerY))
                    {
                        Button bttnAdjacent = ButtonList[checkerY, checkerX];

                        int xAdjacent = Convert.ToInt32(bttnAdjacent.Tag.ToString().Split(',')[0]);
                        int yAdjacent = Convert.ToInt32(bttnAdjacent.Tag.ToString().Split(',')[1]);

                        byte value = Positions[yAdjacent, xAdjacent];

                        if (value == 0)
                        {
                            if (bttnAdjacent.FlatStyle == FlatStyle.Standard)
                            {
                                bttnAdjacent.FlatStyle = FlatStyle.Flat;
                                bttnAdjacent.FlatAppearance.BorderSize = 0;
                                bttnAdjacent.Enabled = false;
                                emptyButtons.Add(bttnAdjacent);
                            }
                        }
                        else if (value != 9)
                        {
                            bttnAdjacent.PerformClick();
                        }
                    }
                }
            }

            // recurses through the empty tiles that are connected to the 'original' clicked empty tile until none of them have any adjacent empty tiles left
            foreach (var bttnEmpty in emptyButtons)
            {
                OpenAdjacentEmptyTiles(bttnEmpty);
            }
        }

        // handles right-clicks -> placing and removing flags
        private void BttnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // if the game has been won or lost, right-clicking doesn't do anything
                if (gameOver) return;
                if (gameWon) return;

                Button bttn = (Button)sender;

                int x = Convert.ToInt32(bttn.Tag.ToString().Split(',')[0]);
                int y = Convert.ToInt32(bttn.Tag.ToString().Split(',')[1]);
                byte value = Positions[y, x];

                // if a non-flagged tile is right clicked, a flag is 'placed'
                // the number of flags available decreases
                if (bttn.BackgroundImage == null && flags > 0 && bttn.FlatStyle != FlatStyle.Flat)
                {
                    bttn.BackgroundImage = msweep.Properties.Resources.flagicon;
                    flags--;

                    // if a mine was flagged, the number of 'diffused' mines increments
                    // (number of total mines as set per difficulty is kept constant)
                    if  (value == 9)
                    {
                        minesDiffused++;
                        Console.WriteLine(minesDiffused);

                        // if the player flags every mine, they win
                        // (the number of correctly flagged mines matches the number of mines that were placed in this difficulty)
                        if (minesDiffused == nMines)
                        {
                            GameWon();
                        }
                    }
                }
                // if a flag is right-clicked, it is removed
                // the player gets a flag 'back' to be used
                else if (bttn.BackgroundImage != msweep.Properties.Resources.flagicon)
                {
                    bttn.BackgroundImage = null;
                    flags++;

                    // if a flag that was placed on a mine is removed, the mine is 'undefused' 
                    // (it has to be flagged again to count towards a win)
                    if (value == 9)
                        minesDiffused--;
                }
            }
        }


        private void GameOver()
        {
            gameOver = true;

            // tells the player they've lost, says their score, and ends the program
            MessageBox.Show($"You hit a mine! \nYour total score was {points}.", "game over :(");
            this.Close();
        }

        private void GameWon()
        {
            gameWon = true;

            // tells the player they've won and ends the program
            MessageBox.Show($"You did it! \nYou found all {minesDiffused} mines.", "congratulations!");
            this.Close();

        }

        private void ResizeFormToFitGameBoard()
        {
            // Calculate the new size of the form based on the game board
            int formWidth = (nColumns * 25) + 24;
            int formHeight = (nRows * 25) + 48;

            // Set the new size of the form
            this.Size = new Size(formWidth, formHeight);
        }
    }
}
