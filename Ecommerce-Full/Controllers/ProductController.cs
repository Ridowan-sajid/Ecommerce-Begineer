using Ecommerce_Full.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_Full.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {



            var db = new Entities();
            var st = (from s in db.Catagories
                      select s).ToList();
            return View(st);


            //return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            var db = new Entities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("ProductList");

        }

        public ActionResult ProductList()
        {
            var db = new Entities();
            var productLi = db.Products.ToList();
            return View(productLi);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new Entities();
            var st = (from s in db.Catagories
                      select s).ToList();
            ViewBag.st = st;

            var st2 = (from s in db.Products
                      where s.id == id
                      select s).SingleOrDefault();
            return View(st2);
        }

        [HttpPost]
        public ActionResult Edit(Product pr)
        {
            var db = new Entities();
            var exst = (from s in db.Products
                        where s.id == pr.id
                        select s).SingleOrDefault();
            exst.name = pr.name;
            exst.catId = pr.catId;
            //exst.type = pr.type;

            db.SaveChanges();

            return RedirectToAction("ProductList");
        }
        public ActionResult Delete(int id)
        {
            var db = new Entities();
            var exst = (from s in db.Products
                        where s.id == id
                        select s).SingleOrDefault();
            db.Products.Remove(exst);
            db.SaveChanges();

            return RedirectToAction("ProductList");
        }

        List<int> cartList = new List<int>();
        public ActionResult Cart(int id)
        {
            
            if (Session["cart"] != null)
            {
                cartList = (List<int>)Session["cart"];
                cartList.Add(id);
                Session["cart"] = cartList;
                return RedirectToAction("ProductList");
            }

            cartList.Add(id);
            Session["cart"] = cartList;
            return RedirectToAction("ProductList");
        }


        public ActionResult ShowCart()
        {
            List<object> cartList2 = new List<object>();
            var db = new Entities();
            var lst = Session["cart"] as List<int>;
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    var st = (from s in db.Products
                              where s.id == item
                              select s).SingleOrDefault();
                    cartList2.Add(st);
                }
            }

            return View(cartList2);
        }



        public ActionResult RemoveCart(int id)
        {

            if (Session["cart"] != null)
            {
                cartList = (List<int>)Session["cart"];
                cartList.Remove(id);
                Session["cart"] = cartList;
                return RedirectToAction("ShowCart");
            }

            Session["cart"] = cartList;
            return RedirectToAction("ShowCart");
        }
    }




}