using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ChatGPT.Models
{
    public class Chat
    {
        MyCookies myCookies = new MyCookies();
        public DataTable chat_table = new DataTable();
        public DataTable history_table = new DataTable();
        public string ChatID { get; set; } = "";

       
    }
}