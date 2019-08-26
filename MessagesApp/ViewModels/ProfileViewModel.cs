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
            //DestinationRadius = 150;
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

        //private double destinationRadius;
        //public double DestinationRadius
        //{
        //    get => destinationRadius;
        //    set => SetProperty(ref destinationRadius, value);
        //}

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

        private double minRadius;
        public double MinRadius
        {
            get => minRadius;
            set => SetProperty(ref minRadius, value);
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

        private BitmapImage image2;
        public BitmapImage Image2
        {
            get => image2;
            set => SetProperty(ref image2, value);
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
            var z = (Radius * Zoom - Radius) / Zoom;
            var zx = (DisplayedWidth / 2 * Zoom - DisplayedWidth / 2) / Zoom;
            var zy = (DisplayedHeight / 2 * Zoom - DisplayedHeight / 2) / Zoom;

            Clamp();
        }

        public void OnUpload()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            if (fileDialog.ShowDialog() == true)
            {
                byte[] buffer = File.ReadAllBytes(fileDialog.FileName);
                string result = System.Text.Encoding.UTF8.GetString(buffer);
                
                //Image = new BitmapImage(new Uri(fileDialog.FileName));

                var img = new BitmapImage();
                var stream = new MemoryStream(buffer);
                img.BeginInit();
                img.StreamSource = stream;
                img.EndInit();

                Image = img;
                OriginalWidth = img.Width;
                OriginalHeight = img.Height;
                
                Calc();
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
            }
            else
            {
                DisplayedWidth = OriginalWidth / OriginalHeight * diameter;
                DisplayedHeight = diameter;
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

            Zoom = 1;
        }

        private Bitmap ImageToBitmap(BitmapImage bitmapImage)
        {
            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(stream);

            return new System.Drawing.Bitmap(stream);
        }

        private BitmapImage BitmapToImage(Bitmap bitmap)
        {
            var stream = new MemoryStream();
            var bitmapImage = new BitmapImage();

            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        private Bitmap ResizeBitmap(Bitmap bitmap, System.Drawing.Size size)
        {
            return new Bitmap(bitmap, size);
        }

        private Bitmap CropBitmap(Bitmap bitmap, Rectangle cropRect)
        {
            return bitmap.Clone(cropRect, bitmap.PixelFormat);
        }

        private void OnCrop()
        {
            var bitmap = ImageToBitmap(Image);

            double diameter = MaxRadius;

            double size = diameter / Zoom;
            double zoomOffset = (diameter - diameter / Zoom) / 2;
            double r = (diameter / 2) / Radius;
            double translateX = (OriginalWidth - diameter) / 2 - LeftScaledOffset * r;
            double translateY = (OriginalHeight - diameter) / 2 - TopScaledOffset * r;
            
            double offsetX = translateX + zoomOffset;
            double offsetY = translateY + zoomOffset;
            
            var croppedImage = CropBitmap(bitmap, new Rectangle((int)offsetX, (int)offsetY, (int)size, (int)size));
            var resizedBitmap = ResizeBitmap(croppedImage, new System.Drawing.Size(280, 280));
            
            Image2 = BitmapToImage(resizedBitmap);
        }
    }
}
