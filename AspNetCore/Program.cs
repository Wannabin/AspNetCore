using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 3) IHost를 만든다.
            // 4) 구동(Run) < 이때부터 Listen을 시작
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            //1) 각종 옵션 설정을 세팅
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //2) Startup 클래스 지정
                    webBuilder.UseStartup<Startup>();
                });
    }
}
