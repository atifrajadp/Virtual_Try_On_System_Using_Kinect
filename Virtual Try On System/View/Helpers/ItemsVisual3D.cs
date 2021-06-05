using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Virtual_Try_On_System.View.Helpers
{
    public class ItemsVisual3D : ModelVisual3D
    {
       
        // Item template property
       
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate3D), typeof(ItemsVisual3D), new PropertyMetadata(null));
       
        // The items source property
      
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(ItemsVisual3D)
            , new PropertyMetadata(null, (s, e) => ((ItemsVisual3D)s).ItemsSourceChanged(e)));
      
        // Gets or sets the <see cref="DataTemplate3D" /> used to display each item.
        
        public DataTemplate3D ItemTemplate
        {
            get { return (DataTemplate3D)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        
        // Gets or sets a collection used to generate the content of the <see cref="ItemsVisual3D" />.
        
        public ICollection ItemsSource
        {
            get { return (ICollection)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
       
        // Handles changes in the ItemsSource property.
      
        private void ItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Children.Clear();

            foreach (var model in (from object item in ItemsSource
                                   select ItemTemplate.CreateItem(item)).Where(model => model != null))
                Children.Add(model);
        }
    }
}
