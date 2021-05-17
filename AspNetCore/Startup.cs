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
        // [����������]                [����������]
        //          [������ MVC EndPoint]

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // 1) IIS, Apache � HTTP ��û
        // 2) ASP.NET Core ���� (Kestrel)����
        // 3) �̵���� ���� : HTTP request / response�� ó���ϴ� �߰� ��ǰ
        // 4) Controller�� ����
        // 5) Controller���� ó���ϰ� View�� ����
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
            //css, javascript �̹��� �� ��û ���� �� ó��
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // �����(Routing) : ������
            // HTTP request <-> ��� Handler ���̸� ����
            // ASP.NET �ʱ� ���������� /hello.aspx�� ���� ó���ϴ� ���� ��ü�� URL�� �Է�
            // 1) ���� �̸��� �ٲ��? -> Ŭ�� �� ó���� ���� ���� ������ ���� �Ұ���
            // 2) /hello.aspx?method=1&id=3 �� �B�� QueryString ����� URL
            // -> ���� ����� /hello/get/3

            // �⺻ ����(Convertion)�� Controller/Action/id ����
            // �ٸ� �̸� �����ϰ� ���� ��?
            // - API ������ ����� ��, URL �ּҰ� � ������ �ϴ��� �� ��Ȯ�ϰ� ��Ʈ�� �ְ� �ʹ�.
            // - ���� Controller�� �������� �ʰ� ����� URL�� ��ü�ϰ� �ʹ�!

            // Routing�� ����Ƿ��� �̵������ �������� ���� ������ �Ǿ�� ��
            // - �߰��� ������ ���ٰų�, Ư�� �̵��� �帧�� ����ë�ٸ� ���� X

            // ���������� ������ ���������� ,MapControllerRoute�� ���� Routing ��Ģ�� ����
            // - ������ �̿��� ������� Routing
            // - Attribute Routing ���
            
            // Route Template (Pattern)
            // name: "default" -> �ټ��� ������ �� �ִ�!

            //app.UseEndpoints(endpoints =>
            //{
            //    // api : literal value(���� ���ڿ� ��? �� �ʿ�)
            //    // {controller} {action} : route parameter (���ʿ�)
            //    // {controller=Home} {action=Index} : Optional Route patameter ( ������ �˾Ƽ� �⺻�� ����)
            //    // {id?} : Optional route parameter (��� ��)
            //    // [����!] controller�� action ������ �������� �մϴ�! (��Ī or �⺻���� ���ؼ�)

            //    // Constraint ���� (�������)
            //    // {controller=Home}/{action=Index}/{id?}
            //    // id�� �������ϴٴ� ������ ���� /1/2/3
            //    // {cc:int} ������
            //    // {cc:min(18)} 18�̻� ������
            //    // {cc:length(5)} 5���� string

            //    endpoints.MapControllerRoute(
            //        name: "test",
            //        pattern: "api/{controller}/{action}/{test?}",
            //        defaults: new {controller = "Home", action = "Privacy"}
                    
            //        );

            //    //����� ���� ����
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
