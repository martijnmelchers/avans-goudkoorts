using GoudKoorts.Model;
using GoudKoorts.View;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GoudKoorts
{
    class Controller
    {
        private readonly OutputView _outputView;
        private readonly InputView _inputView;
        private float COUNTDOWN_SECONDS = 10;
        private GoudKoorts.Model.GoudKoorts _goudKoorts;
        private Task _countdown;
        private System.Timers.Timer _timer;
        private bool _runGame = true;

        private int _score = 0;

        public Controller()
        {
            _outputView = new OutputView();
            _inputView = new InputView();
            _goudKoorts = new Model.GoudKoorts();
            _goudKoorts.State = GameState.SWITCHING;
        }


        public void Start()
        {
            _outputView.Render(_goudKoorts.Origin, _goudKoorts.CalcScore());

            while (_runGame)
            {
                SwitchPhase();
            }

            // Show game over message.
            _outputView.ShowGameOver(_score);
        }

        public void SwitchPhase()
        {
            _goudKoorts.State = GameState.SWITCHING;

            COUNTDOWN_SECONDS = CalcSpeed();
            // Start the countdown.
            _countdown = DoActionAfter(COUNTDOWN_SECONDS, () => RunTrains());

            // While the game is in "Switching" mode, allow the player to open/close Switches.
            while(_goudKoorts.State == GameState.SWITCHING)
            {
                if (_inputView.KeyAvailable())
                {
                    var input = _inputView.GetInput();
                    _goudKoorts.ToggleSwitch(input);
                    _outputView.Render(_goudKoorts.Origin, _goudKoorts.CalcScore());
                }
            }
        }


        // Runs when countdown ends.
        private void RunTrains()
        {
            // Set the game to running, no handling possible.
            _goudKoorts.State = GameState.RUNNING;

            if(_goudKoorts.MoveCartsFresh() == false)
            {
                //End the game.
                _runGame = false;
            }

            _outputView.Render(_goudKoorts.Origin,  _goudKoorts.CalcScore());
        }

        // Calculate the speed at which the game will run.
        private float CalcSpeed()
        {
            var speed = COUNTDOWN_SECONDS - (_score / 10) * 05;
            return speed;
        }

        public static Task DoActionAfter(float delaySeconds, Action action)
        {
            return Task.Delay((int) (delaySeconds * 1000)).ContinueWith(_ => action());
        }
    }
}
