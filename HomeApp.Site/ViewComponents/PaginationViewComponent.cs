using System;
using HomeApp.Site.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.ViewComponents
{
    [ViewComponent]
    public class PaginationViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int dataCount, int pageSize, int numbersCount, 
            int currentPage, string controller, 
            string action, 
            dynamic sort, RealEstateBagFilterViewModel filter)
        {
            int totalPage = dataCount / pageSize + (int)Math.Ceiling(dataCount % pageSize * 0.1);
            int offest = totalPage - numbersCount;

            if (offest < 0)
            {
                ViewBag.Start = 0;
            }
            else
            {
                offest = 0;
                if (currentPage - 5 > 0)
                {
                    offest = currentPage - 5;
                    if (offest + numbersCount > totalPage - 1)
                    {
                        offest = offest - ((offest + numbersCount) - (totalPage - 1));
                    }

                }
                ViewBag.Start = offest;
            }

            ViewBag.Offset = offest;
            ViewBag.NumbersCount = numbersCount;
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = currentPage;
            ViewBag.PageSize = pageSize;

            if (currentPage == 1)
                ViewBag.PrevPage = 1;
            else ViewBag.PrevPage = currentPage - 1;

            if (currentPage == totalPage - 1)
                ViewBag.NextPage = totalPage - 1;
            else ViewBag.NextPage = currentPage + 1;

            ViewBag.Controller = controller;
            ViewBag.Action = action;
            ViewBag.Sort = sort;
            ViewBag.RealEstateBagFilter = filter;

            return View();
        }
    }
}