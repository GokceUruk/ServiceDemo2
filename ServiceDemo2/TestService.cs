using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ServiceDemo2
{
	public partial class TestService : ServiceBase
	{
		System.Timers.Timer timeDelay;
		int count;
		public TestService()
		{
			timeDelay = new System.Timers.Timer();
			timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);

			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			LogService("Service is Started");
			timeDelay.Enabled = true;
		}

		protected override void OnStop()
		{
			LogService("Service Stoped");
			timeDelay.Enabled = false;
		}
		public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
		{
			string process = "Timer Tick " + count;
			LogService(process);
			count++;
		}
		private void LogService(string content)
		{
			FileStream fs = new FileStream(@"C:\Users\gkeur\source\repos\ServiceDemo2\ServiceDemo2\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			sw.BaseStream.Seek(0, SeekOrigin.End);
			sw.WriteLine(content);
			sw.Flush();
			sw.Close();
		}
	}
}
