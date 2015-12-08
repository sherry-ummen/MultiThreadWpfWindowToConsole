using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Akka.Actor;
using DifferentThreadAkka.Commands;
using DifferentThreadAkka.Messages;

namespace DifferentThreadAkka.ViewModel {
    public class MainWindowViewModel : ViewModelBase {

        public string _texting { get; set; }
        public string Texting
        {
            get { return _texting; }
            set
            {
                if (_texting != value) { _texting = value; OnPropertyChanged("Texting"); }
            }
        }

        public ICommand PrintCommand { get; set; }
        public ICommand WindowActivatedCommand { get; set; }

        private ActorSelection _consoleWriterActor;
        private IActorRef _coordinatorActorRef;

        private string _windowId;

        private int _counter = 0;
        public MainWindowViewModel(IActorRef coordinatorActorRef, string windowId) {
            InitializeCommand();
            _windowId = windowId;
            _coordinatorActorRef = coordinatorActorRef;
            Texting = "Starting Receiving of Messages";
            //if (_consoleWriterActor == null) {
            //    _consoleWriterActor = CustomActorSystem.Instance.ActorSystemEx().ActorSelection("/user/ConsoleWriter");
            //}
        }

        private void InitializeCommand() {
            PrintCommand = new RelayCommand(Print);
            WindowActivatedCommand = new RelayCommand(SetActiveChild);
        }

        private async void Print() {
            //while (true) {
                _coordinatorActorRef.Tell(new Messages.Message() { Msg = "Messaging : " + _counter++ });
            object returnvalue = await _coordinatorActorRef.Ask(new AskMessage());
            _coordinatorActorRef.Tell(new Messages.Message() { Msg = "Messaging (Wait is over) : "  + _counter++ });
            //await Task.Delay(3000);
            //}
        }

        public void SetActiveChild() {
            _coordinatorActorRef.Tell(new SetActiveChildMessage() { WindowId = _windowId });
        }
    }
}
