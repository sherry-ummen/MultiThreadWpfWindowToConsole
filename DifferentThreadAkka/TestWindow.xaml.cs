using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Akka.Actor;
using Akka.Util.Internal;
using DifferentThreadAkka.Actors;
using DifferentThreadAkka.ViewModel;

namespace DifferentThreadAkka {
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window {

        //private IActorRef _activeUIActor;
        private MainWindowViewModel _vm;
        public TestWindow(MainWindowViewModel vm) {
            InitializeComponent();
            _vm = vm;
            //var model = new MainWindowViewModel(coordinatorActorref);
            //_activeUIActor = MainWindowUIActorSingleton.Instance.GetRef(model);
            this.DataContext = vm;
        }

        private void TestWindow_OnActivated(object sender, EventArgs e) {
            _vm.SetActiveChild();
        }
    }
}
