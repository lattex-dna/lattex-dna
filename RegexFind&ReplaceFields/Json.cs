using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace JsonPractices
{
    public partial class Json : Form
    {
        public Json()
        {
            InitializeComponent();
            lblNotification.Text = string.Empty;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            acc.Email = txtEmail.Text;
            acc.Password = txtPassword.Text;
            acc.PublicString = string.Format("{0}/{1}", txtEmail.Text, txtPassword.Text);

            txtResult.Text = JsonConvert.SerializeObject(acc);

            //txtResult.Text = txtResult.Text.Insert(txtResult.Text.Length, "\r\n" + acc.PublicString);

            lblNotification.Text = "Json created!";
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> stringDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(txtResult.Text);
            Dictionary<string, string> str = new Dictionary<string, string>(stringDict) { {"Template_Key", "Template_Value" } };

            string key = Regex.Match(txtResult.Text, "(?<=,\").*?(?=\")", RegexOptions.Singleline).Value; //$ string $

            if (str.ContainsKey(key))
            {
                txtResult.Text = string.Format("{0}: {1},\r\n", key, str[key]);
            }
            else txtResult.Text = new Exception(string.Format("Key {0} was not found!", key)).ToString();

            //foreach (KeyValuePair<string, string> item in str)
            //{
            //    string key = item.Key;
            //    string value = item.Value;

            //    txtResult.Text = txtResult.Text.Insert(txtResult.Text.Length, string.Format("\r\n{0}: {1},", key, value));

            //}
            

            //Account acc = JsonConvert.DeserializeObject<Account>(txtResult.Text);
            //txtResult.Text = acc.Email;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtResult.Text))
                Clipboard.SetText(txtResult.Text);

            string jsonString = "{ \"key\":\"value\", \"key2\":\"value2\" }";
            JObject jo = JObject.Parse(jsonString);
            txtResult.Text = jo["key2"].ToString();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtResult.Text = Clipboard.GetText();
        }
    }

    class Account
    {
        private string email, password, publicString;
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }
        public string PublicString { get { return publicString; } set { publicString = value; } }
    }
}
