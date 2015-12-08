using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using DifferentThreadAkka.Messages;
using DifferentThreadAkka.ViewModel;

namespace DifferentThreadAkka.Actors {
    public class MainWindowUICordinatorActor : ReceiveActor {
        private IActorRef _activeActorRef, _consoleActorRef;
        private static int _counter;
        private readonly IDictionary<string, IActorRef> _childWindows;

        public MainWindowUICordinatorActor(IActorRef consoleWriterActor) {
            _childWindows = new Dictionary<string, IActorRef>();
            _consoleActorRef = consoleWriterActor;
            Receive<NewWindow>(message => CreateANewWindow(message));
            Receive<Message>(message => MessageFromChild(message));
            Receive<ActiveChildMessage>(message => MessageForActiveChild(message));
            Receive<SetActiveChildMessage>(message => MessageSetActiveChild(message));
            Receive<AskMessage>(message => MessageChildAsk(message));
        }

        private void CreateANewWindow(NewWindow message) {
            string windowId = "Window" + _counter++;
            var vm = new MainWindowViewModel(Self, windowId);
            _activeActorRef = CustomActorSystem.Instance.ActorSystemEx().ActorOf(Props.Create<MainWindowUIActor>(vm));
            CreateNewWindow.Create(vm);
            _childWindows.Add(windowId, _activeActorRef);
        }

        private void MessageFromChild(Message message) {
            _consoleActorRef.Tell(message);
        }

        private void MessageChildAsk(AskMessage message) {
            //await Task.Delay(10000);
            Sender.Tell("Got this value");
        }

        private void MessageForActiveChild(ActiveChildMessage message) {
            _activeActorRef.Tell(message);
        }

        private void MessageSetActiveChild(SetActiveChildMessage message) {
            if (_childWindows.ContainsKey(message.WindowId)) {
                _activeActorRef = _childWindows[message.WindowId];
            }
        }

    }
}
