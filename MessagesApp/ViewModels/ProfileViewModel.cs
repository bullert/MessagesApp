using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Media;
using System.Drawing.Drawing2D;

namespace MessagesApp.ViewModels
{
    public class ProfileViewModel : BindableBase
    {
        public ProfileViewModel()
        {
            ContainerWidth = 688;
            ContainerHeight = 512;
            Radius = 140;
            Zoom = 1;
            
            UploadCmd = new DelegateCommand(OnUpload, () => true);
            PictureCropCmd = new DelegateCommand(OnCrop, () => true);
            DragEnterCmd = new DelegateCommand(OnDragEnter, () => true);
            DragOverCmd = new DelegateCommand(OnDragOver, () => true);
            DragLeaveCmd = new DelegateCommand(OnDragLeave, () => true);

        }

        public DelegateCommand UploadCmd { get; set; }
        public DelegateCommand PictureCropCmd { get; set; }
        public DelegateCommand DragEnterCmd { get; set; }
        public DelegateCommand DragOverCmd { get; set; }
        public DelegateCommand DragLeaveCmd { get; set; }

        private double containerWidth;
        public double ContainerWidth
        {
            get => containerWidth;
            set
            {
                SetProperty(ref containerWidth, value);
                ContainerRect = new Rect(0, 0, ContainerWidth, ContainerHeight);
            }
        }

        private double containerHeight;
        public double ContainerHeight
        {
            get => containerHeight;
            set
            {
                SetProperty(ref containerHeight, value);
                ContainerRect = new Rect(0, 0, ContainerWidth, ContainerHeight);
            }
        }

        private Rect containerRect;
        public Rect ContainerRect
        {
            get => containerRect;
            set
            {
                SetProperty(ref containerRect, value);
                Center = new System.Windows.Point(ContainerWidth / 2, ContainerHeight / 2);
            }
        }

        private System.Windows.Point center;
        public System.Windows.Point Center
        {
            get => center;
            set => SetProperty(ref center, value);
        }

        private double radius;
        public double Radius
        {
            get => radius;
            set => SetProperty(ref radius, value);
        }

        private double maxRadius;
        public double MaxRadius
        {
            get => maxRadius;
            set => SetProperty(ref maxRadius, value);
        }
        
        private double topScaledOffset;
        public double TopScaledOffset
        {
            get => topScaledOffset;
            set => SetProperty(ref topScaledOffset, value);
        }

        private double leftScaledOffset;
        public double LeftScaledOffset
        {
            get => leftScaledOffset;
            set => SetProperty(ref leftScaledOffset, value);
        }

        private double zoom;
        public double Zoom
        {
            get => zoom;
            set
            {
                SetProperty(ref zoom, value);
                OnResize();
            }
        }

        private double topMaxOffset;
        public double TopMaxOffset
        {
            get => topMaxOffset;
            set
            {
                SetProperty(ref topMaxOffset, value);
                OnResize();
            }
        }

        private double leftMaxOffset;
        public double LeftMaxOffset
        {
            get => leftMaxOffset;
            set
            {
                SetProperty(ref leftMaxOffset, value);
                OnResize();
            }
        }

        private double originalWidth;
        public double OriginalWidth
        {
            get => originalWidth;
            set => SetProperty(ref originalWidth, value);
        }

        private double originalHeight;
        public double OriginalHeight
        {
            get => originalHeight;
            set => SetProperty(ref originalHeight, value);
        }

        private double displayedWidth;
        public double DisplayedWidth
        {
            get => displayedWidth;
            set => SetProperty(ref displayedWidth, value);
        }

        private double displayedHeight;
        public double DisplayedHeight
        {
            get => displayedHeight;
            set => SetProperty(ref displayedHeight, value);
        }

        private double scaledWidth;
        public double ScaledWidth
        {
            get => scaledWidth;
            set => SetProperty(ref scaledWidth, value);
        }

        private double scaledHeight;
        public double ScaledHeight
        {
            get => scaledHeight;
            set => SetProperty(ref scaledHeight, value);
        }

        private System.Windows.Point mousePosition;
        public System.Windows.Point MousePosition
        {
            get => mousePosition;
            set
            {
                SetProperty(ref mousePosition, value);
            }
        }

        private System.Windows.Point lastMousePosition;
        public System.Windows.Point LastMousePosition
        {
            get => lastMousePosition;
            set
            {
                SetProperty(ref lastMousePosition, value);
            }
        }

