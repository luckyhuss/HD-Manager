using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Telerik.WinControls.UI;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace HDManager.Stub
{
    /// <summary>
    /// Utility class for the project
    /// </summary>
    /// <author>Anwar Buchoo</author>
    /// <date>22/03/2013</date>
    public sealed class UtilitySelenium
    {
        // Lazy initialisation of Singleton ( .NET 4.0+)
        private static readonly Lazy<UtilitySelenium> Instance = new Lazy<UtilitySelenium>(() => new UtilitySelenium());
        
        /// <summary>
        /// Gets the ChromeDriver object
        /// </summary>
        public IWebDriver ChromeDriver { get; private set; }

        // Spam URLS - not really in use after dec 2013
        private readonly string[] _shaanigSpamUrl = new[]
            {
                "putlocker", "billionuploads", "180upload", "BillionUploads"
            };

        // RegEx URL
        private const string RegExURL = @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*";
        
        /// <summary>
        /// Gets the single instance of the class Utility
        /// </summary>
        public static UtilitySelenium GetInstance
        {
            get { return Instance.Value; }
        }

        /// <summary>
        /// Forum to thank author
        /// </summary>
        public enum Forum
        {
            X264,
            MHDDL,
            SHAANIG
        }

        /// <summary>
        /// Opens the Chrome browser ( Selenium 2)
        /// </summary>
        public void OpenChromeDriver()
        {
            // already opened, return
            if (GetInstance.ChromeDriver != null) return;

            var chromeOptions = new ChromeOptions();
            var userData = String.Format("--user-data-dir={0}", ConfigurationManager.AppSettings["ChromeUserData"]);
            
            chromeOptions.AddArgument("--start-maximized"); // maximise the window when opened
            chromeOptions.AddArgument(userData); // for history and logins

            GetInstance.ChromeDriver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriver"], chromeOptions);
        }

        /// <summary>
        /// Closes the Chrome browser and chromedriver.exe
        /// </summary>
        public void CloseChromeDriver(bool autoClose)
        {
            if (GetInstance.ChromeDriver == null || !autoClose) return;

            try
            {
                //GetInstance.ChromeDriver.Close(); // close the Chrome browser
                GetInstance.ChromeDriver.Quit(); // close the command window (chromedriver.exe)
            }
            catch (Exception)
            {
            }
            finally
            {
                // nullify the object for later check
                GetInstance.ChromeDriver = null;
            }            
        }

        /// <summary>
        /// Navigate the Chrome browser to the specified url
        /// </summary>
        /// <param name="url">url to navigate to</param>
        public void NavigateChromeDriver(string url)
        {
            GetInstance.ChromeDriver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Clicks the Thanks button on the forum
        /// </summary>
        /// <returns>True if success</returns>
        public bool ThankTheAuthor(Forum forum)
        {
            if (GetInstance.ChromeDriver == null) return false;

            // read the post Id
            IWebElement replyLinkElement;
            string replyLinkXPath = String.Empty;

            switch (forum)
            {
                case Forum.X264:
                    replyLinkXPath = "//*[@id='content_s3']/table[2]/tbody/tr/td[1]/a";
                    break;
                case Forum.MHDDL:
                    replyLinkXPath = "//*[@id='newreplylink_top']";
                    break;
                default:
                    break;
            }

            try
            {
                replyLinkElement = GetInstance.ChromeDriver.FindElement(
                    By.XPath(replyLinkXPath));
            }
            catch (NoSuchElementException)
            {
                return false;
            }

            var replyLink = replyLinkElement.GetAttribute("href").Trim();
            var postId = String.Empty;

            // check if thread is closed (can not reply to this thread)
            if (forum == Forum.MHDDL && 
                replyLinkElement.Text.Equals("closed thread", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (!String.IsNullOrEmpty(replyLink))
            {
                // [21/03/2013] sample http://www.x264-bb.com/newreply.php?do=newreply&noquote=1&p=2034974
                // [03/02/2014] sample http://www.m-hddl.com/newreply.php?p=52654&noquote=1
                var replyUri = new Uri(replyLink);
                postId = HttpUtility.
                    ParseQueryString(replyUri.Query, Encoding.Default).Get("p");
            }

            // post ID is not valid, return
            if (String.IsNullOrEmpty(postId)) return true;

            try
            {
                // look for Thanks button to Thank the uploader :-)
                var thanksButtonElement =
                    GetInstance.ChromeDriver.FindElement(By.XPath(
                        String.Format(
                            "//*[@id='thanks_postbtn.{0}']/a",
                            postId)));

                // click Thanks
                thanksButtonElement.Click();
            }
            catch (NoSuchElementException) { }

            // redirect to post page ( not null)
            GetInstance.NavigateChromeDriver(replyLink);

            return true;
        }

        /// <summary>
        /// Post a thanks message on the forum
        /// </summary>
        public bool PostThanksMessage(Forum forum)
        {
            if (GetInstance.ChromeDriver == null) return false;

            switch (forum)
            {
                case Forum.X264:
                case Forum.SHAANIG:
                    IWebElement messagebox = GetInstance.ChromeDriver.FindElement(By.Name("message"));
                    messagebox.Clear();
                    messagebox.SendKeys("Thank you.");
                    break;
                case Forum.MHDDL:
                    // 03/02/2014
                    // get the iFrame of the Richtext editor
                    IWebElement iFrameMessagebox = GetInstance.ChromeDriver.FindElement(By.XPath("//*[@id='cke_contents_vB_Editor_001_editor']/iframe"));
                    // switch to the iFrame for editing
                    GetInstance.ChromeDriver.SwitchTo().Frame(iFrameMessagebox);
                    // active editable element for iFrame
                    IWebElement editableBodyContent = GetInstance.ChromeDriver.SwitchTo().ActiveElement();
                    // clear content of the /html/body inside the iFrame
                    editableBodyContent.Clear();
                    // send text to the /html/body inside the iFrame
                    editableBodyContent.SendKeys("Thank you.");
                    // reset
                    GetInstance.ChromeDriver.SwitchTo().DefaultContent();
                    break;
                default:
                    break;
            }

            // get the "post comment" button
            var postButton = GetInstance.ChromeDriver.FindElement(By.Name("sbutton"));

            // post message
            postButton.Click();

            return true;
        }

        /// <summary>
        /// Post a thanks message on the www.shaanig.com forum
        /// </summary>
        public bool ShaanigPostThanksMessage()
        {
            return PostThanksMessage(Forum.SHAANIG);
        }

        /// <summary>
        /// Reads all the dowload links for a given post from the www.x264-bb.com forum
        /// </summary>
        /// <param name="radTextBoxLink">Download links</param>
        /// <param name="radTextBoxParentLink">Parent post link</param>
        /// <param name="radTextBoxDescription">Movie title</param>
        /// <param name="radTextBoxPostLink">Movie post link</param>
        /// <param name="radTextBoxImdbRating">IMDB rating</param>
        /// <param name="radDropDownListPassword">Password</param>
        /// <param name="radCheckBoxAutoClose">Autoclose Browser</param>
        public void X264ReadAllLinks(RadTextBox radTextBoxLink, RadTextBox radTextBoxParentLink,
            RadTextBox radTextBoxDescription, RadTextBox radTextBoxPostLink, RadTextBox radTextBoxImdbRating,
            RadDropDownList radDropDownListPassword, RadCheckBox radCheckBoxAutoClose)
        {
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count == 0)
            {
                // this is the case when the link does not open the page with the movie but actually a home with the Download Now button
                IWebElement postLinkElement;

                try
                {
                    // download now link
                    postLinkElement = GetInstance.ChromeDriver.FindElement(
                        By.XPath("//*[@id='content_s3']/div[8]/div[2]/div[1]/table/tbody/tr[2]/td[2]/table/tbody/tr[6]/td/a"));
                }
                catch (NoSuchElementException)
                {
                    // failed to get the download button, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                var postLink = postLinkElement.GetAttribute("href").Trim();

                // update textbox with new post home ( for later redirect)
                radTextBoxParentLink.Text = postLink;
                GetInstance.NavigateChromeDriver(postLink);
            }

            var countPreAfterComment = 4;
            // after navigation ( if any), recheck the content
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count < countPreAfterComment)
            {
                // not yet replied to post

                // Thanks the author first
                if (!GetInstance.ThankTheAuthor(Forum.X264))
                {
                    // failed to get the thanks button, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                // post a Thank you message to activate all download links on page
                if (!GetInstance.PostThanksMessage(Forum.X264))
                {
                    // failed to get the post thanks message, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                // reload the first page of the post
                GetInstance.NavigateChromeDriver(radTextBoxParentLink.Text);
            }

            // after navigation ( if any), recheck the content
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count >= countPreAfterComment)
            {
                var imdbLink = String.Empty;
                var allDownloadLinks = new List<XmlMovieElement.LinkDetails>();
                radTextBoxLink.Clear();
                var linkMirrors = 0;

                // read all download links available on page
                foreach (var downloadLinksElement in GetInstance.ChromeDriver.FindElements(By.TagName("pre")))
                {
                    if (downloadLinksElement.Text.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
                    {
                        // evaluate the url to make sure we get a valid url for later Navigation
                        foreach (var match in Regex.Matches(
                            downloadLinksElement.Text,
                            RegExURL,
                            RegexOptions.IgnoreCase).Cast<object>().Where(
                                match => match.ToString().Contains(ConfigurationManager.AppSettings["ImdbDomain"])))
                        {
                            imdbLink = match.ToString();
                        }
                    }
                    else if (downloadLinksElement.Text.Contains("Pass:"))
                    {
                        radDropDownListPassword.Text = downloadLinksElement.Text.Replace("Pass: ", String.Empty);
                    }
                    else
                    {
                        try
                        {
                            var firstDownloadLink = new Uri(downloadLinksElement.Text.Substring(0, 28));
                            var domain = firstDownloadLink.Host;

                            var downloadLinks = downloadLinksElement.Text.Trim();
                            downloadLinks = Regex.Replace(downloadLinks, "(\r\n){1,}", "\n");
                            downloadLinks = Regex.Replace(downloadLinks, "(\n){1,}", XmlMovieElement.LINK_FILENAME_SEPARATOR.ToString());

                            XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                            linkDetails.DownloadServer = domain;
                            linkDetails.DownloadLink = downloadLinks;
                            linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                            allDownloadLinks.Add(linkDetails);
                            linkMirrors++;
                        }
                        catch (Exception ex)
                        {
                            // not a url, maybe the password
                            if ((ex is UriFormatException || ex is ArgumentOutOfRangeException) && 
                                String.IsNullOrEmpty(radDropDownListPassword.Text))
                            {
                                // password not yet set
                                radDropDownListPassword.Text = downloadLinksElement.Text;
                            }
                        }
                    }
                }

                radTextBoxLink.Text = String.Format("All links are tagged to this control ( {0} mirrors).", linkMirrors);
                radTextBoxLink.Tag = allDownloadLinks;

                radTextBoxDescription.Text = GetInstance.ChromeDriver.Title.Replace("[MULTI]", String.Empty).Trim();
                radTextBoxPostLink.Text = GetInstance.ChromeDriver.Url;
                
                // get movie details from OmdbAPI
                if (!String.IsNullOrEmpty(imdbLink))
                {                    
                    Regex regImdb = new Regex(@"www.imdb.com/title/(?<ImdbID>\w+)");

                    if (regImdb.IsMatch(imdbLink))
                    {
                        var imdbID = regImdb.Match(imdbLink).Groups["ImdbID"].Value;

                        XmlMovieElement xmeImdb = new OmdbAPI().GetMovieDetailsByImdbId(imdbID);
                        // force redirect - 11/02/2014 not in use anymore
                        //XmlMovieElement xmeImdb = GetInstance.GetImdbByMovieTitleOrUrl(null, imdbLink);

                        if (xmeImdb != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = xmeImdb.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = xmeImdb;

                            radTextBoxDescription.Text = xmeImdb.Title;
                        }
                    }
                }
                else
                {
                    FormImdbSearch formImdbSearch = new FormImdbSearch();
                    formImdbSearch.radTextBoxTitle.Text = radTextBoxDescription.Text;

                    if (formImdbSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // user launched the search function
                        // get the IMDB link, IMDB rating and IMDB content rating
                        if (formImdbSearch.ImdbDetails != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = formImdbSearch.ImdbDetails.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = formImdbSearch.ImdbDetails;

                            radTextBoxDescription.Text = formImdbSearch.ImdbDetails.Title;
                        }
                    }
                }
            }

            // close all the instance of Chrome Driver
            GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
        } // X264

        /// <summary>
        /// Reads all the dowload links for a given post from the www.shaanig.com forum
        /// </summary>
        /// <param name="radTextBoxLink">Download links</param>
        /// <param name="radTextBoxParentLink">Parent post link</param>
        /// <param name="radTextBoxDescription">Movie title</param>
        /// <param name="radTextBoxPostLink">Movie post link</param>
        /// <param name="radTextBoxImdbRating">IMDB rating</param>
        /// <param name="radDropDownListPassword">Password</param>
        /// <param name="radCheckBoxAutoClose">Autoclose Browser</param>
        public void ShaanigReadAllLinks(RadTextBox radTextBoxLink, RadTextBox radTextBoxParentLink,
            RadTextBox radTextBoxDescription, RadTextBox radTextBoxPostLink, RadTextBox radTextBoxImdbRating,
            RadDropDownList radDropDownListPassword, RadCheckBox radCheckBoxAutoClose)
        {
            // NOT REQUIRED ANYMORE - dec 2013
            // nonetheless, continue to Thank author to keep a trace - feb 2014
            // check the <pre> count
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count < 5)
            {
                // not yet replied to post                
                // post a Thank you message to activate all download links on page
                if (!GetInstance.ShaanigPostThanksMessage())
                {
                    // failed to get the post thanks message, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                // reload the first page of the post
                GetInstance.NavigateChromeDriver(radTextBoxParentLink.Text);
            }

            // after navigation (if any), recheck the content (pre count changed around April 2013 to 1)
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count >= 1)
            {
                var imdbLink = String.Empty;
                var allDownloadLinks = new List<XmlMovieElement.LinkDetails>();
                radTextBoxLink.Clear();
                var linkMirrors = 0;
                var previousDomain = String.Empty;
                var downloadLinksPart = String.Empty;
                                
                if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count <= 4)
                {
                    // December 2013 - changes on Shaanig
                    // NEW POSTS
                                        
                    if (GetInstance.ChromeDriver.FindElements(By.ClassName("bbcode_container")).Count > 2)
                    {
                        // get IMDB link
                        IWebElement imdbElement = GetInstance.ChromeDriver.FindElements(By.ClassName("bbcode_container"))[0];
                        if (imdbElement.FindElements(By.TagName("a")).Count == 1)
                        {
                            imdbLink = imdbElement.FindElement(By.TagName("a")).GetAttribute("href");
                        }
                        else
                        {
                            imdbLink = imdbElement.Text;
                        }

                        if (!imdbLink.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
                        {
                            imdbLink = String.Empty;
                        }

                        // all download links in one pot
                        var countPre = GetInstance.ChromeDriver.FindElements(By.ClassName("bbcode_container")).Count;

                        // not watch live link (countPre == 3)
                        IWebElement linksElement = GetInstance.ChromeDriver.FindElements(By.ClassName("bbcode_container"))[2];

                        if (countPre > 3)
                        {
                            // November 2014
                            // watch live link available (countPre == 4)
                            linksElement = GetInstance.ChromeDriver.FindElements(By.ClassName("bbcode_container"))[3];
                        }
                        

                        var downloadLinks = String.Empty;
                        var keepLinksMe = String.Empty;

                        // check if one link is present
                        if (linksElement.FindElements(By.TagName("a")).Count >= 1)
                        {
                            keepLinksMe = linksElement.FindElements(By.TagName("a"))[0].GetAttribute("href");
                        }
                        else
                        {
                            // all links are in plain text
                            downloadLinks = linksElement.Text.Trim();
                        }

                        // update posttitle + postlink
                        radTextBoxDescription.Text = GetInstance.ChromeDriver.Title.Replace("[MULTI]", String.Empty).Trim();
                        radTextBoxPostLink.Text = GetInstance.ChromeDriver.Url;

                        if (!String.IsNullOrEmpty(keepLinksMe))
                        {
                            // links are on keeplinks.me
                            GetInstance.NavigateChromeDriver(keepLinksMe);
                        
                        gotoCaptcha:
                            // VB coding
                            var captchaCode = Interaction.InputBox("Input the CAPTCHA for keeplinks.me", "CAPTCHA ?");

                       
                            if (!String.IsNullOrEmpty(captchaCode))
                            {
                                if (GetInstance.ChromeDriver.FindElements(By.Id("norobot")).Count > 0)
                                {
                                    // obsolete @ November 2014 // to delete later
                                    var captchaTextBox = GetInstance.ChromeDriver.FindElement(By.Id("norobot"));
                                    captchaTextBox.Clear();
                                    captchaTextBox.SendKeys(captchaCode);
                                }
                                else
                                {
                                    // updated ID @ November 2014
                                    var captchaTextBox = GetInstance.ChromeDriver.FindElement(By.Id("adcopy_response"));
                                    captchaTextBox.Clear();
                                    captchaTextBox.SendKeys(captchaCode);
                                }

                                var postButton = GetInstance.ChromeDriver.FindElement(By.Id("btnsubmit"));

                                // post message
                                postButton.Click();
                            }

                            // page is loading with links                            
                            // read all download links for the movie from keeplinks.me
                            var countTextarea = GetInstance.ChromeDriver.FindElements(By.TagName("textarea")).Count;
                            if (countTextarea > 1)
                            {
                                var downloadLinksElement = GetInstance.ChromeDriver.FindElements(
                                    By.TagName("textarea"))[countTextarea - 1];

                                downloadLinks = downloadLinksElement.GetAttribute("value").Trim();
                            }
                            else
                            {
                                // captcha error
                                goto gotoCaptcha;
                            }
                        }

                        #region Read download links

                        var firstDownloadLink = new Uri(downloadLinks.Substring(0, 28));
                        var domain = firstDownloadLink.Host;

                        downloadLinks = Regex.Replace(downloadLinks, "(\r\n){1,}", "\n");
                        downloadLinks = Regex.Replace(downloadLinks, "(\n){1,}", ";");

                        var notSingleLink = false;

                        // group of single links
                        foreach (var match in Regex.Matches(
                            downloadLinks,
                            RegExURL,
                            RegexOptions.IgnoreCase).Cast<object>().Select(
                                match => match))
                        {
                            // read all single links available
                            downloadLinks = match.ToString();

                            firstDownloadLink = new Uri(downloadLinks.Substring(0, 28));
                            domain = firstDownloadLink.Host;

                            // check if really single or in part
                            if (notSingleLink || downloadLinks.Contains(".part"))
                            {
                                notSingleLink = true;

                                // in parts, therefore read all of same domain
                                // same link, add to group
                                if (String.IsNullOrEmpty(previousDomain) || previousDomain.Equals(domain))
                                {
                                    downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                                }
                                else
                                {
                                    XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                    linkDetails.DownloadServer = previousDomain;
                                    linkDetails.DownloadLink = downloadLinksPart.Substring(1);
                                    linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                    // add group to menu since end of part
                                    allDownloadLinks.Add(linkDetails);

                                    // count mirrors
                                    linkMirrors++;

                                    downloadLinksPart = String.Empty; // reset list
                                    downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                                }
                            }
                            else
                            {
                                XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                linkDetails.DownloadServer = domain;
                                linkDetails.DownloadLink = downloadLinks;
                                linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                allDownloadLinks.Add(linkDetails);

                                // count mirrors
                                linkMirrors++;
                            }

                            previousDomain = domain;
                        }

                        #endregion

                    } // end if IMDB "bbcode_container" count
                }
                #region Older posts
                else if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count == 1)
                {
                    MessageBox.Show(@"/!\ Older posts");

                    // April 2013 - changes on Shaanig                    
                    // get imdb link
                    foreach (var quoteContainer in GetInstance.ChromeDriver.FindElements(By.ClassName("quote_container")))
                    {
                        if (quoteContainer.FindElements(By.TagName("a")).Count == 1)
                        {
                            imdbLink = quoteContainer.FindElement(By.TagName("a")).GetAttribute("href");
                        }

                        if (!imdbLink.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
                        {
                            imdbLink = String.Empty;
                        }
                    }

                    // all download links in one pot
                    var downloadLinks = GetInstance.ChromeDriver.FindElements(By.TagName("pre"))[0].Text.Trim();

                    var firstDownloadLink = new Uri(downloadLinks.Substring(0, 28));
                    var domain = firstDownloadLink.Host;

                    downloadLinks = Regex.Replace(downloadLinks, "(\r\n){1,}", "\n");
                    downloadLinks = Regex.Replace(downloadLinks, "(\n){1,}", ";");

                    var notSingleLink = false;

                    // group of single links
                    foreach (var match in Regex.Matches(
                        downloadLinks,
                        RegExURL,
                        RegexOptions.IgnoreCase).Cast<object>().Select(
                            match => match))
                    {
                        // read all single links available
                        downloadLinks = match.ToString();

                        firstDownloadLink = new Uri(downloadLinks.Substring(0, 28));
                        domain = firstDownloadLink.Host;

                        // check if really single or in part
                        if (notSingleLink || downloadLinks.Contains(".part"))
                        {
                            notSingleLink = true;

                            // in parts, therefore read all of same domain
                            // same link, add to group
                            if (String.IsNullOrEmpty(previousDomain) || previousDomain.Equals(domain))
                            {
                                downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                            }
                            else
                            {
                                // add group to menu since end of part
                                XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                linkDetails.DownloadServer = previousDomain;
                                linkDetails.DownloadLink = downloadLinksPart.Substring(1);
                                linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                allDownloadLinks.Add(linkDetails);

                                // count mirrors
                                linkMirrors++;

                                downloadLinksPart = String.Empty; // reset list
                                downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                            }
                        }
                        else
                        {
                            XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                            linkDetails.DownloadServer = domain;
                            linkDetails.DownloadLink = downloadLinks;
                            linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                            allDownloadLinks.Add(linkDetails);

                            // count mirrors
                            linkMirrors++;
                        }

                        previousDomain = domain;
                    }
                }
                else
                {
                    // OLDER POSTS
                    // read all download links available on page
                    foreach (var downloadLinksElement in GetInstance.ChromeDriver.FindElements(By.TagName("pre")))
                    {
                        if (downloadLinksElement.Text.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
                        {
                            // evaluate the url to make sure we get a valid url for later Navigation
                            foreach (var match in Regex.Matches(
                                downloadLinksElement.Text,
                                RegExURL,
                                RegexOptions.IgnoreCase).Cast<object>().Where(
                                    match => match.ToString().Contains(ConfigurationManager.AppSettings["ImdbDomain"])))
                            {
                                imdbLink = match.ToString();
                            }
                        }
                        else
                        {
                            try
                            {
                                var firstDownloadLink = new Uri(downloadLinksElement.Text.Substring(0, 28));
                                var domain = firstDownloadLink.Host;

                                var downloadLinks = downloadLinksElement.Text.Trim();
                                downloadLinks = Regex.Replace(downloadLinks, "(\r\n){1,}", "\n");
                                downloadLinks = Regex.Replace(downloadLinks, "(\n){1,}", ";");

                                if (String.IsNullOrEmpty(domain))
                                {
                                    var notSingleLink = false;

                                    // group of single links
                                    foreach (var match in Regex.Matches(
                                        downloadLinksElement.Text,
                                        RegExURL,
                                        RegexOptions.IgnoreCase).Cast<object>().Select(
                                            match => match))
                                    {
                                        // read all single links available
                                        downloadLinks = match.ToString();

                                        firstDownloadLink = new Uri(downloadLinks.Substring(0, 28));
                                        domain = firstDownloadLink.Host;

                                        // check if really single or in part
                                        if (notSingleLink || downloadLinks.Contains(".part"))
                                        {
                                            notSingleLink = true;

                                            // in parts, therefore read all of same domain
                                            // same link, add to group
                                            if (String.IsNullOrEmpty(previousDomain) || previousDomain.Equals(domain))
                                            {
                                                downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                                            }
                                            else
                                            {
                                                // add group to menu since end of part
                                                XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                                linkDetails.DownloadServer = previousDomain;
                                                linkDetails.DownloadLink = downloadLinksPart.Substring(1);
                                                linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                                allDownloadLinks.Add(linkDetails);

                                                // count mirrors
                                                linkMirrors++;

                                                downloadLinksPart = String.Empty; // reset list
                                                downloadLinksPart = String.Concat(downloadLinksPart, ";", downloadLinks);
                                            }
                                        }
                                        else
                                        {
                                            XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                            linkDetails.DownloadServer = domain;
                                            linkDetails.DownloadLink = downloadLinks;
                                            linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                            allDownloadLinks.Add(linkDetails);

                                            // count mirrors
                                            linkMirrors++;
                                        }

                                        previousDomain = domain;
                                    }
                                }
                                else
                                {
                                    // check for spam urls
                                    if (_shaanigSpamUrl.Count(spam => downloadLinks.Contains(spam)) == 0)
                                    {
                                        // does not contain spam urls, add
                                        XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                                        linkDetails.DownloadServer = domain;
                                        linkDetails.DownloadLink = downloadLinks;
                                        linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                                        allDownloadLinks.Add(linkDetails);
                                        linkMirrors++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // not a url, maybe the password
                                if (ex is UriFormatException || ex is ArgumentOutOfRangeException)
                                {
                                    if (!downloadLinksElement.Text.Contains("muxed") &&
                                        !downloadLinksElement.Text.Contains("NONE") &&
                                        String.IsNullOrEmpty(radDropDownListPassword.Text))
                                    {
                                        radDropDownListPassword.Text = downloadLinksElement.Text;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                radTextBoxLink.Text = String.Format("All links are tagged to this control ( {0} mirrors).", linkMirrors);
                radTextBoxLink.Tag = allDownloadLinks;

                // get movie details from OmdbAPI
                if (!String.IsNullOrEmpty(imdbLink))
                {
                    Regex regImdb = new Regex(@"www.imdb.com/title/(?<ImdbID>\w+)");

                    if (regImdb.IsMatch(imdbLink))
                    {
                        var imdbID = regImdb.Match(imdbLink).Groups["ImdbID"].Value;

                        XmlMovieElement xmeImdb = new OmdbAPI().GetMovieDetailsByImdbId(imdbID);
                        // force redirect - 11/02/2014 not in use anymore
                        //XmlMovieElement xmeImdb = GetInstance.GetImdbByMovieTitleOrUrl(null, imdbLink);

                        if (xmeImdb != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = xmeImdb.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = xmeImdb;

                            radTextBoxDescription.Text = xmeImdb.Title;
                        }
                    }
                }
                else
                {
                    FormImdbSearch formImdbSearch = new FormImdbSearch();
                    formImdbSearch.radTextBoxTitle.Text = radTextBoxDescription.Text;

                    if (formImdbSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // user launched the search function
                        // get the IMDB link, IMDB rating and IMDB content rating
                        if (formImdbSearch.ImdbDetails != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = formImdbSearch.ImdbDetails.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = formImdbSearch.ImdbDetails;

                            radTextBoxDescription.Text = formImdbSearch.ImdbDetails.Title;
                        }
                    }
                }
            }

            // close all the instance of Chrome Driver
            GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
        } //Shaanig
        
        /// <summary>
        /// Reads all the dowload links for a given post from the www.m-hddl.com forum
        /// </summary>
        /// <param name="radTextBoxLink">Download links</param>
        /// <param name="radTextBoxParentLink">Parent post link</param>
        /// <param name="radTextBoxDescription">Movie title</param>
        /// <param name="radTextBoxPostLink">Movie post link</param>
        /// <param name="radTextBoxImdbRating">IMDB rating</param>
        /// <param name="radDropDownListPassword">Password</param>
        /// <param name="radCheckBoxAutoClose">Autoclose Browser</param>
        public void MHDDLReadAllLinks(RadTextBox radTextBoxLink, RadTextBox radTextBoxParentLink,
            RadTextBox radTextBoxDescription, RadTextBox radTextBoxPostLink, RadTextBox radTextBoxImdbRating,
            RadDropDownList radDropDownListPassword, RadCheckBox radCheckBoxAutoClose)
        {
            var countPreAfterComment = 4;
            // after navigation ( if any), recheck the content
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count < countPreAfterComment)
            {
                // not yet replied to post

                // Thanks the author first
                if (!GetInstance.ThankTheAuthor(Forum.MHDDL))
                {
                    // failed to get the thanks button, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                // post a Thank you message to activate all download links on page
                if (!GetInstance.PostThanksMessage(Forum.MHDDL))
                {
                    // failed to get the post thanks message, return
                    GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
                    return;
                }

                // reload the first page of the post
                GetInstance.NavigateChromeDriver(radTextBoxParentLink.Text);
            }

            // after navigation ( if any), recheck the content
            if (GetInstance.ChromeDriver.FindElements(By.TagName("pre")).Count >= countPreAfterComment)
            {
                var imdbLink = String.Empty;
                var allDownloadLinks = new List<XmlMovieElement.LinkDetails>();
                radTextBoxLink.Clear();
                var linkMirrors = 0;

                // read all download links available on page
                foreach (var downloadLinksElement in GetInstance.ChromeDriver.FindElements(By.TagName("pre")))
                {
                    if (downloadLinksElement.FindElements(By.TagName("a")).Count > 0)
                    {
                        var imdbLinkFromForum = downloadLinksElement.FindElement(By.TagName("a")).GetAttribute("href");
                        // IMDB link possibly
                        if (imdbLinkFromForum.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
                        {
                            // evaluate the url to make sure we get a valid url for later Navigation
                            foreach (var match in Regex.Matches(
                                imdbLinkFromForum,
                                RegExURL,
                                RegexOptions.IgnoreCase).Cast<object>().Where(
                                    match => match.ToString().Contains(ConfigurationManager.AppSettings["ImdbDomain"])))
                            {
                                imdbLink = match.ToString();
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            var firstDownloadLink = new Uri(downloadLinksElement.Text.Substring(0, 28));
                            var domain = firstDownloadLink.Host;

                            var downloadLinks = downloadLinksElement.Text.Trim();
                            downloadLinks = Regex.Replace(downloadLinks, "(\r\n){1,}", "\n");
                            downloadLinks = Regex.Replace(downloadLinks, "(\n){1,}", ";");

                            XmlMovieElement.LinkDetails linkDetails = new XmlMovieElement.LinkDetails();
                            linkDetails.DownloadServer = domain;
                            linkDetails.DownloadLink = downloadLinks;
                            linkDetails.DownloadLinkStatus = XmlMovieElement.DownloadStatus.New;

                            allDownloadLinks.Add(linkDetails);
                            linkMirrors++;
                        }
                        catch (Exception ex)
                        {
                            // not a url, maybe the password (last <PRE> on the page)
                            if ((ex is UriFormatException || ex is ArgumentOutOfRangeException))
                            {
                                // password not yet set
                                radDropDownListPassword.Text = downloadLinksElement.Text;
                            }
                        }
                    }
                }

                radTextBoxLink.Text = String.Format("All links are tagged to this control ( {0} mirrors).", linkMirrors);
                radTextBoxLink.Tag = allDownloadLinks;

                radTextBoxDescription.Text = GetInstance.ChromeDriver.Title.Replace("[MULTI]", String.Empty).Trim();
                radTextBoxPostLink.Text = GetInstance.ChromeDriver.Url;

                // get movie details from OmdbAPI
                if (!String.IsNullOrEmpty(imdbLink))
                {
                    Regex regImdb = new Regex(@"www.imdb.com/title/(?<ImdbID>\w+)");

                    if (regImdb.IsMatch(imdbLink))
                    {
                        var imdbID = regImdb.Match(imdbLink).Groups["ImdbID"].Value;

                        XmlMovieElement xmeImdb = new OmdbAPI().GetMovieDetailsByImdbId(imdbID);
                        // force redirect - 11/02/2014 not in use anymore
                        //XmlMovieElement xmeImdb = GetInstance.GetImdbByMovieTitleOrUrl(null, imdbLink);

                        if (xmeImdb != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = xmeImdb.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = xmeImdb;

                            radTextBoxDescription.Text = xmeImdb.Title;
                        }
                    }
                }
                else
                {
                    FormImdbSearch formImdbSearch = new FormImdbSearch();
                    formImdbSearch.radTextBoxTitle.Text = radTextBoxDescription.Text;

                    if (formImdbSearch.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // user launched the search function
                        // get the IMDB link, IMDB rating and IMDB content rating
                        if (formImdbSearch.ImdbDetails != null)
                        {
                            // set IMDB rating for the movie
                            radTextBoxImdbRating.Text = formImdbSearch.ImdbDetails.ImdbRating;
                            // set IMDB details for the movie
                            radTextBoxImdbRating.Tag = formImdbSearch.ImdbDetails;

                            radTextBoxDescription.Text = formImdbSearch.ImdbDetails.Title;
                        }
                    }
                }
            }

            // close all the instance of Chrome Driver
            GetInstance.CloseChromeDriver(radCheckBoxAutoClose.Checked);
        } // MHDDL

        
        /// <summary>
        /// Get the IMDB link, rating, content rating by the movie title
        /// </summary>
        [Obsolete("Please use OmdbAPI class instead.", true)]
        public XmlMovieElement GetImdbByMovieTitleOrUrl(string movieTitle, string imdbUrl)
        {
            // both cannot be null
            if (String.IsNullOrEmpty(movieTitle) && String.IsNullOrEmpty(imdbUrl)) return null;

            UtilitySelenium.GetInstance.OpenChromeDriver();
            XmlMovieElement xmeImdb = null; // if not IMDB found, return null

            if (String.IsNullOrEmpty(imdbUrl))
            {
                var googleSearchUrl = String.Format(ConfigurationManager.AppSettings["GoogleFeelingLucky"],
                    HttpUtility.UrlEncode(String.Concat(movieTitle.Trim(), " imdb")));

                // navigate to the movie page entered by title
                GetInstance.NavigateChromeDriver(googleSearchUrl);
            }
            else
            {
                // navigate to the movie page entered by url
                GetInstance.NavigateChromeDriver(imdbUrl);
            }

            if (GetInstance.ChromeDriver.Url.Contains(ConfigurationManager.AppSettings["ImdbDomain"]))
            {
                // the movie's IMDB has been found
                xmeImdb = new XmlMovieElement();

                xmeImdb.ImdbLink = GetInstance.ChromeDriver.Url;
                
                // read IMDB rating for the movie
                var imdbRatingElement = GetInstance.ChromeDriver.FindElement(
                    By.XPath("//*[@id='overview-top']/div[3]/div[1]"));
                xmeImdb.ImdbRating = imdbRatingElement.Text;

                // read IMDB movie title
                var imdbMovieTitleMetaElement = GetInstance.ChromeDriver.FindElement(
                    By.XPath("/html/head/meta[6]"));
                xmeImdb.Title = imdbMovieTitleMetaElement.GetAttribute("content").Trim();

                // read IMDB content rating for the movie
                var imdbContentRatingElement = GetInstance.ChromeDriver.FindElement(
                    By.XPath("//*[@id='overview-top']/div[2]/span[1]"));

                if (imdbContentRatingElement.GetAttribute("content") != null)
                {
                    xmeImdb.ImdbContentRating = imdbContentRatingElement.GetAttribute("content").Trim();
                }
                else
                {
                    xmeImdb.ImdbContentRating = "N/A";
                }

                // read IMDB original title for the movie
                IWebElement imdbOriginalTitleElement = null;
                try
                {
                    imdbOriginalTitleElement = GetInstance.ChromeDriver.FindElement(
                        By.XPath("//*[@id='overview-top']/h1/span[3]"));
                }
                catch (NoSuchElementException)
                {
                    // no original title
                }

                if (imdbOriginalTitleElement != null)
                {
                    var originalTitle = imdbOriginalTitleElement.Text.Replace("\"", String.Empty)
                        .Replace(" (original title)", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty).Trim();
                    // check if a year like 2013
                    int titleValue;
                    if (!int.TryParse(originalTitle, out titleValue))
                    {
                        // if not an int (not a year), then update with original IMDB title
                        xmeImdb.Title = String.Format("{0} [a.k.a {1}]",
                            xmeImdb.Title,
                            originalTitle);
                    }
                }

                // read IMDB poster for the movie
                IWebElement imdbPosterElement = null;
                try
                {
                    imdbPosterElement = GetInstance.ChromeDriver.FindElement(
                        By.XPath("//*[@id='img_primary']/div/a"));
                }
                catch (NoSuchElementException)
                {
                    // no image
                }

                if (imdbPosterElement != null)
                {
                    var posterFilename = xmeImdb.Title;
                     // safen filename
                    Array.ForEach(Path.GetInvalidFileNameChars(), 
                        c => posterFilename = posterFilename.Replace(c.ToString(), String.Empty));

                    var posterUrl = imdbPosterElement.GetAttribute("href");

                    // navigate to the poster image ( larger)
                    GetInstance.NavigateChromeDriver(posterUrl);

                    // get the image url ( larger)
                    imdbPosterElement = GetInstance.ChromeDriver.FindElement(
                        By.XPath("//*[@id='primary-img']"));
                    posterUrl = imdbPosterElement.GetAttribute("src");

                    var posterSavePath = String.Format(@"{0}\{1}{2}",
                        ConfigurationManager.AppSettings["PosterBaseUrl"],
                        posterFilename,
                        Path.GetExtension(posterUrl));

                    Image posterImage = null;
                    HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(posterUrl);
                    HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    Stream stream = httpWebReponse.GetResponseStream();
                    posterImage = Image.FromStream(stream);

                    // save poster image
                    posterImage.Save(posterSavePath);
                    posterImage.Dispose();

                    xmeImdb.ImdbPoster = Path.GetFileName(posterSavePath);
                }
            }

            // close all the instance of Chrome Driver
            GetInstance.CloseChromeDriver(true);
            
            return xmeImdb;
        }

    } // class UtilitySelenium
} // namespace HDManager.Stub
