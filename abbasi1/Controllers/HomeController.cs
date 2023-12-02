using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kumail.Models;
namespace abbasi1.Controllers
{
    public class HomeController : Controller
    {
        CoreModel m = new CoreModel();
       
        public string Test()
        {
            string query = "select name from books where Absent = 2;";
            string response = m.Exec_Outval(query);
            return response;
        }
        public string InsertTest()
        {
            string userBookName = HttpContext.Request.Form["userBookName"];
            string BookId = HttpContext.Request.Form["BookId"];
            string userBookPrice = HttpContext.Request.Form["userBookPrice"];
            string query = "";
            if (string.IsNullOrEmpty(BookId))
            {
                query = $"INSERT INTO books VALUES (default, '{userBookName}', '{userBookPrice}', GETDATE())";
            }
            else
            {
                query = $"UPDATE books SET BookName = '{userBookName}', BookPrice = '{userBookPrice}', UpdatedTime = GETDATE() WHERE Id = '{BookId}'";
            }

            string response = m.Exec(query);
            return query;
        }

        public string Deletebooks()
        {
            string bookId = HttpContext.Request.Form["bookId"];
            string query = $"Delete from books where Id = '{bookId}'";
            string response = m.Exec(query);
            return query;
        }
        [HttpPost]
        public string UpdateBook()
        {
            string bookId = HttpContext.Request.Form["bookId"];
            string query = $"Select * from books where Id = '{bookId}'";
            string response = m.Data2Json(query);
            return response;
        }

        /*  public string Editbooks()
          {
              string bookId = HttpContext.Request.Form["bookId"];
              string query = $"Delete from books where Id = '{bookId}'";
              string response = m.Exec(query);
              return query;
          }*/
        //booklist ka function h
        public string bookslist()
        {
            string query = $"select * from books";
            string responce = m.HTMLTableWithID_TR_Tag(query, "myTable");
            return responce;
        }

        [HttpGet]
        public string GetLastBookName()
        {
            string query = "select TOP 1 BookName from Books Order By UpdatedTime desc;";
            string response = m.Exec_Outval(query);
            return response;
        }
        [HttpGet]
        public string GetLastBookPrice()
        {
            {
                string query = "select TOP 1 BookPrice from Books Order By UpdatedTime desc;";
                string response = m.Exec_Outval(query);
                return response;
            }
        }








 
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
      
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult video()
        {
            return View();
        }
        public ActionResult Hotel()
        {
            return View();
        }
        public ActionResult Car()
        {
            return View();
        }
        public ActionResult Flight()
        {
            return View();
        }

    }
}