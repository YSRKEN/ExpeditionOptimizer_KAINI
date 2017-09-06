using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

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
		private int[] hasSupplyItem = new int[4] {0,0,0,0};
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
		private int[] optionItem = new int[6] {0,0,0,0,0,0};
		public int FleetCountType    { get => optionItem[0]; set { SetProperty(ref optionItem[0], value); } }
		public int GreatSuccessType  { get => optionItem[1]; set { SetProperty(ref optionItem[1], value); } }
		public int CheckIntervalType { get => optionItem[2]; set { SetProperty(ref optionItem[2], value); } }
		public int SupplyBonusType   { get => optionItem[3]; set { SetProperty(ref optionItem[3], value); } }
		public int OpenedMapType     { get => optionItem[4]; set { SetProperty(ref optionItem[4], value); } }
		public int SleepingTimeType  { get => optionItem[5]; set { SetProperty(ref optionItem[5], value); } }
		#endregion
		// 最適化処理
		public string OptimizeExp() {
			return "";
		}
	}
}
