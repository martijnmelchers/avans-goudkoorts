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
        private static readonly int COUNTDOWN_SECONDS = 60;
        private GoudKoorts.Model.GoudKoorts _goudKoorts;
        private static Task _countdown;


        public Controller()
        {
            _outputView = new OutputView();
            _inputView = new InputView();
            _goudKoorts = new Model.GoudKoorts();
        }

        public void Start()
        {
            // Start the countdown.
            _countdown = DoActionAfter(COUNTDOWN_SECONDS, () => RunTrains());
        }


        // Runs when countdown ends.
        private void RunTrains()
        {
            // Set the game to running, no handling possible.
            _goudKoorts.State = GameState.RUNNING;

        }

        public static Task DoActionAfter(int delaySeconds, Action action)
        {
            return Task.Delay((delaySeconds * 1000)).ContinueWith(_ => action());
        }
    }
}
