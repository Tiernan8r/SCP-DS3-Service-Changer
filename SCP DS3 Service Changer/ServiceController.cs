using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Threading;

namespace SCP_DS3_Service_Changer {

	class ServiceToggler {

		public const string SERVICE_NAME = "Ds3Service";

		public static void Prompt(string[] args) {

			ServiceController service = new ServiceController(SERVICE_NAME);
			ServiceControllerStatus status = service.Status;


			ServiceOption option = ServiceOption.START;
			
			string input = "";
			if(args.Length != 0) {
				input = args[0];
			}
			
			bool optionSelected = false;
			while(!optionSelected) {

				switch(input) {
					case "r":
						option = ServiceOption.RESTART;
						optionSelected = true;
						break;
					case "s":
						option = ServiceOption.STOP;
						optionSelected = true;
						break;
					case "t":
						option = ServiceOption.START;
						optionSelected = true;
						break;
					case "q":
						Console.WriteLine("Quitting...");
						optionSelected = true;
						return;
					default:
						Console.WriteLine("The " + service.DisplayName + " is currently: " + service.Status);
						Console.Write("Do you want to Stop(s), Start(t), Restart(r) the Service? OR Quit(q): ");
						input = Console.ReadLine().ToLower();
						// Console.WriteLine("The input: " + input + " is not a valid option.");
						break;
				}
			}

			Console.WriteLine("You selected: " + option);

			if(option == ServiceOption.RESTART) {
				if(status == ServiceControllerStatus.Running) {
					Change(ServiceOption.STOP);
					service.WaitForStatus(ServiceControllerStatus.Stopped);
					Change(ServiceOption.START);
					Console.WriteLine("The Service has been restarted.");
				}
				else {
					Console.WriteLine("The Service is not running so cannot be restarted.");
				}
			}

			if(option == ServiceOption.STOP) {
				if(status == ServiceControllerStatus.Running) {
					Change(option);
					Console.WriteLine("The service was stopped successfully");
				}
				else {
					Console.WriteLine("The Service was already stopped.");
				}
			}

			if(option == ServiceOption.START) {
				if(status == ServiceControllerStatus.Stopped) {
					Change(option);
					Console.WriteLine("The service was started successfully.");
				}
				else {
					Console.WriteLine("The service was already running.");
				}
			}

			Thread.Sleep(3000);

		}

		private static void Change(ServiceOption option) {

			ServiceController service = new ServiceController(SERVICE_NAME);

			ModifyService(option);
			service.WaitForStatus(option.Corresponding());
		}

		private static void ModifyService(ServiceOption option) {

			string commandArgument = "start";

			switch(option) {

				case ServiceOption.START:
					break;
				case ServiceOption.STOP:
					commandArgument = "stop";
					break;
				default:
					return;
			}

			var process = new Process {
				StartInfo = {FileName = "net", Arguments = commandArgument + " " + SERVICE_NAME, Verb = "runas"}
			};
			//run as administrator
			process.Start();
			process.WaitForExit();
		}

	}


}