using System;
using System.Web.UI;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace My.Custom.ImageField.Infrastructure
{
    public class MyImageField : Sitecore.Shell.Applications.ContentEditor.Image
    {
        public MyImageField() : base()
        {
        }

        public override void HandleMessage(Message message)
        {
            Assert.ArgumentNotNull(message, "message");
            if (string.Equals(message.Name, "contentimage:open", StringComparison.CurrentCultureIgnoreCase))
            {
                Browse();
                return;
            }

            if (string.Equals(message.Name, "contentimage:clear", StringComparison.CurrentCultureIgnoreCase))
            {
                ClearImage();
                return;
            }

            if (string.Equals(message.Name, "contentimage:refresh", StringComparison.CurrentCultureIgnoreCase))
            {
                Update();
                return;
            }

            base.HandleMessage(message);
        }

        /// <summary>Browses for an image.</summary>
        protected new void Browse()
        {
            if (this.Disabled)
                return;
            Sitecore.Context.ClientPage.Start((object)this, "BrowseImage");
        }

        /// <summary>
        /// Browse available images
        /// </summary>
        /// <param name="args"></param>
        protected new void BrowseImage(ClientPipelineArgs args)
        {
            if (args.IsPostBack)
            {
                if (string.IsNullOrEmpty(args.Result) || !(args.Result != "undefined"))
                    return;

                this.XmlValue.SetAttribute("mediaid", args.Result);
                this.Value = args.Result;

                this.Update();
                this.SetModified();
            }
            else
            {
                UrlString urlToDialog = new UrlString(Sitecore.Context.Site.TargetHostName + "/api/imagefield/search");

                SheerResponse.ShowModalDialog(new ModalDialogOptions(urlToDialog.ToString())
                {
                    Header = "Select DAM asset",
                    Width = "1000px",
                    Height = "500px",
                    Response = true
                });
                args.WaitForPostBack();
            }
        }

        /// <summary>Clears the image.</summary>
        private void ClearImage()
        {
            if (this.Disabled)
                return;
            if (this.Value.Length > 0)
                this.SetModified();
            this.XmlValue = new XmlValue(string.Empty, "image");
            this.Value = string.Empty;
            this.Update();
        }

        /// <summary>
        /// Updated the rendered field html in Sitecore aswell as the details field
        /// </summary>
        /// <param name="url"></param>
        protected new void Update()
        {
            string src = this.GetSrc();
            SheerResponse.SetAttribute(this.ID + "_image", "src", src);
            SheerResponse.SetInnerHtml(this.ID + "_details", GetDetails());
            SheerResponse.Eval("scContent.startValidators()");
        }
        
        /// <summary>Sets the value.</summary>
        /// <param name="value">The value.</param>
        public override void SetValue(string value)
        {
            Assert.ArgumentNotNull((object) value, nameof(value));
            this.XmlValue = new XmlValue(value, "image");
            this.Value = this.XmlValue.GetAttribute("mediaid");
        }
        
        /// <summary>Handles the Change event.</summary>
        /// <param name="message">The message.</param>
        protected override void DoChange(Message message)
        {
            Assert.ArgumentNotNull((object)message, nameof(message));
            base.DoChange(message);
            if (string.IsNullOrEmpty(this.Value))
            {
                this.ClearImage();
            }
            else
            {
                //string path = this.Value;
                string rawValue = this.XmlValue.GetAttribute("mediaid");
                this.SetValue(rawValue);
                this.Update();
                this.SetModified();
            }

            SheerResponse.SetReturnValue(true);
        }

        /// <summary>
        /// Retrieves the image url from the stored XmlValue
        /// </summary>
        /// <returns></returns>
        private string GetSrc()
        {
            string attribute = this.XmlValue.GetAttribute("mediaid");
            if (attribute.Length <= 0)
                return string.Empty;

            return attribute;
        }

        protected override void DoRender(HtmlTextWriter output)
        {
            Assert.ArgumentNotNull((object)output, nameof(output));

            string src = GetSrc();
            string str1 = " src=\"" + src + "\"";
            string str2 = " id=\"" + this.ID + "_image\"";
            string str3 = " alt=\"static-alt\"";

            this.Attributes["placeholder"] = Translate.Text(this.Placeholder);
            string str = this.Password ? " type=\"password\"" : (this.Hidden ? " type=\"hidden\"" : "");
            this.SetWidthAndHeightStyle();
            output.Write("<input" + this.ControlAttributes + str + ">");
            this.RenderChildren(output);

            output.Write("<div id=\"" + this.ID + "_pane\" class=\"scContentControlImagePane\">");
                string clientEvent = Sitecore.Context.ClientPage.GetClientEvent(this.ID + ".Browse");
                output.Write("<div class=\"scContentControlImageImage\" onclick=\"" + clientEvent + "\">");
                    output.Write("<div align=\"left\" style=\"width:100%; height:128px\">");
                    if (!string.IsNullOrEmpty(src))
                    {
                        output.Write("<img " + str2 + str1 + str3 + " height=\"128px\" >");
                    }
                    output.Write("</div>");
                output.Write("</div>");
                output.Write("<div id=\"" + this.ID + "_details\" class=\"scContentControlImageDetails\">");

                string details = this.GetDetails();
                output.Write(details);
                output.Write("</div>");
            output.Write("</div>");
        }

        /// <summary>Gets the image details.</summary>
        /// <returns>The details.</returns>
        /// <contract>
        ///   <ensures condition="not null" />
        /// </contract>
        private string GetDetails()
        {
            return Translate.Text("This media item has no details.");
        }
    }
}