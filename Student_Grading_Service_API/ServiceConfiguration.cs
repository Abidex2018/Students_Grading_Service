using Student_Grading_Service_API.DAO;
using Student_Grading_Service_API.DAO.Implementation;
using Student_Grading_Service_API.Services;
using Student_Grading_Service_API.Services.ServiceImplementation;

namespace Student_Grading_Service_API
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var key = configuration.GetConnectionString("SQLiteConnection");
            services.AddSingleton<SQLiteContext>();
            services.AddMemoryCache();

            services.AddScoped<IStudentGradeManagementDAO, StudentGradeManagementDAO>();
            services.AddScoped<IStudentGradeService, StudentGradeService>();

            services.AddHttpClient();
            //services.AddSingleton<JwtAuthenticationManager>(new JwtAuthenticationManager(key));
            services.AddScoped(typeof(HttpClientFactoryService<>));
        }
    }
}
