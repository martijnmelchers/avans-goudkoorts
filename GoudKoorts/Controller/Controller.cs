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
        private static readonly float COUNTDOWN_SECONDS = 10;
        private GoudKoorts.Model.GoudKoorts _goudKoorts;
        private static Task _countdown;


        public Controller()
        {
            _outputView = new OutputView();
            _inputView = new InputView();
            _goudKoorts = new Model.GoudKoorts();
            _goudKoorts.State = GameState.SWITCHING;
        }

        public void Start()
        {
            // Start the countdown.
            _countdown = DoActionAfter(COUNTDOWN_SECONDS, () => RunTrains());

            // While the game is in "Switching" mode, allow the player to open/close Switches.
            while(_goudKoorts.State == GameState.SWITCHING)
            {
                var input = _inputView.GetInput();
                _goudKoorts.ToggleSwitch(input);
                _goudKoorts.Render();
            }
        }


        // Runs when countdown ends.
        private void RunTrains()
        {
            // Set the game to running, no handling possible.
            _goudKoorts.State = GameState.RUNNING;

            // Game loop.
            while(_goudKoorts.State == GameState.RUNNING)
            {
                // Instantiate timers.
                // Cart movement.
                // Score
                _goudKoorts.MoveCarts();
                _goudKoorts.Render();
                Thread.Sleep(1000);
            }
        }

        public static Task DoActionAfter(float delaySeconds, Action action)
        {
            return Task.Delay((int) (delaySeconds * 1000)).ContinueWith(_ => action());
        }
    }
}
