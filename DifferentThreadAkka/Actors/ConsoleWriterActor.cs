using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using DifferentThreadAkka.Messages;

namespace DifferentThreadAkka.Actors {
    public class ConsoleWriterActor : ReceiveActor {

        public ConsoleWriterActor() {
            Receive<Message>(ic => ReceiveMessage(ic));
        }

        private void ReceiveMessage(Message message) {
            Console.WriteLine("ThreadID:{0} Receive Message : " + message.Msg,Thread.CurrentThread.ManagedThreadId);
        }
    }
}
