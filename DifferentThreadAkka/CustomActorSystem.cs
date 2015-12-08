using Akka.Actor;
using Akka.Configuration;

namespace DifferentThreadAkka {
    internal sealed class CustomActorSystem {

        private static CustomActorSystem _instance;
        private ActorSystem _actorSystem;
        public static CustomActorSystem Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = new CustomActorSystem();
                    return _instance;
                }
                return _instance;
            }
        }

        private CustomActorSystem() {
        }

        public ActorSystem ActorSystemEx() {
            if (_actorSystem == null) {
                _actorSystem = Akka.Actor.ActorSystem.Create("MainActorSystem",Configuration());
            }
            return _actorSystem;
        }

        private Config Configuration() {
            Config config = ConfigurationFactory.ParseString(@"
 custom-dedicated-dispatcher { type = PinnedDispatcher }
          akka {
            actor{
              deployment{
                  /ConsoleWriter{
                  dispatcher = custom-dedicated-dispatcher 
                }
              }
            }
          }
            ");
            return config;
        }
    }
}
