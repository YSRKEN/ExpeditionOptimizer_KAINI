﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using GlpkWrapperCS;

namespace ExpOptII.Models
{
	class Model : BindableBase {
		#region 上部入力欄
		// 必要量
		private int[] needItem = new int[8] {0,0,0,0,0,0,0,0};
		public int NeedFuel   { get => needItem[0]; set { SetProperty(ref needItem[0], value); } }
		public int NeedAmmo   { get => needItem[1]; set { SetProperty(ref needItem[1], value); } }
		public int NeedSteel  { get => needItem[2]; set { SetProperty(ref needItem[2], value); } }
		public int NeedBaux   { get => needItem[3]; set { SetProperty(ref needItem[3], value); } }
		public int NeedBucket { get => needItem[4]; set { SetProperty(ref needItem[4], value); } }
		public int NeedBurner { get => needItem[5]; set { SetProperty(ref needItem[5], value); } }
		public int NeedGear   { get => needItem[6]; set { SetProperty(ref needItem[6], value); } }
		public int NeedCoin   { get => needItem[7]; set { SetProperty(ref needItem[7], value); } }
		// 自然回復
		private int[] hasSupplyItem = new int[4] {1,1,1,1};
		public int HasSupplyFuel  { get => hasSupplyItem[0]; set { SetProperty(ref hasSupplyItem[0], value); } }
		public int HasSupplyAmmo  { get => hasSupplyItem[1]; set { SetProperty(ref hasSupplyItem[1], value); } }
		public int HasSupplySteel { get => hasSupplyItem[2]; set { SetProperty(ref hasSupplyItem[2], value); } }
		public int HasSupplyBaux  { get => hasSupplyItem[3]; set { SetProperty(ref hasSupplyItem[3], value); } }
		// 消費量/日
		private int[] dailyConsumeItem = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
		public int DailyConsumeFuel   { get => dailyConsumeItem[0]; set { SetProperty(ref dailyConsumeItem[0], value); } }
		public int DailyConsumeAmmo   { get => dailyConsumeItem[1]; set { SetProperty(ref dailyConsumeItem[1], value); } }
		public int DailyConsumeSteel  { get => dailyConsumeItem[2]; set { SetProperty(ref dailyConsumeItem[2], value); } }
		public int DailyConsumeBaux   { get => dailyConsumeItem[3]; set { SetProperty(ref dailyConsumeItem[3], value); } }
		public int DailyConsumeBucket { get => dailyConsumeItem[4]; set { SetProperty(ref dailyConsumeItem[4], value); } }
		public int DailyConsumeBurner { get => dailyConsumeItem[5]; set { SetProperty(ref dailyConsumeItem[5], value); } }
		public int DailyConsumeGear   { get => dailyConsumeItem[6]; set { SetProperty(ref dailyConsumeItem[6], value); } }
		public int DailyConsumeCoin   { get => dailyConsumeItem[7]; set { SetProperty(ref dailyConsumeItem[7], value); } }
		// 生産量
		private int[] productItem = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
		public int ProductFuel   { get => productItem[0]; set { SetProperty(ref productItem[0], value); } }
		public int ProductAmmo   { get => productItem[1]; set { SetProperty(ref productItem[1], value); } }
		public int ProductSteel  { get => productItem[2]; set { SetProperty(ref productItem[2], value); } }
		public int ProductBaux   { get => productItem[3]; set { SetProperty(ref productItem[3], value); } }
		public int ProductBucket { get => productItem[4]; set { SetProperty(ref productItem[4], value); } }
		public int ProductBurner { get => productItem[5]; set { SetProperty(ref productItem[5], value); } }
		public int ProductGear   { get => productItem[6]; set { SetProperty(ref productItem[6], value); } }
		public int ProductCoin   { get => productItem[7]; set { SetProperty(ref productItem[7], value); } }
		// 生産量/日
		private double[] dailyProductItem = new double[8] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
		public double DailyProductFuel   { get => dailyProductItem[0]; set { SetProperty(ref dailyProductItem[0], value); } }
		public double DailyProductAmmo   { get => dailyProductItem[1]; set { SetProperty(ref dailyProductItem[1], value); } }
		public double DailyProductSteel  { get => dailyProductItem[2]; set { SetProperty(ref dailyProductItem[2], value); } }
		public double DailyProductBaux   { get => dailyProductItem[3]; set { SetProperty(ref dailyProductItem[3], value); } }
		public double DailyProductBucket { get => dailyProductItem[4]; set { SetProperty(ref dailyProductItem[4], value); } }
		public double DailyProductBurner { get => dailyProductItem[5]; set { SetProperty(ref dailyProductItem[5], value); } }
		public double DailyProductGear   { get => dailyProductItem[6]; set { SetProperty(ref dailyProductItem[6], value); } }
		public double DailyProductCoin   { get => dailyProductItem[7]; set { SetProperty(ref dailyProductItem[7], value); } }
		#endregion
		#region 下部入力欄
		public int[] optionItem = new int[6] {2,0,2,0,4,4};
		public int FleetCountType    { get => optionItem[0]; set { SetProperty(ref optionItem[0], value); } }
		public int GreatSuccessType  { get => optionItem[1]; set { SetProperty(ref optionItem[1], value); } }
		public int CheckIntervalType { get => optionItem[2]; set { SetProperty(ref optionItem[2], value); } }
		public int SupplyBonusType   { get => optionItem[3]; set { SetProperty(ref optionItem[3], value); } }
		public int OpenedMapType     { get => optionItem[4]; set { SetProperty(ref optionItem[4], value); } }
		public int SleepingTimeType  { get => optionItem[5]; set { SetProperty(ref optionItem[5], value); } }
		#endregion
		// 最適化処理
		public string OptimizeExp() {
			// 遠征リストを作成する
			var nowExpList = new List<List<double>>();
			foreach (var expInfo in Database.ExpList.Values) {
				// 各遠征の「情報」を初期化する
				// [0] 遠征時間(分単位)
				// [1]～[8] 燃弾鋼ボ修炎開貨
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
					var expTimeInterval = new int[] { 1,10,60,120,180,480,1440 };
					int nowExpTimeInterval = expTimeInterval[CheckIntervalType];
					if(rawExpTime % nowExpTimeInterval == 0) {
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
			foreach(var expData in nowExpList) {
				foreach(var data in expData) {
					Console.Write($"{data}\t");
				}
				Console.WriteLine("");
			}
			// 問題データを作成し、解かせる
			// [定数]
			// s1～sM：各資源の必要量
			// Amn：n番目の遠征における資源mの収量(nowExpListから読み取る)
			// Tn：n番目の遠征の遠征時間
			// Yn：n番目の遠征の実行回数上限(可能ならINT.MAX、不可能なら0)
			// FleetCount：出撃する艦隊数
			// [変数]　(N+1)個
			// x1～xN：各遠征の実行回数。0以上の整数
			// ExpTime：最終的な遠征時間。実数
			// [目的関数]
			// ExpTime：最小化したい変数
			// [制約条件]　(M+N*2+1)個
			// Am1 * x1 + Am2 * x2 +...+ AmN * xN ≧ sm：(M本)：資源制約
			// T1 * x1 + T2 * x2 +...+ TN * xN - ExpTime * FleetCount ≦ 0：(1本)：(全遠征の合計消費時間)／艦隊数≦ExpTime
			// Tn * xn - ExpTime ≦ 0：(N本)：各遠征について、合計消費時間≦ExpTime
			// xn ≦ Yn：(N本)：各遠征について、実行回数の上限がある
			using (var problem = new MipProblem()) {
				// 最適化の方向
				problem.ObjDir = ObjectDirection.Minimize;
				// 制約式の数・名前・範囲

			}
			//No.	海域名	位置	遠征名	旗艦練度	合計練度	最小人数	必要艦種	遠征時間(分)
			//報酬燃料	報酬弾薬	報酬鋼材	報酬ボーキ
			//左側バケツ	左側バーナー	左側ギア	左側コイン	右側バケツ	右側バーナー	右側ギア	右側コイン
			//消費燃料	消費弾薬
			return "";
		}
	}
}
