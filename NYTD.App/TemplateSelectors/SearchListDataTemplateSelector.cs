using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NYTD.App.TemplateSelectors
{
    public class SearchListDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VideoTemplate { get; set; }
        public DataTemplate ChannelTemplate { get; set; }
        public DataTemplate PlaylistTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is Video)
            {
                return VideoTemplate;
            }
            else if (item is Channel)
            {
                return ChannelTemplate;
            }
            else if (item is Playlist)
            {
                return PlaylistTemplate;
            }

            return null;
        }
    }
}
