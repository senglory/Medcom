using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Medcom.VM.DTO;

namespace WpfApp1
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultnDataTemplate { get; set; }
        public DataTemplate NoteDataTemplate { get; set; }
        public DataTemplate WebAccDataTemplate { get; set; }
        public DataTemplate CreditCardDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (null == item)
                return DefaultnDataTemplate;
            DependencyPropertyInfo dpi = item as DependencyPropertyInfo;
            if (item.GetType() == typeof(NoteDto))
            {
                return NoteDataTemplate;
            }
            if (item.GetType() == typeof(WebAccDto))
            {
                return WebAccDataTemplate;
            }
            if (item.GetType() == typeof(CreditCardDto))
            {
                return CreditCardDataTemplate;
            }

            return DefaultnDataTemplate;
        }
    }
}
