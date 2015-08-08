using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBank.Models;

namespace MyBank.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login page.";
            UserModels userm = new UserModels();
            userm.AuthStat = "Please login.";
            return View(userm);
        }

        [HttpPost]
        public ActionResult Login(UserModels userm)
        {
            IBusinessAuthentication iau = GenericFactory<BusinessLayer, IBusinessAuthentication>.CreateInstance();
            try
            {
                string chkAcctNum = iau.IsValidUser(Utils.StripPunctuation(userm.UserName),
                    Utils.StripPunctuation(userm.Password));
                //  string chkAcctNum = iau.IsValidUser(txtUsername.Text,
                //      txtPassword.Text); 

                if (chkAcctNum != "")
                {
                    userm.AuthStat = "Welcome User";
                    SessionFacade.USERNAME = userm.UserName;
                    SessionFacade.CHECKINGACCTNUM = chkAcctNum;
                    if (SessionFacade.PAGEREQUESTED != null)
                        Response.Redirect(SessionFacade.PAGEREQUESTED);
                }
                else
                {
                    userm.AuthStat = "Invalid User";
                    //return View(userm);
                }
            }
            catch (Exception ex)
            {
                userm.AuthStat = ex.Message;
            }

            //ViewBag.Message = "Login page.";
            //return RedirectToAction("Login", "Home");
            return View(userm);
        }

        public ActionResult XferChkToSav()
        {
            ViewBag.Message = "Your Transfer page.";
            TransferModels tm = new TransferModels();
            if (SessionFacade.USERNAME == null)
            {
                SessionFacade.PAGEREQUESTED = Request.ServerVariables["SCRIPT_NAME"];
                return RedirectToAction("Login", "Home");
            }

            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
            string savAcctNum = chkAcctNum + "1";
            tm.CheckingBalance = iba.GetCheckingBalance(chkAcctNum).ToString();
            tm.SavingBalance = iba.GetSavingBalance(savAcctNum).ToString();

            return View(tm);
        }

        [HttpPost]
        public ActionResult XferChkToSav(TransferModels tm)
        {
            ViewBag.Message = "Your Transfer page.";
            IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
            try
            {
                string chkAcctNum = SessionFacade.CHECKINGACCTNUM;
                string savAcctNum = chkAcctNum + "1";
                if (iba.TransferFromChkgToSav(chkAcctNum, savAcctNum,
                    double.Parse(tm.Amount.ToString())))
                {
                    //lblStatus.Text = "Transfer successful..";
                    tm.CheckingBalance = iba.GetCheckingBalance(chkAcctNum).ToString();
                    tm.SavingBalance = iba.GetSavingBalance(savAcctNum).ToString();
                    tm.Amount = 0;
                }
            }
            catch (Exception ex)
            {
                //lblStatus.Text = ex.Message;
            }
            return View(tm);
        }

        public ActionResult XferHistory()
        {
            ViewBag.Message = "Transfer History page.";
            TransferModels tm = new TransferModels();
            if (SessionFacade.USERNAME == null)
            {
                SessionFacade.PAGEREQUESTED = Request.ServerVariables["SCRIPT_NAME"];
                return RedirectToAction("Login", "Home");
            }
            try
            {
                IBusinessAccount iba = GenericFactory<BusinessLayer, IBusinessAccount>.CreateInstance();
                List<TransferHistory> TList = iba.GetTransferHistory(SessionFacade.CHECKINGACCTNUM);
                tm.TransferHisList = TList;
            }
            catch (Exception ex)
            {
                //lblStatus.Text = ex.Message;
            }
            return View(tm);
        }

        public ActionResult News()
        {
            ViewBag.Message = "Your contact page.";
            NewsModels nm = new NewsModels();
            nm.NewsInfo = "XYZ Bank announces special rates on a CD for 12 month deposit.";
            return View(nm);
        }

    }
}
