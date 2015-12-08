using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using DifferentThreadAkka.ViewModel;
using DifferentThreadAkka.Actors;

namespace DifferentThreadAkka {
    public sealed class MainWindowUIActorSingleton {

        private IActorRef ActorRef { get; set; }

        private static Lazy<MainWindowUIActorSingleton> g_instance = new Lazy<MainWindowUIActorSingleton>(() => new MainWindowUIActorSingleton());
        public static MainWindowUIActorSingleton Instance { get { return g_instance.Value; } }

        private MainWindowUIActorSingleton() {

        }

        public IActorRef GetRef(MainWindowViewModel model) {
            return ActorRef ?? (ActorRef = CustomActorSystem.Instance.ActorSystemEx().ActorOf(Props.Create<MainWindowUIActor>(model), "MainWindowUI"));
        }
    }
}
