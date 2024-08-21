using JO_MOVIES;
using JO_MOVIES.Models;
using Microsoft.AspNetCore.Hosting;

public class Program
{
	

	public static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
		 
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});
}
