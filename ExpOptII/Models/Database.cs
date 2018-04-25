using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ExpOptII.Models {
	using ExpInfo = Dictionary<string, string>;
	using DataHash = Dictionary<string, Dictionary<string, string>>;
	static class Database {
		public static DataHash ExpList { get; private set; }
		public static Dictionary<string, int> MapIndex = new Dictionary<string, int> { { "鎮守府海域", 0 }, { "南西諸島海域", 1 }, { "北方海域", 2 }, { "西方海域", 3 }, { "南方海域", 4 } };
		public static void Initialize() {
			ExpList = new DataHash();
			using (var sr = new StreamReader(@"ExpList.csv")) {
				string[] header = null;
				while (!sr.EndOfStream) {
					// 1行を読み込む
					string line = sr.ReadLine();
					// タブで区切る
					var temp = line.Split(',');
					if(temp.Length < 23)
						continue;
					// ヘッダー行かどうかを判別する
					if(temp[0] == "No.") {
						// ヘッダー行なので記憶しておく
						header = temp;
					}
					else {
						// データ行なのでデータを生成しつつ代入する
						string key = temp[0];
						var temp2 = new ExpInfo();
						for(int i = 0; i < header.Length; ++i) {
							temp2[header[i]] = temp[i];
						}
						ExpList[key] = temp2;
					}
				}
			}
			return;
		}
	}
}
