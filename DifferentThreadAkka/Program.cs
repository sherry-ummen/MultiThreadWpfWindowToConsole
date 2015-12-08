using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Akka.Actor;
using Akka.Actor.Internal;
using DifferentThreadAkka.Actors;
using DifferentThreadAkka.Messages;

namespace DifferentThreadAkka {
    class Program {
        private static IActorRef _activeConsoleActor, _coordinatorActor;
        private static ActorSelection _uiActor;

        static void Main(string[] args) {
            _activeConsoleActor = CustomActorSystem.Instance.ActorSystemEx().ActorOf(Props.Create<ConsoleWriterActor>().WithDispatcher("custom-dedicated-dispatcher"), "ConsoleWriter");
            //_uiActor = CustomActorSystem.Instance.ActorSystemEx().ActorSelection("/user/MainWindowUI");
            _coordinatorActor =
                CustomActorSystem.Instance.ActorSystemEx()
                    .ActorOf(Props.Create<MainWindowUICordinatorActor>(_activeConsoleActor), "MainWindowUICordinatorActor");

            string command = "";
            _activeConsoleActor.Tell(new Message() { Msg = string.Format("Thread ID: {0}", Thread.CurrentThread.ManagedThreadId) });
            while (command != "exit") {
                _activeConsoleActor.Tell(new Message() { Msg = string.Format("Thread ID: {0} Type 'new' and enter ", Thread.CurrentThread.ManagedThreadId) });
                command = Console.ReadLine();
                if (command == "new") {
                    _coordinatorActor.Tell(new NewWindow());
                } else {
                    _coordinatorActor.Tell(new ActiveChildMessage());
                }
            }
            Console.WriteLine("Press enter once again to exit");
            Console.ReadKey();
            CustomActorSystem.Instance.ActorSystemEx().Shutdown();
        }

    }
}
