using Prism.Mvvm;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagesApp.Views;
using System.Collections.ObjectModel;
using MessagesApp.Models;
using System.Windows.Input;

namespace MessagesApp.ViewModels
{
    public class MouseWheelUp : MouseGesture
    {
        public MouseWheelUp() : base(MouseAction.WheelClick)
        {
        }

        public MouseWheelUp(ModifierKeys modifiers) : base(MouseAction.WheelClick, modifiers)
        {
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs)) return false;
            if (!(inputEventArgs is MouseWheelEventArgs)) return false;
            var args = (MouseWheelEventArgs)inputEventArgs;
            return args.Delta > 0;
        }
    }

    public class MouseWheelDown : MouseGesture
    {
        public MouseWheelDown() : base(MouseAction.WheelClick)
        {
        }

        public MouseWheelDown(ModifierKeys modifiers) : base(MouseAction.WheelClick, modifiers)
        {
        }

        public override bool Matches(object targetElement, InputEventArgs inputEventArgs)
        {
            if (!base.Matches(targetElement, inputEventArgs)) return false;
            if (!(inputEventArgs is MouseWheelEventArgs)) return false;
            var args = (MouseWheelEventArgs)inputEventArgs;
            return args.Delta < 0;
        }
    }

    public class TransitionerViewModel : BindableBase
    {
        private User user = new User { Id = 1, FirstName = "ja", MessageProfilePictureUrl = "/Resources/ProfilePic.jpg" };
        private User user2 = new User { Id = 2, FirstName = "Sasha", IsOnline = true, MessageProfilePictureUrl = "/Resources/ProfilePic.jpg" };
        private User user3 = new User { Id = 3, FirstName = "Jane", MessageProfilePictureUrl = "/Resources/profilepic1.jpg" };

        public TransitionerViewModel()
        {
            Conversation = new Conversation2();
            Conversation.Users.Add(user);
            Conversation.Users.Add(user2);
            Conversation.Users.Add(user3);

            SendMessageCmd = new DelegateCommand(OnSendMessage, () => true);
            PrependMessageCmd = new DelegateCommand(OnPrependMessage, () => true);
            ScrollCmd = new DelegateCommand(OnScroll, () => true).ObservesProperty(() => VerticalOffset);

            MouseWheelUpCmd = new DelegateCommand(OnMouseWheelUp, () => true).ObservesProperty(() => VerticalOffset);
            MouseWheelDownCmd = new DelegateCommand(OnMouseWheelDown, () => true).ObservesProperty(() => VerticalOffset);
            //SizeChangedCmd = new DelegateCommand(OnSizeChanged, () => true);

            ArrowUpPressedCmd = new DelegateCommand(OnArrowUpPressed, () => true).ObservesProperty(() => VerticalOffset);
            ArrowDownPressedCmd = new DelegateCommand(OnArrowDownPressed, () => true).ObservesProperty(() => VerticalOffset);
            
            //LastDateUpdate = new DateTime();

            //Messages = new ObservableCollection<Message>();
            //Messages = new ObservableCollection<MessageViewModel>();
            
            Conversation.AppendMessage(new MessageViewModel { Text = "sdff", Author = user2, Date = new DateTime(1, 1, 2) });
            for (int i = 0; i < 50; i++)
            {
                //if (i % 2 == 0)
                //{
                //    Messages.Add(new MessageViewModel { Text = new string(i.ToString()[0], 30) });
                //}
                //else
                //{
                //    Messages.Add(new MessageViewModel { Text = new string(i.ToString()[0], 30), MessageTemplate = "RightMessageTemplate" });
                //}
                if (i % 2 == 0)
                {
                    Conversation.AppendMessage(new MessageViewModel { Text = i.ToString(), Author = user2 });
                }
                else
                {
                    Conversation.AppendMessage(new MessageViewModel { Text = i.ToString(), Author = user });
                }
            }

            //AppendMessage(new MessageViewModel { Author = user2, Text = "a" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" });
            //AppendMessage(new MessageViewModel { Author = user2, Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "J 0000" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "J 1111 afsdfsdfs" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "22" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "3" });
            //AppendMessage(new MessageViewModel { Author = user2, Text = "cccccccccccccc" });
            //AppendMessage(new MessageViewModel { Author = user2, Text = "d" });
            //AppendMessage(new MessageViewModel { Author = user2, Text = "ee" });
            //AppendMessage(new MessageViewModel { Author = user, Text = "4" });

            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 6, 6, 0, 0, 0), Text = "a" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 6, 16, 0, 0, 0), Text = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 6, 26, 0, 0, 0), Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 6, 26, 1, 0, 0), Text = "J 0000" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 7, 26, 5, 0, 0), Text = "J 1111 afsdfsdfs" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 7, 27, 2, 40, 0), Text = "22" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 7, 28, 0, 0, 0), Text = "3" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 7, 29, 21, 42, 22), Text = "cccccccccccccc" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 8, 2, 7, 0, 0), Text = "d" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 8, 3, 8, 20, 0), Text = "ee" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 8, 3, 8, 22, 0), Text = "e42" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 8, 4, 16, 43, 25), Text = "4" });
            Conversation.AppendMessage(new MessageViewModel { Author = user, Date = new DateTime(2019, 8, 4, 16, 46, 23), Text = "ghfhf" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 8, 4, 16, 49, 54), Text = "hfsfdghdfa" });
            Conversation.AppendMessage(new MessageViewModel { Author = user2, Date = new DateTime(2019, 8, 4, 16, 51, 54), Text = "dgdfgggdd" });
        }

        //private void AppendMessage(MessageViewModel mvm)
        //{
        //    if ((mvm.Date - LastDateUpdate).TotalSeconds > 300)
        //    {
        //        AppendMessage2(new MessageViewModel { CreatedDate = mvm.CreatedDate, MessageTemplate = "DateHeadlineTemplate" });
        //        //LastDateUpdate = mvm.Date;
        //    }
        //    LastDateUpdate = mvm.Date;
        //    int index = Messages.Count;
        //    Messages.Add(mvm);
        //    UpdateTemplate(index - 1);
        //    UpdateTemplate(index);
        //}

        //private void PrependMessages(MessageViewModel mvm)
        //{
        //    Messages.Insert(0, mvm);
        //    UpdateTemplate(0);
        //    UpdateTemplate(1);
        //    if ((mvm.Date - LastDateUpdate).TotalSeconds > 300)
        //    {
        //        PrependMessages2(new MessageViewModel { CreatedDate = mvm.CreatedDate, MessageTemplate = "DateHeadlineTemplate" });
        //    }
        //    LastDateUpdate = mvm.Date;
        //}

        //private void AppendMessage2(MessageViewModel mvm)
        //{
        //    int index = Messages.Count;
        //    Messages.Add(mvm);
        //    UpdateTemplate(index - 1);
        //    UpdateTemplate(index);
        //}

        //private void PrependMessages2(MessageViewModel mvm)
        //{
        //    Messages.Insert(0, mvm);
        //    UpdateTemplate(0);
        //    UpdateTemplate(1);
        //}

        //private DateTime lastDateUpdate;
        //public DateTime LastDateUpdate
        //{
        //    get => lastDateUpdate;
        //    set => SetProperty(ref lastDateUpdate, value);
        //}

        //private void UpdateTemplate(int index)
        //{
        //    if (!IsExist(index)) return;

        //    var message = Messages[index];
        //    if (message.MessageTemplate == "DateHeadlineTemplate") return;

        //    short templateIndex = 0;
        //    string template;
        //    string side = (message.Author.FirstName == user.FirstName) ? "Right" : "Left";
        //    //message.IsLast = false;

        //    if (IsExist(index + 1))
        //    {
        //        var nextMessage = Messages[index + 1];
        //        if (nextMessage.Author.FirstName == message.Author.FirstName) templateIndex++;
        //        message.IsLast = false;
        //    }
        //    else message.IsLast = true;

        //    if (IsExist(index - 1))
        //    {
        //        var prevMessage = Messages[index - 1];
        //        //prevMessage.IsLast = false;
        //        if (prevMessage.Author.FirstName == message.Author.FirstName) templateIndex += 2;
        //    }
            
        //    switch (templateIndex)
        //    {
        //        case 1: template = "TopMessageTemplate"; break;
        //        case 2: template = "BottomMessageTemplate"; break;
        //        case 3: template = "MiddleMessageTemplate"; break;
        //        default: template = "MessageTemplate"; break;
        //    }
            
        //    message.MessageTemplate = side + template;
        //}

        //private bool IsExist(int index)
        //{
        //    return (index >= 0 && index < Messages.Count) ? true : false;
        //}

        public DelegateCommand SendMessageCmd { get; set; }
        public DelegateCommand PrependMessageCmd { get; set; }
        public DelegateCommand ScrollCmd { get; set; }        
        public DelegateCommand SizeChangedCmd { get; set; }

        public DelegateCommand MouseWheelUpCmd { get; set; }
        public DelegateCommand MouseWheelDownCmd { get; set; }

        public DelegateCommand ArrowUpPressedCmd { get; set; }
        public DelegateCommand ArrowDownPressedCmd { get; set; }

        //private ObservableCollection<MessageViewModel> messages;
        //public ObservableCollection<MessageViewModel> Messages
        //{
        //    get => messages;
        //    private set => SetProperty(ref messages, value);
        //}

        private MessageViewModel MessageViewModel { get; set; }

        private Conversation2 conversation;
        public Conversation2 Conversation
        {
            get => conversation;
            set => SetProperty(ref conversation, value);
        }

        private bool mouseWheelUp;
        public bool MouseWheelUp
        {
            get => mouseWheelUp;
            set => SetProperty(ref mouseWheelUp, value);
        }

        private bool mouseWheelDown;
        public bool MouseWheelDown
        {
            get => mouseWheelDown;
            set => SetProperty(ref mouseWheelDown, value);
        }

        private bool arrowUp;
        public bool ArrowUp
        {
            get => arrowUp;
            set => SetProperty(ref arrowUp, value);
        }

        private bool arrowDown;
        public bool ArrowDown
        {
            get => arrowDown;
            set => SetProperty(ref arrowDown, value);
        }

        private double offset;
        public double VerticalOffset
        {
            get => offset;
            set => SetProperty(ref offset, value);
        }

        private double offset2;
        public double VerticalOffset2
        {
            get => offset2;
            set => SetProperty(ref offset2, value);
        }

        private bool val;
        public bool OnScrollToBottom
        {
            get => val;
            set => SetProperty(ref val, value);
        }

        private bool val2;
        public bool OnScrollToOffset
        {
            get => val2;
            set => SetProperty(ref val2, value);
        }

        private string color;
        public string Color
        {
            get => color;
            set => SetProperty(ref color, value);
        }

        private void OnSendMessage()
        {
            Conversation.AppendMessage(new MessageViewModel { Text = "fasfa", Author = user, Date = DateTime.Now });
            //ScrollToOffsetValue = VerticalOffset2;
            RaisePropertyChanged("OnScrollToBottom");
        }

        private void OnPrependMessage()
        {
            if (VerticalOffset == VerticalOffset2)
            {
                Conversation.PrependMessages(new MessageViewModel { Text = "1", Author = user3, Date = DateTime.Now });
                Conversation.PrependMessages(new MessageViewModel { Text = "2", Author = user3, Date = DateTime.Now });
                Conversation.PrependMessages(new MessageViewModel { Text = "3", Author = user3, Date = DateTime.Now });
            }
        }

        private void OnScroll()
        {
            RaisePropertyChanged("VerticalOffset");

            //if (VerticalOffset == 500)
            //{
            //    VerticalOffset = VerticalOffset2;
            //    RaisePropertyChanged("VerticalOffset");
            //}

            //if (VerticalOffset == VerticalOffset2)
            //{
            //    PrependMessages(new MessageViewModel { Text = "1", Author = user3, Date = DateTime.Now });
            //    PrependMessages(new MessageViewModel { Text = "2", Author = user3, Date = DateTime.Now });
            //    PrependMessages(new MessageViewModel { Text = "3", Author = user3, Date = DateTime.Now });
            //    //RaisePropertyChanged("VerticalOffset2");
            //}

            //if (VerticalOffset == 0)
            //{
            //    //RaisePropertyChanged("VerticalOffset2");
            //    //Messages.Insert(0, new Message { Text = "sfasafsfaaafdsfsd" });
            //    //RaisePropertyChanged("OnScrollToOffset");
            //}

            //System.Windows.MessageBox.Show("dfafas");
        }

        private void OnMouseWheelUp()
        {
            RaisePropertyChanged("MouseWheelUp");
        }

        private void OnMouseWheelDown()
        {
            RaisePropertyChanged("MouseWheelDown");
        }

        private void OnArrowUpPressed()
        {
            RaisePropertyChanged("ArrowUp");
        }

        private void OnArrowDownPressed()
        {
            RaisePropertyChanged("ArrowDown");
        }
    }
}
