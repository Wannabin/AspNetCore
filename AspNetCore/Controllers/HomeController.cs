using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    // Model (메모리, 파일 , DB등 정보 추출) 재료
    // Controller (데이터 가공, 필터링, 유효성 체크, 서비스 호출) 재료 손질
    // + 각종 서비스 -> 요리
    // View (최종 결과물을 어떻게 보여줄지) 최종 서빙

    // ex) SPA(Json) Web(HTML) 결과물이 다르면 View Layer만 바꾸고, Controller 재사용 가능

    // Action은 요청에 대한 실제 처리 함수(Handler)
    //Controller 는 Action을 포함하고 있는 그룹

    //Controller 상속이 무조건 필요한 것은 아님
    //View() 처럼 이미 정의된 Helper 기능 사용하고 싶은면 필요
    //UI(View)와 관련된 기능들을 뺀 ControllerBase -> WebApi

    //MVC에서 Controller 각종 데이터 가공을 담당, UI랑 무관
    // 넘길 때 IActionResult를 넘긴다!
    // 자주 사용되는 IActionResult종류
    // 1) ViewResult: HTML View 생성
    // 2) RedirectResult : 요청을 다른 곳으로 토스(다른 페이지로 연결해줄 때)
    // 3) FileResult : 파일을 반환
    // 4) ContentResult : 특정 string을 반환
    // 5) StatusCodeResult: HTTP statuse code 반환
    // 6) NotFoundResult : 404 HTTP status code 반환 (404 못찾음!)

    // new ViewResult() == View()
    // new RedirectResult() == Redirect()
    // new NotFoundResult() == NotFound()

    //MVC에서 V가 빠지고 [MC]만 사용하면 결국 WebApi  
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //데이터 넘기는 테스트
            ViewData["Message"] = "Data From Privarcy";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