        private string bindablePosition;
        public string BindablePosition
        {
            get => bindablePosition;
            set => SetProperty(ref bindablePosition, value);
        }

        private double x;
        public double X
        {
            get => x;
            set => SetProperty(ref x, value);
        }

        private float y;
        public float Y
        {
            get => y;
            set => SetProperty(ref y, value);
        }

        private bool isDrag;
        public bool IsDrag
        {
            get => isDrag;
            set => SetProperty(ref isDrag, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private BitmapImage image;
        public BitmapImage Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private void OnDragEnter()
        {
            IsDrag = true;
            MousePosition = GetMousePosition();
            LastMousePosition = MousePosition;
        }

        private void OnDragOver()
        {
            if (IsDrag)
            {
                System.Windows.Point diff = (System.Windows.Point)(MousePosition - LastMousePosition);
               
                var x = LeftScaledOffset;
                var y = TopScaledOffset;
                var z = 1 / Zoom;
                
                LeftScaledOffset = x + diff.X * z;
                TopScaledOffset = y + diff.Y * z;

                Clamp();
                
                LastMousePosition = MousePosition;
                MousePosition = GetMousePosition();
            }
        }
        
        private void OnDragLeave()
        {
            IsDrag = false;
        }

        private System.Windows.Point GetMousePosition()
        {
            return System.Windows.Input.Mouse.GetPosition(System.Windows.Application.Current.MainWindow);
        }

        private void Clamp()
        {
            var zr = 1 / Zoom * Radius;
            var bx = LeftMaxOffset - zr;
            var by = TopMaxOffset - zr;

            if (LeftScaledOffset < -bx) LeftScaledOffset = -bx;
            else if (LeftScaledOffset > bx) LeftScaledOffset = bx;

            if (TopScaledOffset < -by) TopScaledOffset = -by;
            else if (TopScaledOffset > by) TopScaledOffset = by;
        }

        private void OnResize()
        {
            //var x = LeftOffset;
            //var y = TopOffset;

            var z = (Radius * Zoom - Radius) / Zoom;
            var zx = (DisplayedWidth / 2 * Zoom - DisplayedWidth / 2) / Zoom;
            var zy = (DisplayedHeight / 2 * Zoom - DisplayedHeight / 2) / Zoom;

            Clamp();

            //LeftOffset = (x + r) * Zoom;
            //TopOffset = (y + r) * Zoom;
            //LeftScaledOffset = (LeftOffset) - DisplayedWidth / 2 * s + Radius * s;

            //LeftScaledOffset = (LeftOffset) - zx;
            //TopScaledOffset = TopOffset - zy;

            //LeftOffset = LeftScaledOffset + zx;
            //TopOffset = TopScaledOffset + zy;

            //ScaledWidth = DisplayedWidth * Zoom;
            //ScaledHeight = DisplayedHeight * Zoom;
        }

        public void OnUpload()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (fileDialog.ShowDialog() == true)
            {
                byte[] buffer = File.ReadAllBytes(fileDialog.FileName);
                string result = System.Text.Encoding.UTF8.GetString(buffer);
                //Text = result;

                //Image = new BitmapImage(new Uri(fileDialog.FileName));

                var img = new BitmapImage();
                var stream = new MemoryStream(buffer);
                img.BeginInit();
                img.StreamSource = stream;
                img.EndInit();

                Image = img;
                OriginalWidth = img.Width;
                OriginalHeight = img.Height;

                //double max = (Math.Max(OriginalWidth, OriginalHeight) - 320) / 2;

                Calc();

                Zoom = 1;
            }
        }

        private void Calc()
        {
            double diameter = Radius * 2;

            MaxRadius = Math.Min(OriginalWidth, OriginalHeight);

            if (OriginalWidth < OriginalHeight)
            {
                DisplayedWidth = diameter;
                DisplayedHeight = OriginalHeight / OriginalWidth * diameter;

                //TopOffset = (max - 320) / 2;
                //LeftOffset = 0;
                //VerticalBound = (max - 320) / 2;
                //Bound.Left = (max - 320) / 2;
                //Bound = new Thickness(0, max, 0, -max);
            }
            else
            {
                DisplayedWidth = OriginalWidth / OriginalHeight * diameter;
                DisplayedHeight = diameter;

                //TopOffset = 0;
                //LeftOffset = (max - 320) / 2;
                //HorizontalBound = (max - 320) / 2;
                //Bound = new Thickness(max, 0, max, 0);
            }

            ScaledWidth = DisplayedWidth;
            ScaledHeight = DisplayedHeight;

            double topMaxOffset = (DisplayedHeight - diameter) / 2;
            double leftMaxOffset = (DisplayedWidth - diameter) / 2;

            TopMaxOffset = topMaxOffset + Radius;
            LeftMaxOffset = leftMaxOffset + Radius;
            
            //MessageBox.Show(img.Width.ToString() + " " + DisplayedWidth.ToString());

            //TopOffset = topMaxOffset;
            //LeftOffset = leftMaxOffset;

            //TopOffset = 0;
            //LeftOffset = 0;

            LeftScaledOffset = 0;
            TopScaledOffset = 0;

            //MessageBox.Show($"{leftMaxOffset}, { LeftOffset}, { topMaxOffset}, { TopOffset}");

            //Bound = new Thickness(leftMaxOffset, topMaxOffset, 0, 0);

            //Position = new Point(TopOffset, LeftOffset);

            //var fileInfo = new FileInfo(fileDialog.FileName);

            //Text = fileInfo.Length.ToString() + " " + img.Width.ToString() + " " + img.Height.ToString();

            //using (var ms = new MemoryStream(buffer))
            //{
            //    Image = Image.FromStream(ms);
            //}
        }

        private void OnCrop()
        {
            //System.Windows.Media.Transform tr = new TranslateTransform();
            //tr.Transform.s

            //Bitmap bmPhoto = new Bitmap(333, 333);
            //bmPhoto.SetResolution(72, 72);

            ////MemoryStream outStream = new MemoryStream();
            ////BitmapEncoder enc = new BmpBitmapEncoder();
            ////enc.Frames.Add(BitmapFrame.Create(Image));
            ////enc.Save(outStream);
            ////System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

            //Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            //grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //grPhoto.DrawImage(Image, new Rectangle(0, 0, 333, 333), 0, 0, 222, 222, GraphicsUnit.Pixel);

            MemoryStream outStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(Image));
            enc.Save(outStream);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

            //System.Drawing.Color[,] ar = new System.Drawing.Color[280, 280];
            int size = (int)(MaxRadius / Zoom);
            int xs = (int)(LeftMaxOffset);
            int ys = (int)(TopMaxOffset);
            //int xs = (int)(LeftScaledOffset * (size / DisplayedWidth));
            //int ys = (int)(TopScaledOffset * (size / DisplayedHeight));
            //int size = (int)(Radius * 4);
            var newbitmap = new Bitmap(size, size);

            //int xs2 = 0 + (int)(((size / 2) * (Zoom - 1)));
            //int ys2 = 0 + (int)(((size / 2) * (Zoom - 1)));
            double hw = OriginalWidth / 2;
            double hh = OriginalHeight / 2;

            //int xs2 = 0 + (int)(hw - hw / 5 * (6 - (Zoom)));
            //int ys2 = 0 + (int)(hh - hh / 5 * (6 - (Zoom)));
            double z = (Zoom - 1) * 6 / 5;

            int xs2 = 0 + (int)(300 * z);
            int ys2 = 0 + (int)(300 * z);

            MessageBox.Show($"{xs2} {ys2} {hw} {hh} ");
            //MessageBox.Show($"{Zoom} {xs} {ys} {xs2} {ys2}");
            Rectangle crop = new Rectangle(xs2, ys2, size, size);

            // Here we capture another resource.
            Bitmap croppedImage = bitmap.Clone(crop, bitmap.PixelFormat);


            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        if (i < 0 || i >= newbitmap.Width || i >= bitmap.Width) continue;
            //        if (j < 0 || j >= newbitmap.Height || j >= bitmap.Height) continue;
            //        newbitmap.SetPixel(i, j, bitmap.GetPixel(i + xs, j + ys));
            //        //ar[i - 50, j - 50] = bitmap.GetPixel(i, j);
            //    }
            //}

            //for (int i = 0; i < 280; i++)
            //{
            //    for (int j = 0; j < 280; j++)
            //    {
            //        newbitmap.SetPixel(i, j, ar[i, j]);
            //    }
            //}

            //bitmap.SetResolution(320, 320);


            var memory = new MemoryStream();
            croppedImage.Save(memory, System.Drawing.Imaging.ImageFormat.Jpeg);
            memory.Position = 0;

            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memory;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            
            Image = bitmapImage;
            OriginalWidth = bitmapImage.Width;
            OriginalHeight = bitmapImage.Height;

            MessageBox.Show($"{OriginalWidth} {OriginalHeight}");

            Calc();
        }
    }
}
