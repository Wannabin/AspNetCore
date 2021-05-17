using AspNetCore.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddSingleton<IBaseLogger, FileLogger>();

            //services.AddSingleton(new FileLogSettings("log.txt"));
            //services.AddSingleton<IBaseLogger, DbLogger>();
            //services.AddSingleton(sp => new FileLogSettings("log.txt"));

        }
        // [Request]                    [Response]
        // [파이프라인]                [파이프라인]
        //          [마지막 MVC EndPoint]

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 1) IIS, Apache 등에 HTTP 요청
        // 2) ASP.NET Core 서버 (Kestrel)전달
        // 3) 미들웨어 적용 : HTTP request / response를 처리하는 중간 부품
        // 4) Controller로 전달
        // 5) Controller에서 처리하고 View로 전달
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //css, javascript 이미지 등 요청 받을 때 처리
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // 라우팅(Routing) : 길잡이
            // HTTP request <-> 담당 Handler 사이를 맵핑
            // ASP.NET 초기 버전에서는 /hello.aspx와 같이 처리하는 파일 자체를 URL에 입력
            // 1) 파일 이름이 바뀌면? -> 클라 쪽 처리를 같이 하지 않으면 접속 불가능
            // 2) /hello.aspx?method=1&id=3 와 괕이 QueryString 방식의 URL
            // -> 지금 방식은 /hello/get/3

            // 기본 관례(Convertion)는 Controller/Action/id 형식
            // 다른 이름 지정하고 싶을 땐?
            // - API 서버로 사용할 때, URL 주소가 어떤 역할을 하는지 더 명확하게 힌트를 주고 싶다.
            // - 굳이 Controller를 수정하지 않고 연결된 URL만 교체하고 싶다!

            // Routing이 적용되려면 미들웨어의 파이프에 의해 전달이 되어야 함
            // - 중간에 에러가 난다거나, 특정 미들웨어가 흐름을 가로챘다면 실행 X

            // 파이프라인 끝까지 도달했으면 ,MapControllerRoute에 의해 Routing 규칙이 결정
            // - 패턴을 이용한 방식으로 Routing
            // - Attribute Routing 방식
            
            // Route Template (Pattern)
            // name: "default" -> 다수를 설정할 수 있다!

            //app.UseEndpoints(endpoints =>
            //{
            //    // api : literal value(고정 문자열 값? 꼭 필요)
            //    // {controller} {action} : route parameter (꼭필요)
            //    // {controller=Home} {action=Index} : Optional Route patameter ( 없으면 알아서 기본값 설정)
            //    // {id?} : Optional route parameter (없어도 됨)
            //    // [주의!] controller랑 action 무조건 정해져야 합니다! (매칭 or 기본값을 통해서)

            //    // Constraint 관련 (제약사항)
            //    // {controller=Home}/{action=Index}/{id?}
            //    // id가 광범위하다는 문제가 있음 /1/2/3
            //    // {cc:int} 정수만
            //    // {cc:min(18)} 18이상 정수만
            //    // {cc:length(5)} 5글자 string

            //    endpoints.MapControllerRoute(
            //        name: "test",
            //        pattern: "api/{controller}/{action}/{test?}",
            //        defaults: new {controller = "Home", action = "Privacy"}
                    
            //        );

            //    //라우팅 패턴 설정
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
