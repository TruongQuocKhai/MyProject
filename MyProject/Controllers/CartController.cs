using CommonModels.EF;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonModels.ADO;
using System.Web.Script.Serialization;
using System.Configuration;
using MyProject.CommonS;
using Common;
using PayPal.Api;

namespace MyProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            //var cart = Session[CommonConstSession.CartSession];
            //var list = new List<CartModel>();
            //if (cart != null)
            //{
            //    list = (List<CartModel>)cart;
            //}

            List<CartModel> listCarts = (List<CartModel>)Session[CommonConstSession.CartSession];
            return View(listCarts);
        }

        public ActionResult AddCart(long productId, int quantity)
        {
            var product = new ProductADO().ProductDetail(productId);
            var cart = Session[CommonConstSession.CartSession];
            if (cart != null)
            {
                var list = (List<CartModel>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //create new cart item
                    var item = new CartModel();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Assign to session
                Session[CommonConstSession.CartSession] = list;
            }
            else
            {
                // Create new cart item
                var item = new CartModel();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartModel>();
                list.Add(item);
                //Assign to session
                Session[CommonConstSession.CartSession] = list;
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [ChildActionOnly]
        public PartialViewResult Partial_HeaderCart()
        {
            var cart = Session[CommonConstSession.CartSession];
            var list = new List<CartModel>();
            if (cart != null)
            {
                list = (List<CartModel>)cart;
            }
            return PartialView(list);
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartModel>>(cartModel);
            var sessionCart = (List<CartModel>)Session[CommonConstSession.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstSession.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstSession.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartModel>)Session[CommonConstSession.CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CommonConstSession.CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Order()
        {
            if (Session["User"] != null)
            {
                var id = Session["User"].ToString();
                ViewBag.GetInforUser = new UserADO().GetIdUser(id).ToList();
            }
            List<CartModel> listCarts = (List<CartModel>)Session[CommonConstSession.CartSession];
            return View(listCarts);
            //var cart = Session[CommonConstSession.CartSession];
            //var list = new List<CartModel>();
            //if (cart != null)
            //{
            //    list = (List<CartModel>)cart;
            //}
            //return View(list);
        }
        [HttpPost]
        public ActionResult Order(string name, string mobile, string address, string email)
        {
            var order = new CommonModels.EF.Order();
            order.ShipName = name;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            // get cart information
            try
            {
                var id = new OrderADO().Insert(order);
                var cart = (List<CartModel>)Session[CommonConstSession.CartSession];
                var detailADO = new OrderDetailADO();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Product.Quantity;
                    detailADO.Insert(orderDetail);
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Contents/template/neworder.html"));
                content = content.Replace("{{CustomerName}}", name);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));

                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                new EmailHelper().SendMail(email, "Đơn hàng mới!", content);
                new EmailHelper().SendMail(toEmail, "Đơn hàng mới!", content);

            }
            catch (Exception ex)
            {
                return Redirect("/loi-dat-hang");
            }
            return Redirect("/thanh-cong");
        }

        public ActionResult Success()
        {
            Session[CommonConstSession.CartSession] = null;
            return View();
        }

        // Payment with paypal
        private PayPal.Api.Payment payment;

        //Create a payment by using APIContext
        private Payment CreatePayment(APIContext apicontext, string redirecUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };  // Khoi tao ds vs cac ptu trong Itemlist

            List<CartModel> listCarts = (List<CartModel>)Session[CommonConstSession.CartSession];
            foreach (var cart in listCarts)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.Product.Name,
                    currency = "VND",
                    price = cart.Product.Price.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku",

                });
            }

            var payer = new Payer() { payment_method = "paypal" }; //Khoi bien payer voi ptu papal

            // the configuration RedirecURLs here with redirecURL object
            var redirecUrls = new RedirectUrls()
            {
                cancel_url = redirecUrl,
                return_url = redirecUrl
            };

            // Create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "2",
                subtotal = listCarts.Sum(x => x.Quantity * x.Product.Price).ToString()
            };

            // Create amount object
            var amount = new Amount()
            {
                currency = "VND",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),   //tax + shipping + subtotal
                details = details
            };

            // Create transaction 
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Truong Khai testing description",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirecUrls
            };
            return payment.Create(apicontext);
        }

        // Create Execute payment method
        private Payment ExecutePayment(APIContext apicontext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apicontext, paymentExecution);
        }

        // Create PaymentWithPayPal method
        public ActionResult PaymentWithPayPal()
        {
            //Getting  context from the paypal bases on clientId and clientSecret for payment
            APIContext apiContext = PayPalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    // Creating a payment
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                        "/Cart/PaymentWithPayPal?";
                    var guid = Convert.ToString((new Random()).Next(100000));
                    var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get link returned from paypal response to create call function
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirecUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirecUrl = lnk.href;
                        }
                    }
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirecUrl);
                }
                else
                {
                    // This one will be executed when we have received all the payment params from previous call
                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {

                PayPalLogger.Log("Error:" + ex.Message);
                return View("Failure");
            }
            return View("Success");
        }

    }
}


