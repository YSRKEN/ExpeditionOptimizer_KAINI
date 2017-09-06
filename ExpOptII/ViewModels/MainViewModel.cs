using System;
using System.Reactive.Linq;
using Prism.Events;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ExpOptII.Models;

namespace ExpOptII.ViewModels
{
	class MainViewModel {
		// modelのinstance
		private Model model;
		#region 上部入力欄
		// 必要量
		[IntValidation] public ReactiveProperty<int> NeedFuel { get; }
		[IntValidation] public ReactiveProperty<int> NeedAmmo { get; }
		[IntValidation] public ReactiveProperty<int> NeedSteel { get; }
		[IntValidation] public ReactiveProperty<int> NeedBaux { get; }
		[IntValidation] public ReactiveProperty<int> NeedBucket { get; }
		[IntValidation] public ReactiveProperty<int> NeedBurner { get; }
		[IntValidation] public ReactiveProperty<int> NeedGear { get; }
		[IntValidation] public ReactiveProperty<int> NeedCoin { get; }
		// 自然回復
		public ReactiveProperty<int> HasSupplyFuel { get; }
		public ReactiveProperty<int> HasSupplyAmmo { get; }
		public ReactiveProperty<int> HasSupplySteel { get; }
		public ReactiveProperty<int> HasSupplyBaux { get; }
		// 消費量/日
		[IntValidation] public ReactiveProperty<int> DailyConsumeFuel { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeAmmo { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeSteel { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeBaux { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeBucket { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeBurner { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeGear { get; }
		[IntValidation] public ReactiveProperty<int> DailyConsumeCoin { get; }
		// 生産量
		[IntValidation] public ReactiveProperty<int> ProductFuel { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductAmmo { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductSteel { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductBaux { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductBucket { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductBurner { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductGear { get; set; }
		[IntValidation] public ReactiveProperty<int> ProductCoin { get; set; }
		// 生産量/日
		[IntValidation] public ReactiveProperty<double> DailyProductFuel { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductAmmo { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductSteel { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductBaux { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductBucket { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductBurner { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductGear { get; set; }
		[IntValidation] public ReactiveProperty<double> DailyProductCoin { get; set; }
		#endregion
		#region 下部入力欄
		public ReactiveProperty<int> FleetCountType { get; }
		public ReactiveProperty<int> GreatSuccessType { get; }
		public ReactiveProperty<int> CheckIntervalType { get; }
		public ReactiveProperty<int> SupplyBonusType { get; }
		public ReactiveProperty<int> OpenedMapType { get; }
		public ReactiveProperty<int> SleepingTimeType { get; }
		#endregion
		// 最適化ボタン
		public ReactiveCommand OptimizeExpCommand { get; }
		// コンストラクタ
		public MainViewModel() {
			// 初期化
			model = new Model();
			// 紐付け
			#region 必要量
			NeedFuel = model.ObserveProperty(x => x.NeedFuel).ToReactiveProperty();
			NeedAmmo = model.ObserveProperty(x => x.NeedAmmo).ToReactiveProperty();
			NeedSteel = model.ObserveProperty(x => x.NeedSteel).ToReactiveProperty();
			NeedBaux = model.ObserveProperty(x => x.NeedBaux).ToReactiveProperty();
			NeedBucket = model.ObserveProperty(x => x.NeedBucket).ToReactiveProperty();
			NeedBurner = model.ObserveProperty(x => x.NeedBurner).ToReactiveProperty();
			NeedGear = model.ObserveProperty(x => x.NeedGear).ToReactiveProperty();
			NeedCoin = model.ObserveProperty(x => x.NeedCoin).ToReactiveProperty();
			#endregion
			#region 自然回復
			HasSupplyFuel = model.ObserveProperty(x => x.HasSupplyFuel).ToReactiveProperty();
			HasSupplyAmmo = model.ObserveProperty(x => x.HasSupplyAmmo).ToReactiveProperty();
			HasSupplySteel = model.ObserveProperty(x => x.HasSupplySteel).ToReactiveProperty();
			HasSupplyBaux = model.ObserveProperty(x => x.HasSupplyBaux).ToReactiveProperty();
			#endregion
			#region 消費量/日
			DailyConsumeFuel = model.ObserveProperty(x => x.DailyConsumeFuel).ToReactiveProperty();
			DailyConsumeAmmo = model.ObserveProperty(x => x.DailyConsumeAmmo).ToReactiveProperty();
			DailyConsumeSteel = model.ObserveProperty(x => x.DailyConsumeSteel).ToReactiveProperty();
			DailyConsumeBaux = model.ObserveProperty(x => x.DailyConsumeBaux).ToReactiveProperty();
			DailyConsumeBucket = model.ObserveProperty(x => x.DailyConsumeBucket).ToReactiveProperty();
			DailyConsumeBurner = model.ObserveProperty(x => x.DailyConsumeBurner).ToReactiveProperty();
			DailyConsumeGear = model.ObserveProperty(x => x.DailyConsumeGear).ToReactiveProperty();
			DailyConsumeCoin = model.ObserveProperty(x => x.DailyConsumeCoin).ToReactiveProperty();
			#endregion
			#region 生産量
			ProductFuel = ReactiveProperty.FromObject(model, x => x.ProductFuel);
			ProductAmmo = ReactiveProperty.FromObject(model, x => x.ProductAmmo);
			ProductSteel = ReactiveProperty.FromObject(model, x => x.ProductSteel);
			ProductBaux = ReactiveProperty.FromObject(model, x => x.ProductBaux);
			ProductBucket = ReactiveProperty.FromObject(model, x => x.ProductBucket);
			ProductBurner = ReactiveProperty.FromObject(model, x => x.ProductBurner);
			ProductGear = ReactiveProperty.FromObject(model, x => x.ProductGear);
			ProductCoin = ReactiveProperty.FromObject(model, x => x.ProductCoin);
			#endregion
			#region 生産量/日
			DailyProductFuel = ReactiveProperty.FromObject(model, x => x.DailyProductFuel);
			DailyProductAmmo = ReactiveProperty.FromObject(model, x => x.DailyProductAmmo);
			DailyProductSteel = ReactiveProperty.FromObject(model, x => x.DailyProductSteel);
			DailyProductBaux = ReactiveProperty.FromObject(model, x => x.DailyProductBaux);
			DailyProductBucket = ReactiveProperty.FromObject(model, x => x.DailyProductBucket);
			DailyProductBurner = ReactiveProperty.FromObject(model, x => x.DailyProductBurner);
			DailyProductGear = ReactiveProperty.FromObject(model, x => x.DailyProductGear);
			DailyProductCoin = ReactiveProperty.FromObject(model, x => x.DailyProductCoin);
			#endregion
			#region 下部入力欄
			FleetCountType = model.ObserveProperty(x => x.FleetCountType).ToReactiveProperty();
			GreatSuccessType = model.ObserveProperty(x => x.GreatSuccessType).ToReactiveProperty();
			CheckIntervalType = model.ObserveProperty(x => x.CheckIntervalType).ToReactiveProperty();
			SupplyBonusType = model.ObserveProperty(x => x.SupplyBonusType).ToReactiveProperty();
			OpenedMapType = model.ObserveProperty(x => x.OpenedMapType).ToReactiveProperty();
			SleepingTimeType = model.ObserveProperty(x => x.SleepingTimeType).ToReactiveProperty();
			#endregion
			// ボタン操作を設定
			OptimizeExpCommand = NeedFuel.ObserveHasErrors.ToReactiveCommand();
			OptimizeExpCommand.Subscribe(_ => Messenger.Instance.GetEvent<PubSubEvent<string>>().Publish(model.OptimizeExp()));
		}
	}
}
