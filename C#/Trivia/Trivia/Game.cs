using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        public class Player
        {
            public string Name { get; set; }
            public int Purse { get; set; }
            public int Position { get; set; }
            public bool InPenaltyBox { get; set; }
        }

        private List<Player> players = new List<Player>();

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int currentPlayer;
        private bool isGettingOutOfPenaltyBox;

        private Player CurrentPlayer => players[currentPlayer];

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool Add(string playerName)
        {
            players.Add(new Player
            {
                Position = 0,
                Purse = 0,
                InPenaltyBox = false,
                Name = playerName
            });

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(players[currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (CurrentPlayer.InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer].Name + " is getting out of the penalty box");
                    MovePlayerAndAskQuestion(roll);
                }
                else
                {
                    Console.WriteLine(players[currentPlayer].Name + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                MovePlayerAndAskQuestion(roll);
            }
        }


        private void MovePlayerAndAskQuestion(int roll)
        {
            CurrentPlayer.Position = CurrentPlayer.Position + roll;
            if (CurrentPlayer.Position > 11) CurrentPlayer.Position = CurrentPlayer.Position - 12;

            Console.WriteLine(players[currentPlayer].Name
                              + "'s new location is "
                              + CurrentPlayer.Position);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }


        private void AskQuestion()
        {
            var currentCategory = CurrentCategory();
            switch (currentCategory)
            {
                case "Pop":
                    Console.WriteLine(_popQuestions.First());
                    _popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(_scienceQuestions.First());
                    _scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(_sportsQuestions.First());
                    _sportsQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(_rockQuestions.First());
                    _rockQuestions.RemoveFirst();
                    break;
            }
        }

        private string CurrentCategory()
        {
            return (CurrentPlayer.Position % 4) switch
            {
                0 => "Pop",
                1 => "Science",
                2 => "Sports",
                _ => "Rock"
            };
        }

        public bool WasCorrectlyAnswered()
        {
            if (!CurrentPlayer.InPenaltyBox || isGettingOutOfPenaltyBox) return HandleCorrectAnswer();
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        private bool HandleCorrectAnswer()
        {
            Console.WriteLine("Answer was correct!!!!");
            CurrentPlayer.Purse += 1;
            Console.WriteLine(players[currentPlayer].Name
                              + " now has "
                              + CurrentPlayer.Purse
                              + " Gold Coins.");

            var didPlayerWin = DoPlayerContinueToPlay();
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;

            return didPlayerWin;
        }


        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer].Name + " was sent to the penalty box");
            CurrentPlayer.InPenaltyBox = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool DoPlayerContinueToPlay()
        {
            return CurrentPlayer.Purse != 6;
        }
    }
}