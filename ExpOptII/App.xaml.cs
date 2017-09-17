using ExpOptII.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ExpOptII {
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application {
		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);
			// アプリの起動
			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
			// データベースの読み込み
			Database.Initialize();
		}
	}
}
