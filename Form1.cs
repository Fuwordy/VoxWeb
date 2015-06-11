using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;


namespace SimpleWebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This will close the application when the File->Exit menu item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This will show a box with the author information when the about menu item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was made by Vox Studio");
        }

        /// <summary>
        /// On click of this button the web control will display the page requested in the text box (by url)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }

        /// <summary>
        /// This function will cause the browser to navigate to the URL in the textBox1 control
        /// </summary>
        private void NavigateToPage()
        {
            toolStripStatusLabel1.Text = "Search has started";
            webBrowser1.Navigate(textBox1.Text);
        }

        /// <summary>
        /// This function will start navigation by simulating a click on the navigate button when 'enter' is pressed when textbox1 is in focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // if the keystroke was enter then do something
            if( e.KeyChar == (char)ConsoleKey.Enter )
            {
                //NavigateToPage();
                button1_Click(null, null);
            }
        }

        /// <summary>
        /// When the webpage is finished loading this function will re-enable the disabled controls and indicate success
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Enable the controls that were disabled during navigation
            button1.Enabled = true;
            textBox1.Enabled = true;
            textBox1.Text = webBrowser1.Url.ToString();
            webBrowser1.Navigate("google.com");

            // Indicate loading is complete
            toolStripStatusLabel1.Text = "Search Completed";
            toolStripProgressBar1.ProgressBar.Value = 100;
        }

        /// <summary>
        /// This function will be called as the webpage loads multiple time to indicate the percentage complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            // Don't bother computing percentage if either variable is zero since it will cause a divide by zero error
            if (e.CurrentProgress > 0 && e.MaximumProgress > 0)
            {
                // Calculate percentage
                int percentage =  (int)(e.CurrentProgress * 100 / e.MaximumProgress);
                
                // If the percentage is > 100 it means additional processing was done on the page so we want to ignore it
                if( percentage <= 100)
                {
                    toolStripProgressBar1.ProgressBar.Value = percentage;
                }
            }
            else
            {
                // Set the percentage to zero if we can't compute it
                toolStripProgressBar1.ProgressBar.Value = 0;
            }
        }

        /// <summary>
        /// When the 'Swap Stuff Out' button is pressed on the menu bar this will swap out all of the images on the website with a funny cat picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void swapOutStuffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (HtmlElement image in webBrowser1.Document.Images)
            {
                // Replace all the <img src=""> values with our funny cat picture
                if (image.GetAttribute("src") != "http://evelyngarone.files.wordpress.com/2012/01/little-cat.jpg")
                {
                    image.SetAttribute("src", "http://evelyngarone.files.wordpress.com/2012/01/little-cat.jpg");
                }
            }
        }
        /// <summary>
        /// These are the Back, Foward, Refresh buttons for the Vox Web.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        //Basic button fuction
        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward(); //WTF THIS IS JUST ANNOYING NOT THIS FUNCTION BUT THIS MOTHERFUCKING PROJECT
        }
        //Basuic button fuction
        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            TabPage newPage = new TabPage("New Page");
            tabControl1.TabPages.Add(newPage);
        }
        private void OnBtnAddTab(object sender, EventArgs e)

        {

            string title = "TabPage " + (tabControl1.TabCount + 1).ToString();

            TabPage myTabPage = new TabPage(title);

            tabControl1.TabPages.Add(myTabPage);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }
        private void Navigate(String address)

        {// the webBrowser1 is a member variable of the form

            if (String.IsNullOrEmpty(address)) return;

            if (address.Equals("about:blank")) return;

            if (!address.StartsWith("http://")) address = "http://" + address;

            try

            {

                webBrowser1.Navigate(new Uri(address));

            }

            catch (System.UriFormatException)

            {

                return;

            }

        }



        private Control CreateBrowserObject()

        {

            if (this.webBrowser1 != null) // the webBrowser1 is a member variable of the form

                this.webBrowser1.Dispose();

            this.webBrowser1 = new System.Windows.Forms.WebBrowser();



            this.webBrowser1.Location = new System.Drawing.Point(0, 0);

            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);

            this.webBrowser1.Name = "webBrowser1";

            this.webBrowser1.Size = new System.Drawing.Size(378, 270);

            this.webBrowser1.TabIndex = 0;


            return (webBrowser1);



        }

        private void OnBtnAddWeb2Tab(object sender, EventArgs e)

        {// click a button to do this

            tabControl1.TabPages[0].Controls.Add(CreateBrowserObject());

            Navigate("www.google.com");

            webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
        }
        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)

        {//show the page title in the tab page

            tabControl1.TabPages[0].Text = webBrowser1.Document.Title;

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
