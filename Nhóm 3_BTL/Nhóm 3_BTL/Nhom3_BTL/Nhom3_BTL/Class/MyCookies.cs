using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ChatGPT
{
    public class MyCookies
    {
        public int count{get {return (HttpContext.Current.Request.Cookies["DBID"] != null) ? 1 : 0;}}
        public string DBID()
        {
            string value = "";
            if (HttpContext.Current.Request.Cookies["DBID"] != null)
            {
                value = SecuredData.Decrypted(HttpContext.Current.Request.Cookies["DBID"].Value);
            }
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
        public string DBEmail ()
        {
            string value = "";
            if (HttpContext.Current.Request.Cookies["DBEmail"] != null)
            {
                value = SecuredData.Decrypted(HttpContext.Current.Request.Cookies["DBEmail"].Value);
            }
            return value.ToUpper();
        }

        public string DBName()
        {
            string value = "";
            if (HttpContext.Current.Request.Cookies["DBName"] != null)
            {
                value =  SecuredData.Decrypted(HttpContext.Current.Request.Cookies["DBName"].Value);
                try
                {
                    string FirstName = value.Split(' ')[0];
                    value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FirstName.ToLower());
                }
                catch
                {
                    value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                }
               
            }
            return value;
        }
        public void createCookies(string DBID,string DBName,string DBEmail)
        { 
            DateTime DateToday = DateTime.Now;
            HttpContext.Current.Response.Cookies["DBID"].Value = SecuredData.Encrypted(DBID);
            HttpContext.Current.Response.Cookies["DBName"].Value = SecuredData.Encrypted(DBName);
            HttpContext.Current.Response.Cookies["DBEmail"].Value = SecuredData.Encrypted(DBEmail);
            
            //Set Expiration Date
            HttpContext.Current.Response.Cookies["DBID"].Expires = DateToday.AddDays(30d);
            HttpContext.Current.Response.Cookies["DBName"].Expires = DateToday.AddDays(30d);
            HttpContext.Current.Response.Cookies["DBEmail"].Expires = DateToday.AddDays(30d);
        }
        public  void removeCookies()
        {
            DateTime DateToday = DateTime.Now;
            HttpContext.Current.Response.Cookies["DBID"].Expires = DateToday.AddDays(-30d);
            HttpContext.Current.Response.Cookies["DBName"].Expires = DateToday.AddDays(-30d);
            HttpContext.Current.Response.Cookies["DBEmail"].Expires = DateToday.AddDays(-30d);
        }
    }
}