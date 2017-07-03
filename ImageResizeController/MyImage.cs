using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace ImageResizeController
{
    public class MyImage : Image
    {
        private string emptyImageUrl = "";
        private int maxWidth = 0;
        private int maxHeight = 0;
        private bool thumbnail = false;
        private bool scaled = true;
        private int quality = 75;

        public int Quality
        {
            get { return quality; }
            set { quality = value; }
        }

        public bool Scaled
        {
            get { return scaled; }
            set { scaled = value; }
        }

        public bool Thumbnail
        {
            get { return thumbnail; }
            set { thumbnail = value; }
        }

        public int MaxHeight
        {
            get { return maxHeight; }
            set { maxHeight = value; }
        }

        public int MaxWidth
        {
            get { return maxWidth; }
            set { maxWidth = value; }
        }

        public string EmptyImageUrl
        {
            get
            {
                if (ViewState["img" + this.UniqueID + "emptyImageUrl"] != null)
                    emptyImageUrl = ViewState["img" + this.UniqueID + "emptyImageUrl"].ToString();
                return emptyImageUrl;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    emptyImageUrl = value;
                    ViewState["img" + this.UniqueID + "emptyImageUrl"] = emptyImageUrl;
                }
            }
        }

        public MyImage()
        {
            MaxWidth = 0;
            MaxHeight = 0;
            EmptyImageUrl = "";
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(this.ImageUrl))
                this.ImageUrl = EmptyImageUrl;

            bool thumb = false;
            if (!thumbnail)
            {
                thumbnail = true;
            }
            else
            {
                thumb = true;
                // URL kontrolü yapabilirsiniz
                //if (!HttpContext.Current.Request.IsLocal)
                //    if (this.ImageUrl.StartsWith("http://www.daltinkurt.com") ||
                //       this.ImageUrl.StartsWith("https://www.daltinkurt.com"))
                //        thumb = false;
                ////if (!thumb)
                ////{
                ////    if (this.ImageUrl.StartsWith("http://") ||
                ////   this.ImageUrl.StartsWith("https://"))
                ////        thumb = true;
                ////}
            }
            if (thumb)
            {
                //ImageBoyutlandir();
                //if (this.Attributes["width"] == null)
                //    if (this.Width.Value > 0) this.Attributes.Add("width", this.Width.Value.ToString() + "px");
                //if (this.Attributes["height"] == null)
                //    if (this.Height.Value > 0) this.Attributes.Add("height", this.Height.Value.ToString() + "px");
                this.Width = this.MaxWidth;
                this.Height = this.MaxHeight;
                this.Attributes["weight"] = this.Width.Value.ToString() + "px";
                this.Attributes["height"] = this.Height.Value.ToString() + "px";
                this.Style["weight"] = this.Width.Value.ToString() + "px";
                this.Style["height"] = this.Height.Value.ToString() + "px";
            }
            else
            {
                // &process=no ???
                if (scaled)
                {
                    this.ImageUrl = string.Format("{0}?maxwidth={1}&maxheight={2}&mode=max&quality={3}&cache=always",
                        this.ImageUrl, this.maxWidth, this.maxHeight,
                        this.quality);
                }
                else
                {
                    this.ImageUrl = string.Format("{0}?width={1}&height={2}&mode=stretch&quality={3}&cache=always&scale=both",
                        this.ImageUrl, this.maxWidth, this.maxHeight,
                        this.quality);
                }
            }

            base.Render(writer);
        }

        private void ImageBoyutlandir()
        {
            //if (this.Width.Value == 0 || this.Height.Value == 0) return;
            //try
            //{
            using (System.Drawing.Image img = GetImage())
            {
                if (img != null)
                {
                    if (scaled)
                    {
                        if (img.Width > MaxWidth || img.Height > MaxHeight)
                        {
                            double widthRatio = 1.0;
                            if ((int)MaxWidth > 0)
                                widthRatio = (double)img.Width / (double)MaxWidth;

                            double heightRatio = 1.0;
                            if ((int)MaxHeight > 0)
                                heightRatio = (double)img.Height / (double)MaxHeight;

                            double ratio = Math.Max(widthRatio, heightRatio);
                            int newWidth = (int)(img.Width / ratio);
                            int newHeight = (int)(img.Height / ratio);

                            this.Width = Unit.Pixel(newWidth);
                            this.Height = Unit.Pixel(newHeight);
                        }
                    }
                    else
                    {
                        if (this.Width.Value == 0)
                            this.Width = (maxWidth == 0) ? img.Width : maxWidth;

                        if (this.Height.Value == 0)
                            this.Height = (maxHeight == 0) ? img.Height : maxHeight;
                        //if (this.Width.Value == 0) this.Width = img.Width;
                        //if (this.Height.Value == 0) this.Height = img.Height;
                    }
                }
            }
            //}
            //catch (Exception)
            //{ }
        }

        private System.Drawing.Image GetImage()
        {
            if (string.IsNullOrEmpty(ImageUrl.Trim())) return null;

            try
            {
                if (this.ImageUrl.StartsWith("http://") || this.ImageUrl.StartsWith("https://"))
                {
                    return null;
                    //WebRequest req = WebRequest.Create(this.ImageUrl);
                    //WebResponse response = req.GetResponse();
                    //Stream stream = response.GetResponseStream();
                    //return System.Drawing.Image.FromStream(stream);
                }
                else
                    return System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(this.ImageUrl));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
