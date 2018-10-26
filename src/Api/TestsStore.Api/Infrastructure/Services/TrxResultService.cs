using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TestsStore.Api.Infrastructure.Services
{
	public class TrxResultService
	{
		private readonly IHostingEnvironment hostingEnvironment;

		public TrxResultService(IHostingEnvironment hostingEnvironment)
		{
			this.hostingEnvironment = hostingEnvironment;
		}

		public async Task HandleAsync(Stream stream)
		{
			
		}
	}



}