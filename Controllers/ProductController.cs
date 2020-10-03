using EShopWebMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EShopWebMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                var getTask = client.GetAsync("Get");
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    products = JsonConvert.DeserializeObject<List<Product>>(data);
                }
            }
            return View(products);
        }
        public ActionResult Create(Product s)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                string data = JsonConvert.SerializeObject(s);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var postTask = client.PostAsync("Post", content);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            Product s = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                var getTask = client.GetAsync("Get/" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    s = JsonConvert.DeserializeObject<Product>(data);
                }
            }
            return View("Edit", s);
        }
        [HttpPost]
        public ActionResult Edit(Product s)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                string data = JsonConvert.SerializeObject(s);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var postTask = client.PostAsync("Put/" + s.ProductID, content);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Delete(string id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                var deleteTask = client.DeleteAsync("Delete/" + id);
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            Product s = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:50400/api/Product/");
                var getTask = client.GetAsync("Get/" + id);
                getTask.Wait();
                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    s = JsonConvert.DeserializeObject<Product>(data);
                }
            }
            return View("Details", s);
        }
    }
}