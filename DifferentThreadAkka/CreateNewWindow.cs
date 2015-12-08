using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Akka.Actor;
using DifferentThreadAkka.ViewModel;

namespace DifferentThreadAkka {
    public static class CreateNewWindow {

        private static Application _app;
        public static void Create(MainWindowViewModel vm)
        {
            var wpfThread = new Thread(() => {

                if (_app == null) {
                    _app = new Application();

                }
                if (_app.CheckAccess()) {
                    TestWindow window = new TestWindow(vm);
                    window.Show();
                    _app.Run();
                } else {
                    _app.Dispatcher.Invoke((Action)(() => {
                        TestWindow window = new TestWindow(vm);
                        window.Show();
                    }));
                }
            });
            wpfThread.SetApartmentState(ApartmentState.STA);
            wpfThread.IsBackground = true;
            wpfThread.Start();
        }
    }
}
