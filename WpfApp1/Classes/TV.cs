using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp1.Classes
{
    public class TV
    {
        private int activeChannel;
        private int activeVolume;
        private bool isOn = true;
        public bool IsOn
        {
            get
            {
                return isOn;
            }
            set
            {
                isOn = value;
            }
        }
        public int ActiveChannel
        {
            get
            {
                return activeChannel;
            }
            set
            {
                if (value < 0)
                    activeChannel = Channels.Count - 1;
                else if (value > Channels.Count - 1)
                    activeChannel = 0;
                else
                    activeChannel = value;
            }
        }
        public List<Channel> Channels = new List<Channel>();
        public DispatcherTimer AutoTimer { get; private set; }

        public TV()
        {
            Channels.Add(new Channel()
            {
                Name = "Канал 1",
                Src = @"D:\video1.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Канал 2",
                Src = @"D:\video2.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Канал 3",
                Src = @"D:\video3.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Канал 4",
                Src = @"D:\video4.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Канал 5",
                Src = @"D:\video5.mp4"
            });
            Channels.Add(new Channel()
            {
                Name = "Канал 6",
                Src = @"D:\video6.mp4"
            });
            AutoTimer = new DispatcherTimer();
            AutoTimer.Interval = TimeSpan.FromSeconds(10);
        }

        public void SwapChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            DoubleAnimation StartAnimation = new DoubleAnimation();
            StartAnimation.From = 1;
            StartAnimation.To = 0;
            StartAnimation.Duration = TimeSpan.FromSeconds(0.6);
            StartAnimation.Completed += delegate
            {
                _MediaElement.Source = new Uri(this.Channels[this.ActiveChannel].Src);
                _MediaElement.Play();
                DoubleAnimation EndAnimation = new DoubleAnimation();
                EndAnimation.From = 0;
                EndAnimation.To = 1;
                EndAnimation.Duration = TimeSpan.FromSeconds(0.6);
                _MediaElement.BeginAnimation(MediaElement.OpacityProperty, EndAnimation);
            };
            _MediaElement.BeginAnimation(MediaElement.OpacityProperty, StartAnimation);
            _NameChannel.Content = this.Channels[this.ActiveChannel].Name;
        }

        public void NextChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            if (!isOn) return;
            ActiveChannel++;
            SwapChannel(_MediaElement, _NameChannel);
        }

        public void BackChannel(MediaElement _MediaElement, Label _NameChannel)
        {
            if (!isOn) return;
            ActiveChannel--;
            SwapChannel(_MediaElement, _NameChannel);
        }
        private MediaElement _autoMediaElement;
        private Label _autoNameChannel;

        public void StartAutoSwap(MediaElement _MediaElement, Label _NameChannel)
        {
            AutoTimer.Stop();
            AutoTimer.Tick -= AutoTimer_Tick;
            AutoTimer.Tick += AutoTimer_Tick;
            _autoMediaElement = _MediaElement;
            _autoNameChannel = _NameChannel;
            AutoTimer.Start();
        }

        public void StopAutoSwap()
        {
            AutoTimer.Stop();
            AutoTimer.Tick -= AutoTimer_Tick;
        }

        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            if (!isOn) return;
            ActiveChannel++;
            SwapChannel(_autoMediaElement, _autoNameChannel);
        }

        public void UpSound()
        {

        }

        public void DownSound()
        {

        }
    }
}
