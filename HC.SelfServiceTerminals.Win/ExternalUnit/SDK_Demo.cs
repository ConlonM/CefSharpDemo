using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HC.SelfServiceTerminals.Win.ExternalUnit
{
    public class SDK_Demo
    {

        public void ShowMsg(string msg)
        {
            MessageBox.Show("测试："+msg);
        }  

        public string GetStr()
        {
            return "返回数据";
        }
    }
}
