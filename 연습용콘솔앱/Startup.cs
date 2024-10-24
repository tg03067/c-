using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using 연습용콘솔앱.Models;

namespace 연습용콘솔앱
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) 
        { 
            // 의존성 주입 서비스(저장소) 설정
            services.AddSingleton<INameRepository, NameRepositoryIM>();

            // web api 컨트롤러 사용에 필요한 모든 서비스를 설정
            services.AddControllers()
                .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles); ;

            //service.AddControllersWithViews(); // MVC에 대한 모든 서비스를 설정
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 라우팅이 동작하도록 설정
            app.UseRouting();

            //호출 맵핑 지정
            app.UseEndpoints(endpoints =>
            {
                //endpoint.MapControllerRoute(
                //  name: "default",
                //  pattern: "{controller=Home}/{action=Index}/{id?}"); // URL을 세그먼트로 분석하여,
                // 호출할 엔드 포인트 
                endpoints.MapControllers(); // 웹 API 컨트롤러를 엔드포인트로 설정
            });
            
        }
    }
}
