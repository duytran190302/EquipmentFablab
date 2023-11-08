using Fablab.Data;
using Fablab.Helpers.Mapper;
using Fablab.Repository.Implementation;
using Fablab.Repository.Interface;
using GraphQL;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Fablab
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers()
			.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
		    .AddNewtonsoftJson(); 

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();

			builder.Services.AddScoped<IEquipmentRepository, EquipmentRepository>();
			builder.Services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
			builder.Services.AddScoped<IBorrowRepository,BorrowRepository>();
			builder.Services.AddScoped<ILocationRepository, LocationRepository>();
			builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
			builder.Services.AddAutoMapper(typeof(Program).Assembly);
			builder.Services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("Fablab"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
				//options.EnableSensitiveDataLogging();
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}