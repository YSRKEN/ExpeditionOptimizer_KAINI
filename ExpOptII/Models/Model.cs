using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using GlpkWrapperCS;

namespace ExpOptII.Models {
	class Model : BindableBase {
		#region 上部入力欄
		// 必要量
		private int[] needItem = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
		public int NeedFuel { get => needItem[0]; set { SetProperty(ref needItem[0], value); } }
		public int NeedAmmo { get => needItem[1]; set { SetProperty(ref needItem[1], value); } }
		public int NeedSteel { get => needItem[2]; set { SetProperty(ref needItem[2], value); } }
		public int NeedBaux { get => needItem[3]; set { SetProperty(ref needItem[3], value); } }
		public int NeedBucket { get => needItem[4]; set { SetProperty(ref needItem[4], value); } }
		public int NeedBurner { get => needItem[5]; set { SetProperty(ref needItem[5], value); } }
		public int NeedGear { get => needItem[6]; set { SetProperty(ref needItem[6], value); } }
		public int NeedCoin { get => needItem[7]; set { SetProperty(ref needItem[7], value); } }
		// 自然回復
		private int[] hasSupplyItem = new int[4] { 1, 1, 1, 1 };
		public int HasSupplyFuel { get => hasSupplyItem[0]; set { SetProperty(ref hasSupplyItem[0], value); } }
		public int HasSupplyAmmo { get => hasSupplyItem[1]; set { SetProperty(ref hasSupplyItem[1], value); } }
		public int HasSupplySteel { get => hasSupplyItem[2]; set { SetProperty(ref hasSupplyItem[2], value); } }
		public int HasSupplyBaux { get => hasSupplyItem[3]; set { SetProperty(ref hasSupplyItem[3], value); } }
		// 消費量/日
		private int[] dailyConsumeItem = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
		public int DailyConsumeFuel { get => dailyConsumeItem[0]; set { SetProperty(ref dailyConsumeItem[0], value); } }
		public int DailyConsumeAmmo { get => dailyConsumeItem[1]; set { SetProperty(ref dailyConsumeItem[1], value); } }
		public int DailyConsumeSteel { get => dailyConsumeItem[2]; set { SetProperty(ref dailyConsumeItem[2], value); } }
		public int DailyConsumeBaux { get => dailyConsumeItem[3]; set { SetProperty(ref dailyConsumeItem[3], value); } }
		public int DailyConsumeBucket { get => dailyConsumeItem[4]; set { SetProperty(ref dailyConsumeItem[4], value); } }
		public int DailyConsumeBurner { get => dailyConsumeItem[5]; set { SetProperty(ref dailyConsumeItem[5], value); } }
		public int DailyConsumeGear { get => dailyConsumeItem[6]; set { SetProperty(ref dailyConsumeItem[6], value); } }
		public int DailyConsumeCoin { get => dailyConsumeItem[7]; set { SetProperty(ref dailyConsumeItem[7], value); } }
		// 生産量
		private int[] productItem = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
		public int ProductFuel { get => productItem[0]; set { SetProperty(ref productItem[0], value); } }
		public int ProductAmmo { get => productItem[1]; set { SetProperty(ref productItem[1], value); } }
		public int ProductSteel { get => productItem[2]; set { SetProperty(ref productItem[2], value); } }
		public int ProductBaux { get => productItem[3]; set { SetProperty(ref productItem[3], value); } }
		public int ProductBucket { get => productItem[4]; set { SetProperty(ref productItem[4], value); } }
		public int ProductBurner { get => productItem[5]; set { SetProperty(ref productItem[5], value); } }
		public int ProductGear { get => productItem[6]; set { SetProperty(ref productItem[6], value); } }
		public int ProductCoin { get => productItem[7]; set { SetProperty(ref productItem[7], value); } }
		// 生産量/日
		private double[] dailyProductItem = new double[8] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
		public double DailyProductFuel { get => dailyProductItem[0]; set { SetProperty(ref dailyProductItem[0], value); } }
		public double DailyProductAmmo { get => dailyProductItem[1]; set { SetProperty(ref dailyProductItem[1], value); } }
		public double DailyProductSteel { get => dailyProductItem[2]; set { SetProperty(ref dailyProductItem[2], value); } }
		public double DailyProductBaux { get => dailyProductItem[3]; set { SetProperty(ref dailyProductItem[3], value); } }
		public double DailyProductBucket { get => dailyProductItem[4]; set { SetProperty(ref dailyProductItem[4], value); } }
		public double DailyProductBurner { get => dailyProductItem[5]; set { SetProperty(ref dailyProductItem[5], value); } }
		public double DailyProductGear { get => dailyProductItem[6]; set { SetProperty(ref dailyProductItem[6], value); } }
		public double DailyProductCoin { get => dailyProductItem[7]; set { SetProperty(ref dailyProductItem[7], value); } }
		#endregion
		#region 下部入力欄
		public int[] optionItem = new int[6] { 2, 0, 2, 0, 4, 4 };
		public int FleetCountType { get => optionItem[0]; set { SetProperty(ref optionItem[0], value); } }
		public int GreatSuccessType { get => optionItem[1]; set { SetProperty(ref optionItem[1], value); } }
		public int CheckIntervalType { get => optionItem[2]; set { SetProperty(ref optionItem[2], value); } }
		public int SupplyBonusType { get => optionItem[3]; set { SetProperty(ref optionItem[3], value); } }
		public int OpenedMapType { get => optionItem[4]; set { SetProperty(ref optionItem[4], value); } }
		public int SleepingTimeType { get => optionItem[5]; set { SetProperty(ref optionItem[5], value); } }
		#endregion
		// 最適化処理
		public string OptimizeExp() {
			// 遠征リストを作成する
			// nowExpList[遠征番号n][0-7が資材の種類m、8が遠征時間]
			// [0]～[7] 燃弾鋼ボ修炎開貨
			// [8] 遠征時間(分単位)
			var nowExpList = new List<List<double>>();
			foreach (var expInfo in Database.ExpList.Values) {
				// 各遠征の「情報」を初期化する
				int rawExpTime = 0;
				var rawExpData = Enumerable.Repeat(0.0, 8).ToList();
				rawExpTime = int.Parse(expInfo["遠征時間(分)"]);
				rawExpData[0] = double.Parse(expInfo["報酬燃料"]);
				rawExpData[1] = double.Parse(expInfo["報酬弾薬"]);
				rawExpData[2] = double.Parse(expInfo["報酬鋼材"]);
				rawExpData[3] = double.Parse(expInfo["報酬ボーキ"]);
				rawExpData[4] = double.Parse(expInfo["左側バケツ"]);
				rawExpData[5] = double.Parse(expInfo["左側バーナー"]);
				rawExpData[6] = double.Parse(expInfo["左側ギア"]);
				rawExpData[7] = double.Parse(expInfo["左側コイン"]);
				// 遠征の大成功率を計算する
				double greatSuccessProb = 0.0;
				{
					switch (GreatSuccessType) {
					case 0:
						// 「なし」
						greatSuccessProb = 0.0;
						break;
					case 1:
						// 「半ドラム」
						switch (expInfo["遠征名"]) {
						case "北方鼠輸送作戦":
							greatSuccessProb = 0.375;
							break;
						case "北方航路海上護衛":
							greatSuccessProb = 0.430;
							break;
						case "東京急行":
							greatSuccessProb = 0.450;
							break;
						case "東京急行（弐）":
							greatSuccessProb = 0.417;
							break;
						case "水上機前線輸送":
							greatSuccessProb = 0.427;
							break;
						default:
							greatSuccessProb = 0.0;
							break;
						}
						break;
					case 2:
						// 「全ドラム」
						switch (expInfo["遠征名"]) {
						case "北方鼠輸送作戦":
						case "北方航路海上護衛":
						case "東京急行":
						case "東京急行（弐）":
						case "水上機前線輸送":
							greatSuccessProb = 1.0;
							break;
						default:
							greatSuccessProb = 0.0;
							break;
						}
						break;
					case 3:
						// 「あり」
						greatSuccessProb = 1.0;
						break;
					}
				}
				double greatSuccessMulti = greatSuccessProb * 1.5 + (1.0 - greatSuccessProb) * 1.0;
				// 入力した設定により、遠征の各数値を変化させる
				var nowExpData = Enumerable.Repeat(0.0, 9).ToList();
				{
					// 「確認間隔」で遠征時間を弄る
					var expTimeInterval = new int[] { 1, 10, 60, 120, 180, 480, 1440 };
					int nowExpTimeInterval = expTimeInterval[CheckIntervalType];
					if (rawExpTime % nowExpTimeInterval == 0) {
						nowExpData[8] = rawExpTime;
					}
					else {
						nowExpData[8] = (rawExpTime / nowExpTimeInterval + 1) * nowExpTimeInterval;
					}
				}
				{
					// 「大成功」で大成功率を管理し、
					// 「収量増加」で通常資材の収量を上げる
					var supplyBonusPer = new double[] { 1.0, 1.05, 1.10, 1.15, 1.20 };
					double nowSupplyBonus = supplyBonusPer[SupplyBonusType];
					nowExpData[0] = Math.Floor(rawExpData[0] * nowSupplyBonus * greatSuccessMulti - double.Parse(expInfo["消費燃料"]));
					nowExpData[1] = Math.Floor(rawExpData[1] * nowSupplyBonus * greatSuccessMulti - double.Parse(expInfo["消費弾薬"]));
					nowExpData[2] = Math.Floor(rawExpData[2] * nowSupplyBonus * greatSuccessMulti);
					nowExpData[3] = Math.Floor(rawExpData[3] * nowSupplyBonus * greatSuccessMulti);
				}
				{
					nowExpData[4] = rawExpData[4] + greatSuccessProb * double.Parse(expInfo["右側バケツ"]);
					nowExpData[5] = rawExpData[5] + greatSuccessProb * double.Parse(expInfo["右側バーナー"]);
					nowExpData[6] = rawExpData[6] + greatSuccessProb * double.Parse(expInfo["右側ギア"]);
					nowExpData[7] = rawExpData[7] + greatSuccessProb * double.Parse(expInfo["右側コイン"]);
				}
				// 遠征情報を追加する
				nowExpList.Add(nowExpData);
			}
			// 問題データを作成し、解かせる
			// [定数]
			// s1～sM：各資源の必要量
			// Amn：n番目の遠征における資源mの収量(nowExpListから読み取る)
			// Bm：資源mの1日あたりの消費量
			// Tn：n番目の遠征の遠征時間[分]
			// Yn：n番目の遠征の実行回数上限(可能ならINT.MAX、不可能なら0)
			// Zm：m番目の資源の自然回復量[1／分]
			// ExpTimeRatio：遠征に使える時間／実際の消費時間。一日X時間休むなら(24-X)／24
			// FleetCount：出撃する艦隊数
			// [変数]　(N+1)個
			// x1～xN：各遠征の実行回数。0以上の整数
			// ExpTime：最終的な遠征時間[分]。実数
			// [目的関数]
			// ExpTime：最小化したい変数
			// [制約条件]　(M+N*2+1)個
			// Am1 * x1 + Am2 * x2 +...+ AmN * xN + Zm * ExpTime - Bm * ExpTime / (60 * 24) ≧ sm：(M本)：資源制約
			// T1 * x1 + T2 * x2 +...+ TN * xN - ExpTime * FleetCount * ExpTimeRatio ≦ 0：(1本)：(全遠征の合計消費時間)／艦隊数≦ExpTime・ExpTimeRatio
			// Tn * xn - ExpTime * ExpTimeRatio ≦ 0：(N本)：各遠征について、合計消費時間≦ExpTime・ExpTimeRatio
			// xn ≦ Yn：(N本)：各遠征について、実行回数の上限がある
			string result = "";
			using (var problem = new MipProblem()) {
				int M = 8;                  //資源の種類数
				int N = nowExpList.Count;   //遠征の種類数
											// 最適化の方向
				problem.ObjDir = ObjectDirection.Minimize;
				// 制約式の数・名前・範囲
				problem.AddRows(M + N * 2 + 1);
				{
					// 資源制約
					int p = 0;
					for (int m = 0; m < M; ++m) {
						problem.SetRowBounds(p, BoundsType.Lower, needItem[m], 0.0);
						++p;
					}
					// 総遠征時間制約
					problem.SetRowBounds(p, BoundsType.Upper, 0.0, 0.0);
					++p;
					// 各遠征時間制約
					for (int n = 0; n < N; ++n) {
						problem.SetRowBounds(p, BoundsType.Upper, 0.0, 0.0);
						++p;
					}
					// 実行回数制限
					foreach (var expInfo in Database.ExpList.Values) {
						// 遠征番号を取得する
						int expNo = int.Parse(expInfo["No."]);
						// 遠征海域番号を算出する
						int expAreaNo = (expNo - 1) / 8;
						// 遠征海域番号によって上限を決定する
						if (expAreaNo <= OpenedMapType) {
							problem.SetRowBounds(p, BoundsType.Upper, 0.0, 99999.0);
						}
						else {
							problem.SetRowBounds(p, BoundsType.Upper, 0.0, 0.0);
						}
						++p;
					}
				}
				// 変数の数・名前・範囲
				problem.AddColumns(N + 1);
				{
					int p = 0;
					for (int n = 0; n < N; ++n) {
						problem.SetColumnBounds(p, BoundsType.Lower, 0.0, 0.0);
						problem.ColumnKind[p] = VariableKind.Integer;
						++p;
					}
					problem.SetColumnBounds(p, BoundsType.Free, 0.0, 0.0);
					problem.ColumnKind[p] = VariableKind.Continuous;
				}
				// 目的関数の係数
				{
					int p = 0;
					for (int n = 0; n < N; ++n) {
						problem.ObjCoef[p] = 0.0;
						++p;
					}
					problem.ObjCoef[p] = 1.0;
				}
				// 制約式の係数
				{
					// 記録用のリストを用意
					var ia = new List<int>();
					var ja = new List<int>();
					var ar = new List<double>();
					// 係数を追加していく
					{
						// 資源制約(ia=0～(M-1))
						for (int m = 0; m < M; ++m) {
							for (int n = 0; n < N; ++n) {
								ia.Add(m);
								ja.Add(n);
								ar.Add(nowExpList[n][m]);
							}
							ia.Add(m);
							ja.Add(N);
							double temp = -1.0 * dailyConsumeItem[m] / 60 / 24;
							if (m < 4 && hasSupplyItem[m] == 0) {
								if (m != 3) {
									temp += (1.0);
								}
								else {
									temp += (1.0 / 3);
								}
							}
							ar.Add(temp);
						}
						// 総遠征時間制約(ia=M)
						double expTimeRatio = 1;
						{
							for (int n = 0; n < N; ++n) {
								ia.Add(M);
								ja.Add(n);
								ar.Add(nowExpList[n][8]);
							}
							ia.Add(M);
							ja.Add(N);
							int fleetCount = FleetCountType + 1;
							var sleepingTimeList = new int[] { 0, 2, 4, 6, 8, 10 };
							int sleepingTime = sleepingTimeList[SleepingTimeType];
							expTimeRatio = 1.0 * (24 - sleepingTime) / 24;
							ar.Add(-fleetCount * expTimeRatio);
						}
						// 各遠征時間制約(ia=M+1～M+N)
						{
							for (int n = 0; n < N; ++n) {
								ia.Add(M + n + 1);
								ja.Add(n);
								ar.Add(nowExpList[n][8]);
								ia.Add(M + n + 1);
								ja.Add(N);
								ar.Add(-expTimeRatio);
							}
						}
						// 実行回数制限(ia=M+N+1～M+N+N)
						{
							for (int n = 0; n < N; ++n) {
								ia.Add(M + N + n + 1);
								ja.Add(n);
								ar.Add(1.0);
							}
						}
					}
					problem.LoadMatrix(ia.ToArray(), ja.ToArray(), ar.ToArray());
				}
				//string hoge = problem.ToLpString();
				//Console.WriteLine(hoge);
				// 最適化を実行
				var mipResult = problem.BranchAndCut(false, 60);
				// 結果を読み取る
				if (mipResult == SolverResult.OK || mipResult == SolverResult.ErrorTimeLimit) {
					double allExpTime = problem.MipObjValue;
					result += $"総遠征時間：{ToTimeString(allExpTime)}\n";
					result += "遠征内容：\n";
					// 各遠征を読み取る
					var doExpList = Enumerable.Repeat(new Tuple<int, string, int>(0, "", 0), N).ToList();
					for (int n = 0; n < N; ++n) {
						int expCount = (int)(problem.MipColumnValue[n]);
						if (expCount < 0)
							expCount = 0;
						doExpList[n] = new Tuple<int, string, int>(
							expCount,
							Database.ExpList.Values.ToList()[n]["遠征名"] + "("
							+ "Lv." + Database.ExpList.Values.ToList()[n]["旗艦練度"]
							+ ",Lv." + Database.ExpList.Values.ToList()[n]["合計練度"]
							+ "," + Database.ExpList.Values.ToList()[n]["最小人数"] + "隻"
							+ "," + Database.ExpList.Values.ToList()[n]["必要艦種"] + ")",
							(int)(nowExpList[n][8] * expCount)
						);
					}
					// 稼いだ資源量を画面に反映させる
					ProductFuel = 0; ProductAmmo = 0; ProductSteel = 0; ProductBaux = 0;
					ProductBucket = 0; ProductBurner = 0; ProductGear = 0; ProductCoin = 0;
					for (int n = 0; n < N; ++n) {
						ProductFuel   += (int)(nowExpList[n][0] * doExpList[n].Item1);
						ProductAmmo   += (int)(nowExpList[n][1] * doExpList[n].Item1);
						ProductSteel  += (int)(nowExpList[n][2] * doExpList[n].Item1);
						ProductBaux   += (int)(nowExpList[n][3] * doExpList[n].Item1);
						ProductBucket += (int)(nowExpList[n][4] * doExpList[n].Item1);
						ProductBurner += (int)(nowExpList[n][5] * doExpList[n].Item1);
						ProductGear   += (int)(nowExpList[n][6] * doExpList[n].Item1);
						ProductCoin   += (int)(nowExpList[n][7] * doExpList[n].Item1);
					}
					ProductFuel  += (int)((HasSupplyFuel  == 0 ? 1.0 : 0.0) * allExpTime);
					ProductAmmo  += (int)((HasSupplyAmmo  == 0 ? 1.0 : 0.0) * allExpTime);
					ProductSteel += (int)((HasSupplySteel == 0 ? 1.0 : 0.0) * allExpTime);
					ProductBaux  += (int)((HasSupplyBaux  == 0 ? 1.0 : 0.0) * allExpTime / 3);
					DailyProductFuel   = Math.Round(ProductFuel * 60 * 24 / problem.MipObjValue, 1);
					DailyProductAmmo   = Math.Round(ProductAmmo * 60 * 24 / problem.MipObjValue, 1);
					DailyProductSteel  = Math.Round(ProductSteel * 60 * 24 / problem.MipObjValue, 1);
					DailyProductBaux   = Math.Round(ProductBaux * 60 * 24 / problem.MipObjValue, 1);
					DailyProductBucket = Math.Round(ProductBucket * 60 * 24 / problem.MipObjValue, 1);
					DailyProductBurner = Math.Round(ProductBurner * 60 * 24 / problem.MipObjValue, 1);
					DailyProductGear   = Math.Round(ProductGear * 60 * 24 / problem.MipObjValue, 1);
					DailyProductCoin   = Math.Round(ProductCoin * 60 * 24 / problem.MipObjValue, 1);
					// 結果を表示する
					foreach (var expInfo in doExpList.Where(p => p.Item1 > 0).OrderByDescending(p => p.Item3)) {
						result += $"・{expInfo.Item2}　{expInfo.Item1}回\n";
					}
				}
				else {
					result = "実行可能解が出せませんでした。";
				}
			}
			return result;
		}
		// [分]を[日時分]に変換する
		string ToTimeString(double minute) {
			string output = "";
			if (minute >= 60 * 24) {
				int day = (int)Math.Floor(minute / 60 / 24);
				output += $"{day}日";
				minute -= day * 60 * 24;
			}
			if (minute >= 60) {
				int hour = (int)Math.Floor(minute / 60);
				output += $"{hour}時間";
				minute -= hour * 60;
			}
			output += $"{(int)minute}分";
			return output;
		}
	}
}
