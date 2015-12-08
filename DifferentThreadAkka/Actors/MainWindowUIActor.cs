using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using DifferentThreadAkka.ViewModel;
using DifferentThreadAkka.Messages;

namespace DifferentThreadAkka.Actors {
    public class MainWindowUIActor : ReceiveActor {

        private MainWindowViewModel _viewModel;
        private IActorRef _coordinatorActorRef;
        public MainWindowUIActor(MainWindowViewModel viewModel) {
            _viewModel = viewModel;
            //_coordinatorActorRef = coordinatorActorRef;
            Receive<ActiveChildMessage>(message => ReceivedMessage(message));
        }   

        private void ReceivedMessage(ActiveChildMessage message) {
            _viewModel.Texting += string.Format("\nThreadID: {0} Received message received",Thread.CurrentThread.ManagedThreadId);
        }
    }
}
