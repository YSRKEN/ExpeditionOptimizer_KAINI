using System;
using System.Reactive.Linq;
using Prism.Events;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ExpOptII.Models;
using System.Linq;

namespace ExpOptII.ViewModels
{
	class MainViewModel {
		// modelのinstance
		private Model model;
		#region 上部入力欄
		// 必要量
		[IntValidation] public ReactiveProperty<string> NeedFuel { get; }
		[IntValidation] public ReactiveProperty<string> NeedAmmo { get; }
		[IntValidation] public ReactiveProperty<string> NeedSteel { get; }
		[IntValidation] public ReactiveProperty<string> NeedBaux { get; }
		[IntValidation] public ReactiveProperty<string> NeedBucket { get; }
		[IntValidation] public ReactiveProperty<string> NeedBurner { get; }
		[IntValidation] public ReactiveProperty<string> NeedGear { get; }
		[IntValidation] public ReactiveProperty<string> NeedCoin { get; }
		// 自然回復
		public ReactiveProperty<int> HasSupplyFuel { get; }
		public ReactiveProperty<int> HasSupplyAmmo { get; }
		public ReactiveProperty<int> HasSupplySteel { get; }
		public ReactiveProperty<int> HasSupplyBaux { get; }
		// 消費量/日
		[IntValidation] public ReactiveProperty<string> DailyConsumeFuel { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeAmmo { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeSteel { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeBaux { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeBucket { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeBurner { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeGear { get; }
		[IntValidation] public ReactiveProperty<string> DailyConsumeCoin { get; }
		// 生産量
		public ReactiveProperty<int> ProductFuel { get; set; }
		public ReactiveProperty<int> ProductAmmo { get; set; }
		public ReactiveProperty<int> ProductSteel { get; set; }
		public ReactiveProperty<int> ProductBaux { get; set; }
		public ReactiveProperty<int> ProductBucket { get; set; }
		public ReactiveProperty<int> ProductBurner { get; set; }
		public ReactiveProperty<int> ProductGear { get; set; }
		public ReactiveProperty<int> ProductCoin { get; set; }
		// 生産量/日
		public ReactiveProperty<double> DailyProductFuel { get; set; }
		public ReactiveProperty<double> DailyProductAmmo { get; set; }
		public ReactiveProperty<double> DailyProductSteel { get; set; }
		public ReactiveProperty<double> DailyProductBaux { get; set; }
		public ReactiveProperty<double> DailyProductBucket { get; set; }
		public ReactiveProperty<double> DailyProductBurner { get; set; }
		public ReactiveProperty<double> DailyProductGear { get; set; }
		public ReactiveProperty<double> DailyProductCoin { get; set; }
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
			NeedFuel   = model.ToReactivePropertyAsSynchronized(w => w.NeedFuel  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedFuel  );
			NeedAmmo   = model.ToReactivePropertyAsSynchronized(w => w.NeedAmmo  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedAmmo  );
			NeedSteel  = model.ToReactivePropertyAsSynchronized(w => w.NeedSteel , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedSteel );
			NeedBaux   = model.ToReactivePropertyAsSynchronized(w => w.NeedBaux  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedBaux  );
			NeedBucket = model.ToReactivePropertyAsSynchronized(w => w.NeedBucket, w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedBucket);
			NeedBurner = model.ToReactivePropertyAsSynchronized(w => w.NeedBurner, w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedBurner);
			NeedGear   = model.ToReactivePropertyAsSynchronized(w => w.NeedGear  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedGear  );
			NeedCoin   = model.ToReactivePropertyAsSynchronized(w => w.NeedCoin  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => NeedCoin  );
			#endregion
			#region 自然回復
			HasSupplyFuel = model.ObserveProperty(x => x.HasSupplyFuel).ToReactiveProperty();
			HasSupplyAmmo = model.ObserveProperty(x => x.HasSupplyAmmo).ToReactiveProperty();
			HasSupplySteel = model.ObserveProperty(x => x.HasSupplySteel).ToReactiveProperty();
			HasSupplyBaux = model.ObserveProperty(x => x.HasSupplyBaux).ToReactiveProperty();
			#endregion
			#region 消費量/日
			DailyConsumeFuel   = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeFuel  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeFuel  );
			DailyConsumeAmmo   = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeAmmo  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeAmmo  );
			DailyConsumeSteel  = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeSteel , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeSteel );
			DailyConsumeBaux   = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeBaux  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeBaux  );
			DailyConsumeBucket = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeBucket, w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeBucket);
			DailyConsumeBurner = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeBurner, w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeBurner);
			DailyConsumeGear   = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeGear  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeGear  );
			DailyConsumeCoin   = model.ToReactivePropertyAsSynchronized(w => w.DailyConsumeCoin  , w => w.ToString(), int.Parse, ignoreValidationErrorValue: true).SetValidateAttribute(() => DailyConsumeCoin  );
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
			FleetCountType = model.ToReactivePropertyAsSynchronized(x => x.FleetCountType);
			GreatSuccessType = model.ToReactivePropertyAsSynchronized(x => x.GreatSuccessType);
			CheckIntervalType = model.ToReactivePropertyAsSynchronized(x => x.CheckIntervalType);
			SupplyBonusType = model.ToReactivePropertyAsSynchronized(x => x.SupplyBonusType);
			OpenedMapType = model.ToReactivePropertyAsSynchronized(x => x.OpenedMapType);
			SleepingTimeType = model.ToReactivePropertyAsSynchronized(x => x.SleepingTimeType);
			#endregion
			// ボタン操作を設定
			OptimizeExpCommand = new[]{
				NeedFuel   .ObserveHasErrors,
				NeedAmmo   .ObserveHasErrors,
				NeedSteel  .ObserveHasErrors,
				NeedBaux   .ObserveHasErrors,
				NeedBucket .ObserveHasErrors,
				NeedBurner .ObserveHasErrors,
				NeedGear   .ObserveHasErrors,
				NeedCoin   .ObserveHasErrors,
				DailyConsumeFuel   .ObserveHasErrors,
				DailyConsumeAmmo   .ObserveHasErrors,
				DailyConsumeSteel  .ObserveHasErrors,
				DailyConsumeBaux   .ObserveHasErrors,
				DailyConsumeBucket .ObserveHasErrors,
				DailyConsumeBurner .ObserveHasErrors,
				DailyConsumeGear   .ObserveHasErrors,
				DailyConsumeCoin   .ObserveHasErrors,
			}.CombineLatestValuesAreAllFalse().ToReactiveCommand();
			OptimizeExpCommand.Subscribe(_ => Messenger.Instance.GetEvent<PubSubEvent<string>>().Publish(model.OptimizeExp()));
		}
	}
}
