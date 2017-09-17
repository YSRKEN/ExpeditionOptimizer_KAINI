using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows;
using ExpOptII.Views;

namespace ExpOptII {
	class Bootstrapper : UnityBootstrapper {
		protected override DependencyObject CreateShell() {
			return Container.Resolve<MainView>();
		}

		protected override void InitializeShell() {
			Application.Current.MainWindow.Show();
		}
	}
}
