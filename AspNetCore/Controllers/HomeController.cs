﻿using AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Controllers
{
    #region [MVC] MVC Intro
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
    #endregion
    #region [M] Model
    // M (Model)
    // 데이터 모델
    // 데이터 종류가 다양하다
    // -Binding Model
    // 클라에서 보낸 Request를 파싱하기 위한 데이터 모델 << 유효성 검증 필수
    // - Application Model
    // 서버의 각종 서비스들이 사용하는 데이터 모델(ex. RankingService 라면 RankingData)
    // - View Model
    // ResPonse UI를 만들기 위한 데이터 모델
    // - API Model
    // WebAPI Controller에서 JSON/ XML으로 포맷으로 응답할 떄 필요한 데이터 모델

    // 일반적인 MVC 순서
    // 1)HTTP Request가 옴
    // 2) Routing에 의해 Controller /Action 정해짐
    // 3) Model Binding으로 Request에 있는 데이터를 파싱(Validation)
    // 4) 담당 서비스로 전달 (Application Model)
    // 5) 담당 서비스가 결과물을 Action 돌려 주면
    // 6) Action에서 ViewModel을 이용해서 View로 전달
    // 7) View에서 HTML 생성
    // 8) Response로 HTML 결과물을 전송

    //Model Binding
    // 1) Form Values
    //  Request의 Body에서 보낸 값(HTTP POST방식의 요청)
    // 2) Routes Values
    // URL 매칭, Default Value
    // 3) Query String Values
    // URL 끝에 붙이는 방법, ?Name=Rookiss(HTTP GET 방식의 요청) - 약간 옛날 방식

    // 매우 감동적 -> 일일히 추출하지 않고 마치 ORM 사용하듯이 편리하게! 알아서!
    // 다른 프레임워크에선 수동르ㅗ 하나하나 꺼내야 하는 경우 있음!

    // Route + QueryString 혼합해서 사용도 가능하긴 하지만,
    // 하나만 골라서 사용하는게 일반적 -> 혼합해야한다면 ID만 Route 방식으로

    // Complex Types
    // 넘겨받을 인자가 너무 많아지면, 부담스러우니
    // 그냥 별도의 모델링 클래스

    // Collections
    // 더 나아가서 List나 Dictionary로도 매핑을 할 수가 있음

    // Binding Source 지정
    // 기본적으로 Binding Model은 Form, Route, QueryString
    // 위의 삼총사 중 하나를 명시적으로 지정해서 파싱하거나, 다른 애로 지정할 수도 있음
    // ex) 대표적으로 Body에서 JSON 형태로 데이터를 보내주고 싶을 때
    //참고) Part9 WebAPI 실습에서도 사용한 적 있음

    // [FromHeader] HeaderValue에서 찾아라
    // [FromQuery] QueryString에서 찾아라
    // [FromRoute] Route Parameter에서 찾아라
    // [FromForm] POST Body에서 찾아라
    // [FromBody] 그냥 Body에서 찾아라 (디폴트 JSON -> 다른 형태로도 세팅 가능)

    // Validation
    // 그런데 클라-서버 구조는 늘 그렇지만, 신용할 수 없음 (해킹, 조작)
    // ex) 전화번호를 abcde 문자열을 보낸다거나..
    // 셀카 사진을 보냈더니, 10GB 파일이 오거나
    // 구매 수량을 음수로 보냈다거나..

    // Q) 근데 클라에서 Javascript 등으로 체크를 해서 걸러내면 되지 않을까?
    // A) 정상적인 유저라면 OK 게임서버나 웹에서 클라는 잠재적인 범죄자!

    // DataAnnotation
    // Blazor등 UI를 만들 때 사용했음
    // 이렇게 공용으로 사용되는 모델 -> 기본 검증 모델을 하나만 만들고 여러군데서 사용가능
    // 하지만 세부적인 검사는 하기 힘들다!

    // [Required] 무조건 있어야한다!
    // [CreditCard] 올바른 결제카드 번호인지
    // [EmailAddress] 올바른 이메일 주소인지
    // [StringLenght(max)] String 길이가 최대 max인지
    // [MinLength(min)] Collection의 크기가 최소 min인지
    // [Phone] 올바른 전화번호인지
    // [Range(min, max)] Min-Max 사이의 값인지
    // [Url] 올바른 Url인지
    // [Compare] 2개의 Property 비고 (Password, confirmpassword)

    // [!]Validation은 Attribute만 적용하면 알아서 자동으로 적용되지만,
    // 하지만 결과에 대해서 어떻게 처리할지는 Action에서 정해야 함.
    // ControllerBase에서 ModelState에 결과를 저장한다.
    #endregion
    #region[V] View
    //V (View)
    // Razor View Page (.cshtml)
    // 기본적으로 HTML과 유사한 느낌
    // HTML은 동적 처리가 애매하다.
    // - 동적이라 함은 if else 과 분기문 처리라거나
    // - 특정 리스트 개수에 따라서 <ul><li>이 유동적일때
    // 따라서 C#을 이용해서 생명을 불어넣겠다!
    
    // HTML : 고정 부분 담당 (실제로 클라에 응답을 줄 HTML)
    // C# : 동적으로 변화하는 부분을 담당
    // Razor Template 을 만들어 주고,
    // 이를 Razor Template Engine이 Template를 분석해서, 최종 결과물을 동적을 생성

    // 일반적으로 View에 데이터를 건내주는 역활은 Action에서 한다.
    // 데이터를 전달하는 방법은 다수 존재
    // 1) ViewMode( Best)
    // - 클래스로 만들어서 넘겨주는 방식
    // 2) ViewData
    // - Dictionary<string, object>  key/ value 넘기는 방식
   
    // ViewModel 그냥 클래스일뿐. 뭔가 특별한것은 아님
    // 간단한 데이터를 넘긴다면 ViewData로 넘겨도 괜찮다
    // 실제로 Error 페이지를 살펴보자.

    // Layout, PartialView, _ViewStart
    // 보통 웹사이트에서는 공통적으로 등장하는 UI가 많다 (ex. Header, Footer)
    // 심지어 동일한 CSS, Javascript 사용할 가능성도 높음
    // 공통적인 부분만 따로 빼서 관리하는 Layout

    // Layout도 그냥 Razor Template과 크게 다르진 않다.
    // 다만 무조건 @RenderBody()를 포함해야 한다
    // ChildView가 어디에 위치하는지를 지정하는 것
    // 실제 ChildView의 HTML이 복붙 된다고 보면 됨

    // 그런데 1개의 위치가 아니라, 이리저리 Child를 뿌려주고 싶다면?
    // RenderSection을 이용해 다른 이름으로 넣어준다.

    // _ViewStart, _ViewImports라는 정체는? -> 그냥 공통적인 부분을 넣어주는 곳
    // 모든 View마다 어떤 Layout을 적용할지 일일히 코드를 넣어주긴 귀찮다.
    // 모든 View마다 공통적으로 들어가는 부분 (ex. @using ~~) 일일히 넣어주기 귀찮다...
    // _ViewStart, _ViewImports를 만들어 주면 -> 해당 폴더 안에 있는 모든 View에 일괄 적용

    // 참고) PartialView라고 반복적으로 등장하는 View
    // 재사용할 수 있게 만들 수 있는데, 이름 앞에 _를 붙여야 함.
    // [!]_이 붙으면, _ViewStart가 적용되지 않는다.

    #endregion
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Test()
        {
            TestViewModel testViewModel = new TestViewModel()
            {
                Names = new List<string>()
                {
                    "Faker", "Deft", "Dopa"
                }
            };

            return View(testViewModel);
            //return View("Privacy");
            //return View("Views/Shared/Error.cshtm");
        }

        //public IActionResult Test2(TestModel testModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("Error");
        //    }
        //    return null;
        //}

        //// 1) names[0] = Faker&names[1]=Deft
        //// 2) [0] = Faker&[1]=Deft
        //// 3) names=Faker&names=Deft
        //public IActionResult Test3(List<string> names)
        //{
        //    return null;
        //}

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
