using System.ServiceProcess;

namespace SCP_DS3_Service_Changer {

	public enum ServiceOption {

		START,
		STOP,
		RESTART

	}

	static class ServiceOptionMethods {

		public static ServiceControllerStatus Corresponding(this ServiceOption option) {
			switch(option) {
				case ServiceOption.START:
					return ServiceControllerStatus.Running;
				case ServiceOption.STOP:
					return ServiceControllerStatus.Stopped;
				case ServiceOption.RESTART:
					return ServiceControllerStatus.Running;
				default:
					return ServiceControllerStatus.Stopped;
			}
		}

		public static ServiceOption Inverse(this ServiceOption option) {
			switch(option) {
				case ServiceOption.STOP:
					return ServiceOption.START;
				case ServiceOption.START:
					return ServiceOption.STOP;
				case ServiceOption.RESTART:
					return ServiceOption.RESTART;
				default:
					return ServiceOption.STOP;
			}
		}
		
	}
	
}