using System;
using System.ServiceProcess;
using System.Threading;

namespace SCP_DS3_Status {

	internal class Program {


		public static void Main(string[] args) {

			ServiceController controller = new ServiceController("Ds3Service");

			ServiceControllerStatus status = controller.Status;

			Console.WriteLine("The SCP Service is: " + status);
			Thread.Sleep(3000);

		}

	}

}