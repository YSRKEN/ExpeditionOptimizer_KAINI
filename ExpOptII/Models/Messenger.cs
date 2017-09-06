using Prism.Events;

namespace ExpOptII.Models
{
	class Messenger : EventAggregator {
		private static Messenger _instance;

		public static Messenger Instance {
			get => _instance ?? (_instance = new Messenger());
		}
	}
}
